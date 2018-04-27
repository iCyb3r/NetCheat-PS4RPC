using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PS4NC
{
    public partial class Connect : Form
    {
        public Connect()
        {
            InitializeComponent();
            fw.SelectedIndex = 0;
        }

        private void inj_Click(object sender, EventArgs e)
        {
            byte[] pload;
            byte[] kload;
            try
            {
                string patch_path = Application.StartupPath;
                switch (fw.SelectedIndex)
                {
                    case 0:
                        pload = Properties.Resources.payload455;
                        kload = Properties.Resources.kpayload455;
                        break;
                    case 1:
                        pload = Properties.Resources.payload405;
                        kload = Properties.Resources.kpayload405;
                        break;
                    default:
                        throw new System.ArgumentException("Unknown version.");
                }
                send_pay_load(ipBox.Text, pload, 9020);
                Thread.Sleep(1000);
                msg.Text = "Injecting kpayload.elf...";
                send_pay_load(ipBox.Text, kload, 9023);
                Thread.Sleep(1000);
                msg.ForeColor = Color.Green;
                msg.Text = "Payload injected successfully!";
                cnct.Enabled = true;
            }
            catch (Exception exception)
            {
                msg.Text = "Injection failed";
                msg.ForeColor = Color.Red;
                MessageBox.Show(exception.Message, exception.Source, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void send_pay_load(string IP, byte[] payload, int port)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(new IPEndPoint(IPAddress.Parse(IP), port));
            socket.Send(payload, payload.Length, SocketFlags.None);
            socket.Close();
        }
    }
}
