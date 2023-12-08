using DNSUpdater.Config;
using DNSUpdater.Models.DTO;
using DNSUpdater.Models.DTO.Config;
using DNSUpdater.Services.Base;
using DNSUpdater.Utils;
using DNSUpdater.Utils.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DNSUpdater.Services.Strategy
{
    public class StrategyECHO_IP : StrategyBase
    {
        private HttpService httpClient = FactoryClass.GetHTTPClient();
        public async override Task<StrategyResponseDTO> Execute(ConfigModelDTO configModel, List<PropertiesDTO> properties)
        {
            try
            {
                FunctionHelper.CheckRequiredProperties(properties, new List<string>() { BusinessConfig.PROPERTY_GETURL }, configModel.ServiceName);
            }
            catch (Exception e)
            {
                return await Task.FromResult<StrategyResponseDTO>(new StrategyResponseDTO(Enums.StrategyResponseStatusEnum.Error, e.GetBaseException().Message));
            }


            string ip = await httpClient.GetAsync(properties.GetPropertyeValueByName(BusinessConfig.PROPERTY_GETURL)); //GetAwaiter().GetResult();
            if (properties.Any(x => x.Name.ToLower().Equals(BusinessConfig.PROPERTY_OUTPUT_PROPERTIE)))
            {
                JObject obj = JsonConvert.DeserializeObject<JObject>(ip);
                JToken validation = obj.SelectToken(properties.GetPropertyeValueByName(BusinessConfig.PROPERTY_OUTPUT_PROPERTIE));
                if (validation.)
                    string test = obj.origin;
            }
            return await Task.FromResult<StrategyResponseDTO>(new StrategyResponseDTO(Enums.StrategyResponseStatusEnum.Success, ip));



        }
    }
}
