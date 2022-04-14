using Checkito.Environments.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Checkito.Environments.Services
{
    public interface IExecutionEnvService
    {
        Task Save(IList<ExecutionEnv> executionEnvs);
        Task<ExecutionEnv> GetEnvById(Guid id);

        string ResolveVariables(ExecutionEnv executionEnv, string value);
    }
}
