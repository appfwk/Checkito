using Checkito.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Checkito.Testing.Models
{
    public class TestCase : EntityBase
    {
        public string Name { get; set; } = "Test";

        public IList<TestCaseStep> Steps { get; set; } = new List<TestCaseStep>();

        public TestCaseStep? GetStepById(Guid stepId)
        {
            return Steps.FirstOrDefault(x => x.Id == stepId);
        }
    }
}
