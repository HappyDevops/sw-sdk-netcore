using System;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.Threading.Tasks;
using SW.NetStandard20.Config;
using SW.NetStandard20.Exceptions;
using SW.NetStandard20.Exceptions.Asserts;
using SW.NetStandard20.Helpers.Extensions;
using SW.NetStandard20.Services.Parameters;
using SW.NetStandard20.Services.Responses;

[assembly: InternalsVisibleTo("SW-sdk-NetStandard20.UnitTests")]
namespace SW.NetStandard20.Services.Authentication
{

    public class Authentication
    {
        private const string GET_TOKEN_SERVICE_PATH = "security/authenticate";

        public bool InfiniteTokenSpecified => !string.IsNullOrWhiteSpace(InfiniteToken);
        public string InfiniteToken { get; }
        private UserCredentialsParameters Parameters { get; }

        private GlobalConfiguration GlobalConfiguration { get; }
        public ProxySettings ProxySettings { get; }

        public Authentication(TokenServiceParameters parameters)
        {
            ArgsCheck.IsNotNull(parameters);
            ArgsCheck.IsNotNullOrEmptyOrWhiteSpace(nameof(parameters.Token), parameters.Token);

            InfiniteToken = parameters.Token;
            GlobalConfiguration = UtilsGlobalConfiguration.GetConfiguration();
            ProxySettings = parameters.ProxySettings;
        }

        internal Authentication(TokenServiceParameters parameters, GlobalConfiguration configuration)
        {
            ArgsCheck.IsNotNull(parameters);
            ArgsCheck.IsNotNullOrEmptyOrWhiteSpace(nameof(parameters.Token), parameters.Token);
            ArgsCheck.IsNotNull(configuration);

            InfiniteToken = parameters.Token;
            GlobalConfiguration = configuration;
            ProxySettings = parameters.ProxySettings;
        }

        public Authentication(UserCredentialsParameters parameters)
        {
            ArgsCheck.IsNotNull(parameters);

            InfiniteToken = null;
            Parameters = parameters;
            ProxySettings = parameters.ProxySettings;
            GlobalConfiguration = UtilsGlobalConfiguration.GetConfiguration();
        } 
        internal Authentication(UserCredentialsParameters parameters, GlobalConfiguration configuration)
        {
            ArgsCheck.IsNotNull(parameters);
            ArgsCheck.IsNotNull(configuration);

            InfiniteToken = null;
            Parameters = parameters;
            ProxySettings = parameters.ProxySettings;
            GlobalConfiguration = configuration;
        }

        private HttpClient GetHttpClient()
        {
            HttpClientHandler handler;
            
            if (GlobalConfiguration.UseGlobalProxySettings && (GlobalConfiguration.GlobalProxySettings?.AreSetted ?? true))
            {
                handler = GetClientHandler(GlobalConfiguration.GlobalProxySettings);
                return  new HttpClient(handler);
            }

            handler = GetClientHandler(ProxySettings);
            return !ProxySettings.AreSetted ? new HttpClient() : new HttpClient(handler);
        }

        private HttpClientHandler GetClientHandler(ProxySettings settings)
        {
            var address = settings.BuildAddress();
            var byPassOnLocal = settings.ByPassOnLocal;

            return new HttpClientHandler
            {
                Proxy = new WebProxy(address,byPassOnLocal),
                UseProxy = true
            };
        }

        public Response<AuthenticationData> GetToken()
        {
            if (InfiniteTokenSpecified)
            {
                var data = new AuthenticationData(InfiniteToken);
                return new Response<AuthenticationData>(data);
            }

            return HandleInnerExceptions(() =>
            {
                var client = GetHttpClient();
                client.BaseAddress = new Uri(GlobalConfiguration.Host);
                client.Timeout = GlobalConfiguration.Timeout;
                client = client.AddAuthenticationHeaders(Parameters);

                var postResponse  = client.PostAsync(GET_TOKEN_SERVICE_PATH, null);

                return PresentPostResponse(postResponse);
            });
        }

        private Response<AuthenticationData> PresentPostResponse(Task<HttpResponseMessage> response)
        {
            ArgsCheck.IsNotNull(nameof(response), response);

            if (response.IsFaulted)
            {
            }

            if (response.IsCanceled)
            {

            }

            var data = new AuthenticationData();
            return  new Response<AuthenticationData>();
        }

        private T HandleInnerExceptions<T>(Func<T> func)
        {
            ArgsCheck.IsNotNull(nameof(func), func);

            try
            {
                return func.Invoke();
            }
            catch (FaultException<FaultResponse>)
            {
                throw;
            }
            catch (HttpRequestException ex)
            {
                ExceptionHandler.HandleError(ex, ErrorHandlePolicy.Authentication);
                throw;
            }
            catch (Exception ex)
            {
               ExceptionHandler.HandleError(ex, ErrorHandlePolicy.Authentication);
               throw;
            }
        }
    }
}
