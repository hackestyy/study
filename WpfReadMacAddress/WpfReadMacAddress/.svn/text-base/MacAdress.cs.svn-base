using System;
using System.Collections.Generic;
using Microsoft.Win32; 
using System.Runtime.InteropServices;

namespace MacAddress.Utils
{
    public interface INetCard
    {
        string GetLocalMac();
    }
    public class NetCard : INetCard
    {
        public NetCard()
        { }
        public string GetLocalMac()
        {
            List<string> netCardList = GetNetCardList();
            List<string>.Enumerator enumNetCard = netCardList.GetEnumerator();

            string macAddr = string.Empty;
            while (enumNetCard.MoveNext())
            {
                macAddr = GetPhysicalAddr(enumNetCard.Current);
                if (macAddr != string.Empty)
                {
                    break;
                }
            }
            return macAddr;
        }

        private List<string> GetNetCardList()
        {
            List<string> cardList = new List<string>();
            try
            {
                RegistryKey regNetCards = Registry.LocalMachine.OpenSubKey(Win32Utils.REG_NET_CARDS_KEY);
                if (regNetCards != null)
                {
                    string[] names = regNetCards.GetSubKeyNames();
                    RegistryKey subKey = null;
                    foreach (string name in names)
                    {
                        subKey = regNetCards.OpenSubKey(name);
                        if (subKey != null)
                        {
                            object o = subKey.GetValue("ServiceName");
                            if (o != null)
                            {
                                cardList.Add(o.ToString());
                            }
                        }
                    }
                }
            }
            catch { }

            return cardList;
        }

        private string GetPhysicalAddr(string cardId)
        {
            string macAddress = string.Empty;
            uint device = 0;
            try
            {
                string driveName = "\\\\.\\" + cardId;
                device = Win32Utils.CreateFile(driveName,
                                         Win32Utils.GENERIC_READ | Win32Utils.GENERIC_WRITE,
                                         Win32Utils.FILE_SHARE_READ | Win32Utils.FILE_SHARE_WRITE,
                                         0, Win32Utils.OPEN_EXISTING, 0, 0);
                if (device != Win32Utils.INVALID_HANDLE_VALUE)
                {
                    byte[] outBuff = new byte[6];
                    uint bytRv = 0;
                    int intBuff = Win32Utils.PERMANENT_ADDRESS;

                    if (0 != Win32Utils.DeviceIoControl(device, Win32Utils.IOCTL_NDIS_QUERY_GLOBAL_STATS,
                                        ref intBuff, 4, outBuff, 6, ref bytRv, 0))
                    {
                        string temp = string.Empty;
                        foreach (byte b in outBuff)
                        {
                            temp = Convert.ToString(b, 16).PadLeft(2, '0');
                            macAddress += temp;
                            temp = string.Empty;
                        }
                    }
                }
            }
            finally
            {
                if (device != 0)
                {
                    Win32Utils.CloseHandle(device);
                }
            }

            return macAddress;
        }
    }

    #region Win32Utils
    public class Win32Utils
    {
        public const string REG_NET_CARDS_KEY = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\NetworkCards";
        public const uint GENERIC_READ = 0x80000000;
        public const uint GENERIC_WRITE = 0x40000000;
        public const uint FILE_SHARE_READ = 0x00000001;
        public const uint FILE_SHARE_WRITE = 0x00000002;
        public const uint OPEN_EXISTING = 3;
        public const uint INVALID_HANDLE_VALUE = 0xffffffff;
        public const uint IOCTL_NDIS_QUERY_GLOBAL_STATS = 0x00170002;
        public const int PERMANENT_ADDRESS = 0x01010101;

        [DllImport("kernel32.dll")]
        public static extern int CloseHandle(uint hObject);

        [DllImport("kernel32.dll")]
        public static extern int DeviceIoControl(uint hDevice,
                                                 uint dwIoControlCode,
                                                 ref int lpInBuffer,
                                                 int nInBufferSize,
                                                 byte[] lpOutBuffer,
                                                 int nOutBufferSize,
                                                 ref uint lpbytesReturned,
                                                 int lpOverlapped);

        [DllImport("kernel32.dll")]
        public static extern uint CreateFile(string lpFileName,
                                             uint dwDesiredAccess,
                                             uint dwShareMode,
                                             int lpSecurityAttributes,
                                             uint dwCreationDisposition,
                                             uint dwFlagsAndAttributes,
                                             int hTemplateFile);

    }
    #endregion
}
