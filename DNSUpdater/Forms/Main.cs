using DNSUpdater.Config;
using DNSUpdater.Models.DTO.Config;
using DNSUpdater.Utils.Exceptions;
using DNSUpdater.Utils.Helpers;
using Newtonsoft.Json;
using Timer = System.Windows.Forms.Timer;

namespace DNSUpdater
{
    public partial class Main : Form
    {
        private List<ConfigModelDTO> configuration;
        private ConfigModelDTO? selectedScheduledItem;
        public Main()
        {
            InitializeComponent();
        }

        #region PrivateMethods
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
                List<ConfigModelDTO> configuration = JsonConvert.DeserializeObject<List<ConfigModelDTO>>(configJSON);
                configuration.ForEach(config =>
                {
                    config.Timer = new Timer()
                    {
                        Tag = config,
                        Enabled = config.Enabled,
                        Interval = config.Interval
                    };
                    config.Timer.Tick += new EventHandler(ScheduledTaskRun);
                });
                return configuration;
            }
            catch
            {
                MessageBox.Show(DictionaryError.ERROR_CONFIG_FILE_IS_INVALID(configFilePath), BusinessConfig.ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                return null;
            }
        }

        private void ScheduledTaskRun(object source, EventArgs e)
        {
            var timer = (Timer)source;

            ConfigModelDTO configModel = (ConfigModelDTO)timer.Tag;
            ListViewItem item = servicesList.Items.Cast<ListViewItem>().Where(elem => elem.Tag.Equals(configModel.Key)).FirstOrDefault();
            item.SubItems[servicesList.Columns.Cast<ColumnHeader>().Where(search => search.Text.Equals(BusinessConfig.LAST_UPDATED)).FirstOrDefault().Index].Text = DateTime.Now.ToString(BusinessConfig.DATETIME_OUTPUT);
            item.SubItems[servicesList.Columns.Cast<ColumnHeader>().Where(search => search.Text.Equals(BusinessConfig.LAST_UPDATED)).FirstOrDefault().Index].Text = DateTime.Now.ToString(BusinessConfig.DATETIME_OUTPUT);
            // Item.SubItems[BusinessConfig.LAST_UPDATED].Text = DateTime.Now.ToString(BusinessConfig.DATETIME_OUTPUT);

            //MessageBox.Show($"Teste Service Name is {configModel.ServiceName} and item text is {item.Text}");
        }

        /*private void CheckSelectedItem()
        {
            if (servicesList.SelectedItems.Count == 0)
            {
                MessageBox.Show(DictionaryError.ERROR_NO_SCHEDULED_TASK_SELECTED(), BusinessConfig.ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }*/

        private void DefineSelectedScheduledJob()
        {
            if (servicesList.SelectedItems.Count == 0)
                selectedScheduledItem = null;
            else
            {
                ConfigModelDTO scheduledJob = configuration.Where(search => search.ServiceName.Equals(servicesList.FocusedItem.Text)).FirstOrDefault();
                if (scheduledJob == null)
                    throw new ProjectException(DictionaryError.ERROR_NOT_WAS_POSSIBLE_LOAD_SELECTED_SCHEDULED_JOB(servicesList.FocusedItem.Text));
                selectedScheduledItem = scheduledJob;
            }
        }

        private void UpdateSelectedItemInfo()
        {
            /*if (servicesList.SelectedItems.Count == 0)
                return;*/

            btnStartStop.Enabled = selectedScheduledItem != null;
            btnStartStop.Text = (selectedScheduledItem == null ? "Invalid Selection" : (selectedScheduledItem.Timer.Enabled ? BusinessConfig.DISABLE : BusinessConfig.ENABLE));
            servicesList.FocusedItem.SubItems[servicesList.Columns.Cast<ColumnHeader>().Where(search => search.Text.Equals(BusinessConfig.ENABLED)).FirstOrDefault().Index].Text = (btnStartStop.Text.Equals(BusinessConfig.ENABLE) ? BusinessConfig.FALSE : BusinessConfig.TRUE);
        }

        private void LoadListView(List<ConfigModelDTO> configuration)
        {
            List<string> columns = new List<string>() { BusinessConfig.SERVICE_NAME, BusinessConfig.ENABLED, BusinessConfig.INTERVAL, BusinessConfig.LAST_UPDATED, BusinessConfig.NEXT_UPDATE };
            columns.ForEach(elem =>
            {
                ColumnHeader ch = new ColumnHeader();
                ch.Text = elem;
                servicesList.Columns.Add(ch);

            });

            //define um item listview
            ListViewItem item;
            configuration.ForEach(config =>
            {
                item = new ListViewItem();
                item.Tag = config.Key;
                item.Text = config.ServiceName;
                item.SubItems.Add(config.Enabled ? BusinessConfig.TRUE : BusinessConfig.FALSE);
                item.SubItems.Add(config.Interval.ToString());
                item.SubItems.Add(BusinessConfig.NOT_RUNED_YET);
                item.SubItems.Add(!config.Enabled ? BusinessConfig.NOT_SCHEDULED : DateTime.Now.AddMilliseconds(config.Interval).ToString(BusinessConfig.DATETIME_OUTPUT));

                servicesList.Items.Add(item);
            });
            //servicesList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            FunctionHelper.AutoSizeColumnList(servicesList);
        }
        #endregion

        #region EventControls
        private void Form1_Load(object sender, EventArgs e)
        {
            configuration = LoadConfiguration();
            if (configuration != null)
            {
                LoadListView(configuration);
                timer_Tick(sender, e);
            }
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            lblNow.Text = $"Now Is {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}";
        }

        private void btnStartStop_Click(object sender, EventArgs e)
        {
            selectedScheduledItem.Timer.Enabled = !selectedScheduledItem.Timer.Enabled;
            selectedScheduledItem.Enabled = selectedScheduledItem.Timer.Enabled;
            UpdateSelectedItemInfo();
        }

        private void servicesList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            DefineSelectedScheduledJob();

            if (servicesList.SelectedItems.Count == 0)
                return;
            UpdateSelectedItemInfo();
        }

        private void Main_Resize(object sender, EventArgs e)
        {
            FunctionHelper.AutoSizeColumnList(servicesList);
        }

        #endregion
    }
}