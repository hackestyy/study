using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Management;

namespace ZteApp.ProductService.EAServices.BusinessWork
{
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerSession)]
    public partial class Service:IBacklight
    {
        private static readonly ManagementScope mScope = new ManagementScope("\\root\\wmi");
        private static readonly SelectQuery mQuery=new SelectQuery("WmiMonitorBrightnessMethods");

        public Service()
        {
            Initialise();
        }

        partial void Initialise();
        partial void Initialise()
        {
            mScope.Connect();
        }

        #region IBacklight members
        public void SetBrightness(byte targetBrightness)
        {
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(mScope, mQuery))
            {
                using (ManagementObjectCollection objectCollection = searcher.Get())
                {
                    foreach (ManagementObject mObj in objectCollection)
                    {
                        mObj.InvokeMethod("WmiSetBrightness",
                            new Object[] { UInt32.MaxValue, targetBrightness });
                        return;
                    }
                }
            }
        }
        #endregion
    }
}
