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
    class ReadAPBoardOperator : IBusinessOperator
    {

        public String Name { get { return "ReadAPBoard"; } }
        public byte[] CmdHead { get { return new byte[] { 0xE1, 0x4B, 0xCD, 0x80, 0x00 }; } }
        private Encoding _ASCIIencodeing = Encoding.ASCII;
        public byte[] Do(byte[] cmd,int size)
        {
            byte[] respBody = ReadAPBoard();
            return ArrayTool.JoinTwoByteArray(CmdHead, respBody);
        }
        private byte[] ReadAPBoard()
        {
            
            return new byte[] { 0x00 };
        }
    }

    class WriteAPBoardOperator : IBusinessOperator
    {

        public String Name { get { return "WriteAPBoard"; } }
        public byte[] CmdHead { get { return new byte[] { 0xE1, 0x4B, 0xCD, 0x81, 0x00 }; } }
        private Encoding _ASCIIencodeing = Encoding.ASCII;
        public byte[] Do(byte[] cmd, int size)
        {
            byte[] respBody = WriteAPBoard();
            return ArrayTool.JoinTwoByteArray(CmdHead, respBody);
        }
        private byte[] WriteAPBoard()
        {

            return new byte[] { 0x00 };
        }
    }
    class EnterPowerSavingModeOperator : IBusinessOperator
    {

        public String Name { get { return "EnterPowerSavingMode"; } }
        public byte[] CmdHead { get { return new byte[] { 0xE1, 0x4B, 0xFA, 0x00, 0x00 }; } }
        private Encoding _ASCIIencodeing = Encoding.ASCII;
        public byte[] Do(byte[] cmd, int size)
        {
            byte[] respBody = EnterPowerSavingMode();
            return ArrayTool.JoinTwoByteArray(CmdHead, respBody);
        }
        private byte[] EnterPowerSavingMode()
        {

            return new byte[] { 0x00 };
        }
    }
}
