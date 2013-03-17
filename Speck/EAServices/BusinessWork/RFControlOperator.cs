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
    class SetNetPriorityOperator : IBusinessOperator
    {

        public String Name { get { return "SetNetPriority"; } }
        public byte[] CmdHead { get { return new byte[] { 0xE0, 0x4B, 0xB1, 0x00, 0x00 }; } }
        private Encoding _ASCIIencodeing = Encoding.ASCII;
        public byte[] Do(byte[] cmd,int size)
        {
            byte[] respBody = SetNetPriority();
            return ArrayTool.JoinTwoByteArray(CmdHead, respBody);
        }
        private byte[] SetNetPriority()
        {
            return new byte[] { 0x00 };
        }
    }
    class ReadIMSIOperator : IBusinessOperator
    {

        public String Name { get { return "ReadIMSI"; } }
        public byte[] CmdHead { get { return new byte[] { 0xE0, 0x4B, 0x04, 0x0F, 0x00 }; } }
        private Encoding _ASCIIencodeing = Encoding.ASCII;
        public byte[] Do(byte[] cmd, int size)
        {
            byte[] respBody = ReadIMSI();
            return ArrayTool.JoinTwoByteArray(CmdHead, respBody);
        }
        private byte[] ReadIMSI()
        {
            return new byte[] { 0x00 };
        }
    }
    class ReadModemBoardNumOperator : IBusinessOperator
    {

        public String Name { get { return "ReadModemBoardNum"; } }
        public byte[] CmdHead { get { return new byte[] { 0xE0 }; } }
        private Encoding _ASCIIencodeing = Encoding.ASCII;
        public byte[] Do(byte[] cmd, int size)
        {
            byte[] respBody = ReadModemBoardNum();
            return ArrayTool.JoinTwoByteArray(CmdHead, respBody);
        }
        private byte[] ReadModemBoardNum()
        {
            return new byte[] { 0x00 };
        }
    }
    class WriteModemBoardNumOperator : IBusinessOperator
    {

        public String Name { get { return "WriteModemBoardNum"; } }
        public byte[] CmdHead { get { return new byte[] { 0xE0,0x00 }; } }
        private Encoding _ASCIIencodeing = Encoding.ASCII;
        public byte[] Do(byte[] cmd, int size)
        {
            byte[] respBody = WriteModemBoardNum();
            return ArrayTool.JoinTwoByteArray(CmdHead, respBody);
        }
        private byte[] WriteModemBoardNum()
        {
            return new byte[] { 0x00 };
        }
    }
    class ReadTestMarkBitOperator : IBusinessOperator
    {

        public String Name { get { return "ReadTestMarkBit"; } }
        public byte[] CmdHead { get { return new byte[] { 0xE0,0x26 }; } }
        private Encoding _ASCIIencodeing = Encoding.ASCII;
        public byte[] Do(byte[] cmd, int size)
        {
            byte[] respBody = ReadTestMarkBit();
            return ArrayTool.JoinTwoByteArray(CmdHead, respBody);
        }
        private byte[] ReadTestMarkBit()
        {
            return new byte[] { 0x00 };
        }
    }
    class WriteTestMarkBitOperator : IBusinessOperator
    {

        public String Name { get { return "WriteTestMarkBit"; } }
        public byte[] CmdHead { get { return new byte[] { 0xE0,0x27 }; } }
        private Encoding _ASCIIencodeing = Encoding.ASCII;
        public byte[] Do(byte[] cmd, int size)
        {
            byte[] respBody = WriteTestMarkBit();
            return ArrayTool.JoinTwoByteArray(CmdHead, respBody);
        }
        private byte[] WriteTestMarkBit()
        {
            return new byte[] { 0x00 };
        }
    }
    class SetDeviceStateOperator : IBusinessOperator
    {

        public String Name { get { return "SetDeviceState"; } }
        public byte[] CmdHead { get { return new byte[] { 0xE0, 0x29, 0x03 }; } }
        private Encoding _ASCIIencodeing = Encoding.ASCII;
        public byte[] Do(byte[] cmd, int size)
        {
            byte[] respBody = SetDeviceState();
            return ArrayTool.JoinTwoByteArray(CmdHead, respBody);
        }
        private byte[] SetDeviceState()
        {
            return new byte[] { 0x00 };
        }
    }
    class SetChannelSwichOperator : IBusinessOperator
    {

        public String Name { get { return "SetChannelSwich"; } }
        public byte[] CmdHead { get { return new byte[] { 0xE0, 0x4B, 0x44, 0x00, 0x40 }; } }
        private Encoding _ASCIIencodeing = Encoding.ASCII;
        public byte[] Do(byte[] cmd, int size)
        {
            byte[] respBody = SetChannelSwich();
            return ArrayTool.JoinTwoByteArray(CmdHead, respBody);
        }
        private byte[] SetChannelSwich()
        {
            return new byte[] { 0x00 };
        }
    }
    class ReadModemVersionOperator : IBusinessOperator
    {

        public String Name { get { return "ReadModemVersion"; } }
        public byte[] CmdHead { get { return new byte[] { 0xE0, 0x39 }; } }
        private Encoding _ASCIIencodeing = Encoding.ASCII;
        public byte[] Do(byte[] cmd, int size)
        {
            byte[] respBody = ReadModemVersion();
            return ArrayTool.JoinTwoByteArray(CmdHead, respBody);
        }
        private byte[] ReadModemVersion()
        {
            return new byte[] { 0x00 };
        }
    }
}
