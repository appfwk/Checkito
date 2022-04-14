using Checkito.Entities.Models;
using Checkito.Testing.Models;
using System;

namespace Checkito.Assertions.Models
{
    public class AssertEvaluation : EntityBase
    {
        private readonly TestCaseStepEvaluation _testCaseStepEvaluation;
        private bool _isSuccess;
        public AssertEvaluation(Guid assertDefinitionId, TestCaseStepEvaluation testCaseStepEvaluation)
        {
            AssertDefinitionId = assertDefinitionId;
            _testCaseStepEvaluation = testCaseStepEvaluation;
        }
        public Guid AssertDefinitionId { get; set; }


        public string? Message { get; set; }
        public bool IsSuccess
        {
            get => _isSuccess;
            set
            {
                if (!_isSuccess)
                {
                    _testCaseStepEvaluation.IsSuccess = false;
                }

                _isSuccess = value;
            }
        }
    }
}
