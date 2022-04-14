using Checkito.Apis.Models;
using Checkito.Assertions.Models;
using Checkito.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Checkito.Testing.Models
{
    public class TestCaseStep : EntityBase
    {
        public string Name { get; set; } = "Step";

        public Guid? ApiCallId { get; set; }

        public ApiCallDefinition? ApiCall { get; set; }

        public IList<AssertDefinition> Asserts { get; set; } = new List<AssertDefinition>();

        public AssertDefinition GetAssertById(Guid id)
        {
            return Asserts.FirstOrDefault(x => x.Id == id);
        }
    }
}
