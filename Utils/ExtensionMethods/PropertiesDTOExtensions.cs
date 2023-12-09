using DNSUpdater.Config;
using DNSUpdater.Models.DTO.Config;
using DNSUpdater.Utils.Exceptions;

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

        public static void CheckDuplicatesProperties(this List<PropertiesDTO> propertiesList, string serviceName)
        {
            var findDuplicatedParams = propertiesList.GroupBy(x => x.Name)
                        .Where(g => g.Count() > 1)
                        .Select(y => y.Key)
                        .ToList();

            if (findDuplicatedParams.Count() > 0)
                throw new ProjectException(DictionaryError.ERROR_DUPLICATED_PROPERTIES(serviceName, string.Join(", ", findDuplicatedParams)));
        }

        public static void CheckRequiredProperties(this List<PropertiesDTO> propertiesList, List<string> requiredField, string serviceName)
        {
            var result = requiredField.Where(p => !propertiesList.Any(p2 => p2.Name.ToLower().Equals(p.ToLower()) && !string.IsNullOrEmpty(p2.Value)));
            if (result.Count() > 0)
                throw new ProjectException(DictionaryError.ERROR_REQUIRED_PROPERTIES(serviceName, string.Join(", ", result)));
        }

        public static void CheckPropertyeIsValid(this PropertiesDTO propertieName, List<string> validValues, string serviceName)
        {
            if (!validValues.Select(sel => sel.ToLower()).Contains(propertieName.Value.ToLower()))
                throw new ProjectException(DictionaryError.ERROR_PROPERTIE_VALUE_IS_INVALID(serviceName, propertieName.Name, propertieName.Value));
        }
    }
}
