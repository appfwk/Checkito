using Checkito.Entities.Models;
using System.Collections.Generic;

namespace Checkito.Apis.Models
{
    public class ApiCallDefinition : EntityBase
    {
        public string? Name { get; set; }

        public string? Uri { get; set; }

        public HttpVerb HttpMethod { get; set; }

        public string? Body { get; set; }

        public IDictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
    }
}
