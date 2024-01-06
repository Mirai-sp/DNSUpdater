using DNSUpdater.Config;
using DNSUpdater.Models.DTO.Config;
using DNSUpdater.Services;
using Newtonsoft.Json;

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

        public static async Task<string> MakeRequestByListOfProperties(List<PropertiesDTO> properties, List<string> ignoredParams, HttpService httpService)
        {
            string? headerParam = properties.FirstOrDefault(p => p.Name.ToLower().Equals(BusinessConfig.PROPERTY_REQUEST_HEADERS))?.Value;
            List<PropertiesDTO> headers = headerParam == null ? new List<PropertiesDTO>() : JsonConvert.DeserializeObject<List<PropertiesDTO>>(headerParam);

            List<PropertiesDTO> data = properties.Where(p => !ignoredParams.Any(p2 => p2.ToLower() == p.Name.ToLower())).ToList();

            return await httpService.SendAsync(properties.GetPropertyeValueByName(BusinessConfig.PROPERTY_SERVICEURL), properties.GetPropertyeValueByName(BusinessConfig.PROPERTY_HTTP_VERB), JsonConvert.SerializeObject(data), headers, properties.GetPropertyeValueByName(BusinessConfig.PROPERTY_CONTENT_TYPE));
        }
    }
}
