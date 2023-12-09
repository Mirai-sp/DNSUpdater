namespace DNSUpdater
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            Timer = new System.Windows.Forms.Timer(components);
            btnStartStop = new Button();
            servicesList = new ListView();
            lblNow = new Label();
            btnReloadConfiguration = new Button();
            btnAbout = new Button();
            SuspendLayout();
            // 
            // Timer
            // 
            Timer.Enabled = true;
            Timer.Interval = 1000;
            Timer.Tick += timer_Tick;
            // 
            // btnStartStop
            // 
            btnStartStop.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnStartStop.Enabled = false;
            btnStartStop.Location = new Point(12, 405);
            btnStartStop.Name = "btnStartStop";
            btnStartStop.Size = new Size(68, 23);
            btnStartStop.TabIndex = 1;
            btnStartStop.Text = "Start";
            btnStartStop.UseVisualStyleBackColor = true;
            btnStartStop.Click += btnStartStop_Click;
            // 
            // servicesList
            // 
            servicesList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            servicesList.FullRowSelect = true;
            servicesList.Location = new Point(12, 12);
            servicesList.MultiSelect = false;
            servicesList.Name = "servicesList";
            servicesList.Size = new Size(883, 370);
            servicesList.TabIndex = 3;
            servicesList.UseCompatibleStateImageBehavior = false;
            servicesList.View = View.Details;
            servicesList.ItemSelectionChanged += servicesList_ItemSelectionChanged;
            // 
            // lblNow
            // 
            lblNow.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblNow.AutoSize = true;
            lblNow.Location = new Point(12, 385);
            lblNow.Name = "lblNow";
            lblNow.Size = new Size(38, 15);
            lblNow.TabIndex = 4;
            lblNow.Text = "label1";
            // 
            // btnReloadConfiguration
            // 
            btnReloadConfiguration.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnReloadConfiguration.Location = new Point(86, 405);
            btnReloadConfiguration.Name = "btnReloadConfiguration";
            btnReloadConfiguration.Size = new Size(75, 23);
            btnReloadConfiguration.TabIndex = 5;
            btnReloadConfiguration.Text = "Reload";
            btnReloadConfiguration.UseVisualStyleBackColor = true;
            btnReloadConfiguration.Click += btnReloadConfiguration_Click;
            // 
            // btnAbout
            // 
            btnAbout.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnAbout.Location = new Point(820, 405);
            btnAbout.Name = "btnAbout";
            btnAbout.Size = new Size(75, 23);
            btnAbout.TabIndex = 6;
            btnAbout.Text = "About";
            btnAbout.UseVisualStyleBackColor = true;
            btnAbout.Click += btnAbout_Click;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(907, 440);
            Controls.Add(btnAbout);
            Controls.Add(btnReloadConfiguration);
            Controls.Add(lblNow);
            Controls.Add(servicesList);
            Controls.Add(btnStartStop);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Main";
            Text = "DNSUpdater";
            WindowState = FormWindowState.Maximized;
            Load += Form1_Load;
            Resize += Main_Resize;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Timer Timer;
        private Button btnStartStop;
        private ListView servicesList;
        private Label lblNow;
        private Button btnReloadConfiguration;
        private Button btnAbout;
    }
}