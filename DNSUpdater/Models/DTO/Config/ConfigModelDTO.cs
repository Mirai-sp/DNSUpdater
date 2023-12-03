using DNSUpdater.Models.DTO.Config.Properties;

namespace DNSUpdater.Models.DTO.Config
{
    public class ConfigModelDTO
    {
        public string ServiceName { get; set; }
        public bool Enabled { get; set; }
        public int Interval { get; set; }
        public List<PropertiesDTO> Properties { get; set; }
    }
}
