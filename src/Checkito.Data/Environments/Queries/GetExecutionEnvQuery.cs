using Checkito.Data.Infrastructure.Databases;
using Checkito.Environments.Models;
using MediatR;

namespace Checkito.Data.Environments.Queries
{
    public class GetExecutionEnvQuery : IRequest<GetExecutionEnvQueryResult>
    {
        public bool IsSelected { get; set; }
    }

    public class GetExecutionEnvQueryHandler : IRequestHandler<GetExecutionEnvQuery, GetExecutionEnvQueryResult>
    {
        private readonly IDatabase _database;

        public GetExecutionEnvQueryHandler(IDatabase database)
        {
            _database = database;
        }

        public async Task<GetExecutionEnvQueryResult> Handle(GetExecutionEnvQuery request, CancellationToken cancellationToken)
        {
            var envs = await _database.GetExecutionEnvs();
            var env = request.IsSelected ? envs.FirstOrDefault(x => x.IsSelected) : null;
            return new GetExecutionEnvQueryResult { ExecutionEnv = env };
        }
    }

    public class GetExecutionEnvQueryResult
    {
        public ExecutionEnv? ExecutionEnv { get; set; }
    }
}
