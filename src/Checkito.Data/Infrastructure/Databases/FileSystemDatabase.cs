using Checkito.Apis.Models;
using Checkito.Environments.Models;
using Checkito.Testing.Models;
using System.Reflection;
using System.Text.Json;

namespace Checkito.Data.Infrastructure.Databases
{
    public class FileSystemDatabase : IDatabase
    {
        private static readonly string _currentFolder = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location) ?? default!;
        private static readonly string _apisPath = Path.Combine(_currentFolder, "apis.json");
        private static readonly string _testCasesPath = Path.Combine(_currentFolder, "testCases.json");
        private static readonly string _executionEnvsPath = Path.Combine(_currentFolder, "executionEnvs.json");

        private IList<ApiDefinition>? _apis;
        private IList<TestCase>? _testCases;
        private IList<ExecutionEnv>? _executionEnvs;


        public async Task<IList<ApiDefinition>> GetApis()
        {
            if (_apis != null)
            {
                return _apis;
            }

            return await GetData<ApiDefinition>(_apisPath);
        }

        public async Task<IList<TestCase>> GetTestCases()
        {
            if (_testCases != null)
            {
                return _testCases;
            }

            return await GetData<TestCase>(_testCasesPath);
        }

        public Task SaveApis(IList<ApiDefinition> apis)
        {
            SaveData(apis, _apisPath);
            _apis = apis;
            return Task.CompletedTask;
        }

        public Task SaveTestCases(IList<TestCase> testCases)
        {
            SaveData(testCases, _testCasesPath);
            _testCases = testCases;
            return Task.CompletedTask;
        }

        private Task SaveData<TData>(IList<TData> data, string path)
        {
            File.WriteAllText(
                path,
                JsonSerializer.Serialize(data),
                System.Text.Encoding.UTF8);

            return Task.CompletedTask;
        }

        private async Task<IList<TData>> GetData<TData>(string path)
        {
            if (!File.Exists(path))
            {
                return GetEmptyData<TData>();
            }

            var data = JsonSerializer.Deserialize<List<TData>>(await File.ReadAllTextAsync(path));
            if (data == null)
            {
                return GetEmptyData<TData>();
            }

            return data;
        }

        private IList<TData> GetEmptyData<TData>()
        {
            return new List<TData>();
        }

        public async Task<IList<ExecutionEnv>> GetExecutionEnvs()
        {
            if (_executionEnvs != null)
            {
                return _executionEnvs;
            }

            return await GetData<ExecutionEnv>(_executionEnvsPath);
        }

        public Task SaveExecutionEnvs(IList<ExecutionEnv> executionEnvs)
        {
            SaveData(executionEnvs, _executionEnvsPath);
            _executionEnvs = executionEnvs;
            return Task.CompletedTask;
        }
    }
}
