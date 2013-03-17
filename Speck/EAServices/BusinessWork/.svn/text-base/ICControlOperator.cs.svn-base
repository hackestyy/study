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
    class GetBTICIDOperator : IBusinessOperator
    {

        public String Name { get { return "GetBTICID"; } }
        public byte[] CmdHead { get { return new byte[] { 0xE1, 0x4B, 0xCD, 0x23, 0x00 }; } }
        private Encoding _ASCIIencodeing = Encoding.ASCII;
        public byte[] Do(byte[] cmd,int size)
        {
            byte[] respBody = GetBTICID();
            return ArrayTool.JoinTwoByteArray(CmdHead, respBody);
        }
        private byte[] GetBTICID()
        {
            
            return new byte[] { 0x00 };
        }
    }

    class GetGPSICIDOperator : IBusinessOperator
    {

        public String Name { get { return "GetGPSICID"; } }
        public byte[] CmdHead { get { return new byte[] { 0xE1, 0x4B, 0xCD, 0x51, 0x00 }; } }
        private Encoding _ASCIIencodeing = Encoding.ASCII;
        public byte[] Do(byte[] cmd, int size)
        {
            byte[] respBody = GetGPSICID();
            return ArrayTool.JoinTwoByteArray(CmdHead, respBody);
        }
        private byte[] GetGPSICID()
        {

            return new byte[] { 0x00 };
        }
    }

    class GetWiFiICIDOperator : IBusinessOperator
    {

        public String Name { get { return "GetWiFiICID"; } }
        public byte[] CmdHead { get { return new byte[] { 0xE1, 0x4B, 0xCD, 0x41, 0x00 }; } }
        private Encoding _ASCIIencodeing = Encoding.ASCII;
        public byte[] Do(byte[] cmd, int size)
        {
            byte[] respBody = GetWiFiICID();
            return ArrayTool.JoinTwoByteArray(CmdHead, respBody);
        }
        private byte[] GetWiFiICID()
        {

            return new byte[] { 0x00 };
        }
    }
    class GetAcceleratorICIDOperator : IBusinessOperator
    {

        public String Name { get { return "GetAcceleratorICID"; } }
        public byte[] CmdHead { get { return new byte[] { 0xE1, 0x4B, 0xCD, 0x42, 0x00 }; } }
        private Encoding _ASCIIencodeing = Encoding.ASCII;
        public byte[] Do(byte[] cmd, int size)
        {
            byte[] respBody = GetAcceleratorICID();
            return ArrayTool.JoinTwoByteArray(CmdHead, respBody);
        }
        private byte[] GetAcceleratorICID()
        {

            return new byte[] { 0x00 };
        }
    }
    class GetCompassICIDOperator : IBusinessOperator
    {

        public String Name { get { return "GetCompassICID"; } }
        public byte[] CmdHead { get { return new byte[] { 0xE1, 0x4B, 0xCD, 0x43, 0x00 }; } }
        private Encoding _ASCIIencodeing = Encoding.ASCII;
        public byte[] Do(byte[] cmd, int size)
        {
            byte[] respBody = GetCompassICID();
            return ArrayTool.JoinTwoByteArray(CmdHead, respBody);
        }
        private byte[] GetCompassICID()
        {

            return new byte[] { 0x00 };
        }
    }
    class GetGyroscopeICIDOperator : IBusinessOperator
    {

        public String Name { get { return "GetGyroscopeICID"; } }
        public byte[] CmdHead { get { return new byte[] { 0xE1, 0x4B, 0xA0, 0x00, 0x04 }; } }
        private Encoding _ASCIIencodeing = Encoding.ASCII;
        public byte[] Do(byte[] cmd, int size)
        {
            byte[] respBody = GetGyroscopeICID();
            return ArrayTool.JoinTwoByteArray(CmdHead, respBody);
        }
        private byte[] GetGyroscopeICID()
        {

            return new byte[] { 0x00 };
        }
    }
}
