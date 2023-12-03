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
                int retryCount = 1;

                StrategyResponseDTO response = new StrategyResponseDTO();
                try
                {
                    retryParam = FunctionHelper.GetPropertyeValueByName(properties, BusinessConfig.PROPERTY_RETRY);
                    int.Parse(retryParam);
                }
                catch (Exception ex)
                {
                    configModel.Response = await Task.FromResult<StrategyResponseDTO>(new StrategyResponseDTO(StrategyResponseStatusEnum.Error, DictionaryError.ERROR_UNABLE_TO_CONVERT_VALUE(retryParam, "Int"))).ConfigureAwait(false);
                    return;
                }

                try
                {
                    delayParam = FunctionHelper.GetPropertyeValueByName(properties, BusinessConfig.PROPERTY_DELAY);
                    int.Parse(delayParam);
                }
                catch (Exception ex)
                {
                    configModel.Response = await Task.FromResult<StrategyResponseDTO>(new StrategyResponseDTO(StrategyResponseStatusEnum.Error, DictionaryError.ERROR_UNABLE_TO_CONVERT_VALUE(delayParam, "Int"))).ConfigureAwait(false);
                    return;
                }

                while (retryCount <= int.Parse(retryParam))
                {

                    try
                    {
                        configModel.Response = await strategyInstange.Execute(configModel, properties).ConfigureAwait(false);
                        return;
                    }
                    catch (Exception ex)
                    {
                        retryCount++;
                        configModel.Response = await Task.FromResult<StrategyResponseDTO>(new StrategyResponseDTO(StrategyResponseStatusEnum.Error, DictionaryError.ERROR_ATTEMPT_RESEND(retryCount.ToString(), retryParam, "teste", ex.GetBaseException().Message))).ConfigureAwait(false);
                        await Task.Delay(int.Parse(delayParam)).ConfigureAwait(false);
                        //var task = Task.Factory.StartNew(() => Thread.Sleep(new TimeSpan(0, 0, 2)));
                        //Task.WaitAll(new[] { task });
                        //throw new ProjectException(DictionaryError.ERROR_ATTEMPT_RESEND(retryCount.ToString(), retryParam, "teste", ex.GetBaseException().Message));
                    }
                    _ = "Teste";
                    //await Task.Run(() => Thread.Sleep(int.Parse(delayParam))).ConfigureAwait(true);
                    //await Task.Delay(int.Parse(delayParam));


                }
                return;
            }
        }
    }
}
