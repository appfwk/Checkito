using Checkito.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Checkito.Testing.Models
{
    public class TestCaseEvaluation : EntityBase
    {
        public TestCaseEvaluation(Guid testCaseId)
        {
            TestCaseId = testCaseId;
        }

        public Guid TestCaseId { get; set; }

        public IList<TestCaseStepEvaluation> StepEvaluations { get; set; } = new List<TestCaseStepEvaluation>();
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public string GetFormattedResult()
        {
            var formattedResult = new StringBuilder(Message);
            foreach (var step in StepEvaluations)
            {
                if (!string.IsNullOrEmpty(step.Message))
                {
                    formattedResult.AppendLine(step.Message);
                }

                foreach (var assertEvaluation in step.AssertEvaluations)
                {
                    if (!string.IsNullOrEmpty(assertEvaluation.Message))
                    {
                        formattedResult.AppendLine(assertEvaluation.Message);
                    }
                }
            }

            return formattedResult.ToString();
        }
    }
}
