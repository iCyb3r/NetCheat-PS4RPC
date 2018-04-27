using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NCAppInterface;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Automation;
using System.Threading;
using System.Diagnostics;
using System.ComponentModel;
using System.Drawing;

namespace PS4NC
{
    public class API : IAPI
    {
        public class Range
        {
            public string Name { get; set; }
            public ulong Start { get; set; }
            public ulong Stop { get; set; }
            public ulong Size { get; set; }
        }

        List<Range> ranges = new List<Range>();

        PS4RPC PS4;
        Process p;
        ulong baseAdd = 0;
        ulong endAdd = 0;
        bool attached = false;
        public API()
        {
            PS4 = new PS4RPC();
        }

        //Declarations of all our internal API variables
        string myName = "PS4 RPC";
        string myDescription = "Implementation of jkpatch RPC to work with NetCheat, everything you need is included with the API.";
        string myAuthor = "Cyb3r";
        string myVersion = "1.0";
        string myPlatform = "PS4";
        string myContactLink = "http://www.rivalgamer.com/";

        //If you want an Icon, use resources to load an image
        System.Drawing.Image myIcon = Properties.Resources.ps4;

        /// <summary>
        /// Website link to contact info or download (leave "" if no link)
        /// </summary>
        public string ContactLink
        {
            get { return myContactLink; }
        }

        /// <summary>
        /// Name of the API (displayed on title bar of NetCheat)
        /// </summary>
        public string Name
        {
            get { return myName; }
        }

        /// <summary>
        /// Description of the API's purpose
        /// </summary>
        public string Description
        {
            get { return myDescription; }
        }

        /// <summary>
        /// Author(s) of the API
        /// </summary>
        public string Author
        {
            get { return myAuthor; }

        }

        /// <summary>
        /// Current version of the API
        /// </summary>
        public string Version
        {
            get { return myVersion; }
        }

        /// <summary>
        /// Name of platform (abbreviated, i.e. PC, PS3, XBOX, iOS)
        /// </summary>
        public string Platform
        {
            get { return myPlatform; }
        }

        /// <summary>
        /// Returns whether the platform is little endian by default
        /// </summary>
        public bool isPlatformLittleEndian
        {
            get { return BitConverter.IsLittleEndian; }
        }

