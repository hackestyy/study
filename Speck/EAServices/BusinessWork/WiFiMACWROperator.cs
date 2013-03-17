using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Zte.Manufacture.Service.BusinessWork;
using Zte.Manufacture.Service.Common;
using ZteApp.ProductService.EAServices.CommandInterpretion;
using ZteApp.ProductService.EAServicesProvision;

namespace Zte.Manufacture.Service.BusinessWork
{
    class WiFiWriteMACOperator : IBusinessOperator
    {

        public String Name { get { return "WiFiWrite"; } }
        public byte[] CmdHead { get { return new byte[] { 0xE1, 0x4B, 0xC8, 0x02, 0x00 }; } }
        private Encoding _ASCIIencodeing = Encoding.ASCII;
        public byte[] Do(byte[] cmd,int size)
        {

            byte[] respBody = WriteWiFiAddress(_ASCIIencodeing.GetString(cmd, 6, 12));
            return ArrayTool.JoinTwoByteArray(CmdHead, respBody);
        }
        private byte[] WriteWiFiAddress(string address)
        {
            ZTEFactoryTool zTEFactoryTool = new ZTEFactoryTool();
            zTEFactoryTool.WriteWIFIAddress(address);
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
            WIFI wifi = new WIFI();
            byte[] result = wifi.Read(null) as byte[];
            if (result == null)
            {
                return new byte[] { 0x01, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            }
            else
            {
                return ArrayTool.JoinTwoByteArray(new byte[]{0x00},result);
            }
        }
    }
    
}
