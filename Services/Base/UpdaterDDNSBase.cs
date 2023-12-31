﻿using DNSUpdater.Config;
using DNSUpdater.Models.DTO;
using DNSUpdater.Models.DTO.Config;

namespace DNSUpdater.Services.Base
{
    public abstract class UpdaterDDNSBase
    {
        public abstract Task UpdateDNS(ConfigModelDTO configModel, ListViewItem listViewItem);

        public async static Task UpdateDNSByConfigModel(ConfigModelDTO configModel, ListViewItem listViewItem)
        {
            Type serviceType = Type.GetType($"DNSUpdater.Services.Updater.{configModel.ServiceName}");
            if (serviceType == null)
                await Task.Run(() => configModel.Response = new StrategyResponseDTO(Enums.StrategyResponseStatusEnum.Error, BusinessConfig.FAILED_RUN_STRATEGY(DictionaryError.ERROR_UPDATER_CLASS_NOT_FOUND(configModel.ServiceName)), ""));
            else
            {
                UpdaterDDNSBase serviceInstance = (UpdaterDDNSBase)Activator.CreateInstance(serviceType);
                await Task.Run(() => serviceInstance.UpdateDNS(configModel, listViewItem));
            }
        }
    }
}
