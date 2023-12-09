using DNSUpdater.Services;
using DNSUpdater.Services.Base;
using DNSUpdater.Services.Updater;

namespace DNSUpdater.Utils
{
    public static class FactoryClass
    {
        private static UpdaterDDNSBase dynDNS6;
        private static HttpService httpClient;

        public static UpdaterDDNSBase GetUpdaterDDNSDynDNS6()
        {
            if (dynDNS6 == null)
            {
                dynDNS6 = new UpdaterDDNSByHttpRequest();
            }
            return dynDNS6;
        }

        public static HttpService GetHTTPClient()
        {
            if (httpClient == null)
            {
                httpClient = new HttpService();
            }
            return httpClient;
        }
    }
}
