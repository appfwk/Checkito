using Checkito.Testing.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Checkito.Testing.Services
{
    public interface ITestManager
    {
        Task Save(IList<TestCase> testCases);
    }
}
