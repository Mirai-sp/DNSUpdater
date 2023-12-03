using DNSUpdater.Config;
using DNSUpdater.Enums;
using DNSUpdater.Models.DTO;
using DNSUpdater.Models.DTO.Config;
using DNSUpdater.Utils.Helpers;

namespace DNSUpdater.Services.Base
{
    public abstract class StrategyBase
    {
        public abstract Task<StrategyResponseDTO> Execute(ConfigModelDTO configModel, List<PropertiesDTO> properties);

        public async static Task ExecuteByStrategyName(ConfigModelDTO configModel, string strategyName, List<PropertiesDTO> properties)
        {
            Type strategyType = Type.GetType($"DNSUpdater.Services.Strategy.{strategyName}");
            if (strategyType == null)
            {
                configModel.Response = await Task.FromResult<StrategyResponseDTO>(new StrategyResponseDTO(Enums.StrategyResponseStatusEnum.Error, BusinessConfig.FAILED(DictionaryError.ERROR_UPDATER_CLASS_NOT_FOUND(strategyName))));
                return;
            }
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
                    retryParam = FunctionHelper.GetPropertyeValueByName(properties, BusinessConfig.PROPERTY_RETRY);
                    urlParam = FunctionHelper.GetPropertyeValueByName(properties, BusinessConfig.PROPERTY_GETURL);
                    int.Parse(retryParam);
                }
                catch (Exception ex)
                {
                    configModel.Response = await Task.FromResult<StrategyResponseDTO>(new StrategyResponseDTO(StrategyResponseStatusEnum.Error, DictionaryError.ERROR_UNABLE_TO_CONVERT_VALUE(retryParam, "Int")));
                    return;
                }

                try
                {
                    delayParam = FunctionHelper.GetPropertyeValueByName(properties, BusinessConfig.PROPERTY_DELAY);
                    int.Parse(delayParam);
                }
                catch (Exception ex)
                {
                    configModel.Response = await Task.FromResult<StrategyResponseDTO>(new StrategyResponseDTO(StrategyResponseStatusEnum.Error, DictionaryError.ERROR_UNABLE_TO_CONVERT_VALUE(delayParam, "Int")));
                    return;
                }

                while (retryCount <= int.Parse(retryParam))
                {

                    try
                    {
                        configModel.Response = await strategyInstange.Execute(configModel, properties);
                        return;
                    }
                    catch (Exception ex)
                    {
                        configModel.Response = await Task.FromResult<StrategyResponseDTO>(new StrategyResponseDTO(StrategyResponseStatusEnum.Error, DictionaryError.ERROR_ATTEMPT_RESEND(retryCount.ToString(), retryParam, strategyName, urlParam, ex.GetBaseException().Message)));
                        retryCount++;
                        await Task.Delay(int.Parse(delayParam));

                    }
                }
                return;
            }
        }
    }
}
