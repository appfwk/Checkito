using Checkito.Apis.Models;
using Checkito.Assertions.Models;
using Checkito.Assertions.Services;
using Checkito.Data.Apis.Queries;
using Checkito.Http.Services;
using Checkito.Testing.Models;
using Checkito.Testing.Services.Params;
using IdentityModel.Client;
using MediatR;

namespace Checkito.Testing.Services
{
    public class TestRunner : ITestRunner
    {
        private readonly IMediator _mediator;
        private ApiDefinition _api = default!;

        public TestRunner(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<RunTestCaseResult> Run(RunTestCaseParams param)
        {
            var testEvaluation = new TestCaseEvaluation(param.TestCase.Id);
            testEvaluation.IsSuccess = true;

            var result = new RunTestCaseResult(testEvaluation);

            try
            {
                if (_api == null)
                {
                    var getApisResult = await _mediator.Send(new GetApisQuery());

                    if (getApisResult.Apis.Count == 0)
                    {
                        throw new ApplicationException("Cannot find API");
                    }

                    _api = getApisResult.Apis[0];
                }

                var testCase = param.TestCase;

                var env = param.TestContext.ExecutionEnv;


                var token = _api.Authentication.Token;

                if (_api.Authentication.Type != AuthenticationType.None && _api.Authentication.Type != AuthenticationType.ExistingToken)
                {
                    if (_api.Authentication.Type == AuthenticationType.ClientCredentials)
                    {
                        var httpClient = new HttpClient();
                        var tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
                        {
                            Address = env.ResolveVariables(_api.Authentication.Url),

                            ClientId = _api.Authentication.Login,
                            ClientSecret = _api.Authentication.Password,
                        });

                        if (tokenResponse.IsError)
                        {
                            throw new ApplicationException($"Authentication failed. {tokenResponse.Error}");
                        }

                        token = tokenResponse.AccessToken;
                    }

                }

                foreach (var step in testCase.Steps)
                {
                    var stepEvaluation = new TestCaseStepEvaluation(step.Id, testEvaluation);
                    try
                    {

                        testEvaluation.StepEvaluations.Add(stepEvaluation);

                        if (!step.ApiCallId.HasValue)
                        {
                            stepEvaluation.Message = "Endpoint/Api Call is not assigned to test step";
                            continue;
                        }

                        var apiCall = _api.GetCallDefinitionById(step.ApiCallId.Value);

                        if (apiCall == null)
                        {
                            stepEvaluation.Message = $"Cannot find Endpoint/Api Call Id:'{step.ApiCallId}'.";
                            continue;
                        }


                        var response = await new HttpCallExecutor().Execute(apiCall, token, env);
                        stepEvaluation.IsSuccess = true;
                        foreach (var assert in step.Asserts)
                        {
                            try
                            {
                                var assertEvaluation = await new AssertEvaluator().Evaluate(response, assert, stepEvaluation);
                                stepEvaluation.AssertEvaluations.Add(assertEvaluation);
                                if (!assertEvaluation.IsSuccess)
                                {
                                    stepEvaluation.IsSuccess = false;
                                    testEvaluation.IsSuccess = false;
                                }
                            }
                            catch (Exception ex)
                            {
                                stepEvaluation.AssertEvaluations.Add(new AssertEvaluation(assert.Id, stepEvaluation)
                                {
                                    Message = $"Assert evaluation failed by exception. Exception: '{ex}'."
                                });
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        stepEvaluation.IsSuccess = false;
                        stepEvaluation.Message = $"Assert evaluation failed by exception. Exception: '{ex}'. Original message: '{stepEvaluation.Message}'.";
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                result.TestCaseEvaluation.IsSuccess = false;
                result.TestCaseEvaluation.Message = $"Test case evaluation failed by exception. Exception: '{ex}'. Original message: '{result.TestCaseEvaluation.Message}'.";
                return result;
            }
        }
    }
}
