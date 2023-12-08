using DNSUpdater.Models.DTO.Config;

namespace System
{
    public static class PropertiesDTOExtensions
    {
        public static PropertiesDTO GetPropertyeByName(this List<PropertiesDTO> properties, string propertyName)
        {
            return properties.Where(search => search.Name.ToLower().Equals(propertyName.ToLower())).DefaultIfEmpty().Select(sel => sel).FirstOrDefault();
        }

        public static string GetPropertyeValueByName(this List<PropertiesDTO> propertiesList, string propertyName)
        {
            return GetPropertyeByName(propertiesList, propertyName)?.Value;
        }
    }
}
