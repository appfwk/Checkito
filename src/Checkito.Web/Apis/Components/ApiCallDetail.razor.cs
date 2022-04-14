using Checkito.Apis.Models;
using Checkito.Apis.Services;
using Checkito.Data.Apis.Queries;
using Checkito.Web.General.Components;
using Microsoft.AspNetCore.Components;
using System.Text;

namespace Checkito.Web.Apis.Components
{
    public class ApiCallDetailBase : ScopedComponentBase
    {
        private IApiService _apiService = default!;
        private ApiDefinition? _api;
        private bool _isNew;



        [Parameter]
        public Guid? ApiCallId { get; set; }

        public ApiCallDefinition? ApiCall { get; set; }

        public string? HeadersContent { get; set; }

        protected override void OnParametersSet()
        {
            _apiService = GetService<IApiService>();
        }

        protected override async Task OnInitializedAsync()
        {
            var result = await Mediator.Send(new GetApisQuery());
            if (result.Apis.Count == 0)
            {
                return;
            }

            _api = result.Apis[0];

            if (ApiCallId.HasValue)
            {
                ApiCall = _api.GetCallDefinitionById(ApiCallId.Value);

                if (ApiCall == null)
                {
                    throw new ApplicationException("Cannot load Api Call Definition");
                }

                if (ApiCall.Headers.Count > 0)
                {
                    var headersBuilder = new StringBuilder();
                    foreach (var header in ApiCall.Headers)
                    {

                        headersBuilder
                            .Append(header.Key)
                            .Append(": ")
                            .Append(header.Value);

                        headersBuilder.AppendLine();
                    }

                    HeadersContent = headersBuilder.ToString();
                }
            }
            else
            {
                ApiCall = new ApiCallDefinition();
                _isNew = true;
            }
        }

        public async Task OnOk(NavigationManager navManager)
        {
            if (_api == null)
            {
                return;
            }

            if (_isNew)
            {
                _api.ApiCallDefinitions.Add(ApiCall!);
            }

            if (ApiCall != null)
            {
                ApiCall.Headers.Clear();

                if (!string.IsNullOrWhiteSpace(HeadersContent))
                {
                    foreach (var headerLine in HeadersContent.Split(Environment.NewLine))
                    {
                        if (string.IsNullOrWhiteSpace(headerLine))
                        {
                            continue;
                        }

                        var parsedHeader = headerLine.Split(':');
                        ApiCall.Headers.Add(parsedHeader[0].Trim(), parsedHeader[1].Trim());
                    }
                };
            }



            await _apiService.Save(new List<ApiDefinition> { _api });

            navManager.NavigateTo("apis");
        }
    }
}
