using DNSUpdater.Config;
using DNSUpdater.Models.DTO;
using DNSUpdater.Models.DTO.Config;
using DNSUpdater.Services.Base;
using DNSUpdater.Utils;
using DNSUpdater.Utils.Helpers;

namespace DNSUpdater.Services.Strategy
{
    public class StrategyECHO_IP : StrategyBase
    {
        private HttpService httpClient = FactoryClass.GetHTTPClient();
        public override StrategyResponseDTO Execute(ConfigModelDTO configModel, List<PropertiesDTO> properties)
        {
            try
            {
                FunctionHelper.CheckRequiredProperties(properties, new List<string>() { BusinessConfig.PROPERTY_GETURL }, configModel.ServiceName);
            }
            catch (Exception e)
            {
                return new StrategyResponseDTO(Enums.StrategyResponseStatusEnum.Error, e.GetBaseException().Message);
            }

            string ip = httpClient.GetAsync(FunctionHelper.GetPropertyeValueByName(properties, BusinessConfig.PROPERTY_GETURL)); //GetAwaiter().GetResult();


            return new StrategyResponseDTO(Enums.StrategyResponseStatusEnum.Success, ip);
        }
    }
}
