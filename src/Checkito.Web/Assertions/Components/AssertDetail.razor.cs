using Checkito.Apis.Services;
using Checkito.Assertions.Models;
using Checkito.Data.Apis.Queries;
using Checkito.Testing.Models;
using Checkito.Testing.Services;
using Checkito.Web.General.Components;
using Microsoft.AspNetCore.Components;

namespace Checkito.Web.Assertions.Components
{
    public class AssertDetailBase : ScopedComponentBase
    {
        private ITestManager _testManager = default!;
        private IApiService _apiService = default!;
        private IList<TestCase> _testCases = default!;
        private bool _isNew;

        [Parameter]
        public Guid TestCaseId { get; set; }
        [Parameter]
        public Guid StepId { get; set; }
        [Parameter]
        public Guid AssertId { get; set; }

        public TestCase TestCase { get; set; } = default!;
        public TestCaseStep Step { get; set; } = default!;
        public AssertDefinition Assert { get; set; } = default!;

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
                throw new ApplicationException("Cannot find TestCase Step");
            }

            Assert = Step.GetAssertById(AssertId);
            if (Assert == null)
            {
                CreateNewAssert();
            }
        }

        private void CreateNewAssert()
        {
            Assert = new AssertDefinition();
            _isNew = true;
        }

        public async Task OnOk(NavigationManager navManager)
        {
            await Save();

            navManager.NavigateTo(string.Concat("tests/testcases/", TestCase.Id, "/steps/", Step.Id));
        }

        public async Task Save()
        {
            if (_isNew)
            {
                Step.Asserts.Add(Assert);
            }

            await _testManager.Save(_testCases);
        }
    }
}
