using Checkito.Data.Infrastructure.Databases;
using Checkito.Environments.Models;
using MediatR;

namespace Checkito.Data.Environments.Queries
{
    public class GetExecutionEnvsQuery : IRequest<GetExecutionEnvsQueryResult>
    {

    }

    public class GetExecutionEnvsQueryHandler : IRequestHandler<GetExecutionEnvsQuery, GetExecutionEnvsQueryResult>
    {
        private readonly IDatabase _database;

        public GetExecutionEnvsQueryHandler(IDatabase database)
        {
            _database = database;
        }

        public async Task<GetExecutionEnvsQueryResult> Handle(GetExecutionEnvsQuery request, CancellationToken cancellationToken)
        {
            return new GetExecutionEnvsQueryResult { ExecutionEnvs = await _database.GetExecutionEnvs() };
        }
    }

    public class GetExecutionEnvsQueryResult
    {
        public IList<ExecutionEnv> ExecutionEnvs { get; set; } = new List<ExecutionEnv>();
    }
}
