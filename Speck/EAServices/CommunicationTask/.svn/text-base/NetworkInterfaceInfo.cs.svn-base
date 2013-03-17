using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZteApp.ProductService.EAServices.NICTask;
using System.ServiceModel;
using ZteApp.ProductService.Core;

namespace ZteApp.ProductService.EAServices.CommunicationTask
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class NetworkInterfaceInfo : INetworkInterfaceInfo
    {
        private INetworkInterfaceInfo mNetworkInterfaceInfoGenuine;

        public NetworkInterfaceInfo()
        {
            try
            {
                Task[] tasks = TaskExcuteProcess.Instance.DispatchedTasks;
                Task task = tasks.First(t =>
                {
                    if ((t as ServiceTask).Tag == Resource1.NICTaskName)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                });

                var behavior = task.BehaviorDependencys.First(kvpair =>
                {
                    if (kvpair.Key is INetworkInterfaceInfo)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                });

                mNetworkInterfaceInfoGenuine = behavior.Key as INetworkInterfaceInfo;
            }
            catch (InvalidOperationException e)
            { }
        }

        public double CurrentSentRate
        {
            get 
            {
                return mNetworkInterfaceInfoGenuine.CurrentSentRate;
            }
        }

        public double CurrentReceivedRate
        {
            get 
            {
                return mNetworkInterfaceInfoGenuine.CurrentReceivedRate;
            }
        }

        public double BandWidth
        {
            get 
            {
                return mNetworkInterfaceInfoGenuine.BandWidth;
            }
        }

        public double CurrentUtilization
        {
            get
            {
                return mNetworkInterfaceInfoGenuine.CurrentUtilization;
            }
        }
    }
}
