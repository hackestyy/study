using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZteApp.ProductService.EAServicesProvision;
using ZteApp.ProductService.EAServices.Helper;
using System.Net.NetworkInformation;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace ZteApp.ProductService.EAServices.CommandInterpretion
{
    public class WIFI:IAccessiable
    {
        private Regex mWIFIAdapterQualcommName = new Regex("(?i).*(qualcomm).*");
        public void Write(object param)
        {
            var obj = param as string;
            if (obj == null)
            {
                throw new InvalidCastException("param is not type of string!");
            }

            //TODO:add write address here
            ZTEFactoryTool zTEFactoryTool = new ZTEFactoryTool();
            zTEFactoryTool.WriteWIFIAddress(obj);
        }

        public object Read(object param)
        {
            //reaad wifi address
            Trace.WriteLine("Read wifi address in.");
            byte[] result=null;
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            if (nics == null || nics.Length < 1)
            {
                throw new Exception("No network interface found!");  
            }

            int loopCount = 0;
            foreach (NetworkInterface nic in nics)
            {
                if (nic.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 && mWIFIAdapterQualcommName.IsMatch(nic.Description))
                {
                    result = nic.GetPhysicalAddress().GetAddressBytes();
#if TRACE
                    StringBuilder sb=new StringBuilder();
                    foreach(byte b in result)
                    {
                        sb.Append(b.ToString("X2"));
                    }
                    Trace.WriteLine(string.Format("wifi address got {0}",sb.ToString()));
#endif
                    break;
                }
                loopCount++;
            }
            if (loopCount == nics.Length)
            {
                //not found the destination interface
                result =null;
            }
            Trace.WriteLine("Read wifi address out.");
            return result;
        }

        public object Check(object param)
        {
            throw new NotImplementedException();
        }
    }
}
