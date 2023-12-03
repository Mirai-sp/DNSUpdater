using DNSUpdater.Models.DTO.Config;
using DNSUpdater.Services.Base;

namespace DNSUpdater.Services.Strategy
{
    public class UpdaterDDNSDyndns6 : UpdaterDDNSBase
    {
        public override string UpdateDNS(ConfigModelDTO configModel, ListViewItem listViewItem)
        {
            return "DDNS6 Succesfull";
        }
    }
}
