using DNSUpdater.Config;
using DNSUpdater.Utils.Helpers;
using Newtonsoft.Json;
using Timer = System.Windows.Forms.Timer;

namespace DNSUpdater.Models.DTO.Config
{
    public class ConfigModelDTO
    {
        public string ServiceName { get; set; }
        public string DomainName { get; set; }
        public bool Enabled { get; set; }
        public int Interval { get; set; }
        public List<PropertiesDTO> Properties { get; set; }
        public List<WorkStrategyDTO> WorkStrategy { get; set; }

        [JsonIgnore]
        public Timer Timer { get; set; }
        [JsonIgnore]
        public string Key = FunctionHelper.GenerateRandonString();

        [JsonIgnore]
        public string IP = string.Empty;

        [JsonIgnore]
        public StrategyResponseDTO Response { get; set; } = new StrategyResponseDTO(Enums.StrategyResponseStatusEnum.Error, BusinessConfig.PENDING);
    }
}
