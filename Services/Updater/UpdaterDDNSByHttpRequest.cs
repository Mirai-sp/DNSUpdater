using DNSUpdater.Config;
using DNSUpdater.Enums;
using DNSUpdater.Models.DTO.Config;
using DNSUpdater.Services.Base;
using DNSUpdater.Utils;
using Newtonsoft.Json;

namespace DNSUpdater.Services.Updater
{
    public class UpdaterDDNSByHttpRequest : UpdaterDDNSBase
    {
        private HttpService httpService = FactoryClass.GetHTTPClient();
        public override async Task UpdateDNS(ConfigModelDTO configModel, ListViewItem listViewItem)
        {
            configModel.Response.Status = StrategyResponseStatusEnum.Error;
            configModel.Response.Message = BusinessConfig.INITIALIZING_UPDATE(configModel.ServiceName);

            foreach (WorkStrategyDTO strategy in configModel.WorkStrategy)
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
                        if (configModel.Properties.GetPropertyeValueByName(BusinessConfig.PROPERTY_HTTP_VERB).ToLower().Equals(BusinessConfig.HTTP_GET.ToLower()))
                        {
                            updateResponse = await httpService.GetAsync(configModel.Properties.GetPropertyeValueByName(BusinessConfig.PROPERTY_SERVICEURL));
                            configModel.IP = configModel.Response.IP;
                            configModel.Response.Message = BusinessConfig.SUBMITED(updateResponse);
                        }
                        else
                        {
                            List<string> ignoredParams = new List<string>() { BusinessConfig.PROPERTY_SERVICEURL, BusinessConfig.PROPERTY_HTTP_VERB, BusinessConfig.PROPERTY_CONTENT_TYPE };
                            List<PropertiesDTO> data = configModel.Properties.Where(p => !ignoredParams.Any(p2 => p2.ToLower() == p.Name.ToLower())).ToList();

                            updateResponse = await httpService.SendAsync(configModel.Properties.GetPropertyeValueByName(BusinessConfig.PROPERTY_SERVICEURL), configModel.Properties.GetPropertyeValueByName(BusinessConfig.PROPERTY_HTTP_VERB), JsonConvert.SerializeObject(data), configModel.Properties.GetPropertyeValueByName(BusinessConfig.PROPERTY_CONTENT_TYPE));
                            configModel.IP = configModel.Response.IP;
                            configModel.Response.Message = BusinessConfig.SUBMITED(updateResponse);
                        }
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
