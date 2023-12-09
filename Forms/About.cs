using System.Diagnostics;

namespace DNSUpdater.Forms
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void About_Load(object sender, EventArgs e)
        {
            lblDevelopedBy.Text = $"This application was developed by Denis Queiroz (2022 - {DateTime.Now.ToString("yyyy")})";
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string url;
            if (e.Link.LinkData != null)
                url = e.Link.LinkData.ToString();
            else
                url = linkLabel1.Text.Substring(e.Link.Start, e.Link.Length);

            if (!url.Contains("://"))
                url = "https://" + url;

            var si = new ProcessStartInfo(url);
            Process.Start(new ProcessStartInfo { FileName = url, UseShellExecute = true });
            linkLabel1.LinkVisited = true;

        }
    }
}
