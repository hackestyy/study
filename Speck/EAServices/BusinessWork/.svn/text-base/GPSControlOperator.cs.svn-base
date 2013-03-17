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
    class GPSEnterOperator : IBusinessOperator
    {

        public String Name { get { return "GPSEnter"; } }
        public byte[] CmdHead { get { return new byte[] { 0xE1, 0x4B, 0xCD, 0x71, 0x00}; } }
        private Encoding _ASCIIencodeing = Encoding.ASCII;
        public byte[] Do(byte[] cmd,int size)
        {
            byte[] respBody = GPSEnter();
            return ArrayTool.JoinTwoByteArray(CmdHead, respBody);
        }
        private byte[] GPSEnter()
        {
            
            return new byte[] { 0x00 };
        }
    }

    class GPSLeaveOperator : IBusinessOperator
    {

        public String Name { get { return "GPSLeave"; } }
        public byte[] CmdHead { get { return new byte[] { 0xE1, 0x4B, 0xCD, 0x71, 0x00, 0x01 }; } }
        private Encoding _ASCIIencodeing = Encoding.ASCII;
        public byte[] Do(byte[] cmd, int size)
        {
            byte[] respBody = GPSLeave();
            return ArrayTool.JoinTwoByteArray(CmdHead, respBody);
        }
        private byte[] GPSLeave()
        {
           
            return new byte[] { 0x00 };
        }
    }
    class GPSGetResultOperator : IBusinessOperator
    {

        public String Name { get { return "GPSGetResult"; } }
        public byte[] CmdHead { get { return new byte[] { 0xE1, 0x4B, 0xCD, 0x71, 0x00, 0x02 }; } }
        private Encoding _ASCIIencodeing = Encoding.ASCII;
        public byte[] Do(byte[] cmd, int size)
        {
            byte[] respBody = GPSGetResult();
            return ArrayTool.JoinTwoByteArray(CmdHead, respBody);
        }
        private byte[] GPSGetResult()
        {

            return new byte[] { 0x00 };
        }
    }
}
