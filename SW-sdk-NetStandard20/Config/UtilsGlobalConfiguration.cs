using SW.NetStandard20.Services.Parameters;

namespace SW.NetStandard20.Config
{
    public static class UtilsGlobalConfiguration
    {
        public static GlobalConfiguration GetConfiguration()
        {
            return new GlobalConfiguration
            {
                Host = "http://services.test.sw.com.mx",
                UseGlobalProxySettings = false,
                GlobalProxySettings = ProxySettings.GetEmptySettings()
            };
        }
    }
}
