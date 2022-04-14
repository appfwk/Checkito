using Checkito.Apis.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Checkito.Apis.Services
{
    public interface IApiService
    {
        Task Save(IList<ApiDefinition> apis);
    }
}
