using DNSUpdater.Models.DTO.Config;

namespace DNSUpdater.Services.Base
{
    public abstract class ServiceBase
    {
        public abstract bool UpdateDNS(List<WorkStrategyDTO> strategyWorkList);
    }
}
