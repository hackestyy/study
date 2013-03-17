using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZteApp.ProductService.Core;
using System.ServiceModel;

namespace ZteApp.ProductService.EAServices.CommunicationTask
{
    public class MessagePort:Behavior
    {
        private ServiceHost mServiceHost=null;

        public MessagePort(Type serviceType,params Uri[] baseAddresses):base()
        {
            mServiceHost = new ServiceHost(serviceType, baseAddresses);
        }

        public MessagePort(object singleton,params Uri[] baseAddresses):base()
        {
            mServiceHost = new ServiceHost(singleton, baseAddresses);
        }

        protected override void OnBeforeStart()
        {
            base.OnBeforeStart();

            //start listening from here
            mServiceHost.Open();
        }

        protected override void DoWork()
        {
            //becase listening is a asynchronous work, so we pasue the master work here
            Pause();
        }

        protected override void OnClose()
        {
            if (mServiceHost != null)
            {
                mServiceHost.Close();
                mServiceHost = null;
            }
            base.OnClose();
        }
    }
}
