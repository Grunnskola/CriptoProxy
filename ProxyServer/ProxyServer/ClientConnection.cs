using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using EncryptionLibrary;

namespace ProxyServer
{
    class ClientConnection
    {
        private Socket clientSocket;
        private ConnectionInfo info;
        private Thread handler;
        private bool continueReceive;

        public ClientConnection(Socket client, ConnectionInfo inf)
        {
            this.info = inf;
            client.SendTimeout = 50000;
            client.ReceiveTimeout = 50000;
            this.clientSocket = client;
            continueReceive = true;
        }
        public void StartHandling()
        {
            handler = new Thread(new ThreadStart(Handler)); //указывает какой метод будет выполняться
            handler.Priority = ThreadPriority.Highest;
            handler.Start();
        }

        public void StopHandling()
        {
            continueReceive = false;
            handler.Abort();
        }

        private void Handler()
        {
            bool recvReq = true;
            string EOL = "\r\n";
            string reqPayload = "";
            List<string> requestLines = new List<string>();

            byte[] requestBuffer = new byte[1];
            byte[] responseBuffer = new byte[1];

            requestLines.Clear();
            try
            {
                while (recvReq && continueReceive)
                {
                    if (this.clientSocket.Receive(requestBuffer) == 0)
                    {
                        break;
                    }
                    string fromByte = UTF8Encoding.UTF8.GetString(requestBuffer);
                    reqPayload += fromByte;
                    if (reqPayload.EndsWith(EOL + EOL))
                    {
                        recvReq = false;
                    }
                }

                //connect to server
                Socket client_server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                client_server.Connect(this.info.ServerIP, this.info.ServerPort);
                client_server.ReceiveTimeout = 50000;
                client_server.SendTimeout = 50000;


                if (this.info.EncryptionEnabled)
                {
                    client_server.Send(ASCIIEncoding.ASCII.GetBytes(Encryption.Encrypt(reqPayload, this.info.Key) + "serverfinish"));
                    
                }
                else
                {
                    client_server.Send(ASCIIEncoding.ASCII.GetBytes(reqPayload + "serverfinish"));
                }

                if (false)
                {
                    //while (client_server.Receive(responseBuffer) != 0)
                    //{
                    //    int temp = responseBuffer[0];
                    //    temp -= 2;
                    //    if (temp < 0)
                    //    {
                    //        temp += 256;
                    //    }
                    //    responseBuffer[0] = (byte)temp;
                    //    this.clientSocket.Send(responseBuffer);
                    //}
                }
                else 
                {
                    while(client_server.Receive(responseBuffer) != 0) 
                    {
                        this.clientSocket.Send(responseBuffer);
                    }
                }
                client_server.Disconnect(false);
                this.clientSocket.Disconnect(false);

            }
            catch (Exception ex)
            {
                this.info.MessageCenter.setMessage(ex.Message);

            }
        }
    }
}
