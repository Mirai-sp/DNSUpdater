using DNSUpdater.Config;
using DNSUpdater.Models.DTO.Config;
using DNSUpdater.Utils.Exceptions;

namespace DNSUpdater.Utils.Helpers
{
    public static class FunctionHelper
    {
        public static void AutoSizeColumnList(ListView listView)
        {
            //Prevents flickering
            listView.BeginUpdate();

            Dictionary<int, int> columnSize = new Dictionary<int, int>();

            //Auto size using header
            listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            //Grab column size based on header
            foreach (ColumnHeader colHeader in listView.Columns)
                columnSize.Add(colHeader.Index, colHeader.Width);

            //Auto size using data
            listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

            //Grab comumn size based on data and set max width
            foreach (ColumnHeader colHeader in listView.Columns)
            {
                int nColWidth;
                if (columnSize.TryGetValue(colHeader.Index, out nColWidth))
                    colHeader.Width = Math.Max(nColWidth, colHeader.Width);
                else
                    //Default to 50
                    colHeader.Width = Math.Max(50, colHeader.Width);
            }

            listView.EndUpdate();
        }

        public static string GenerateRandonString()
        {
            Guid g = Guid.NewGuid();
            string GuidString = Convert.ToBase64String(g.ToByteArray());
            GuidString = GuidString.Replace("=", "");
            GuidString = GuidString.Replace("+", "");
            return GuidString;
        }

        public static void CheckDuplicatesPropertyes(List<PropertiesDTO> propertiesList, string serviceName)
        {
            var findDuplicatedParams = propertiesList.GroupBy(x => x.Name)
                        .Where(g => g.Count() > 1)
                        .Select(y => y.Key)
                        .ToList();

            if (findDuplicatedParams.Count() > 0)
                throw new ProjectException(DictionaryError.ERROR_DUPLICATED_PROPERTIES(serviceName, string.Join(", ", findDuplicatedParams)));
        }

        public static void CheckRequiredProperties(List<PropertiesDTO> propertiesList, List<string> requiredField, string serviceName)
        {
            var result = requiredField.Where(p => !propertiesList.Any(p2 => p2.Name.ToLower().Equals(p.ToLower()) && !string.IsNullOrEmpty(p2.Value)));
            if (result.Count() > 0)
                throw new ProjectException(DictionaryError.ERROR_REQUIRED_PROPERTIES(serviceName, string.Join(", ", result)));
        }

        public static void CheckPropertyeIsValid(PropertiesDTO propertieName, List<string> validValues, string serviceName)
        {
            if (!validValues.Select(sel => sel.ToLower()).Contains(propertieName.Value.ToLower()))
                throw new ProjectException(DictionaryError.ERROR_PROPERTIE_VALUE_IS_INVALID(serviceName, propertieName.Name, propertieName.Value));
        }

        public static PropertiesDTO GetPropertyeByName(List<PropertiesDTO> propertiesList, string propertyName)
        {
            return propertiesList.Where(search => search.Name.ToLower().Equals(propertyName.ToLower())).DefaultIfEmpty().Select(sel => sel).FirstOrDefault();
        }

        public static string GetPropertyeValueByName(List<PropertiesDTO> propertiesList, string propertyName)
        {
            return GetPropertyeByName(propertiesList, propertyName)?.Value;
        }

    }
}
