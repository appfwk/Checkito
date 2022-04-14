using Checkito.Environments.Models;
using System;
using System.Text.Json.Serialization;

namespace Checkito.Testing.Models
{
    public class TestContext
    {
        public Guid? ExecutionEnvId { get; set; }
        [JsonIgnore]
        public ExecutionEnv ExecutionEnv { get; set; } = new ExecutionEnv();
    }
}
