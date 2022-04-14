using Checkito.Apis.Models;
using Checkito.Apis.Services;
using Checkito.Data.Apis.Queries;
using Checkito.Testing.Models;
using Checkito.Testing.Services;
using Checkito.Web.General.Components;
using Microsoft.AspNetCore.Components;

namespace Checkito.Web.Testing.Components
{
    public class TestCaseStepDetailBase : ScopedComponentBase
    {
        private ITestManager _testManager = default!;
        private IApiService _apiService = default!;
        private IList<TestCase> _testCases = default!;
        private bool _isNew;

        [Parameter]
        public Guid TestCaseId { get; set; }
        [Parameter]
        public Guid StepId { get; set; }

        public TestCase TestCase { get; set; } = default!;
        public TestCaseStep Step { get; set; } = default!;

        protected override void OnParametersSet()
        {
            _testManager = GetService<ITestManager>();
        }

        protected override async Task OnInitializedAsync()
        {
            var result = await Mediator.Send(new GetTestCasesQuery());
            _testCases = result.TestCases;


            TestCase = _testCases.FirstOrDefault(x => x.Id == TestCaseId);
            if (TestCase == null)
            {
                throw new ApplicationException("Cannot find TestCase");
            }

            Step = TestCase.GetStepById(StepId);
            if (Step == null)
            {
                CreateNewStep();
            }

            var getApiResult = await Mediator.Send(new GetApisQuery());
            if (getApiResult.Apis.Count > 0)
            {
                ApiCalls = getApiResult.Apis[0].ApiCallDefinitions;
            }

            ApiCalls.Insert(0, new ApiCallDefinition { Id = Guid.Empty, Name = string.Empty });
        }

        protected IList<ApiCallDefinition> ApiCalls
        {
            get;
            set;
        } = new List<ApiCallDefinition>();

        private void CreateNewStep()
        {
            Step = new TestCaseStep();

            _isNew = true;
        }

        protected string GetManageAssertUrl(Guid assertId)
        {
            return string.Concat("tests/testcases/", TestCase.Id, "/steps/", StepId, "/asserts/", assertId);
        }


        public async Task CreateAssert(NavigationManager navManager)
        {
            if (_isNew)
            {
                await Save();
            }
            navManager.NavigateTo(GetManageAssertUrl(Guid.Empty));
        }

        public async Task OnOk(NavigationManager navManager)
        {
            await Save();

            navManager.NavigateTo(string.Concat("tests/testcases/", TestCase.Id));
        }

        public async Task Save()
        {
            if (_isNew)
            {
                TestCase.Steps.Add(Step);
            }

            if (Step.ApiCallId == Guid.Empty)
            {
                Step.ApiCallId = null;
            }

            await _testManager.Save(_testCases);
        }
    }
}
