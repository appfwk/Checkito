using Checkito.Entities.Models;

namespace Checkito.Assertions.Models
{
    public class AssertDefinition : EntityBase
    {
        public AssertType Type { get; set; }
        public string? Source { get; set; }
        public string? ExpectedValue { get; set; }
    }
}
