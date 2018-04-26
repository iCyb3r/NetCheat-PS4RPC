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

namespace PS4NC
{
    public class API : IAPI
    {
        PS4RPC PS4;
        Process p;
        ulong baseAdd = 0;
        ulong endAdd = 0;
        List<ulong> startAdds = new List<ulong>();
        List<ulong> endAdds = new List<ulong>();
        bool attached = false;
        List<string> ranges = new List<string>();
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



        private string getFullAddress(ulong add)
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
                    p = PS4.GetProcessList().FindProcess("eboot.bin");
                    this.refreshMemoryRegons();
                    ranges.RemoveAt(0);
                    startAdds.RemoveAt(0);
                    endAdds.RemoveAt(0);
                    BindingSource bs = new BindingSource();
                    bs.DataSource = ranges;
                    attachForm.mem.DataSource = bs;
                    attachForm.select.Click += (o, e) => { endAdd = endAdds[attachForm.mem.SelectedIndex] - startAdds[attachForm.mem.SelectedIndex]; baseAdd = startAdds[attachForm.mem.SelectedIndex]; attachForm.Close(); attachForm.DialogResult = DialogResult.OK; };
                    attachForm.ShowDialog();
                    if (attachForm.DialogResult != DialogResult.OK && !attached)
                    {
                        MessageBox.Show("You have to select a region");
                        return false;
                    } else if(attachForm.DialogResult == DialogResult.OK)
                    {
                        System.Diagnostics.Process[] pr = System.Diagnostics.Process.GetProcesses();//.GetProcessesByName("NetCheatPS3");
                        foreach(System.Diagnostics.Process nc in pr)
                        {
                            if(nc.ProcessName.ToLower().Contains("netcheatps3"))
                            {
                                AutomationElement ae = AutomationElement.FromHandle(nc.MainWindowHandle);
                                (ae.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.AutomationIdProperty, "TabCon")).FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.NameProperty, "Search"))).SetFocus();
                                (ae.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.AutomationIdProperty, "startAddrTB")).GetCurrentPattern(ValuePattern.Pattern) as ValuePattern).SetValue("0x0");
                                (ae.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.AutomationIdProperty, "stopAddrTB")).GetCurrentPattern(ValuePattern.Pattern) as ValuePattern).SetValue("0x" + (endAdds[attachForm.mem.SelectedIndex] - baseAdd).ToString("X"));
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

        private string convertToKBorMB(ulong start, ulong end)
        {
            ulong total = ((end - start) / 1024);
            if (total >= 1024)
                return (total / 1024).ToString() + "MB";
            return total.ToString() + "KB";
        }

        private void refreshMemoryRegons()
        {

            ProcessInfo pi = PS4.GetProcessInfo(p.pid);
            string currentRangeName = "";

            ranges.Clear();
            startAdds.Clear();
            endAdds.Clear();

            for (int i = 0; i < pi.entries.Length; i++)
            {

                MemoryEntry me = pi.entries[i];

                currentRangeName = String.IsNullOrEmpty(me.name) ? "UnknownRange" : me.name;
                startAdds.Add(me.start);
                endAdds.Add(me.end);
                ranges.Add("[" + currentRangeName + "]:" + getFullAddress(me.start) + "-" + getFullAddress(me.end) + "-" + convertToKBorMB(me.start, me.end));
            }
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
