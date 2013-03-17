using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ZteApp.ProductService.EAServices.BusinessWork
{
    struct SystemPowerStatus
    {

        /// BYTE->unsigned char
        public byte ACLineStatus;

        /// BYTE->unsigned char
        public byte BatteryFlag;

        /// BYTE->unsigned char
        public byte BatteryLifePercent;

        /// BYTE->unsigned char
        public byte Reserved1;

        /// DWORD->unsigned int
        public uint BatteryLifeTime;

        /// DWORD->unsigned int
        public uint BatteryFullLifeTime;
    }
    [ServiceContract]
    interface IGetBatteryStatus
    {
        [OperationContract]
        void InitialBatteryQery();
        [OperationContract]
        string GetBatteryIsPluginPower();
        [OperationContract]
        string GetBatteryTech();
        [OperationContract]
        string GetBatteryTemp();
        [OperationContract]
        string GetBatteryStatus();
        [OperationContract]
        string GetBatteryVoltage();
        [OperationContract]
        string GetBatteryConsumption();
        [OperationContract]
        string GetBatteryRange();
        [OperationContract]
        string GetBatteryHealth();
        [OperationContract]
        string GetBatteryLeftTime();
        [OperationContract]
        string GetSystemStatedTime();
    }
    
    public partial class Service : IGetBatteryStatus
    {
        private SystemPowerStatus status = new SystemPowerStatus();
        private ManagementObject mObj ;
        #region private method
        [DllImport("Kernel32.dll")]
        private static extern bool GetSystemPowerStatus(out SystemPowerStatus lpSystemPowerStatus);

        [DllImport("Kernel32.dll")]
        private static extern uint GetTickCount();

        private  bool GetBatteryStatus(out SystemPowerStatus status)
        {
            return GetSystemPowerStatus(out status);
        }
        private ManagementObject GetBatteryWMIObject()
        {
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(new ManagementScope("\\root\\CIMV2"), new SelectQuery("Win32_Battery")))
            {
                using (ManagementObjectCollection objectCollection = searcher.Get())
                {
                    foreach (ManagementObject mObj in objectCollection)
                    {
                        return mObj;
                    }
                }
            }
            return null;
        }
        #endregion
        #region public method
        public void InitialBatteryQery()
        {
            GetBatteryStatus(out status);
            mObj = GetBatteryWMIObject();
        }
        public string GetBatteryIsPluginPower()
        {
            if (status.ACLineStatus.ToString()=="1")
            {
                return "Yes";
            }
            else if (status.ACLineStatus.ToString()=="0")
            {
                return "No";
            }
            return "N/A";
        }
        public string GetBatteryTech()
        {
            int chemistryValue = Convert.ToInt16(mObj.GetPropertyValue("Chemistry"));
            switch (chemistryValue)
            {
                case 1:
                    return "Other";
                case 2:
                    return "Unknown";
                case 3:
                    return "Lead Acid";
                case 4:
                    return "Nickel Cadmium";
                case 5:
                    return "Nickel Metal Hydride";
                case 6:
                    return "Lithium-ion";
                case 7:
                    return "Zinc air";
                case 8:
                    return "Lithium Polymer";
                default:
                    break;
            }
            return "";
        }
        public string GetBatteryTemp()
        {
            return "";
        }
        public string GetBatteryStatus()
        {
            int value = Convert.ToInt32(status.BatteryFlag);
            if (value == 128)
            {
                return "No system battery";
            }
            else if (value == 255)
            {
                return "Unknown status—unable to read the battery flag information";
            }
            else if (value >= 8)
            {
                return "Charging";
            }
            else
            {
                return "Discharging";
            }
        }
        public string GetBatteryVoltage()
        {
            long batteryVoltage = Convert.ToInt64(mObj.GetPropertyValue("DesignVoltage"));
            return batteryVoltage.ToString() + "mV";
        }
        public string GetBatteryConsumption()
        {
            if (status.BatteryLifePercent.ToString() == "255")
            {
                return "N/A";
            }
            return status.BatteryLifePercent.ToString();
        }
        public string GetBatteryRange()
        {
            return "";
        }
        public string GetBatteryHealth()
        {
            return mObj.GetPropertyValue("Status").ToString();
        }
        public string GetBatteryLeftTime()
        {
            uint seconds = Convert.ToUInt32(status.BatteryLifeTime);
            if ((int)seconds == -1)
            {
                return "N/A";
            }

            return seconds / 3600 + ":" + seconds % 3600 / 60 + ":" + seconds % 3600 % 60;
        }
        public string GetSystemStatedTime()
        {
            uint milliseconds = GetTickCount();
            uint seconds = milliseconds/1000;
            return seconds / 3600 + ":" + seconds % 3600 / 60 + ":" + seconds % 3600 % 60;
        }
        #endregion
    }
}
