using Checkito.Data.Apis.Queries;
using Checkito.Infrastructure.Application.Services;
using Checkito.Testing.Models;
using Checkito.Testing.Services;
using Checkito.Web.General.Components;
using Microsoft.AspNetCore.Components;

namespace Checkito.Web.Testing.Components
{
    public class TestRunnerBase : ScopedComponentBase
    {
        private ITestRunner _testRunner = default!;
        private ITestManager _testManager = default!;
        private IAppContext _appContext = default!;
        [Parameter]
        public Guid TestCaseId { get; set; } = default!;

        public TestCase TestCase { get; set; } = default!;

        protected override async Task OnParametersSetAsync()
        {
            _testRunner = GetService<ITestRunner>();
            _testManager = GetService<ITestManager>();
            _appContext = GetService<IAppContext>();

            var result = await Mediator.Send(new GetTestCasesQuery());
            TestCase = result.TestCases.FirstOrDefault(x => x.Id == TestCaseId)!;
            if (TestCase == null)
            {
                throw new ApplicationException("Cannot find TestCase");
            }

            await _testRunner.Run(new Checkito.Testing.Services.Params.RunTestCaseParams(TestCase, await _appContext.GetTestContext()));
        }
    }
}
