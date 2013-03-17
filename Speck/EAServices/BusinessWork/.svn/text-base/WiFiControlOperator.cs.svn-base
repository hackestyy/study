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
    class WiFiEnterLeaveOperator : IBusinessOperator
    {

        public String Name { get { return "WiFiEnterLeave"; } }
        public byte[] CmdHead { get { return new byte[] { 0xE1, 0x4B, 0xCD, 0x07, 0x00 }; } }
        private Encoding _ASCIIencodeing = Encoding.ASCII;
        public byte[] Do(byte[] cmd,int size)
        {
            byte[] respBody = WiFiEnterLeave();
            return ArrayTool.JoinTwoByteArray(CmdHead, respBody);
        }
        private byte[] WiFiEnterLeave()
        {

            return new byte[] { 0x00 };
        }
    }
    class WiFiTXTestOperatorStart : IBusinessOperator
    {
        public String Name { get { return "WiFiTXTestStart"; } }
        public byte[] CmdHead { get { return new byte[] { 0xE1, 0x4B, 0xCD, 0x05, 0x00 }; } }
        private Encoding _ASCIIencodeing = Encoding.ASCII;
        public byte[] Do(byte[] cmd, int size)
        {
            byte[] respBody = WiFiTXTest();
            return ArrayTool.JoinTwoByteArray(CmdHead, respBody);
        }
        private byte[] WiFiTXTest()
        {

            return new byte[] { 0x00 };
        }
    }

    class WiFiTXTestOperatorStop : IBusinessOperator
    {
        public String Name { get { return "WiFiTXTestStop"; } }
        public byte[] CmdHead { get { return new byte[] { 0xE1, 0x4B, 0xCD, 0x05, 0x00,0x00 }; } }
        private Encoding _ASCIIencodeing = Encoding.ASCII;
        public byte[] Do(byte[] cmd, int size)
        {
            byte[] respBody = WiFiTXTest();
            return ArrayTool.JoinTwoByteArray(CmdHead, respBody);
        }
        private byte[] WiFiTXTest()
        {

            return new byte[] { 0x00 };
        }
    }

    class WiFiRXTestOperatorStart : IBusinessOperator
    {
        public String Name { get { return "WiFiRXTestStart"; } }
        public byte[] CmdHead { get { return new byte[] { 0xE1, 0x4B, 0xCD, 0x06, 0x00 }; } }
        private Encoding _ASCIIencodeing = Encoding.ASCII;
        public byte[] Do(byte[] cmd, int size)
        {
            byte[] respBody = WiFiRXTest();
            return ArrayTool.JoinTwoByteArray(CmdHead, respBody);
        }
        private byte[] WiFiRXTest()
        {

            return new byte[] { 0x00 };
        }
    }

    class WiFiRXTestOperatorStop : IBusinessOperator
    {
        public String Name { get { return "WiFiRXTestStop"; } }
        public byte[] CmdHead { get { return new byte[] { 0xE1, 0x4B, 0xCD, 0x06, 0x00,0x00 }; } }
        private Encoding _ASCIIencodeing = Encoding.ASCII;
        public byte[] Do(byte[] cmd, int size)
        {
            byte[] respBody = WiFiRXTest();
            return ArrayTool.JoinTwoByteArray(CmdHead, respBody);
        }
        private byte[] WiFiRXTest()
        {

            return new byte[] { 0x00 };
        }
    }
}
