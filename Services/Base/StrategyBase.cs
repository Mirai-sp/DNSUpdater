using DNSUpdater.Config;
using DNSUpdater.Enums;
using DNSUpdater.Models.DTO;
using DNSUpdater.Models.DTO.Config;

namespace DNSUpdater.Services.Base
{
    public abstract class StrategyBase
    {
        public abstract Task<StrategyResponseDTO> Execute(ConfigModelDTO configModel, List<PropertiesDTO> properties);

        public async static Task<StrategyResponseDTO> ExecuteByStrategyName(ConfigModelDTO configModel, string strategyName, List<PropertiesDTO> properties)
        {
            Type strategyType = Type.GetType($"DNSUpdater.Services.Strategy.{strategyName}");
            if (strategyType == null)
                return await Task.FromResult<StrategyResponseDTO>(new StrategyResponseDTO(Enums.StrategyResponseStatusEnum.Error, BusinessConfig.FAILED_RUN_STRATEGY(DictionaryError.ERROR_UPDATER_CLASS_NOT_FOUND(strategyName)), ""));
            else
            {
                StrategyBase strategyInstange = (StrategyBase)Activator.CreateInstance(strategyType);
                string retryParam = string.Empty;
                string delayParam = string.Empty;
                string urlParam = string.Empty;
                int retryCount = 1;

                StrategyResponseDTO response = new StrategyResponseDTO();
                try
                {
                    retryParam = properties.GetPropertyeValueByName(BusinessConfig.PROPERTY_RETRY);
                    urlParam = properties.GetPropertyeValueByName(BusinessConfig.PROPERTY_GETURL);
                    int.Parse(retryParam);
                }
                catch
                {
                    return await Task.FromResult(new StrategyResponseDTO(StrategyResponseStatusEnum.Error, DictionaryError.ERROR_UNABLE_TO_CONVERT_VALUE(retryParam, "Int"), "")); ;
                }

                try
                {
                    delayParam = properties.GetPropertyeValueByName(BusinessConfig.PROPERTY_DELAY);
                    int.Parse(delayParam);
                }
                catch
                {
                    return await Task.FromResult(new StrategyResponseDTO(StrategyResponseStatusEnum.Error, DictionaryError.ERROR_UNABLE_TO_CONVERT_VALUE(delayParam, "Int"), "")); ;
                }

                while (retryCount <= int.Parse(retryParam))
                {

                    try
                    {
                        return await strategyInstange.Execute(configModel, properties); ;
                    }
                    catch (Exception ex)
                    {
                        configModel.Response = new StrategyResponseDTO(StrategyResponseStatusEnum.Error, DictionaryError.ERROR_ATTEMPT_RESEND(retryCount.ToString(), retryParam, strategyName, urlParam, ex.GetBaseException().Message), "");
                        retryCount++;
                        await Task.Delay(int.Parse(delayParam));
                    }
                }
                return await Task.FromResult(new StrategyResponseDTO(Enums.StrategyResponseStatusEnum.Error, BusinessConfig.FAILED_RUN_STRATEGY(DictionaryError.ERROR_UPDATER_ALL_ATTEMPTS_WAS_TRIED(strategyName, configModel.ServiceName)), ""));
            }
        }
    }
}
