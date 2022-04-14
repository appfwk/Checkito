using Checkito.Apis.Models;
using Checkito.Data.Infrastructure.Databases;
using MediatR;

namespace Checkito.Data.Apis.Queries
{
    public class GetApisQuery : IRequest<GetApisQueryResult>
    {

    }

    public class GetApisQueryHandler : IRequestHandler<GetApisQuery, GetApisQueryResult>
    {
        private readonly IDatabase _database;

        public GetApisQueryHandler(IDatabase database)
        {
            _database = database;
        }

        public async Task<GetApisQueryResult> Handle(GetApisQuery request, CancellationToken cancellationToken)
        {
            return new GetApisQueryResult { Apis = await _database.GetApis() };
        }
    }

    public class GetApisQueryResult
    {
        public IList<ApiDefinition> Apis { get; set; } = new List<ApiDefinition>();
    }
}
