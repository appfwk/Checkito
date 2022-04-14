using Checkito.Data.Infrastructure.Databases;
using Checkito.Testing.Models;

namespace Checkito.Infrastructure.Application.Services
{
    public class AppContext : IAppContext
    {
        private TestContext? _testContext = null;
        private readonly IDatabase _database;

        public AppContext(IDatabase database)
        {
            _database = database;
        }

        public async Task<TestContext> GetTestContext()
        {
            if (_testContext == null)
            {
                var envs = await _database.GetExecutionEnvs();
                _testContext = new TestContext();
                if (envs.Count > 0)
                {
                    var selectedEnv = envs.FirstOrDefault(x => x.IsSelected);
                    if (selectedEnv == null)
                    {
                        selectedEnv = envs[0];
                        selectedEnv.IsSelected = true;
                        await _database.SaveExecutionEnvs(envs);
                    }
                    _testContext.ExecutionEnv = selectedEnv;
                }
            }

            return _testContext;
        }
    }
}
