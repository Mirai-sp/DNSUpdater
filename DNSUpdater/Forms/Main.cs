using DNSUpdater.Config;
using DNSUpdater.Models.DTO.Config;
using DNSUpdater.Utils.Exceptions;
using Newtonsoft.Json;

namespace DNSUpdater
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<ConfigModelDTO> configuration = LoadConfiguration();
        }

        private List<ConfigModelDTO> LoadConfiguration()
        {
            string configFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config.json");
            if (!File.Exists(configFilePath))
                throw new ProjectException(DictionaryError.ERROR_CONFIG_FILE_DOES_NOT_EXISTS(configFilePath));

            string configJSON = File.ReadAllText(configFilePath);

            if (string.IsNullOrEmpty(configJSON))
                throw new ProjectException(DictionaryError.ERROR_CONFIG_FILE_IS_EMPTY(configFilePath));

            try
            {
                return JsonConvert.DeserializeObject<List<ConfigModelDTO>>(configJSON);
            }
            catch
            {
                MessageBox.Show(DictionaryError.ERROR_CONFIG_FILE_IS_INVALID(configFilePath), BusinessConfig.ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                return null;
            }
        }

        private void Teste(object source, EventArgs e)
        {
            MessageBox.Show("Teste");
        }

        private void LoadListView()
        {
            List<string> columns = new List<string>() { "URL", "LastUpdated" };
            columns.ForEach(elem =>
            {
                ColumnHeader ch = new ColumnHeader();
                ch.Text = elem;
                servicesList.Columns.Add(ch);

            });

            //define um item listview
            ListViewItem item;



        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}