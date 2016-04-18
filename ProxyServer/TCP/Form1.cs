using System;
using System.Threading.Tasks;
using System.Net;
using System.Net.LazyAsyncResult;



namespace System.Net.Sockets
{
    public class TcpListener
    {
        private IPEndPoint _serverSocketEP;
        private Socket _serverSocket;
        private bool _active;
        private bool _exclusiveAddressUse;

        // Initializes a new instance of the TcpListener class with the specified local end point.
        public TcpListener(IPEndPoint localEP)
        {

            if (localEP == null)
            {
                throw new ArgumentNullException(nameof(localEP));
            }
            _serverSocketEP = localEP;
            _serverSocket = new Socket(_serverSocketEP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

        }

        // Initializes a new instance of the TcpListener class that listens to the specified IP address
        // and port.
        public TcpListener(IPAddress localaddr, int port)
        {


            if (localaddr == null)
            {
                throw new ArgumentNullException(nameof(localaddr));
            }

            _serverSocketEP = new IPEndPoint(localaddr, port);
            _serverSocket = new Socket(_serverSocketEP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

        }

        // Used by the class to provide the underlying network socket.
        public Socket Server
        {
            get
            {
                return _serverSocket;
            }
        }

        // Used by the class to indicate that the listener's socket has been bound to a port
        // and started listening.
        protected bool Active
        {
            get
            {
                return _active;
            }
        }

        // Gets the m_Active EndPoint for the local listener socket.
        public EndPoint LocalEndpoint
        {
            get
            {
                return _active ? _serverSocket.LocalEndPoint : _serverSocketEP;
            }
        }

        public bool ExclusiveAddressUse
        {
            get
            {
                return _serverSocket.ExclusiveAddressUse;
            }
            set
            {
                if (_active)
                {
                    throw new InvalidOperationException(SR.net_tcplistener_mustbestopped);
                }

                _serverSocket.ExclusiveAddressUse = value;
                _exclusiveAddressUse = value;
            }
        }

        // Starts listening to network requests.
        public void Start()
        {
            Start((int)SocketOptionName.MaxConnections);
        }

        public void Start(int backlog)
        {
            if (backlog > (int)SocketOptionName.MaxConnections || backlog < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(backlog));
            }



            if (_serverSocket == null)
            {
                throw new InvalidOperationException(SR.net_InvalidSocketHandle);
            }

            // Already listening.
            if (_active)
            {


                return;
            }

            _serverSocket.Bind(_serverSocketEP);
            try
            {
                _serverSocket.Listen(backlog);
            }
            catch (SocketException)
            {
                // When there is an exception, unwind previous actions (bind, etc).
                Stop();
                throw;
            }

            _active = true;

        }

        // Closes the network connection.
        public void Stop()
        {


            if (_serverSocket != null)
            {
                _serverSocket.Dispose();
                _serverSocket = null;
            }

            _active = false;
            _serverSocket = new Socket(_serverSocketEP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            if (_exclusiveAddressUse)
            {
                _serverSocket.ExclusiveAddressUse = true;
            }

        }

        // Determine if there are pending connection requests.
        public bool Pending()
        {
            if (!_active)
            {
                throw new InvalidOperationException(SR.net_stopped);
            }

            return _serverSocket.Poll(0, SelectMode.SelectRead);
        }

        internal IAsyncResult BeginAcceptSocket(AsyncCallback callback, object state)
        {

            if (!_active)
            {
                throw new InvalidOperationException(SR.net_stopped);
            }

            IAsyncResult result = _serverSocket.BeginAccept(callback, state);

            return result;
        }

        internal Socket EndAcceptSocket(IAsyncResult asyncResult)
        {

            if (asyncResult == null)
            {
                throw new ArgumentNullException(nameof(asyncResult));
            }

            LazyAsyncResult lazyResult = asyncResult as LazyAsyncResult;
            Socket asyncSocket = lazyResult == null ? null : lazyResult.AsyncObject as Socket;
            if (asyncSocket == null)
            {
                throw new ArgumentException(SR.net_io_invalidasyncresult, nameof(asyncResult));
            }

            // This will throw ObjectDisposedException if Stop() has been called.
            Socket socket = asyncSocket.EndAccept(asyncResult);


            return socket;
        }

        internal IAsyncResult BeginAcceptTcpClient(AsyncCallback callback, object state)
        {


            if (!_active)
            {
                throw new InvalidOperationException(SR.net_stopped);
            }

            IAsyncResult result = _serverSocket.BeginAccept(callback, state);


            return result;
        }

        internal TcpClient EndAcceptTcpClient(IAsyncResult asyncResult)
        {


            if (asyncResult == null)
            {
                throw new ArgumentNullException(nameof(asyncResult));
            }

            LazyAsyncResult lazyResult = asyncResult as LazyAsyncResult;
            Socket asyncSocket = lazyResult == null ? null : lazyResult.AsyncObject as Socket;
            if (asyncSocket == null)
            {
                throw new ArgumentException(SR.net_io_invalidasyncresult, nameof(asyncResult));
            }

            // This will throw ObjectDisposedException if Stop() has been called.
            Socket socket = asyncSocket.EndAccept(asyncResult);



            return new TcpClient(socket);
        }

        public Task<Socket> AcceptSocketAsync()
        {
            return Task<Socket>.Factory.FromAsync(
                (callback, state) => ((TcpListener)state).BeginAcceptSocket(callback, state),
                asyncResult => ((TcpListener)asyncResult.AsyncState).EndAcceptSocket(asyncResult),
                state: this);
        }

        public Task<TcpClient> AcceptTcpClientAsync()
        {
            return Task<TcpClient>.Factory.FromAsync(
                (callback, state) => ((TcpListener)state).BeginAcceptTcpClient(callback, state),
                asyncResult => ((TcpListener)asyncResult.AsyncState).EndAcceptTcpClient(asyncResult),
                state: this);
        }
    }
}
