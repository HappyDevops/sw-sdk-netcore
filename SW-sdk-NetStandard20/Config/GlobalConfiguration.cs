using System;
using SW.NetStandard20.Services.Parameters;

namespace SW.NetStandard20.Config
{
    public class GlobalConfiguration
    {
        public string Host { get; set; }
        public TimeSpan Timeout { get; set; }

        public bool UseGlobalProxySettings { get; set; }
        public  ProxySettings GlobalProxySettings { get; set; }
    }
}