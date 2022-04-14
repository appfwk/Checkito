using Checkito.Apis.Models;
using Checkito.Apis.Services;
using Checkito.Data.Apis.Queries;
using Checkito.Web.General.Components;
using Microsoft.AspNetCore.Components;

namespace Checkito.Web.Apis.Components
{
    public class ApiDetailBase : ScopedComponentBase
    {
        private IApiService _apiService = default!;
        private NavigationManager _navManager = default!;
        public ApiDefinition? Api { get; set; }

        protected override void OnParametersSet()
        {
            _apiService = GetService<IApiService>();
            _navManager = GetService<NavigationManager>();
        }
        protected override async Task OnInitializedAsync()
        {
            var result = await Mediator.Send(new GetApisQuery());
            if (result.Apis.Count == 0)
            {
                result.Apis.Add(new ApiDefinition
                {
                    Name = "Default"
                });
            }

            Api = result.Apis[0];
        }

        public async Task OnOk()
        {
            await _apiService.Save(new List<ApiDefinition> { Api! });
        }
    }
}
