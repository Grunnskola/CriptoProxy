using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using EncryptionLibrary;

namespace ProxyServer_Server
{
    class ClientConnection
    {
        private Socket clientSocket;
        private ConnectionInfo info;
        private Thread handler;

        public ClientConnection(Socket client, ConnectionInfo info)
        {
            this.info = info;
            client.SendTimeout = 50000;
            client.ReceiveTimeout = 50000;
            this.clientSocket = client;
        }

        public void StartHandling()
        {
            handler = new Thread(Handler);
            handler.Priority = ThreadPriority.Highest;
            handler.Start();
        }

        private void Handler()
        {
            bool recvRequest = true;
            string EOL = "\r\n";

            string reqPayload = "";
            string reqTempLine = "";
            List<string> reqLines = new List<string>();
            byte[] requestBuffer = new byte[1];
            byte[] responseBuffer = new byte[1];

            reqLines.Clear();

            try
            {
                while (recvRequest)
                {
                    this.clientSocket.Receive(requestBuffer);
                    string fromByte = UTF8Encoding.UTF8.GetString(requestBuffer);
                    reqPayload += fromByte;
                    reqTempLine += fromByte;

                    if (reqPayload.EndsWith("serverfinish"))
                    {
                        recvRequest = false;
                    }
                }

                string reciveData;

                if (this.info.EncryptionEnabled)
                {
                    reciveData = Encryption.Decrypt(reqPayload.Replace("serverfinish", ""), this.info.Key);
                }
                else
                {
                    reciveData = reqPayload.Replace("serverfinish", "");
                }
                reqPayload = "";
                reqTempLine = "";

                int counter = 0;
                //State 0: Handle Request from Client
                while (counter < reciveData.Length)
                {
                    string fromByte = reciveData[counter].ToString();
                    counter++;
                    reqPayload += fromByte;
                    reqTempLine += fromByte;

                    if (reqTempLine.EndsWith(EOL))
                    {
                        reqLines.Add(reqTempLine.Trim());
                        reqTempLine = "";

                    }
                    if (reqPayload.EndsWith(EOL + EOL))
                    {
                        break;
                    }
                }

                if (reqLines.Count == 0)
                {
                    return;
                }

                string remoteHost = reqLines[0].Split(' ')[1].Replace("http://", "").Split('/')[0];
                string requestFile = reqLines[0].Replace("http://", "").Replace(remoteHost, "");
                reqLines[0] = requestFile;

                this.info.MessageCenter.setMsg(string.Format("Request to {0}", remoteHost));

                reqPayload = "";
                foreach (string line in reqLines)
                {
                    reqPayload += line;
                    reqPayload += EOL;
                }

                Socket destServerSocke = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                destServerSocke.Connect(remoteHost,80);
                destServerSocke.ReceiveTimeout = 120000;
                destServerSocke.SendTimeout = 120000;

                destServerSocke.Send(ASCIIEncoding.ASCII.GetBytes(reqPayload));

                if (false)
                {
                    while(destServerSocke.Receive(responseBuffer) != 0) 
                    {
                        int temp = responseBuffer[0]; 
                        temp +=2;
                        if(temp>255) 
                        {
                            temp -=256;
                        }
                        responseBuffer[0] = (byte)temp;
                        this.clientSocket.Send(responseBuffer);
                    }
                }

                while(destServerSocke.Receive(responseBuffer) != 0) 
                {
                    this.clientSocket.Send(responseBuffer);
                }

                destServerSocke.Disconnect(false);
                this.clientSocket.Disconnect(false);

            }
            catch (Exception ex) {
                this.info.MessageCenter.setMsg(ex.Message);
            }
        }

    }
}
