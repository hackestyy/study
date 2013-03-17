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
    class WiFiWriteMACOperator : IBusinessOperator
    {

        public String Name { get { return "WiFiWrite"; } }
        public byte[] CmdHead { get { return new byte[] { 0xE1, 0x4B, 0xC8, 0x02, 0x00 }; } }
        private Encoding _ASCIIencodeing = Encoding.ASCII;
        public byte[] Do(byte[] cmd,int size)
        {
            byte[] respBody = WriteWiFiAddress();
            return ArrayTool.JoinTwoByteArray(CmdHead, respBody);
        }
        private byte[] WriteWiFiAddress()
        {
            //BTWorker btWorker = new BTWorker();
            //btWorker.WriteBTAddress(address);
            return new byte[] { 0x00 };
        }
    }
    class WiFiReadMACOperator : IBusinessOperator
    {
        public String Name { get { return "WiFiRead"; } }
        public byte[] CmdHead { get { return new byte[] { 0xE1, 0x4B, 0xC8, 0x05, 0x00 }; } }
        private Encoding _ASCIIencodeing = Encoding.ASCII;
        public byte[] Do(byte[] cmd, int size)
        {
            byte[] respBody = ReadWiFiAddress();
            return ArrayTool.JoinTwoByteArray(CmdHead, respBody);
        }
        private byte[] ReadWiFiAddress()
        {
            //BTWorker btWorker = new BTWorker();
            //btWorker.WriteBTAddress(address);
            return new byte[] { 0x00 };
        }
    }
    
}
