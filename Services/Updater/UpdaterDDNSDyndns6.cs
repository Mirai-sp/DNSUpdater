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
                await StrategyBase.ExecuteByStrategyName(configModel, strategy.StrategyName, strategy.Properties);
                if (configModel.Response.Status.Equals(StrategyResponseStatusEnum.Success))
                    //configModel.IP = configModel.Response.Message;
                    break;
            }

            if (configModel.Response.Status.Equals(StrategyResponseStatusEnum.Success))
            {
                if ((string.IsNullOrEmpty(configModel.IP) || !configModel.Response.Message.Equals(configModel.IP)))
                {
                    configModel.IP = configModel.Response.Message;
                    configModel.Response.Message = BusinessConfig.SUCCESS(configModel.IP);
                }
                else
                {
                    configModel.Response.Status = StrategyResponseStatusEnum.NotNecessary;
                    configModel.Response.Message = BusinessConfig.NO_CHANGED(configModel.IP);
                }
            }
            return;
        }
    }
}
