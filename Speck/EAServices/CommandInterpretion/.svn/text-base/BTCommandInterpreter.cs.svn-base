using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ZteApp.ProductService.EAServices.CommandInterpretion;
using ZteApp.ProductService.EAServices.CommunicationTask;
using ZteApp.ProductService.EAServices.Helper;
using ZteApp.ProductService.EAServices.CommandInterpretion;

namespace ZteApp.ProductService.EAServices.CommandInterpretion
{
    public class BTCommandInterpreter : ICommandInterpretion
    {
        private byte[] mResult;
        private static Regex mInterestedPattern = new Regex(@"bt",RegexOptions.IgnoreCase);
        private static Regex mWriteBTAddressPattern = new Regex(@"(?i)\s*write[\s-[\r\n]]+(bt)[\s-[\r\n]]+address[\s-[\r\n]]+([abcdef\d]+)[\s-[\r\n]]*;");
        private static Regex mReadBTAddressPattern = new Regex(@"(?i)\s*read[\s-[\r\n]]+(bt)[\s-[\r\n]]+address[\s-[\r\n]]*;");
        private string[] mAssemblyQualifiePath4Executoer = new string[] { typeof(BTCommandInterpreter).Namespace };
        private IAccessiable mExecuter;

        //write bt address command table
        private static readonly byte[] mWriteBTAddressCmdCode= {0x4b,0xFA,0x06,0x00};
        //private static readonly string mWriteBTAddressCmdCodeString = "4bfa0600";
        private static readonly byte[] mReadBTAddressCmdCode = { 0x4b, 0xfa, 0x05, 0x00 };
        //private static readonly string mReadBTAddressCmdCodeString = "4bfa0500";
        private static Func<byte[],string> ByteArray2String= byteArray =>{
            StringBuilder sb = new StringBuilder();
            foreach (byte b in byteArray)
            {
                sb.Append(b.ToString());
            }
            return sb.ToString();
    };

        //write bt address request
        private static readonly object[,] mWriteBTAddressRequestFormat = {
                                                   {"CMD_AP",1,0xe1},
                                                   {"CMD_CODE",4,mWriteBTAddressCmdCode},
                                                   {"LENGTH",1,"the length of the code"},
                                                   {"MAC",6,"mac address"}
                                                   };

        //write bt address response
        private static readonly object[,] mWriteBTAddressResponseFormat = {
                                                          {"CMD_AP",1,0xe1},
                                                          {"CMD_CODE",4,mWriteBTAddressCmdCode},
                                                          {"ERROR_CODE",1,"0x00 for success, otherwise false"}
                                                          }; 

        //read bt address request
        private static readonly object[,] mReadBTAddressRequestFormat = {
                                                                      {"CMD_AP",1,0xe1},
                                                                      {"CMD_CODE",4,mReadBTAddressCmdCode}
                                                                  };

        //read bt address response
        private static readonly object[,] mReadBTAddressResponseFormat = {
                                                                       {"CMD_AP",1,0xe1},
                                                                       {"CMD_CODE",4,mReadBTAddressCmdCode},
                                                                       {"ERROR_CODE",1,"0x00 for success, otherwise false"},
                                                                       {"MAC_ADDRESS",6," mac address value"}
                                                                   };

        //public BTCommandInterpreter()
        //{
        //    Initialize();
        //}

        //protected virtual void Initialize()
        //{

        //}

