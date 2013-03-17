using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Zte.Manufacture.Service.Common;
using Zte.Manufacture.Service.BusinessWork;

namespace Zte.Manufacture.Service.CommunicationTask
{
    public class SocketServer 
    {
        private Socket _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private Thread _socketDataTransferThread;
        public void DoWork()
        {
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 9050);//本机预使用的IP和端口
            _serverSocket.Bind(ipep);//绑定
            _serverSocket.Listen(10);//监听
            Console.WriteLine("waiting for a client");
            while (true)
            {
                try
                {
                    Socket instance = _serverSocket.Accept();//当有可用的客户端连接尝试时执行，并返回一个新的socket,用于与客户端之间的通信
                    SocketDataTransfer dataTransfer = new SocketDataTransfer(instance);
                    _socketDataTransferThread = new Thread(new ThreadStart(dataTransfer.DealData));
                    _socketDataTransferThread.Start();
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    break;
                }
            }
        }

        public void Abort()
        {
            _serverSocket.Close();
        }
    }
    class SocketDataTransfer
    {
        
        private Socket _client;
        private byte[] _receiveBytes = new byte[1024];
        private int _receiveCount = 0;
        private SocketCmdOperator _socketCmdOperator;
        public SocketDataTransfer(Socket clientSocket)
        {
            _client = clientSocket;
        }

        public void DealData()
        {
            IPEndPoint clientip = (IPEndPoint)_client.RemoteEndPoint;
            Console.WriteLine("connect with client:" + clientip.Address + " at port:" + clientip.Port);

            try
            {
                while (true)
                {
                    _receiveCount = _client.Receive(_receiveBytes);
                    Console.WriteLine("recv=" + _receiveCount.ToString());
                    if (_receiveCount == 0)//当信息长度为0，说明客户端连接断开
                        break;
                    _socketCmdOperator = new SocketCmdOperator();
                    byte[] result = _socketCmdOperator.Do(_receiveBytes, _receiveCount);
                    _client.Send(result, result.Length, SocketFlags.None);
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            Console.WriteLine("Disconnected from" + clientip.Address);
            _client.Close();
        }
        
    }
}
