using DNSUpdater.Config;
using DNSUpdater.Enums;
using DNSUpdater.Models.DTO;
using DNSUpdater.Models.DTO.Config;
using DNSUpdater.Services.Base;

namespace DNSUpdater.Services.Updater
{
    public class UpdaterDDNSDyndns6 : UpdaterDDNSBase
    {
        public override StrategyResponseDTO UpdateDNS(ConfigModelDTO configModel, ListViewItem listViewItem)
        {
            StrategyResponseDTO response = new StrategyResponseDTO(StrategyResponseStatusEnum.Error, BusinessConfig.FAILED(DictionaryError.ERROR_UNABLE_TO_UPDATE));
            foreach (WorkStrategyDTO strategy in configModel.WorkStrategy)
            {
                response = StrategyBase.ExecuteByStrategyName(configModel, strategy.StrategyName, strategy.Properties);
                if (response.Status.Equals(StrategyResponseStatusEnum.Success)) { }
                break;
            }
            return response;
        }
    }
}
