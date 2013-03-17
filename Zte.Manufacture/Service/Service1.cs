using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Zte.Manufacture.Service.CommunicationTask;

namespace Zte.Manufacture.Service
{
    public partial class Service1 : ServiceBase
    {
        private Thread socketThread;
        private SocketServer socketPort;
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            socketPort = new SocketServer();
            socketThread = new Thread(new ThreadStart(socketPort.DoWork));
        }

        protected override void OnStop()
        {
            socketPort.Abort();
            socketThread.Abort();
        }
    }
}