        /// <summary>
        /// Icon displayed along with the other data in the API tab, if null NetCheat icon is displayed
        /// </summary>
        public System.Drawing.Image Icon
        {
            get { return myIcon; }
        }
        /// <summary>
        /// Read bytes from memory of target process.
        /// Returns read bytes into bytes array.
        /// Returns false if failed.
        /// </summary>
        public bool GetBytes(ulong address, ref byte[] bytes)
        {
            try
            {
                bytes = PS4.ReadMemory(p.pid, address + baseAdd, bytes.Length);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Write bytes to the memory of target process.
        /// </summary>
        public void SetBytes(ulong address, byte[] bytes)
        {
            if (address > endAdd) MessageBox.Show("Address is out of range");
            else PS4.WriteMemory(p.pid, address + baseAdd, bytes);
        }

        /// <summary>
        /// Shutdown game or platform
        /// </summary>
        public void Shutdown()
        {

        }

        /// <summary>
        /// Connects to target.
        /// If platform doesn't require connection, just return true.
        /// IMPORTANT:
        /// Since NetCheat connects and attaches a few times after the user does (Constant write thread, searching, ect)
        /// You must have it automatically use the settings that the user input, instead of asking again
        /// This can be reset on Disconnect()
        /// </summary>
        public bool Connect()
        {
            //attached = false;
            if (PS4.IsConnected)
                return true;
            string ip = string.Empty;
            using (Connect connectForm = new Connect())
            {
                try
                {
                    if (String.IsNullOrEmpty(Properties.Settings.Default.ip)) connectForm.ipBox.Text = "192.168.1.3";
                    else connectForm.ipBox.Text = Properties.Settings.Default.ip;
                    connectForm.cnct.Click += (o, e) => { connectForm.Close(); connectForm.DialogResult = DialogResult.OK; };
                    connectForm.ShowDialog();
                    Properties.Settings.Default.ip = connectForm.ipBox.Text;
                    Properties.Settings.Default.Save();
                    if (connectForm.DialogResult != DialogResult.OK)
                        return false;
                    ip = connectForm.ipBox.Text;
                    PS4 = new PS4RPC(ip);
                    PS4.Connect();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Disconnects from target.
        /// </summary>
        public void Disconnect()
        {
            attached = false;
            PS4.Disconnect();
        }

        /// <summary>
        /// Attaches to target process.
        /// This should automatically continue the process if it is stopped.
        /// IMPORTANT:
        /// Since NetCheat connects and attaches a few times after the user does (Constant write thread, searching, ect)
        /// You must have it automatically use the settings that the user input, instead of asking again
        /// This can be reset on Disconnect()
        /// </summary>
        public bool Attach()
        {
            try
            {
                if (!attached)
                {
                    if (this.ContinueProcess())
                        attached = true;
                    else return false;
                }
                return true;
            }
            catch (OperationCanceledException)
            {
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
        }



        private string toHex(ulong add)
        {
            return add.ToString("X");
        }

        /// <summary>
        /// Pauses the attached process (return false if not available feature)
        /// </summary>
        public bool PauseProcess()
        {
            return false;
        }

        /// <summary>
        /// Continues the attached process (return false if not available feature)
        /// </summary>
        public bool ContinueProcess()
        {
            using (Attach attachForm = new Attach())
            {
                try
                {
                    this.refreshMemoryRegions(attachForm.mem);
                    attachForm.select.Click += (o, e) => { endAdd = ranges[attachForm.mem.CurrentRow.Index].Stop - ranges[attachForm.mem.CurrentRow.Index].Start; baseAdd = ranges[attachForm.mem.CurrentRow.Index].Start; attachForm.Close(); attachForm.DialogResult = DialogResult.OK; };
                    attachForm.close.Click += (o, e) => { attachForm.Close(); attachForm.DialogResult = DialogResult.Cancel; };
                    attachForm.refresh.Click += (o, e) => { this.refreshMemoryRegions(attachForm.mem); };
                    attachForm.mem.DoubleClick += (o, e) => { endAdd = ulong.Parse(attachForm.mem.CurrentRow.Cells[5].Value.ToString()) - ulong.Parse(attachForm.mem.CurrentRow.Cells[4].Value.ToString()); baseAdd = ulong.Parse(attachForm.mem.CurrentRow.Cells[4].Value.ToString()); attachForm.Close(); attachForm.DialogResult = DialogResult.OK; };
                    attachForm.mem.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.sortBySize);
                    attachForm.mem.MouseClick += this.ListClick;

                    attachForm.ShowDialog();
                    if (attachForm.DialogResult != DialogResult.OK && !attached)
                    {
                        MessageBox.Show("You have to select a region");
                        return false;
                    } else if(attachForm.DialogResult == DialogResult.OK)
                    {
                        System.Diagnostics.Process[] pr = System.Diagnostics.Process.GetProcesses();
                        foreach(System.Diagnostics.Process nc in pr)
                        {
                            if(nc.ProcessName.ToLower().Contains("netcheatps3"))
                            {
                                AutomationElement ae = AutomationElement.FromHandle(nc.MainWindowHandle);
                                (ae.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.AutomationIdProperty, "TabCon")).FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.NameProperty, "Search"))).SetFocus();
                                (ae.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.AutomationIdProperty, "startAddrTB")).GetCurrentPattern(ValuePattern.Pattern) as ValuePattern).SetValue("0x0");
                                (ae.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.AutomationIdProperty, "stopAddrTB")).GetCurrentPattern(ValuePattern.Pattern) as ValuePattern).SetValue("0x" + (ulong.Parse(attachForm.mem.CurrentRow.Cells[5].Value.ToString()) - baseAdd).ToString("X"));
                            }
                        }
                    }

