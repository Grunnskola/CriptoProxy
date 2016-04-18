using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;


namespace ProxyServer
{
    class ProxyTCPListener:IDisposable
    {
        private TcpListener1 listener;
        private ConnectionInfo info;
        private List<ClientConnection> clients;

        public ProxyTCPListener(ConnectionInfo info)
        {
            this.info = info;
            this.listener = new TcpListener1(this.info.LocalIP, this.info.LocalPort);
            this.clients = new List<ClientConnection>();
        }

        public void StartServer()
        {
            this.listener.Start();
        }

        public void AcceptConnection()
        {
            if (this.listener.Pending())
            {
                Socket newClient = this.listener.();
                this.info.MessageCenter.setMessage("client accepted..");
                ClientConnection client = new ClientConnection(newClient, this.info);
                client.StartHandling();
                clients.Add(client);
            }
            Thread.Sleep(200);
        }

        public void Dispose()
        {
            foreach (ClientConnection client in clients)
            {
                client.StopHandling();
            }
            listener.Stop();
        }
    }
}
