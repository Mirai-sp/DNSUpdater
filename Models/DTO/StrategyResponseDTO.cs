using DNSUpdater.Enums;

namespace DNSUpdater.Models.DTO
{
    public class StrategyResponseDTO
    {
        public StrategyResponseStatusEnum Status { get; set; }
        public string Message { get; set; }
        public string IP { get; set; }

        public StrategyResponseDTO() { }

        public StrategyResponseDTO(StrategyResponseStatusEnum status, string message, string iP)
        {
            Status = status;
            Message = message;
            IP = iP;
        }
    }
}