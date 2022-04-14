using Checkito.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Checkito.Apis.Models
{
    public class ApiDefinition : EntityBase
    {
        public string Name { get; set; } = "Default";

        public AuthenticationDefinition Authentication { get; set; } = new AuthenticationDefinition();

        public IList<ApiCallDefinition> ApiCallDefinitions { get; set; } = new List<ApiCallDefinition>();

        public ApiCallDefinition? GetCallDefinitionById(Guid callDefinitionId)
        {
            return ApiCallDefinitions.FirstOrDefault(x => x.Id == callDefinitionId);
        }
    }
}
