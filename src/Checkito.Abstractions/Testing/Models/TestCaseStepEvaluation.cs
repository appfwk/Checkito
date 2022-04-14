using Checkito.Assertions.Models;
using Checkito.Entities.Models;
using System;
using System.Collections.Generic;

namespace Checkito.Testing.Models
{
    public class TestCaseStepEvaluation : EntityBase
    {
        private readonly TestCaseEvaluation _testCaseEvaluation;
        public TestCaseStepEvaluation(Guid testCaseStepId, TestCaseEvaluation testCaseEvaluation)
        {
            TestCaseStepId = testCaseStepId;
            _testCaseEvaluation = testCaseEvaluation;
        }

        public Guid TestCaseStepId { get; set; }
        public string? Message { get; set; }
        public bool IsSuccess
        {
            get;
            set;
        }

        public IList<AssertEvaluation> AssertEvaluations = new List<AssertEvaluation>();
    }
}
