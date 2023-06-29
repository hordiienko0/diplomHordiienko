using System.Net.Http;
using System.Text;
using MediatR;
using Newtonsoft.Json;

namespace Ctor.IntegrationTests;

internal static class RequestExtensions
{
    public static StringContent ToJsonContent(this IRequest request)
    {
        var json = JsonConvert.SerializeObject(request);
        return new StringContent(json, Encoding.UTF8, "application/json");
    }
}