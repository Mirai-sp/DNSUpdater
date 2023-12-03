using DNSUpdater.Models.DTO.Config;

namespace System
{
    public static class ListViewExtensions
    {
        public static ListViewItem GetListViewByConfigModelKey(this ListView listView, ConfigModelDTO configModel)
        {
            return listView.Items.Cast<ListViewItem>().Where(elem => elem.Tag.Equals(configModel.Key)).FirstOrDefault();
        }

        public static int GetSubItemIndexByText(this ListView listView, string text)
        {
            return listView.Columns.Cast<ColumnHeader>().Where(search => search.Text.Equals(text)).FirstOrDefault().Index;
        }
    }
}
