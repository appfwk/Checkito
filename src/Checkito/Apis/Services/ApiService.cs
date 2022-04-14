using Checkito.Apis.Models;
using Checkito.Data.Infrastructure.Databases;

namespace Checkito.Apis.Services
{
    public class ApiService : IApiService
    {
        private readonly IDatabase _database;

        public ApiService(IDatabase database)
        {
            _database = database;
        }

        public async Task Save(IList<ApiDefinition> apis)
        {
            await _database.SaveApis(apis);
        }
    }
}
