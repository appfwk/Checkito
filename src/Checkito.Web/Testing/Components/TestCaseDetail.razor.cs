using Checkito.Data.Apis.Queries;
using Checkito.Testing.Models;
using Checkito.Testing.Services;
using Checkito.Web.General.Components;
using Microsoft.AspNetCore.Components;

namespace Checkito.Web.Testing.Components
{
    public class TestCaseDetailBase : ScopedComponentBase
    {
        private ITestManager _testManager = default!;
        private IList<TestCase> _testCases = default!;
        private bool _isNew;

        [Parameter]
        public Guid TestCaseId { get; set; }

        public TestCase? TestCase { get; set; }

        protected override void OnParametersSet()
        {
            _testManager = GetService<ITestManager>();
        }

        protected override async Task OnInitializedAsync()
        {
            var result = await Mediator.Send(new GetTestCasesQuery());
            _testCases = result.TestCases;
            if (TestCaseId == Guid.Empty)
            {
                CreateNewTestCase();

                return;
            }

            TestCase = result.TestCases.FirstOrDefault(x => x.Id == TestCaseId);
            if (TestCase == null)
            {
                CreateNewTestCase();
            }
        }

        private void CreateNewTestCase()
        {
            TestCase = new TestCase
            {
                Name = "Test"
            };

            _isNew = true;
        }

        public async Task CreateStep(NavigationManager navManager)
        {
            if (_isNew)
            {
                await Save();
            }
            navManager.NavigateTo(GetManageStepUrl(Guid.Empty));
        }

        public async Task Run(NavigationManager navManager)
        {
            if (_isNew)
            {
                await Save();
            }

            navManager.NavigateTo(String.Concat(navManager.Uri, "/run"));
        }

        protected string GetManageStepUrl(Guid stepId)
        {
            return string.Concat(string.Concat("tests/testcases/", TestCase!.Id, "/steps/", stepId));
        }

        public async Task OnOk(NavigationManager navManager)
        {
            await Save();

            navManager.NavigateTo("tests/testcases");
        }

        public async Task Save()
        {
            if (_isNew)
            {
                _testCases.Add(TestCase!);
            }

            await _testManager.Save(_testCases);
        }
    }
}
