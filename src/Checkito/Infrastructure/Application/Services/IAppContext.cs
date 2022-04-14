using Checkito.Testing.Models;

namespace Checkito.Infrastructure.Application.Services
{
    public interface IAppContext
    {
        Task<TestContext> GetTestContext();
    }
}
