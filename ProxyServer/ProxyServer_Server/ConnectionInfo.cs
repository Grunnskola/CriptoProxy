using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace ProxyServer_Server
{
    class ConnectionInfo
    {
        public IPAddress LocalIP { get; set; }
        public int LocalPort { get; set; }
        public string Key { get; set; }
        public Server MessageCenter { get; set; }
        public bool EncryptionEnabled { get; set; }

        public ConnectionInfo(string localIP, string localPort, string key, Server messg, bool encr )
            :this(IPAddress.Parse(localIP), int.Parse(localPort), key, messg , encr)
        { 
        }

        public ConnectionInfo(IPAddress server, int port, string key, Server msg, bool enxr)
        {
            LocalIP = server;
            LocalPort = port;
            Key = key;
            MessageCenter = msg;
            EncryptionEnabled = enxr; 

        }
    }
}
