using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ZteApp.ProductService.EAServices.Helper;
using ZteApp.ProductService.EAServices.CommunicationTask;

namespace ZteApp.ProductService.EAServices.CommandInterpretion
{
    public class WIFICommandInterpreter:ICommandInterpretion
    {
        private IAccessiable mExecuter;
        private static Regex mInterestedPattern = new Regex(@"wifi", RegexOptions.IgnoreCase);
        private static Regex mWriteWIFIAddressPattern = new Regex(@"(?i)\s*write[\s-[\r\n]]+(wifi)[\s-[\r\n]]+address[\s-[\r\n]]+([abcdef\d]+)[\s-[\r\n]]*;");
        private static Regex mReadWIFIAddressPattern = new Regex(@"(?i)\s*read[\s-[\r\n]]+(wifi)[\s-[\r\n]]+address[\s-[\r\n]]*;");
        private string[] mAssemblyQualifiePath4Executoer = new string[] { typeof(BTCommandInterpreter).Namespace };
        private readonly static byte[] mWriteWIFIAddressCmdCode = new byte[] { 0xE1, 0x4B, 0xC8, 0x02, 0x00 };
        private static readonly byte[] mReadWIFIAddressCmdCode = { 0xE1, 0x4B, 0xC8, 0x05, 0x00 };
        private const int mWIFIAddressLength = 6;

        public void CommandEventHandler(object sender, CommunicationTask.CommandEventArgs e)
        {
            byte[] decodedBinaryCode;
            if (e.CommandString.StartsWith(mWriteWIFIAddressCmdCode[0].ToString("x2"), StringComparison.InvariantCultureIgnoreCase))
            {
                decodedBinaryCode = ASCIIRepresentor.String2ByteArray(e.CommandString);
            }
            else
            {
                decodedBinaryCode = null;
            }

            //binary process
            if (decodedBinaryCode != null && decodedBinaryCode[0] == mWriteWIFIAddressCmdCode[0])
            {
                bool isWriteWIFIAddressCmd = true;
                for (int i = 0; i < mWriteWIFIAddressCmdCode.Length; i++)
                {
                    if (decodedBinaryCode[i] != mWriteWIFIAddressCmdCode[i])
                    {
                        isWriteWIFIAddressCmd = false;
                        break;
                    }
                }

                bool isReadWIFIAddressCmd = true;
                for (int i = 0; i < mReadWIFIAddressCmdCode.Length; i++)
                {
                    if (decodedBinaryCode[i] != mReadWIFIAddressCmdCode[i])
                    {
                        isReadWIFIAddressCmd = false;
                        break;
                    }
                }

                if (!isWriteWIFIAddressCmd && !isReadWIFIAddressCmd)
                {
                    //no, I'm not interesting with the command
                    return;
                }

                if (isWriteWIFIAddressCmd)
                {
                    byte[] wifiAddress = new byte[mWIFIAddressLength];
                    Array.Copy(decodedBinaryCode, mWriteWIFIAddressCmdCode.Length, wifiAddress, 0, mWIFIAddressLength);
                    string commandString = string.Format("write wifi address {0};", ASCIIRepresentor.ASCIIByteArray2String(wifiAddress));
                    e.CommandString = commandString;
                    CommandEventHandler(sender, e);


                    e.CommandResult = new byte[mWriteWIFIAddressCmdCode.Length + 1];
                    //command.Result[0] = Convert.ToByte(mWriteBTAddressResponseFormat[0, 2]);
                    Array.Copy(mWriteWIFIAddressCmdCode, 0, e.CommandResult, 1, mWriteWIFIAddressCmdCode.Length);
                    e.CommandResult[mWriteWIFIAddressCmdCode.Length] = 0x00;
                }
                else if(isReadWIFIAddressCmd)
                {
                    e.CommandString="read wifi address;";
                    CommandEventHandler(sender,e);
                    byte[] tempResult=new byte[12];
                    Array.Copy(mReadWIFIAddressCmdCode, 0, tempResult, 0, mReadWIFIAddressCmdCode.Length);
                    if (e.CommandResult != null)
                    {
                        tempResult[mReadWIFIAddressCmdCode.Length] = 0x00;
                        Array.Copy(e.CommandResult, 0, tempResult, mReadWIFIAddressCmdCode.Length + 1, mWIFIAddressLength);
                    }
                    else
                    {
                        tempResult[mReadWIFIAddressCmdCode.Length] = 0x01;
                        Array.Clear(tempResult, mReadWIFIAddressCmdCode.Length + 1, mWIFIAddressLength);
                    }
                    e.CommandResult = tempResult;
                }
            }
            //literals process
            else if(mInterestedPattern.IsMatch(e.CommandString))
            {
                //I'm interesting now, pass me the code
                bool isWriteWIFICmd = mWriteWIFIAddressPattern.IsMatch(e.CommandString);
                bool isReadWIFICmd = mReadWIFIAddressPattern.IsMatch(e.CommandString);

                if (isWriteWIFICmd)
                {
                    Match match = mWriteWIFIAddressPattern.Match(e.CommandString);
                    if (mExecuter == null)
                    {
                        mExecuter = ResolveExecuter(match.Groups[1].Value);
                    }
                    mExecuter.Write(match.Groups[2].Value);
                }
                else if (isReadWIFICmd)
                {
                    Match match=mReadWIFIAddressPattern.Match(e.CommandString);
                    if(mExecuter==null)
                    {
                        mExecuter=ResolveExecuter(match.Groups[1].Value);
                    }
                    var result=mExecuter.Read(null);
                    e.CommandResult=result as byte[];
                }
            }
        }

        protected  IAccessiable ResolveExecuter(string typeName)
        {
            object result = null;
            Type type;
            foreach (string qulifiedPath in mAssemblyQualifiePath4Executoer)
            {
                string triedName = qulifiedPath + "." + typeName;
                try
                {
                    type = Type.GetType(triedName, true,/*ignore type case*/true);
                    result = Activator.CreateInstance(type);
                    break;
                }
                catch (TypeLoadException ex)
                {
                    continue;
                }
                catch (System.IO.FileNotFoundException ex)
                {
                    continue;
                }
            }

            return (result as IAccessiable);
        }
    }
}
