using DNSUpdater.Config;
using DNSUpdater.Models.DTO;
using DNSUpdater.Models.DTO.Config;

namespace DNSUpdater.Services.Base
{
    public abstract class UpdaterDDNSBase
    {
        public abstract StrategyResponseDTO UpdateDNS(ConfigModelDTO configModel, ListViewItem listViewItem);

        public static StrategyResponseDTO UpdateDNSByConfigModel(ConfigModelDTO configModel, ListViewItem listViewItem)
        {
            Type serviceType = Type.GetType($"DNSUpdater.Services.Updater.{configModel.ServiceName}");
            if (serviceType == null)
                return new StrategyResponseDTO(Enums.StrategyResponseStatusEnum.Error, BusinessConfig.FAILED(DictionaryError.ERROR_UPDATER_CLASS_NOT_FOUND(configModel.ServiceName)));
            else
            {
                UpdaterDDNSBase serviceInstance = (UpdaterDDNSBase)Activator.CreateInstance(serviceType);
                return serviceInstance.UpdateDNS(configModel, listViewItem);
            }
        }
    }
}
