using DNSUpdater.Config;
using DNSUpdater.Models.DTO.Config;

namespace DNSUpdater.Services.Base
{
    public abstract class UpdaterDDNSBase
    {
        public abstract string UpdateDNS(ConfigModelDTO configModel, ListViewItem listViewItem);

        public static string UpdateDNSByConfigModel(ConfigModelDTO configModel, ListViewItem listViewItem)
        {
            Type serviceType = Type.GetType($"DNSUpdater.Services.Updater.{configModel.ServiceName}");
            if (serviceType == null)
                return BusinessConfig.FAILED(DictionaryError.ERROR_UPDATER_CLASS_NOT_FOUND(configModel.ServiceName));
            else
            {
                UpdaterDDNSBase serviceInstance = (UpdaterDDNSBase)Activator.CreateInstance(serviceType);
                return serviceInstance.UpdateDNS(configModel, listViewItem);
            }
        }
    }
}
