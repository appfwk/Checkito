using Checkito.Apis.Models;
using Checkito.Environments.Models;
using IdentityModel.Client;

namespace Checkito.Http.Services
{
    public class HttpCallExecutor
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        public async Task<HttpResponseMessage> Execute(ApiCallDefinition apiCallDefinition, string? token, ExecutionEnv executionEnv)
        {
            var request = new HttpRequestMessage(
                apiCallDefinition.HttpMethod.ToHttpMethod(),
                executionEnv.ResolveVariables(apiCallDefinition.Uri));

            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.SetBearerToken(token);
            }
            //foreach (var header in apiCallDefinition.Headers)
            //{
            //    request.Headers.Add(header.Key, header.Value);
            //}

            if (!string.IsNullOrEmpty(apiCallDefinition.Body))
            {
                request.Content = new StringContent(executionEnv.ResolveVariables(apiCallDefinition.Body)!, System.Text.Encoding.UTF8, "application/json");
            }

            return await _httpClient.SendAsync(request);
        }
    }
}
