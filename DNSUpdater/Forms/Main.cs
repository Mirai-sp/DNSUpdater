using DNSUpdater.Config;
using DNSUpdater.Models.DTO;
using DNSUpdater.Models.DTO.Config;
using DNSUpdater.Utils.Exceptions;
using DNSUpdater.Utils.Helpers;
using Newtonsoft.Json;
using Timer = System.Windows.Forms.Timer;

namespace DNSUpdater
{
    public partial class Main : Form
    {
        private List<ScheduledJobsDTO> configuration;
        private ScheduledJobsDTO? selectedScheduledItem;
        public Main()
        {
            InitializeComponent();
        }

        #region PrivateMethods
        private List<ScheduledJobsDTO> LoadConfiguration()
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
                List<ScheduledJobsDTO> scheduledJobsList = new List<ScheduledJobsDTO>();
                configuration.ForEach(elem =>
                {
                    ScheduledJobsDTO scheduledItem = new ScheduledJobsDTO()
                    {
                        ServiceName = elem.ServiceName,
                        Enabled = elem.Enabled,
                        Interval = elem.Interval,
                        Properties = elem.Properties,
                        Timer = new Timer()
                        {
                            Tag = elem,
                            Enabled = elem.Enabled.ToString().ToLower().Equals("true"),
                            Interval = elem.Interval
                        }
                    };
                    scheduledItem.Timer.Tick += new EventHandler(ScheduledTaskRun);
                    scheduledJobsList.Add(scheduledItem);
                });
                return scheduledJobsList;
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
            ListViewItem Item = servicesList.Items.Find(configModel.ServiceName, false).FirstOrDefault();

            MessageBox.Show($"Teste Service Name is {configModel.ServiceName}");
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
                ScheduledJobsDTO scheduledJob = configuration.Where(search => search.ServiceName.Equals(servicesList.FocusedItem.Text)).FirstOrDefault();
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
            servicesList.FocusedItem.SubItems[1].Text = (btnStartStop.Text.Equals(BusinessConfig.ENABLE) ? BusinessConfig.FALSE : BusinessConfig.TRUE);
        }

        private void LoadListView(List<ScheduledJobsDTO> configuration)
        {
            List<string> columns = new List<string>() { "Service Name", "Enabled", "Interval", "Last Updated", "Next Update" };
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
                item.Text = config.ServiceName;
                item.SubItems.Add(config.Enabled ? BusinessConfig.TRUE : BusinessConfig.FALSE);
                item.SubItems.Add(config.Interval.ToString());
                item.SubItems.Add(BusinessConfig.NOT_RUNED_YET);
                item.SubItems.Add(!config.Enabled ? BusinessConfig.NOT_SCHEDULED : DateTime.Now.AddMilliseconds(config.Interval).ToString("dd/MM/yyyy HH:mm:ss"));

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