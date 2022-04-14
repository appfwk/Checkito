using Checkito.Data.Infrastructure.Databases;
using Checkito.Testing.Models;
using MediatR;

namespace Checkito.Data.Apis.Queries
{
    public class GetTestCasesQuery : IRequest<GetTestCasesQueryResult>
    {

    }

    public class GetTestCasesQueryHandler : IRequestHandler<GetTestCasesQuery, GetTestCasesQueryResult>
    {
        private readonly IDatabase _database;

        public GetTestCasesQueryHandler(IDatabase database)
        {
            _database = database;
        }

        public async Task<GetTestCasesQueryResult> Handle(GetTestCasesQuery request, CancellationToken cancellationToken)
        {
            return new GetTestCasesQueryResult { TestCases = await _database.GetTestCases() };
        }
    }

    public class GetTestCasesQueryResult
    {
        public IList<TestCase> TestCases { get; set; } = new List<TestCase>();
    }
}