                    return true;
                }
                catch
                {
                    return true;
                }
            }
        }

        private void ListClick(object sender, MouseEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (e.Button == MouseButtons.Right)
            {

                ContextMenu m = new ContextMenu();
                m.MenuItems.Add(new MenuItem("Copy"));
                int currentMouseOverRow = dgv.HitTest(e.X, e.Y).RowIndex;
                m.Show(dgv, new Point(e.X, e.Y));
                if (dgv.GetCellCount(DataGridViewElementStates.Selected) > 0)
                {
                    try
                    {
                        Clipboard.SetDataObject(
                            dgv.GetClipboardContent());
                    }
                    catch (System.Runtime.InteropServices.ExternalException)
                    {

                        MessageBox.Show("The Clipboard could not be accessed. Please try again.");
                    }
                }
            }
        }

        private void sortBySize(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (e.ColumnIndex == 1)
            {
                ListSortDirection order = ListSortDirection.Ascending;
                if (dgv.SortedColumn != null && dgv.SortedColumn.Index == 4 && dgv.SortOrder == SortOrder.Ascending)
                    order = ListSortDirection.Descending;
                dgv.Sort(dgv.Columns[4], order);
            }
            else if (e.ColumnIndex == 2)
            {
                ListSortDirection order = ListSortDirection.Ascending;
                if (dgv.SortedColumn != null && dgv.SortedColumn.Index == 5 && dgv.SortOrder == SortOrder.Ascending)
                    order = ListSortDirection.Descending;
                dgv.Sort(dgv.Columns[5], order);
            }
            else if (e.ColumnIndex == 3)
            {
                ListSortDirection order = ListSortDirection.Ascending;
                if (dgv.SortedColumn != null && dgv.SortedColumn.Index == 6 && dgv.SortOrder == SortOrder.Ascending)
                    order = ListSortDirection.Descending;
                dgv.Sort(dgv.Columns[6], order);
            }
        }

        private string convertByte(ulong size)
        {
            int i = 0;
            for (; size >= 1024; i++) size = size / 1024;
            string type;
            switch(i)
            {
                case 1: type = "KB"; break;
                case 2: type = "MB"; break;
                case 3: type = "GB"; break;
                default: type = "B"; break;
            }
            return size.ToString() + type;
        }



        private void refreshMemoryRegions(DataGridView rangeList)
        {
            p = PS4.GetProcessList().FindProcess("eboot.bin");

            ProcessInfo pi = PS4.GetProcessInfo(p.pid);
            string currentRangeName = "";

            ranges.Clear();
            rangeList.Rows.Clear();
            rangeList.Refresh();

            for (int i = 0; i < pi.entries.Length; i++)
            {

                MemoryEntry me = pi.entries[i];

                currentRangeName = String.IsNullOrEmpty(me.name) ? "UnnamedRange" : me.name;
                ranges.Add(new Range() { Name = currentRangeName, Start = me.start, Stop = me.end, Size = me.end - me.start });
            }

            ranges.RemoveAt(0);
            foreach (Range r in ranges) rangeList.Rows.Add(r.Name, toHex(r.Start), toHex(r.Stop), convertByte(r.Size), r.Start, r.Stop, r.Size);
        }

        /// <summary>
        /// Tells NetCheat if the process is currently stopped (return false if not available feature)
        /// </summary>
        public bool isProcessStopped()
        {
            return false;
        }

        /// <summary>
        /// Called by user.
        /// Should display options for the API.
        /// Can be used for other things.
        /// </summary>
        public void Configure()
        {

        }

        /// <summary>
        /// Called on initialization
        /// </summary>
        public void Initialize()
        {

        }

        /// <summary>
        /// Called when disposed
        /// </summary>
        public void Dispose()
        {
            attached = false;
            PS4.Disconnect();
        }
    }
}
