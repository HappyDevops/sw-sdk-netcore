namespace SW.NetStandard20.Services.Parameters
{
    public class ProxySettings
    {
        private const int NOT_SETTED_PORT_VALUE = -1;

        public ProxySettings()
        {
            Host = null;
            Port = NOT_SETTED_PORT_VALUE;
        }

        public bool AreSetted => Host != null && Port != NOT_SETTED_PORT_VALUE;

        public  string Host { get; set; }
        public  int Port { get; set; }
    }
}
