using Checkito.Data.Infrastructure.Databases;
using Checkito.Environments.Models;

namespace Checkito.Environments.Services
{
    public class ExecutionEnvService : IExecutionEnvService
    {
        private readonly IDatabase _database;

        public ExecutionEnvService(IDatabase database)
        {
            _database = database;
        }

        public async Task<ExecutionEnv?> GetEnvById(Guid id)
        {
            return (await _database.GetExecutionEnvs()).FirstOrDefault(x => x.Id == id);
        }

        public string ResolveVariables(ExecutionEnv executionEnv, string value)
        {
            //if("")
            return value;
        }

        public async Task Save(IList<ExecutionEnv> executionEnvs)
        {
            await _database.SaveExecutionEnvs(executionEnvs);
        }
    }
}
