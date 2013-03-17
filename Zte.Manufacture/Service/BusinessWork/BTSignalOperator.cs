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
    class BTOpenOperator : IBusinessOperator
    {

        public String Name { get { return "BTOpen"; } }
        public byte[] CmdHead { get { return new byte[] { 0xE1, 0x4B, 0xCD, 0x25, 0x00 }; } }
        private Encoding _ASCIIencodeing = Encoding.ASCII;
        public byte[] Do(byte[] cmd,int size)
        {
            byte[] respBody = BTOpen();
            return ArrayTool.JoinTwoByteArray(CmdHead, respBody);
        }
        private byte[] BTOpen()
        {
            
            return new byte[] { 0x00 };
        }
    }

    class BTCloseOperator : IBusinessOperator
    {

        public String Name { get { return "BTClose"; } }
        public byte[] CmdHead { get { return new byte[] { 0xE1, 0x4B, 0xCD, 0x26, 0x00 }; } }
        private Encoding _ASCIIencodeing = Encoding.ASCII;
        public byte[] Do(byte[] cmd, int size)
        {
            byte[] respBody = BTClose();
            return ArrayTool.JoinTwoByteArray(CmdHead, respBody);
        }
        private byte[] BTClose()
        {
           
            return new byte[] { 0x00 };
        }
    }
}
