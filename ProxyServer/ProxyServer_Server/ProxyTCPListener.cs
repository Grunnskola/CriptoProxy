using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace ProxyServer_Server
{
    class ProxyTCPListener
    {
        private TcpListener listener;
        private ConnectionInfo info;

        public ProxyTCPListener(ConnectionInfo inf)
        {
            this.info = inf;
            this.listener = new TcpListener(this.info.LocalIP, this.info.LocalPort);
        }

        public void StartServer(Server msg, string key)
        {
            this.listener.Start();
        }

        public void AcceptConnection()
        {
            if (this.listener.Pending())
            {
                Socket newClient = this.listener.AcceptSocket();
                this.info.MessageCenter.setMsg("ClientAccepted..");
                ClientConnection client = new ClientConnection(newClient, this.info);
                client.StartHandling();
            }
        }
    }
}
