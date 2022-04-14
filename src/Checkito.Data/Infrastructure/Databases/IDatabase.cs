using Checkito.Apis.Models;
using Checkito.Environments.Models;
using Checkito.Testing.Models;

namespace Checkito.Data.Infrastructure.Databases
{
    public interface IDatabase
    {
        Task<IList<ApiDefinition>> GetApis();
        Task<IList<TestCase>> GetTestCases();
        Task<IList<ExecutionEnv>> GetExecutionEnvs();
        Task SaveApis(IList<ApiDefinition> apis);
        Task SaveTestCases(IList<TestCase> testCases);
        Task SaveExecutionEnvs(IList<ExecutionEnv> executionEnvs);
    }
}
