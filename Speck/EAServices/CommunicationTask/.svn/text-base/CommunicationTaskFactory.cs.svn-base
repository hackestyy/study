using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZteApp.ProductService.Core;

namespace ZteApp.ProductService.EAServices.CommunicationTask
{
    public class CommunicationTaskFactory
    {
        private static readonly object mSyncRoot = new object();
        private static CommunicationTaskFactory mInstamce = null;
        private ServiceTask mCommunicationTask;

        private CommunicationTaskFactory()
        { }

        public static CommunicationTaskFactory Instance
        {
            get
            {
                if (mInstamce == null)
                {
                    lock (mSyncRoot)
                    {
                        if (mInstamce == null)
                        {
                            mInstamce = new CommunicationTaskFactory();
                        }
                    }
                }
                return mInstamce;
            }
        }

        public ServiceTask GetTask()
        {
            mCommunicationTask = new ServiceTask();
            mCommunicationTask.Tag = Resource1.CommunicationTaskName;
            return mCommunicationTask;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="communicationTask"></param>
        /// <param name="serviceType"></param>
        /// <param name="baseAddresses"></param>
        public void InsertMessagePort(ServiceTask communicationTask, Type serviceType, params Uri[] baseAddresses)
        {
            if (communicationTask == null || serviceType == null)
            {
                return;
            }
            var messagePort= new MessagePort(serviceType, baseAddresses);
            messagePort.PlannedWorkPattern = BehaviorExecutePattern.Asynchronization;
            communicationTask.Add(messagePort);
        }

        public void InsertMessagePort(ServiceTask communicationTask, object singletonInstance, params Uri[] baseAddresses)
        {
            if (communicationTask == null || singletonInstance == null)
            {
                return;
            }
            var messagePort = new MessagePort(singletonInstance, baseAddresses);
            messagePort.PlannedWorkPattern = BehaviorExecutePattern.Asynchronization;
            communicationTask.Add(messagePort);
        }
        public void InsertSocketPort(ServiceTask communicationTask, object commandDispatcher)
        {
            if (communicationTask == null || commandDispatcher == null)
            {
                return;
            }
            SocketPort socketPort = new SocketPort(commandDispatcher);
            socketPort.PlannedWorkPattern = BehaviorExecutePattern.Asynchronization;
            communicationTask.Add(socketPort);
        }
    }
}
