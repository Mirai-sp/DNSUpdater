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

            /*try
            {*/
            string ip = await httpClient.GetAsync(FunctionHelper.GetPropertyeValueByName(properties, BusinessConfig.PROPERTY_GETURL)).ConfigureAwait(false); //GetAwaiter().GetResult();
            return await Task.FromResult<StrategyResponseDTO>(new StrategyResponseDTO(Enums.StrategyResponseStatusEnum.Success, ip)).ConfigureAwait(false);
            /*}
            catch (Exception e)
            {
                return await Task.FromResult<StrategyResponseDTO>(new StrategyResponseDTO(Enums.StrategyResponseStatusEnum.Error, e.GetBaseException().Message)).ConfigureAwait(false);
            }*/



        }
    }
}
