using DNSUpdater.Config;
using DNSUpdater.Models.DTO;
using DNSUpdater.Models.DTO.Config;
using DNSUpdater.Services.Base;
using DNSUpdater.Utils;
using DNSUpdater.Utils.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;

namespace DNSUpdater.Services.Strategy
{
    public class StrategyECHO_IP : StrategyBase
    {
        private HttpService httpClient = FactoryClass.GetHTTPClient();
        public async override Task<StrategyResponseDTO> Execute(ConfigModelDTO configModel, List<PropertiesDTO> properties)
        {
            try
            {
                properties.CheckRequiredProperties(new List<string>() { BusinessConfig.PROPERTY_GETURL }, configModel.ServiceName);
            }
            catch (Exception e)
            {
                return await Task.FromResult<StrategyResponseDTO>(new StrategyResponseDTO(Enums.StrategyResponseStatusEnum.Error, e.GetBaseException().Message, ""));
            }

            string response = await httpClient.GetAsync(properties.GetPropertyeValueByName(BusinessConfig.PROPERTY_GETURL));
            if (properties.Any(x => x.Name.ToLower().Equals(BusinessConfig.PROPERTY_PATH_PROPERTIE.ToLower())))
            {
                JObject obj = JsonConvert.DeserializeObject<JObject>(response);
                string outputPropertiePath = properties.GetPropertyeValueByName(BusinessConfig.PROPERTY_PATH_PROPERTIE);
                JToken validation = obj.SelectToken(outputPropertiePath);
                if (validation == null)
                    throw new ProjectException(DictionaryError.ERROR_UNABLE_GET_PROPERTIE_VALUE_FROM_RESPONSE(configModel.ServiceName, response, outputPropertiePath));

                response = validation.ToString();
            }

            IPAddress address;
            if (!IPAddress.TryParse(response, out address))
                throw new ProjectException(DictionaryError.ERROR_RESPONSE_VALUE_IS_NOT_A_VALID_IP_ADDRESS(configModel.ServiceName, response));

            return await Task.FromResult<StrategyResponseDTO>(new StrategyResponseDTO(Enums.StrategyResponseStatusEnum.Success, BusinessConfig.STRATEGY_RETRIEVE_IP_SUCESSFULL(configModel.ServiceName, response), response));
        }
    }
}
