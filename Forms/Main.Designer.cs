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
            this.components = new System.ComponentModel.Container();
            this.Timer = new System.Windows.Forms.Timer(this.components);
            this.btnStartStop = new System.Windows.Forms.Button();
            this.servicesList = new System.Windows.Forms.ListView();
            this.lblNow = new System.Windows.Forms.Label();
            this.btnReloadConfiguration = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Timer
            // 
            this.Timer.Enabled = true;
            this.Timer.Interval = 1000;
            this.Timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // btnStartStop
            // 
            this.btnStartStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnStartStop.Enabled = false;
            this.btnStartStop.Location = new System.Drawing.Point(12, 405);
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Size = new System.Drawing.Size(68, 23);
            this.btnStartStop.TabIndex = 1;
            this.btnStartStop.Text = "Start";
            this.btnStartStop.UseVisualStyleBackColor = true;
            this.btnStartStop.Click += new System.EventHandler(this.btnStartStop_Click);
            // 
            // servicesList
            // 
            this.servicesList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.servicesList.FullRowSelect = true;
            this.servicesList.Location = new System.Drawing.Point(12, 12);
            this.servicesList.MultiSelect = false;
            this.servicesList.Name = "servicesList";
            this.servicesList.Size = new System.Drawing.Size(883, 370);
            this.servicesList.TabIndex = 3;
            this.servicesList.UseCompatibleStateImageBehavior = false;
            this.servicesList.View = System.Windows.Forms.View.Details;
            this.servicesList.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.servicesList_ItemSelectionChanged);
            // 
            // lblNow
            // 
            this.lblNow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblNow.AutoSize = true;
            this.lblNow.Location = new System.Drawing.Point(12, 385);
            this.lblNow.Name = "lblNow";
            this.lblNow.Size = new System.Drawing.Size(38, 15);
            this.lblNow.TabIndex = 4;
            this.lblNow.Text = "label1";
            // 
            // btnReloadConfiguration
            // 
            this.btnReloadConfiguration.Location = new System.Drawing.Point(86, 405);
            this.btnReloadConfiguration.Name = "btnReloadConfiguration";
            this.btnReloadConfiguration.Size = new System.Drawing.Size(75, 23);
            this.btnReloadConfiguration.TabIndex = 5;
            this.btnReloadConfiguration.Text = "Reload";
            this.btnReloadConfiguration.UseVisualStyleBackColor = true;
            this.btnReloadConfiguration.Click += new System.EventHandler(this.btnReloadConfiguration_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(907, 440);
            this.Controls.Add(this.btnReloadConfiguration);
            this.Controls.Add(this.lblNow);
            this.Controls.Add(this.servicesList);
            this.Controls.Add(this.btnStartStop);
            this.Name = "Main";
            this.Text = "DNSUpdater";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Main_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer Timer;
        private Button btnStartStop;
        private ListView servicesList;
        private Label lblNow;
        private Button btnReloadConfiguration;
    }
}