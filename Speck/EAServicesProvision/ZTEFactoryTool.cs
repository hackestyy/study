using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Diagnostics;

/*===========================================================================
                             Edit History
    when       who     what, where, why
    --------   ---     ----------------------------------------------------------
    2012/09/12 zhanghao add bt control related op
 *  
===========================================================================*/

namespace ZteApp.ProductService.EAServicesProvision
{
    public sealed class ZTEFactoryTool
    {
        private const string mFactoryParamPath = "Provision";
        private const string mBTFactoryParamFileName = "BT.PROVISION";
        private const string mWIFIFactoryParamFielName = "WLAN.PROVISION";
        private const int mBTAddressLength = 6;
        private const int mWIFIAddressLength = 6;
        private const uint mErrorSuccess = 0;
        ArrayWrapper<byte> mBTAddress = new ArrayWrapper<byte>(mBTAddressLength);

        [DllImport("ZteFactoryTool.dll",CharSet = CharSet.Ansi)]
        private static extern int SetFactoryParam(int argc,[In] string[] argv);

        [DllImport("qcbtradioctrl8960.dll", EntryPoint = "IsBluetoothRadioEnabled")]
        private static extern uint _IsBluetoothRadioEnabled(out bool enabled);

        [DllImport("qcbtradioctrl8960.dll", EntryPoint = "BluetoothEnableRadio")]
        private static extern uint _BluetoothEnableRadio(bool enabled);

        [DllImport("qcbtradioctrl8960.dll", EntryPoint = "BluetoothSentIOCTL")]
        private static extern uint _BluetoothSentIOCTL(uint ioControlCode,
                                                     IntPtr inBufferPointer,
                                                     uint inBufferSize,
                                                     IntPtr outBufferPointer,
                                                     uint outBufferSize);

        public void WriteBTAddress(string address)
        {
            string fullPath=System.IO.Path.GetDirectoryName(typeof(ZTEFactoryTool).Assembly.Location) +"\\"+ mFactoryParamPath;
            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }
            using (Stream s = new FileStream(fullPath + "\\" + mBTFactoryParamFileName, FileMode.Create))
            {
                using (BinaryWriter sw = new BinaryWriter(s))
                {
                    ///TODO:write bt address to fileSystem 
                    byte[] needWriteData=null;
                    if (address.Length == mBTAddressLength*2)
                    {
                        //suppose the string is ascii string
                        needWriteData = new byte[mBTAddressLength];
                        for (int i = 0; i < mBTAddressLength; i++)
                        {
                            needWriteData[i]=byte.Parse(address.Substring(2 * i, 2),System.Globalization.NumberStyles.HexNumber);
                        }

                    }
                    else
                    {
                        needWriteData = String2ByteArray(address);
                    }
                    byte needWriteDataLength =Convert.ToByte(needWriteData.Length);
                    sw.Write((byte)0x1);
                    sw.Write(needWriteDataLength);
                    for (byte i = 0; i < needWriteDataLength; i++)
                    {
                        sw.Write(needWriteData[i]);
                    }
                }
            }
            int ret = SetFactoryParam(5, new string[] { "", "/Provision", mFactoryParamPath, "/Type", "QCOM"});  
        }

        //BYTE* GetBTAddress();
        [DllImport("ZteFactoryTool.dll", CharSet = CharSet.Ansi, EntryPoint = "GetBTAddress", CallingConvention = CallingConvention.Cdecl)]
        //[DllImport("TestDll.dll", CharSet = CharSet.Ansi, EntryPoint = "GetBTAddress", CallingConvention = CallingConvention.Cdecl)]
        //[return: MarshalAsAttribute(UnmanagedType.LPArray, SizeConst = 6, ArraySubType = UnmanagedType.U1)]
        private static extern int _GetBTAddress([MarshalAs(UnmanagedType.LPArray,SizeConst=6)]byte[] btAddress);

        public string GetBTAddress()
        {
            Trace.WriteLine("GetBTAddress in.");
            byte[] tempBTAddress=new byte[mBTAddressLength];
            _GetBTAddress(tempBTAddress);
            for (int i = 0; i < mBTAddressLength; i++)
            {
                mBTAddress[i] = tempBTAddress[i];
            }
            Trace.WriteLine(string.Format("bt address is {0}.", mBTAddress.ToString()));
            Trace.WriteLine("GetBTAddress out.");

            return ASCIIByteArray2String(mBTAddress.Array, 0, mBTAddress.Array.Length);
        }

        public void WriteWIFIAddress(string address)
        {
            string fullPath = System.IO.Path.GetDirectoryName(typeof(ZTEFactoryTool).Assembly.Location) + "\\" + mFactoryParamPath;
            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }
            using (Stream s = new FileStream(fullPath+"\\"+mWIFIFactoryParamFielName,FileMode.Create))
            {
                using (BinaryWriter sw = new BinaryWriter(s))
                {
                    ///TODO: write wifi address to filesytem
                    byte[] needWriteData = String2ByteArray(address);
                    sw.Write(new byte[] { 0x01, 0x19, 0x04 });
                    for (int i = 0; i < 4; i++)
                    {
                        sw.Write(needWriteData);
                    }
                }
            }
            SetFactoryParam(5, new string[] { "", "/Provision", mFactoryParamPath, "/Type", "QCOM" });
        }

        public bool IsBluetoothRadioEnabled()
        {
            bool result;
            uint status=_IsBluetoothRadioEnabled(out result);
            if (status != mErrorSuccess)
            {
                throw new Exception("Invoke Exception!");
            }
            return result;
        }

        public void BluetoothEnableRadio(bool enabled)
        {
            uint status = _BluetoothEnableRadio(enabled);
            if (status != mErrorSuccess)
            {
                throw new Exception("Invoke Exception!");
            }
        }

        public void BluetoothSentIOCTL(uint ioControlCode,
                                        byte[] inBuffer,
                                        int inBufferSize,
                                        byte[] outBuffer,
                                        int outBufferSize)
        {
            IntPtr inBufferPtr = IntPtr.Zero;
            IntPtr outBufferPtr = IntPtr.Zero;
            try
            {
                inBufferPtr=Marshal.AllocHGlobal(inBufferSize);
                outBufferPtr = Marshal.AllocHGlobal(outBufferSize);
                Marshal.Copy(inBuffer, 0, inBufferPtr, inBufferSize);
                if (mErrorSuccess != _BluetoothSentIOCTL(ioControlCode, inBufferPtr, (uint)inBufferSize, outBufferPtr, (uint)outBufferSize))
                {
                    throw new Exception("Invoke Exception!");
                }
            }
            finally
            {
                Marshal.FreeHGlobal(inBufferPtr);
                Marshal.FreeHGlobal(outBufferPtr);
            }
        }


        /*************************************helper function****************************************/
        private byte[] String2ByteArray(string str)
        {
            if (str.Length % 2 != 0)
            {
                throw new Exception("Invalid ascii representation!");
            }

            int byteArrayLength = str.Length / 2;
            byte[] byteArray = new byte[byteArrayLength];
            for (int i = 0; i < byteArrayLength; i++)
            {
                byteArray[i] = byte.Parse(str.Substring(2 * i, 2), System.Globalization.NumberStyles.HexNumber);
            }
            return byteArray;
        }

        private string ASCIIByteArray2String(byte[] byteArray, int index, int count)
        {
            StringBuilder sb = new StringBuilder();
            int endIndex = index + count;
            for (int i = index; i < endIndex; i++)
            {
                sb.Append(byteArray[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}