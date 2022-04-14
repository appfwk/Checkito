using Checkito.Testing.Models;

namespace Checkito.Web.Testing.Models
{
    public class TestCaseViewModel
    {
        public TestCaseViewModel(TestCase testCase)
        {
            TestCase = testCase;
        }

        public TestCase TestCase { get; set; }

        public TestCaseEvaluation? Evaluation { get; set; }
    }
}
