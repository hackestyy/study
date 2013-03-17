using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Zte.Manufacture.Service.BusinessWork;
using Zte.Manufacture.Service.Common;

namespace Zte.Manufacture.Service.BusinessWork
{
    class GetCPEVersionOperator : IBusinessOperator
    {

        public String Name { get { return "GetCPEVersion"; } }
        public byte[] CmdHead { get { return new byte[] { 0x60}; } }
        private Encoding _ASCIIencodeing = Encoding.ASCII;
        public byte[] Do(byte[] cmd,int size)
        {
            byte[] respBody = GetCPEVersion();
            return ArrayTool.JoinTwoByteArray(CmdHead, respBody);
        }
        private byte[] GetCPEVersion()
        {
            
            return new byte[] { 0x00 };
        }
    }

    class RestoreDeviceOperator : IBusinessOperator
    {

        public String Name { get { return "RestoreDevice"; } }
        public byte[] CmdHead { get { return new byte[] { 0x33 }; } }
        private Encoding _ASCIIencodeing = Encoding.ASCII;
        public byte[] Do(byte[] cmd, int size)
        {
            byte[] respBody = RestoreDevice();
            return ArrayTool.JoinTwoByteArray(CmdHead, respBody);
        }
        private byte[] RestoreDevice()
        {
           
            return new byte[] { 0x00 };
        }
    }
}