        public void CommandEventHandler(object sender, CommandEventArgs e)
        {
            //binary code process
            byte[] decodedBinaryCode;
            if (e.CommandString.StartsWith(Convert.ToByte(mWriteBTAddressRequestFormat[0, 2]).ToString("x2"),StringComparison.InvariantCultureIgnoreCase))
            {
                decodedBinaryCode = ASCIIRepresentor.String2ByteArray(e.CommandString);
            }
            else
            {
                decodedBinaryCode = null;
            }

            if (decodedBinaryCode!=null &&　decodedBinaryCode[0] == (int)mWriteBTAddressRequestFormat[0, 2])
            {
                byte[] writeBTAddressCmdCode=(byte[])mWriteBTAddressRequestFormat[1,2];
                int writeBTAddressRequestFormatPrecursorLength=(int)mWriteBTAddressRequestFormat[0,1];
                bool isWriteBTAddressCmd=true;
                for (int i = 0; i < writeBTAddressCmdCode.Length; i++)
                {
                    if (decodedBinaryCode[writeBTAddressRequestFormatPrecursorLength + i] != writeBTAddressCmdCode[i])
                    {
                        isWriteBTAddressCmd = false;
                        break;
                    }
                }

                byte[] readBTAddressCmdCode = (byte[])mReadBTAddressRequestFormat[1, 2];
                int readBTAddressRequestFormatPrecursorLength = (int)mReadBTAddressRequestFormat[0, 1];
                bool isReadBTAddressCmd = true;
                for (int i = 0; i < readBTAddressCmdCode.Length; i++)
                {
                    if (decodedBinaryCode[readBTAddressRequestFormatPrecursorLength + i] != readBTAddressCmdCode[i])
                    {
                        isReadBTAddressCmd = false;
                        break;
                    }
                }

                if (!isWriteBTAddressCmd && !isReadBTAddressCmd)
                {
                    //no, I'm not interesting with the command
                    return;
                }
                if (isWriteBTAddressCmd)
                {
                    byte[] btAddress=new byte[(int)mWriteBTAddressRequestFormat[3,1]];
                    Array.Copy(decodedBinaryCode,
                        (int)mWriteBTAddressRequestFormat[0, 1] + (int)mWriteBTAddressRequestFormat[1, 1] + (int)mWriteBTAddressRequestFormat[2, 1],
                        btAddress, 0, (int)mWriteBTAddressRequestFormat[3, 1]);
                    string commandString = string.Format("write bt address {0};", ASCIIRepresentor.ASCIIByteArray2String(btAddress));
                    e.CommandString=commandString;
                    CommandEventHandler(sender, e);
                    var command = sender as Command;
                    if (command != null)
                    {
                        command.Result = new byte[(int)mWriteBTAddressResponseFormat[0, 1] + (int)mWriteBTAddressResponseFormat[1, 1] + (int)mWriteBTAddressResponseFormat[2, 1]];
                        command.Result[0] = Convert.ToByte(mWriteBTAddressResponseFormat[0, 2]);
                        Array.Copy((byte[])mWriteBTAddressResponseFormat[1, 2], 0, command.Result, (int)mWriteBTAddressResponseFormat[0, 1], (int)mWriteBTAddressResponseFormat[1,1]);
                        command.Result[(int)mWriteBTAddressResponseFormat[0, 1] + (int)mWriteBTAddressResponseFormat[1, 1]] = 0x00;
                    }
                }
                else if (isReadBTAddressCmd)
                {
                    e.CommandString = "read bt address;";
                    CommandEventHandler(sender, e);

                    //modify result
                    var command = sender as Command;
                    if (command != null)
                    {
                        int headLength=(int)mReadBTAddressResponseFormat[0, 1] + (int)mReadBTAddressResponseFormat[1, 1]+ (int)mReadBTAddressResponseFormat[2, 1];
                        var tempResult = command.Result;
                        command.Result = new byte[headLength  + (int)mReadBTAddressResponseFormat[3, 1]];
                        Array.Copy(tempResult, 0, command.Result, headLength, (int)mReadBTAddressResponseFormat[3, 1]);
                        command.Result[0] = Convert.ToByte(mReadBTAddressResponseFormat[0, 2]);
                        Array.Copy((byte[])mReadBTAddressResponseFormat[1, 2], 0, command.Result, (int)mReadBTAddressResponseFormat[0, 1], (int)mReadBTAddressResponseFormat[1,1]);
                        command.Result[(int)mReadBTAddressResponseFormat[0, 1] + (int)mReadBTAddressResponseFormat[1, 1]] = 0x00;
                    }
                }
            }
            //literals string process
            else
            {
                if (!mInterestedPattern.IsMatch(e.CommandString))
                {
                    //no, I'm not interesting with the command
                    return;
                }
                //pass command string to interpreter series
                bool isWriteBTCmd = mWriteBTAddressPattern.IsMatch(e.CommandString);
                bool isReadBTCmd = mReadBTAddressPattern.IsMatch(e.CommandString);

                if(isWriteBTCmd)
                {
                    Match match = mWriteBTAddressPattern.Match(e.CommandString);
                    if(mExecuter==null)
                    {
                        mExecuter = ResolveExecuter(match.Groups[1].Value);  
                    }
                    mExecuter.Write(match.Groups[2].Value);
                }
                else if (isReadBTCmd)
                {
                    Match match = mReadBTAddressPattern.Match(e.CommandString);
                    if (mExecuter == null)
                    {
                        mExecuter = ResolveExecuter(match.Groups[1].Value);
                    }
                    e.CommandResult=mExecuter.Read(null) as byte[];
                }
                //var context = new Context(e.CommandString);
                //var expression = new Expression(this);
                //expression.Parse(context);
                //expression.Interprete();

                ////result dump
                //StringBuilder sb =new StringBuilder();
                //foreach (var result in context.Result)
                //{
                //    sb.Append(result.ToString());
                //}

                //mResult = Encoding.Unicode.GetBytes(sb.ToString());
                //var commander=sender as Command;
                //if(commander!=null)
                //{
                //    commander.Result=mResult;
                //}
            }
        }

        public byte[] Result
        {
            get
            {
                return mResult;
            }
            set
            {
                mResult = value;
            }
        }


/*************helper function**********************************/
        private bool IsByteArrayEqual(byte[] byteArray1,byte[] byteArray2)
        {
            if(byteArray1==byteArray2) return true;
            if(byteArray1==null || byteArray2==null) return false;
            if (byteArray1.Length != byteArray2.Length) return false;
            for (int i = 0; i < byteArray1.Length; i++)
            {
                if (byteArray1[i] != byteArray2[i]) return false;
            }
            return true;
        }

        public IAccessiable ResolveExecuter(string typeName)
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
