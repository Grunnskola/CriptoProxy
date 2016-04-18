using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ProxyServer_Server
{
    public partial class Server : Form
    {
        public Server()
        {
            InitializeComponent();
        }

        ProxyTCPListener proxyListener;
        delegate void SetTextCallback(string msg);
        Thread StartListenApp;

        private void startAcc()
        {
            ConnectionInfo info = new ConnectionInfo(ListenIP.Text, ListenPort.Text, KeyInput.Text, this, checkBox1.Checked);
            proxyListener = new ProxyTCPListener(info);
            proxyListener.StartServer(this, KeyInput.Text);

            while(true) 
            {
                proxyListener.AcceptConnection();
            }

        }

        public void setMsg(string msg)
        {
            if (this.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(setMsg);
                this.Invoke(d, new object[] { msg });
                return;
            }

            textBox1.Text += msg + "\r\n";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (StartListenApp == null || StartListenApp.ThreadState == ThreadState.Stopped)
            {
                StartListenApp = new Thread(new ThreadStart(startAcc));
                StartListenApp.Start();
            }
            else
            {
                setMsg("Cannot start again");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (StartListenApp != null)
            {
                StartListenApp.Abort();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.PerformClick();

            this.Close(); 
            this.Dispose();
           
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            KeyInput.Enabled = checkBox1.Checked;
        }

        
    }
}
