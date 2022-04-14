using Checkito.Data.Apis.Queries;
using Checkito.Infrastructure.Application.Services;
using Checkito.Testing.Services;
using Checkito.Testing.Services.Params;
using Checkito.Web.General.Components;
using Checkito.Web.Testing.Models;
using Microsoft.AspNetCore.Components;

namespace Checkito.Web.Testing.Components
{
    public class TestCaseListBase : ScopedComponentBase
    {
        private ITestManager _testManager = default!;
        private ITestRunner _testRunner = default!;
        private IAppContext _appContext = default!;
        public IList<TestCaseViewModel> TestCases { get; set; } = new List<TestCaseViewModel>();
        protected override void OnParametersSet()
        {
            _testManager = GetService<ITestManager>();
            _testRunner = GetService<ITestRunner>();
            _appContext = GetService<IAppContext>();
        }

        protected override async Task OnInitializedAsync()
        {
            var result = await Mediator.Send(new GetTestCasesQuery());
            foreach (var testCase in result.TestCases)
            {
                TestCases.Add(new TestCaseViewModel(testCase));
            }
        }

        public async Task OnOk()
        {
            await _testManager.Save(TestCases.Select(x => x.TestCase).ToList());
        }

        public void OnCreateTestCase(NavigationManager navManager)
        {
            navManager.NavigateTo(string.Concat("tests/testcases/", Guid.Empty));
        }

        public async Task OnRun()
        {
            IsBussy = true;
            try
            {
                var testContext = await _appContext.GetTestContext();
                foreach (var testCaseVm in TestCases)
                {
                    var testCaseEvaluationResult = await _testRunner.Run(new RunTestCaseParams(testCaseVm.TestCase, testContext));
                    testCaseVm.Evaluation = testCaseEvaluationResult.TestCaseEvaluation;
                }
            }
            finally
            {
                IsBussy = false;
            }
        }


    }
}
