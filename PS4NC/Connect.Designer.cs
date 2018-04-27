namespace PS4NC
{
    partial class Connect
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ipBox = new System.Windows.Forms.TextBox();
            this.cnct = new System.Windows.Forms.Button();
            this.fw = new System.Windows.Forms.ComboBox();
            this.inj = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.msg = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ipBox
            // 
            this.ipBox.Location = new System.Drawing.Point(67, 12);
            this.ipBox.Name = "ipBox";
            this.ipBox.Size = new System.Drawing.Size(228, 20);
            this.ipBox.TabIndex = 0;
            this.ipBox.Text = "192.168.1.3";
            this.ipBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // cnct
            // 
            this.cnct.Location = new System.Drawing.Point(185, 38);
            this.cnct.Name = "cnct";
            this.cnct.Size = new System.Drawing.Size(110, 23);
            this.cnct.TabIndex = 1;
            this.cnct.Text = "Connect";
            this.cnct.UseVisualStyleBackColor = true;
            // 
            // fw
            // 
            this.fw.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fw.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.fw.FormattingEnabled = true;
            this.fw.Items.AddRange(new object[] {
            "4.55",
            "4.05"});
            this.fw.Location = new System.Drawing.Point(12, 11);
            this.fw.Name = "fw";
            this.fw.Size = new System.Drawing.Size(49, 21);
            this.fw.TabIndex = 3;
            // 
            // inj
            // 
            this.inj.Location = new System.Drawing.Point(67, 38);
            this.inj.Name = "inj";
            this.inj.Size = new System.Drawing.Size(110, 23);
            this.inj.TabIndex = 4;
            this.inj.Text = "Inject Payload";
            this.inj.UseVisualStyleBackColor = true;
            this.inj.Click += new System.EventHandler(this.inj_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.msg});
            this.statusStrip1.Location = new System.Drawing.Point(0, 70);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(310, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(42, 17);
            this.toolStripStatusLabel1.Text = "Status:";
            // 
            // msg
            // 
            this.msg.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.msg.Name = "msg";
            this.msg.Size = new System.Drawing.Size(26, 17);
            this.msg.Text = "Idle";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(310, 92);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.inj);
            this.Controls.Add(this.fw);
            this.Controls.Add(this.cnct);
            this.Controls.Add(this.ipBox);
            this.Font = new System.Drawing.Font("Tahoma", 8F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Enter PS4 IP";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox ipBox;
        internal System.Windows.Forms.Button cnct;
        private System.Windows.Forms.ComboBox fw;
        private System.Windows.Forms.Button inj;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel msg;

    }
}