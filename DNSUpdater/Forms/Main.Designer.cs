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
            this.dnsUpdaterTime = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.btnStartStop = new System.Windows.Forms.Button();
            this.lblURLService = new System.Windows.Forms.Label();
            this.servicesList = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // dnsUpdaterTime
            // 
            this.dnsUpdaterTime.Interval = 1000;
            this.dnsUpdaterTime.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "DNS:";
            // 
            // btnStartStop
            // 
            this.btnStartStop.Location = new System.Drawing.Point(12, 82);
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Size = new System.Drawing.Size(68, 23);
            this.btnStartStop.TabIndex = 1;
            this.btnStartStop.Text = "Start";
            this.btnStartStop.UseVisualStyleBackColor = true;
            // 
            // lblURLService
            // 
            this.lblURLService.AutoSize = true;
            this.lblURLService.Location = new System.Drawing.Point(51, 9);
            this.lblURLService.Name = "lblURLService";
            this.lblURLService.Size = new System.Drawing.Size(38, 15);
            this.lblURLService.TabIndex = 2;
            this.lblURLService.Text = "label2";
            // 
            // servicesList
            // 
            this.servicesList.Location = new System.Drawing.Point(146, 52);
            this.servicesList.Name = "servicesList";
            this.servicesList.Size = new System.Drawing.Size(326, 112);
            this.servicesList.TabIndex = 3;
            this.servicesList.UseCompatibleStateImageBehavior = false;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 382);
            this.Controls.Add(this.servicesList);
            this.Controls.Add(this.lblURLService);
            this.Controls.Add(this.btnStartStop);
            this.Controls.Add(this.label1);
            this.Name = "Main";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer dnsUpdaterTime;
        private Label label1;
        private Button btnStartStop;
        private Label lblURLService;
        private ListView servicesList;
    }
}