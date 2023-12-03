using DNSUpdater.Config;
using DNSUpdater.Models.DTO.Config;

namespace DNSUpdater.Services.Base
{
    public abstract class StrategyBase
    {
        public abstract string Execute(List<PropertiesDTO> properties);

        public static string ExecuteByStrategyName(string strategyName, List<PropertiesDTO> properties)
        {
            Type strategyType = Type.GetType($"DNSUpdater.Services.Updater.{strategyName}");
            if (strategyType == null)
                return BusinessConfig.FAILED(DictionaryError.ERROR_UPDATER_CLASS_NOT_FOUND(strategyName));
            else
            {
                StrategyBase strategyInstange = (StrategyBase)Activator.CreateInstance(strategyType);
                return strategyInstange.Execute(properties);
            }
        }
    }
}
