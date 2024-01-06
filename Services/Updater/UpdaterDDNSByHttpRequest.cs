using DNSUpdater.Config;
using DNSUpdater.Enums;
using DNSUpdater.Models.DTO.Config;
using DNSUpdater.Services.Base;
using DNSUpdater.Utils;
using DNSUpdater.Utils.Helpers;

namespace DNSUpdater.Services.Updater
{
    public class UpdaterDDNSByHttpRequest : UpdaterDDNSBase
    {
        private HttpService httpService = FactoryClass.GetHTTPClient();
        public override async Task UpdateDNS(ConfigModelDTO configModel, ListViewItem listViewItem)
        {
            configModel.Response.Status = StrategyResponseStatusEnum.Error;
            configModel.Response.Message = BusinessConfig.INITIALIZING_UPDATE(configModel.ServiceName);

            foreach (WorkStrategyDTO strategy in configModel.WorkStrategy.Where(find => find.Enabled.Equals(true)))
            {
                configModel.Response = await StrategyBase.ExecuteByStrategyName(configModel, strategy.StrategyName, strategy.Properties);
                if (configModel.Response.Status.Equals(StrategyResponseStatusEnum.Success))
                    break;
            }

            if (configModel.Response.Status.Equals(StrategyResponseStatusEnum.Success))
            {
                if ((string.IsNullOrEmpty(configModel.IP) || !configModel.Response.IP.Equals(configModel.IP)))
                {
                    string updateResponse = "";

                    try
                    {
                        List<string> ignoredParams = new List<string>() { BusinessConfig.PROPERTY_SERVICEURL, BusinessConfig.PROPERTY_HTTP_VERB, BusinessConfig.PROPERTY_CONTENT_TYPE, BusinessConfig.PROPERTY_REQUEST_HEADERS };

                        updateResponse = await FunctionHelper.MakeRequestByListOfProperties(configModel.Properties, ignoredParams, httpService);
                        configModel.IP = configModel.Response.IP;
                        configModel.Response.Message = BusinessConfig.SUBMITED(updateResponse);
                    }
                    catch (Exception ex)
                    {
                        configModel.IP = "";
                        configModel.Response.Status = StrategyResponseStatusEnum.Error;
                        configModel.Response.Message = BusinessConfig.FAILED_UPDATE(ex.GetBaseException().Message);
                    }
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
