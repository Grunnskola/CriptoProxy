using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ProxyServer
{
    public partial class Client : Form
    {
        public Client()
        {
            InitializeComponent();
        }

        ProxyTCPListener proxyListener;
        delegate void SetTxtCallback(string msg);
        Thread StartListenApp;

        private void StartAccept()
        {
            ConnectionInfo inf = new ConnectionInfo(ServerIP.Text, ServerPort.Text, ListenIP.Text, ListenPort.Text, KeyInput.Text, this, Encrypt.Checked);

            proxyListener = new ProxyTCPListener(inf);
            proxyListener.StartServer();

            while (true)
            {
                proxyListener.AcceptConnection();
            }
        }

        public void setMessage(string msg)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    SetTxtCallback d = new SetTxtCallback(setMessage);
                    this.Invoke(d, new object[] { msg });
                    return;
                }
                Messages.Text += msg + "\r\n";
            }
            catch
                {

                }
            }
        
        private void Start_Click(object sender, EventArgs e)
        {
            if (StartListenApp == null || StartListenApp.ThreadState == ThreadState.Stopped || StartListenApp.ThreadState == ThreadState.Aborted)
            {
                StartListenApp = new Thread(new ThreadStart(StartAccept));
                StartListenApp.Start();
            }

            else
            {
                setMessage("Cannot start again!");
            }
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            if (StartListenApp != null)
            {
                StartListenApp.Abort();
                proxyListener.Dispose();
            }
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Stop.PerformClick();            
            this.Dispose();
            this.Close();

        }

        private void Encrypt_CheckedChanged(object sender, EventArgs e)
        {
            KeyInput.Enabled = Encrypt.Checked;
        }

    }
}
