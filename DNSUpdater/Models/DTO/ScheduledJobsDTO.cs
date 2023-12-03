using DNSUpdater.Models.DTO.Config;
using DNSUpdater.Utils.Helpers;
using Timer = System.Windows.Forms.Timer;

namespace DNSUpdater.Models.DTO
{
    public class ScheduledJobsDTO : ConfigModelDTO
    {
        public Timer Timer { get; set; }
        public string Key = FunctionHelper.GenerateRandonString();
    }
}
