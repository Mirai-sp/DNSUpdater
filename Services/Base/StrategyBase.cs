using DNSUpdater.Config;
using DNSUpdater.Enums;
using DNSUpdater.Models.DTO;
using DNSUpdater.Models.DTO.Config;
using DNSUpdater.Utils.Exceptions;
using DNSUpdater.Utils.Helpers;

namespace DNSUpdater.Services.Base
{
    public abstract class StrategyBase
    {
        public abstract StrategyResponseDTO Execute(ConfigModelDTO configModel, List<PropertiesDTO> properties);

        public static StrategyResponseDTO ExecuteByStrategyName(ConfigModelDTO configModel, string strategyName, List<PropertiesDTO> properties)
        {
            Type strategyType = Type.GetType($"DNSUpdater.Services.Strategy.{strategyName}");
            if (strategyType == null)
                return new StrategyResponseDTO(Enums.StrategyResponseStatusEnum.Error, BusinessConfig.FAILED(DictionaryError.ERROR_UPDATER_CLASS_NOT_FOUND(strategyName)));
            else
            {
                StrategyBase strategyInstange = (StrategyBase)Activator.CreateInstance(strategyType);
                string retryParam = string.Empty;
                string delayParam = string.Empty;

                int retryTime = 1;
                StrategyResponseDTO response = new StrategyResponseDTO();
                try
                {
                    retryParam = FunctionHelper.GetPropertyeValueByName(properties, BusinessConfig.PROPERTY_RETRY);
                    int.Parse(retryParam);
                }
                catch (Exception ex)
                {
                    throw new ProjectException(DictionaryError.ERROR_UNABLE_TO_CONVERT_VALUE(retryParam, "Int"));
                }

                try
                {
                    delayParam = FunctionHelper.GetPropertyeValueByName(properties, BusinessConfig.PROPERTY_DELAY);
                    int.Parse(delayParam);
                }
                catch (Exception ex)
                {
                    throw new ProjectException(DictionaryError.ERROR_UNABLE_TO_CONVERT_VALUE(delayParam, "Int"));
                }

                while (retryTime <= int.Parse(retryParam))
                {
                    try
                    {
                        response = strategyInstange.Execute(configModel, properties);
                        if (response.Status.Equals(Enums.StrategyResponseStatusEnum.Success))
                            break;
                    }
                    catch (Exception ex)
                    {
                        response = new StrategyResponseDTO(StrategyResponseStatusEnum.Error, ex.GetBaseException().Message);
                    }
                    retryTime++;
                    Thread.Sleep(int.Parse(delayParam));
                }
                return response;
            }
        }
    }
}
