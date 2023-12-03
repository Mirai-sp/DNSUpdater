using DNSUpdater.Enums;

namespace DNSUpdater.Models.DTO
{
    public class StrategyResponseDTO
    {
        public StrategyResponseStatusEnum Status { get; set; }
        public string Message { get; set; }

        public StrategyResponseDTO() { }

        public StrategyResponseDTO(StrategyResponseStatusEnum status, string message)
        {
            Status = status;
            Message = message;
        }
    }
}