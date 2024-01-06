using DNSUpdater.Config;
using DNSUpdater.Models.DTO;
using DNSUpdater.Models.DTO.Config;
using DNSUpdater.Services.Base;
using DNSUpdater.Utils;
using DNSUpdater.Utils.Exceptions;
using DNSUpdater.Utils.Helpers;
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
                properties.CheckRequiredProperties(new List<string>() { BusinessConfig.PROPERTY_SERVICEURL, BusinessConfig.PROPERTY_HTTP_VERB }, configModel.ServiceName);
            }
            catch (Exception e)
            {
                return await Task.FromResult<StrategyResponseDTO>(new StrategyResponseDTO(Enums.StrategyResponseStatusEnum.Error, e.GetBaseException().Message, ""));
            }

            List<string> ignoredParams = new List<string>() { BusinessConfig.PROPERTY_SERVICEURL, BusinessConfig.PROPERTY_HTTP_VERB, BusinessConfig.PROPERTY_CONTENT_TYPE, BusinessConfig.PROPERTY_REQUEST_HEADERS, BusinessConfig.PROPERTY_RETRY, BusinessConfig.PROPERTY_DELAY, BusinessConfig.PROPERTY_PATH_PROPERTIE };

            string response = await FunctionHelper.MakeRequestByListOfProperties(properties, ignoredParams, httpClient);
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
