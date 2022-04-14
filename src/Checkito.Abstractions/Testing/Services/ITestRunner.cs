using Checkito.Testing.Services.Params;
using System.Threading.Tasks;

namespace Checkito.Testing.Services
{
    public interface ITestRunner
    {
        Task<RunTestCaseResult> Run(RunTestCaseParams param);
    }
}
