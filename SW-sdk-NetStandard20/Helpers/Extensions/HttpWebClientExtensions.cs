using System.Net.Http;
using SW.NetStandard20.Exceptions.Asserts;
using SW.NetStandard20.Services.Parameters;

namespace SW.NetStandard20.Helpers.Extensions
{
    internal static class HttpWebClientExtensions
    {
        public static HttpClient AddAuthenticationHeaders(this HttpClient client, UserCredentialsParameters parameters)
        {
            ArgsCheck.IsNotNull(nameof(client), client);
            ArgsCheck.IsNotNull(nameof(parameters), parameters);

            var headers = parameters.ToAuthenticationHeaders();
            foreach (var key in headers.Keys)
            {
                client.DefaultRequestHeaders.Add(key, headers[key]);
            }

            return client;
        }
    }
}