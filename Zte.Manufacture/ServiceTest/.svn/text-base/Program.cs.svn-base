using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Zte.Manufacture.Service.CommunicationTask;

namespace Zte.Manufacture.Service.ServiceTest
{
    class Program
    {
        static void Main(string[] args)
        {
            SocketServer socketServer = new SocketServer();
            Thread socketThread = new Thread(new ThreadStart(socketServer.DoWork));
            socketThread.Start();
            Console.WriteLine("socket server start");
            Console.Read();
            socketServer.Abort();
            socketThread.Abort();
        }
    }
}
