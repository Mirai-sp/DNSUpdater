using DNSUpdater.Models.DTO.Config;

namespace System
{
    public static class ListViewExtensions
    {
        public static ListViewItem GetListViewByConfigModelKey(this ListView listView, ConfigModelDTO configModel)
        {
            /* if (listView.InvokeRequired)
                 return listView.Invoke(new Func<Object[]>(() => listView.Items.Cast<ListViewItem>().Where(elem => elem.Tag.Equals(configModel.Key)).FirstOrDefault()));
             else*/

            return listView.Items.Cast<ListViewItem>().Where(elem => elem.Tag.Equals(configModel.Key)).FirstOrDefault();
        }

        public static int GetSubItemIndexByText(this ListView listView, string text)
        {
            /*if (listView.InvokeRequired)
                return listView.Invoke(new Func<int>(() => listView.Columns.Cast<ColumnHeader>().Where(search => search.Text.Equals(text)).FirstOrDefault().Index));
            else*/
            return listView.Columns.Cast<ColumnHeader>().Where(search => search.Text.Equals(text)).FirstOrDefault().Index;
        }
    }
}
