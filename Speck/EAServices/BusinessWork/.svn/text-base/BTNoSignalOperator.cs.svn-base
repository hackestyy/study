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
    class BTQueryICNumberOperator : IBusinessOperator
    {

        public String Name { get { return "BTQueryICNumber"; } }
        public byte[] CmdHead { get { return new byte[] { 0xE1, 0x4B, 0xCD, 0x60, 0x00 }; } }
        private Encoding _ASCIIencodeing = Encoding.ASCII;
        public byte[] Do(byte[] cmd,int size)
        {
            byte[] respBody = BTQueryICNumberAddress();
            return ArrayTool.JoinTwoByteArray(CmdHead, respBody);
        }
        private byte[] BTQueryICNumberAddress()
        {
            
            return new byte[] { 0x00 };
        }
    }

    class BTRFTXControlOperator : IBusinessOperator
    {

        public String Name { get { return "BTRFTXControl"; } }
        public byte[] CmdHead { get { return new byte[] { 0xE1, 0x4B, 0xCD, 0x61, 0x00 }; } }
        private Encoding _ASCIIencodeing = Encoding.ASCII;
        public byte[] Do(byte[] cmd, int size)
        {
            byte[] respBody = BTRFTXControl();
            return ArrayTool.JoinTwoByteArray(CmdHead, respBody);
        }
        private byte[] BTRFTXControl()
        {
           
            return new byte[] { 0x00 };
        }
    }
    class BTRFRXControlOperator : IBusinessOperator
    {

        public String Name { get { return "BTRFRXControl"; } }
        public byte[] CmdHead { get { return new byte[] { 0xE1, 0x4B, 0xCD, 0x62, 0x00 }; } }
        private Encoding _ASCIIencodeing = Encoding.ASCII;
        public byte[] Do(byte[] cmd, int size)
        {
            byte[] respBody = BTRFRXControl();
            return ArrayTool.JoinTwoByteArray(CmdHead, respBody);
        }
        private byte[] BTRFRXControl()
        {

            return new byte[] { 0x00 };
        }
    }
    class BTGetRevDataOperator : IBusinessOperator
    {

        public String Name { get { return "BTGetRevData"; } }
        public byte[] CmdHead { get { return new byte[] { 0xE1, 0x4B, 0xCD, 0x63, 0x00 }; } }
        private Encoding _ASCIIencodeing = Encoding.ASCII;
        public byte[] Do(byte[] cmd, int size)
        {
            byte[] respBody = BTGetRevData();
            return ArrayTool.JoinTwoByteArray(CmdHead, respBody);
        }
        private byte[] BTGetRevData()
        {

            return new byte[] { 0x00 };
        }
    }
}
