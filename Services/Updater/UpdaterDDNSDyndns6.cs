using DNSUpdater.Config;
using DNSUpdater.Enums;
using DNSUpdater.Models.DTO.Config;
using DNSUpdater.Services.Base;

namespace DNSUpdater.Services.Updater
{
    public class UpdaterDDNSDyndns6 : UpdaterDDNSBase
    {
        public override async Task UpdateDNS(ConfigModelDTO configModel, ListViewItem listViewItem)
        {
            configModel.Response.Status = StrategyResponseStatusEnum.Error;
            configModel.Response.Message = BusinessConfig.FAILED(DictionaryError.ERROR_UNABLE_TO_UPDATE);

            foreach (WorkStrategyDTO strategy in configModel.WorkStrategy)
            {
                await StrategyBase.ExecuteByStrategyName(configModel, strategy.StrategyName, strategy.Properties).ConfigureAwait(true);
                if (configModel.Response.Status.Equals(StrategyResponseStatusEnum.Success)) { }
                break;
            }
            return;
        }
    }
}
