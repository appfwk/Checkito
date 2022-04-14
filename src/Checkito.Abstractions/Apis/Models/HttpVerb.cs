using System;
using System.Net.Http;

namespace Checkito.Apis.Models
{
    public enum HttpVerb
    {
        Get,
        Post,
        Put,
        Delete,
        Head,
        Patch,
        Options
    }

    public static class HttpVerbExtensions
    {
        public static HttpMethod ToHttpMethod(this HttpVerb httpVerb)
        {
            switch (httpVerb)
            {
                case HttpVerb.Get:
                    return HttpMethod.Get;
                case HttpVerb.Post:
                    return HttpMethod.Post;
                case HttpVerb.Put:
                    return HttpMethod.Put;
                case HttpVerb.Delete:
                    return HttpMethod.Delete;
                case HttpVerb.Head:
                    return HttpMethod.Head;
                case HttpVerb.Patch:
                    return new HttpMethod("PATCH");
                case HttpVerb.Options:
                    return HttpMethod.Options;
                default:
                    throw new NotImplementedException(httpVerb.ToString());
            }
        }
    }
}
