using DNSUpdater.Config;
using DNSUpdater.Models.DTO;
using DNSUpdater.Models.DTO.Config;

namespace DNSUpdater.Services.Base
{
    public abstract class UpdaterDDNSBase
    {
        public abstract Task<StrategyResponseDTO> UpdateDNS(ConfigModelDTO configModel, ListViewItem listViewItem);

        public async static Task<StrategyResponseDTO> UpdateDNSByConfigModel(ConfigModelDTO configModel, ListViewItem listViewItem)
        {
            Type serviceType = Type.GetType($"DNSUpdater.Services.Updater.{configModel.ServiceName}");
            if (serviceType == null)
                return await Task.FromResult<StrategyResponseDTO>(new StrategyResponseDTO(Enums.StrategyResponseStatusEnum.Error, BusinessConfig.FAILED(DictionaryError.ERROR_UPDATER_CLASS_NOT_FOUND(configModel.ServiceName)))).ConfigureAwait(false);
            else
            {
                UpdaterDDNSBase serviceInstance = (UpdaterDDNSBase)Activator.CreateInstance(serviceType);
                return await serviceInstance.UpdateDNS(configModel, listViewItem).ConfigureAwait(false);
            }
        }
    }
}
