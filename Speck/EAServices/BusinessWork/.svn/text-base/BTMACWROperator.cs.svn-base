using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ZteApp.ProductService.EAServices.Helper;
using ZteApp.ProductService.EAServicesProvision;
using Zte.Manufacture.Service.BusinessWork;
using Zte.Manufacture.Service.Common;

namespace Zte.Manufacture.Service.BusinessWork
{
    class BTWriteMACOperator : IBusinessOperator
    {
        
        public String Name { get { return "BTWrite"; } }
        public byte[] CmdHead { get { return new byte[] { 0xE1, 0x4B, 0xFA, 0x06, 0x00 }; } }
        private Encoding _ASCIIencodeing = Encoding.ASCII;
        public byte[] Do(byte[] cmd,int size)
        {
            byte[] respBody = WriteBTAddress(_ASCIIencodeing.GetString(cmd, 6, 12));
            return ArrayTool.JoinTwoByteArray(CmdHead, respBody);
        }
        private byte[] WriteBTAddress(string address)
        {
            ZTEFactoryTool zTEFactoryTool = new ZTEFactoryTool();
            zTEFactoryTool.WriteBTAddress(address);
            return new byte[] { 0x00 };
        }
    }
    class BTReadMACOperator : IBusinessOperator
    {
        public String Name { get { return "BTRead"; } }
        public byte[] CmdHead { get { return new byte[] { 0xE1, 0x4B, 0xFA, 0x05, 0x00 }; } }
        private Encoding _ASCIIencodeing = Encoding.ASCII;
        public byte[] Do(byte[] cmd, int size)
        {
            byte[] respBody = ReadBTAddress(_ASCIIencodeing.GetString(cmd, 6, 12));
            return ArrayTool.JoinTwoByteArray(CmdHead, respBody);
        }
        private byte[] ReadBTAddress(string address)
        {
            ZTEFactoryTool zTEFactoryTool = new ZTEFactoryTool();
            return ArrayTool.JoinTwoByteArray(new byte[]{0x00},ASCIIRepresentor.String2ByteArray(zTEFactoryTool.GetBTAddress()));
        }
    }
    class BTWorker
    {
        private const string mFactoryParamPath = "Provision";
        private const string mFactoryParamFileName = "BT.PROVISION";

        [DllImport("ZteFactoryTool.dll", CharSet = CharSet.Ansi)]
        private static extern int SetFactoryParam(int argc, [In] string[] argv);

        public void WriteBTAddress(string address)
        {
            string fullPath = System.IO.Path.GetDirectoryName(typeof(BTWorker).Assembly.Location) + "\\" + mFactoryParamPath;
            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }
            using (Stream s = new FileStream(fullPath + "\\" + mFactoryParamFileName, FileMode.Create))
            {
                using (BinaryWriter sw = new BinaryWriter(s))
                {
                    ///TODO:write bt address to fileSystem

                    byte[] needWriteData = Encoding.UTF8.GetBytes(address);
                    byte needWriteDataLength = Convert.ToByte(needWriteData.Length);
                    sw.Write((byte)0x1);
                    sw.Write(needWriteDataLength);
                    for (byte i = 0; i < needWriteDataLength; i++)
                    {
                        sw.Write(needWriteData[i]);
                    }
                }
            }
            int ret = SetFactoryParam(5, new string[] { "", "/Provision", mFactoryParamPath, "/Type", "QCOM" });
        }
    }
}
