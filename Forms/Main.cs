using DNSUpdater.Config;
using DNSUpdater.Forms;
using DNSUpdater.Models.DTO.Config;
using DNSUpdater.Services.Base;
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
            List<ConfigModelDTO> configReader;
            if (!File.Exists(configFilePath))
                throw new ProjectException(DictionaryError.ERROR_CONFIG_FILE_DOES_NOT_EXISTS(configFilePath));

            string configJSON = File.ReadAllText(configFilePath);

            if (string.IsNullOrEmpty(configJSON))
                throw new ProjectException(DictionaryError.ERROR_CONFIG_FILE_IS_EMPTY(configFilePath));

            try
            {
                configReader = JsonConvert.DeserializeObject<List<ConfigModelDTO>>(configJSON);
            }
            catch
            {
                MessageBox.Show(DictionaryError.ERROR_CONFIG_FILE_IS_INVALID(configFilePath), BusinessConfig.ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<ConfigModelDTO>();
            }

            configReader.ForEach(config =>
            {
                FunctionHelper.CheckDuplicatesProperties(config.Properties, config.ServiceName);
                FunctionHelper.CheckRequiredProperties(config.Properties, new List<string>() { BusinessConfig.PROPERTY_SERVICEURL, BusinessConfig.PROPERTY_HTTP_VERB }, config.ServiceName);
                FunctionHelper.CheckPropertyeIsValid(config.Properties.GetPropertyeByName(BusinessConfig.PROPERTY_HTTP_VERB), new List<string>() { BusinessConfig.HTTP_GET, BusinessConfig.HTTP_POST, BusinessConfig.HTTP_PUT, BusinessConfig.HTTP_DELETE, BusinessConfig.HTTP_PATCH }, config.ServiceName);
                config.WorkStrategy.ForEach(workStrategy =>
                {
                    FunctionHelper.CheckDuplicatesProperties(workStrategy.Properties, config.ServiceName);
                    FunctionHelper.CheckRequiredProperties(workStrategy.Properties, new List<string>() { BusinessConfig.PROPERTY_RETRY, BusinessConfig.PROPERTY_DELAY }, config.ServiceName);
                });
            });

            configReader.ForEach(config =>
                {
                    config.Timer = new Timer()
                    {
                        Tag = config,
                        Enabled = config.Enabled,
                        Interval = config.Interval
                    };
                    config.Timer.Tick += new EventHandler(ScheduledTaskRun);
                });
            return configReader;

        }

        private void ReloadConfiguration()
        {
            selectedScheduledItem = null;
            if (configuration != null)
            {
                configuration.ForEach(config =>
                {
                    config.Timer.Enabled = false;
                    config.Timer.Tick -= ScheduledTaskRun;
                    config.Timer = null;
                });
                configuration.Clear();
                servicesList.Clear();
            }

            try
            {
                configuration = LoadConfiguration();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetBaseException().Message, BusinessConfig.ERROR_TITLE, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (configuration != null && configuration.Count > 0)
                LoadListView(configuration);

            btnStartStop.Enabled = false;
            btnStartStop.Text = BusinessConfig.ENABLED;
        }

        private async void ScheduledTaskRun(object source, EventArgs e)
        {
            var timer = (Timer)source;
            ConfigModelDTO configModel = (ConfigModelDTO)timer.Tag;
            configModel.Timer.Enabled = false;

            ListViewItem item = servicesList.GetListViewByConfigModelKey(configModel);

            item.SubItems[servicesList.GetSubItemIndexByText(BusinessConfig.NEXT_UPDATE)].Text = BusinessConfig.UPDATING;
            await UpdaterDDNSBase.UpdateDNSByConfigModel(configModel, item);
            item.SubItems[servicesList.GetSubItemIndexByText(BusinessConfig.LAST_UPDATED)].Text = DateTime.Now.ToString(BusinessConfig.DATETIME_OUTPUT);

            configModel.Timer.Enabled = true;
            item.SubItems[servicesList.GetSubItemIndexByText(BusinessConfig.NEXT_UPDATE)].Text = DateTime.Now.AddMilliseconds(configModel.Interval).ToString(BusinessConfig.DATETIME_OUTPUT);
        }

        private void DefineSelectedScheduledJob()
        {
            if (servicesList.SelectedItems.Count == 0)
                selectedScheduledItem = null;
            else
            {
                ConfigModelDTO scheduledJob = configuration.Where(search => search.Key.Equals(servicesList.FocusedItem.Tag)).FirstOrDefault();
                if (scheduledJob == null)
                    throw new ProjectException(DictionaryError.ERROR_NOT_WAS_POSSIBLE_LOAD_SELECTED_SCHEDULED_JOB(servicesList.FocusedItem.Text));
                selectedScheduledItem = scheduledJob;
            }
        }

        private void UpdateSelectedItemInfo(bool setScheduleInfo)
        {
            btnStartStop.Enabled = selectedScheduledItem != null;
            btnStartStop.Text = (selectedScheduledItem == null ? "Invalid Selection" : (selectedScheduledItem.Timer.Enabled ? BusinessConfig.DISABLE : BusinessConfig.ENABLE));
            servicesList.FocusedItem.SubItems[servicesList.GetSubItemIndexByText(BusinessConfig.ENABLED)].Text = (btnStartStop.Text.Equals(BusinessConfig.ENABLE) ? BusinessConfig.FALSE : BusinessConfig.TRUE);
            if (setScheduleInfo)
            {
                servicesList.FocusedItem.SubItems[servicesList.GetSubItemIndexByText(BusinessConfig.LAST_UPDATED)].Text = BusinessConfig.NOT_RUNED_YET;
                servicesList.FocusedItem.SubItems[servicesList.GetSubItemIndexByText(BusinessConfig.NEXT_UPDATE)].Text = !selectedScheduledItem.Timer.Enabled ? BusinessConfig.NOT_SCHEDULED : DateTime.Now.AddMilliseconds(selectedScheduledItem.Interval).ToString(BusinessConfig.DATETIME_OUTPUT);
                servicesList.FocusedItem.SubItems[servicesList.GetSubItemIndexByText(BusinessConfig.STATUS)].Text = BusinessConfig.PENDING;
            }
            FunctionHelper.AutoSizeColumnList(servicesList);
        }

        private void UpdateStatusListView()
        {
            if (configuration != null)
            {
                configuration.ForEach(configModel =>
                {
                    ListViewItem item = servicesList.GetListViewByConfigModelKey(configModel);
                    FunctionHelper.AutoSizeColumnList(servicesList);
                    item.SubItems[servicesList.GetSubItemIndexByText(BusinessConfig.STATUS)].Text = BusinessConfig.UPDATING;
                    item.SubItems[servicesList.GetSubItemIndexByText(BusinessConfig.STATUS)].Text = configModel.Response.Message;
                });
            }

        }

        private void LoadListView(List<ConfigModelDTO> configuration)
        {
            List<string> columns = new List<string>() { BusinessConfig.SERVICE_NAME, BusinessConfig.DOMAIN_NAME, BusinessConfig.ENABLED, BusinessConfig.INTERVAL, BusinessConfig.LAST_UPDATED, BusinessConfig.NEXT_UPDATE, BusinessConfig.STATUS };
            columns.ForEach(elem =>
            {
                ColumnHeader ch = new ColumnHeader();
                ch.Text = elem;
                servicesList.Columns.Add(ch);

            });

            ListViewItem item;
            configuration.ForEach(config =>
            {
                item = new ListViewItem();
                item.Tag = config.Key;
                item.Text = config.ServiceName;
                item.SubItems.Add(config.DomainName);
                item.SubItems.Add(config.Enabled ? BusinessConfig.TRUE : BusinessConfig.FALSE);
                item.SubItems.Add(config.Interval.ToString());
                item.SubItems.Add(BusinessConfig.NOT_RUNED_YET);
                item.SubItems.Add(!config.Enabled ? BusinessConfig.NOT_SCHEDULED : DateTime.Now.AddMilliseconds(config.Interval).ToString(BusinessConfig.DATETIME_OUTPUT));
                item.SubItems.Add(BusinessConfig.PENDING);

                servicesList.Items.Add(item);
            });
            //servicesList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            FunctionHelper.AutoSizeColumnList(servicesList);
        }
        #endregion

        #region EventControls
        private void Form1_Load(object sender, EventArgs e)
        {
            ReloadConfiguration();
            timer_Tick(sender, e);
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            lblNow.Text = $"Now Is {DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}";
            UpdateStatusListView();
        }

        private void btnStartStop_Click(object sender, EventArgs e)
        {
            selectedScheduledItem.Timer.Enabled = !selectedScheduledItem.Timer.Enabled;
            selectedScheduledItem.Enabled = selectedScheduledItem.Timer.Enabled;
            UpdateSelectedItemInfo(true);
        }

        private void servicesList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            DefineSelectedScheduledJob();

            if (servicesList.SelectedItems.Count == 0)
                return;
            UpdateSelectedItemInfo(false);
        }

        private void Main_Resize(object sender, EventArgs e)
        {
            FunctionHelper.AutoSizeColumnList(servicesList);
        }

        #endregion

        private void btnReloadConfiguration_Click(object sender, EventArgs e)
        {
            ReloadConfiguration();
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            About frmAbout = new About();
            frmAbout.ShowDialog(this);
            frmAbout.Dispose();
        }
    }
}