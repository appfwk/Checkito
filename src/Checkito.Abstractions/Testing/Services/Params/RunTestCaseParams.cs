using Checkito.Testing.Models;

namespace Checkito.Testing.Services.Params
{
    public class RunTestCaseParams
    {
        public RunTestCaseParams(TestCase testCase, TestContext testContext)
        {
            TestCase = testCase;
            TestContext = testContext;
        }

        public TestCase TestCase { get; set; }

        public TestContext TestContext { get; set; }
    }

    public class RunTestCaseResult
    {
        public TestCaseEvaluation TestCaseEvaluation { get; set; }

        public RunTestCaseResult(TestCaseEvaluation testCaseEvaluation)
        {
            TestCaseEvaluation = testCaseEvaluation;
        }
    }
}
