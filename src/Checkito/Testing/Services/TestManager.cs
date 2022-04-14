using Checkito.Data.Infrastructure.Databases;
using Checkito.Testing.Models;

namespace Checkito.Testing.Services
{
    public class TestManager : ITestManager
    {
        private readonly IDatabase _database;

        public TestManager(IDatabase database)
        {
            _database = database;
        }

        public async Task Save(IList<TestCase> testCases)
        {
            await _database.SaveTestCases(testCases);
        }
    }
}
