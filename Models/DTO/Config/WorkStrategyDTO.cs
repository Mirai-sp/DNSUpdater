﻿namespace DNSUpdater.Models.DTO.Config
{
    public class WorkStrategyDTO
    {
        public string StrategyName { get; set; }
        public bool Enabled { get; set; }
        public List<PropertiesDTO> Properties { get; set; }
    }
}