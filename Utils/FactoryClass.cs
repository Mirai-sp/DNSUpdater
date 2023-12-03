using DNSUpdater.Services.Base;
using DNSUpdater.Services.Updater;

namespace DNSUpdater.Utils
{
    public static class FactoryClass
    {
        private static UpdaterDDNSBase dynDNS6;

        public static UpdaterDDNSBase GetUpdaterDDNSDynDNS6()
        {
            if (dynDNS6 == null)
            {
                dynDNS6 = new UpdaterDDNSDyndns6();
            }
            return dynDNS6;
        }
    }
}
