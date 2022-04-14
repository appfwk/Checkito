using Checkito.Assertions.Models;
using Checkito.Testing.Models;

namespace Checkito.Assertions.Services
{
    public class AssertEvaluator
    {
        public async Task<AssertEvaluation> Evaluate(HttpResponseMessage response, AssertDefinition assert, TestCaseStepEvaluation testCaseStepEvaluation)
        {
            var evaluation = new AssertEvaluation(assert.Id, testCaseStepEvaluation);
            var rawValue = (object?)null;
            try
            {
                if (Is(assert.Source, AssertSourceCodes.HttpStatus))
                {
                    rawValue = response.StatusCode;
                    var value = Convert.ToInt32(rawValue);
                    evaluation.IsSuccess = value == Convert.ToInt32(assert.ExpectedValue);
                    SetUnsuccessMessage(evaluation, assert.ExpectedValue, value);
                    return evaluation;
                }

                if (Is(assert.Source, AssertSourceCodes.Json))
                {
                    if (response.Content == null)
                    {
                        throw new InvalidOperationException("Content is null");
                    }

                    var value = await response.Content.ReadAsStringAsync();
                    rawValue = value;
                    evaluation.IsSuccess = new JsonComparer().Compare(Convert.ToString(assert.ExpectedValue), value);
                    SetUnsuccessMessage(evaluation, assert.ExpectedValue, value);
                    return evaluation;
                }

                evaluation.Message = "Assert is not supported. Check product update or contact support please";
                return evaluation;
            }
            catch (Exception ex)
            {
                SetUnsuccessMessage(evaluation, assert.ExpectedValue, $"Raw Value: '{rawValue}', Exception: '{ex}'");
                return evaluation;
            }
        }

        private void SetUnsuccessMessage(AssertEvaluation assertEvaluation, object? expectedValue, object? value)
        {
            if (assertEvaluation.IsSuccess)
            {
                return;
            }

            assertEvaluation.Message = $"Expected value: '{expectedValue}', current value:'{value}'";
        }

        private bool Is(string? code, string requiredCode)
        {
            return string.Equals(code, requiredCode, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
