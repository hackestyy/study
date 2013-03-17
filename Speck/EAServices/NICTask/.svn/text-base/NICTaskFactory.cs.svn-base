using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZteApp.ProductService.Core;
using ZteApp.ProductService.EAServices.CommunicationTask;

namespace ZteApp.ProductService.EAServices.NICTask
{
    public class NICTaskFactory
    {
        private static readonly object mSyncRoot=new object();
        private static NICTaskFactory mInstance=null;
        private NetworkUtilization mNetworkUtilization;
        private ServiceTask mNICTask;

        private NICTaskFactory()
        { }

        public static NICTaskFactory Instance
        {
            get
            {
                if (mInstance == null)
                {
                    lock (mSyncRoot)
                    {
                        if (mInstance == null)
                        {
                            mInstance = new NICTaskFactory();
                        }
                    }
                }
                return mInstance;
            }
        }

        public ServiceTask GetTask()
        {
            mNICTask = new ServiceTask();
            mNICTask.Tag = Resource1.NICTaskName;
            return mNICTask;
        }

        public void InsertNetworkUtilization(ServiceTask NICTask)
        {
            if (NICTask == null)
            {
                return;
            }
            mNetworkUtilization = new NetworkUtilization();
            mNetworkUtilization.PlannedWorkPattern = BehaviorExecutePattern.Asynchronization;
            NICTask.Add(mNetworkUtilization);
        }

        public NetworkUtilization GetNetworkUtilization()
        {
            mNetworkUtilization = new NetworkUtilization();
            mNetworkUtilization.PlannedWorkPattern = BehaviorExecutePattern.Asynchronization;
            return mNetworkUtilization;
        }
    }
}
