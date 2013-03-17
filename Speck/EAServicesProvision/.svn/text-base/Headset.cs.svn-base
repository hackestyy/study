using System;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace ZteApp.ProductService.EAServicesProvision
{
    public sealed class Headset
    {
        //[DllImport("HeadsetLib.dll", CallingConvention=CallingConvention.Cdecl)]
        //private static extern bool GetCount();

        public bool IsHeadsetIn()
        {
            Trace.WriteLine("Headset in.");
            BusinessLib.Headset headset = new BusinessLib.Headset();
            return headset.GetCount();
        }
    }
}
