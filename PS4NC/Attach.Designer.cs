namespace PS4NC
{
    partial class Attach
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
            this.mem = new System.Windows.Forms.ComboBox();
            this.select = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mem
            // 
            this.mem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mem.FormattingEnabled = true;
            this.mem.Location = new System.Drawing.Point(12, 12);
            this.mem.Name = "mem";
            this.mem.Size = new System.Drawing.Size(321, 21);
            this.mem.TabIndex = 0;
            // 
            // select
            // 
            this.select.Location = new System.Drawing.Point(12, 39);
            this.select.Name = "select";
            this.select.Size = new System.Drawing.Size(321, 23);
            this.select.TabIndex = 1;
            this.select.Text = "Select";
            this.select.UseVisualStyleBackColor = true;
            // 
            // Attach
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 72);
            this.Controls.Add(this.select);
            this.Controls.Add(this.mem);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Attach";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Memory Region";
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.ComboBox mem;
        internal System.Windows.Forms.Button select;

    }
}