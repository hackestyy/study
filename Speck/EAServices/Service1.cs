using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using ZteApp.ProductService.Core;
using ZteApp.ProductService.EAServices.NICTask;
using ZteApp.ProductService.EAServices.CommunicationTask;
using System.IO;
using System.Threading;
using Zte.Manufacture.Service.CommunicationTask;
using Zte.Manufacture.Service.Common;

namespace ZteApp.ProductService.EAServices
{
    public partial class Service1 : ServiceBase
    {
        private FileStream mLogFileStream;

        public Service1()
        {
            InitializeComponent();

            string fullPath = System.IO.Path.GetDirectoryName(typeof(Service1).Assembly.Location);
            mLogFileStream = new FileStream(fullPath +"\\"+ typeof(Service1).Name + ".log", FileMode.Create);
            TextWriterTraceListener listener = new System.Diagnostics.TextWriterTraceListener(mLogFileStream);
            Trace.Listeners.Clear();
            Trace.Listeners.Add(listener);
            Trace.AutoFlush = true;
            
            //change current directory to local directory instead of system32
            System.IO.Directory.SetCurrentDirectory(fullPath);

            /*******************************Initialize service task*************************************************/

            //Add networkMonitor task
            //var nICTask = NICTaskFactory.Instance.GetTask();
            //NICTaskFactory.Instance.InsertNetworkUtilization(nICTask);
            //TaskExcuteProcess.Instance.Add(nICTask);

            //Add communicaiton task 
            Command command = new Command();
            command.CommandReceived += new CommandInterpretion.BTCommandInterpreter().CommandEventHandler;
            command.CommandReceived += new CommandInterpretion.WIFICommandInterpreter().CommandEventHandler;

            SocketCmdOperator socketCmdOperator = new SocketCmdOperator();

            var communicationTask = CommunicationTaskFactory.Instance.GetTask();
            //CommunicationTaskFactory.Instance.InsertMessagePort(communicationTask,typeof(NetworkInterfaceInfo));
            CommunicationTaskFactory.Instance.InsertMessagePort(communicationTask, command);
            //CommunicationTaskFactory.Instance.InsertSocketPort(communicationTask, command);
            CommunicationTaskFactory.Instance.InsertSocketPort(communicationTask, socketCmdOperator);
            CommunicationTaskFactory.Instance.InsertMessagePort(communicationTask,typeof(BusinessWork.Service));
            TaskExcuteProcess.Instance.Add(communicationTask);


        }

        protected override void OnStart(string[] args)
        {
            TaskExcuteProcess.Instance.StartWork();
        }

        protected override void OnStop()
        {
            TaskExcuteProcess.Instance.Stop();
            if (mLogFileStream != null)
            {
                mLogFileStream.Close();
            }
        }
    }
}
