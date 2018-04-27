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
            this.select = new System.Windows.Forms.Button();
            this.refresh = new System.Windows.Forms.Button();
            this.close = new System.Windows.Forms.Button();
            this.mem = new System.Windows.Forms.DataGridView();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.start = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.size = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uStart = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uStop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.actualSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.mem)).BeginInit();
            this.SuspendLayout();
            // 
            // select
            // 
            this.select.Location = new System.Drawing.Point(12, 256);
            this.select.Name = "select";
            this.select.Size = new System.Drawing.Size(115, 23);
            this.select.TabIndex = 1;
            this.select.Text = "Select";
            this.select.UseVisualStyleBackColor = true;
            // 
            // refresh
            // 
            this.refresh.Location = new System.Drawing.Point(133, 256);
            this.refresh.Name = "refresh";
            this.refresh.Size = new System.Drawing.Size(115, 23);
            this.refresh.TabIndex = 1;
            this.refresh.Text = "Refresh";
            this.refresh.UseVisualStyleBackColor = true;
            // 
            // close
            // 
            this.close.Location = new System.Drawing.Point(254, 256);
            this.close.Name = "close";
            this.close.Size = new System.Drawing.Size(115, 23);
            this.close.TabIndex = 1;
            this.close.Text = "Close";
            this.close.UseVisualStyleBackColor = true;
            // 
            // mem
            // 
            this.mem.AllowUserToAddRows = false;
            this.mem.AllowUserToDeleteRows = false;
            this.mem.AllowUserToResizeColumns = false;
            this.mem.AllowUserToResizeRows = false;
            this.mem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.mem.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.name,
            this.start,
            this.stop,
            this.size,
            this.uStart,
            this.uStop,
            this.actualSize});
            this.mem.Location = new System.Drawing.Point(12, 12);
            this.mem.MultiSelect = false;
            this.mem.Name = "mem";
            this.mem.RowHeadersVisible = false;
            this.mem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.mem.Size = new System.Drawing.Size(357, 238);
            this.mem.TabIndex = 3;
            // 
            // name
            // 
            this.name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.name.HeaderText = "Name";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            this.name.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // start
            // 
            this.start.HeaderText = "Start";
            this.start.Name = "start";
            this.start.ReadOnly = true;
            this.start.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.start.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.start.Width = 80;
            // 
            // stop
            // 
            this.stop.HeaderText = "Stop";
            this.stop.Name = "stop";
            this.stop.ReadOnly = true;
            this.stop.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.stop.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.stop.Width = 80;
            // 
            // size
            // 
            this.size.HeaderText = "Size";
            this.size.Name = "size";
            this.size.ReadOnly = true;
            this.size.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.size.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.size.Width = 50;
            // 
            // uStart
            // 
            this.uStart.HeaderText = "uStart";
            this.uStart.Name = "uStart";
            this.uStart.ReadOnly = true;
            this.uStart.Visible = false;
            // 
            // uStop
            // 
            this.uStop.HeaderText = "uStop";
            this.uStop.Name = "uStop";
            this.uStop.ReadOnly = true;
            this.uStop.Visible = false;
            // 
            // actualSize
            // 
            this.actualSize.HeaderText = "AcutalSize";
            this.actualSize.Name = "actualSize";
            this.actualSize.ReadOnly = true;
            this.actualSize.Visible = false;
            // 
            // Attach
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 291);
            this.Controls.Add(this.close);
            this.Controls.Add(this.refresh);
            this.Controls.Add(this.select);
            this.Controls.Add(this.mem);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Attach";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Memory Region";
            ((System.ComponentModel.ISupportInitialize)(this.mem)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button select;
        internal System.Windows.Forms.Button refresh;
        internal System.Windows.Forms.Button close;
        internal System.Windows.Forms.DataGridView mem;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn start;
        private System.Windows.Forms.DataGridViewTextBoxColumn stop;
        private System.Windows.Forms.DataGridViewTextBoxColumn size;
        private System.Windows.Forms.DataGridViewTextBoxColumn uStart;
        private System.Windows.Forms.DataGridViewTextBoxColumn uStop;
        private System.Windows.Forms.DataGridViewTextBoxColumn actualSize;

    }
}