using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zte.Manufacture.Service.BusinessWork;

namespace Zte.Manufacture.Service.Common
{
    interface IBusinessOperator
    {
        String Name { get; }
        byte[] CmdHead { get; }
        byte[] Do(byte[] cmd,int size);
    }

    public class SocketCmdOperator
    {
        private List<IBusinessOperator> _cmds = new List<IBusinessOperator>();
        public SocketCmdOperator()
        {
            _cmds.Add(new BTWriteMACOperator());
            _cmds.Add(new BTReadMACOperator());
            _cmds.Add(new WiFiWriteMACOperator());
            _cmds.Add(new WiFiReadMACOperator());
            _cmds.Add(new ReadAPBoardOperator());
            _cmds.Add(new WriteAPBoardOperator());
            _cmds.Add(new EnterPowerSavingModeOperator());
            _cmds.Add(new BTQueryICNumberOperator());
            _cmds.Add(new BTRFTXControlOperator());
            _cmds.Add(new BTRFRXControlOperator());
            _cmds.Add(new BTGetRevDataOperator());
            _cmds.Add(new BTOpenOperator());
            _cmds.Add(new BTCloseOperator());
            _cmds.Add(new GetCPEVersionOperator());
            _cmds.Add(new RestoreDeviceOperator());
            _cmds.Add(new GPSEnterOperator());
            _cmds.Add(new GPSLeaveOperator());
            _cmds.Add(new GPSGetResultOperator());
            _cmds.Add(new GetBTICIDOperator());
            _cmds.Add(new GetGPSICIDOperator());
            _cmds.Add(new GetWiFiICIDOperator());
            _cmds.Add(new GetAcceleratorICIDOperator());
            _cmds.Add(new GetCompassICIDOperator());
            _cmds.Add(new GetGyroscopeICIDOperator());
            _cmds.Add(new SetNetPriorityOperator());
            _cmds.Add(new ReadIMSIOperator());
            _cmds.Add(new ReadModemBoardNumOperator());
            _cmds.Add(new WriteModemBoardNumOperator());
            _cmds.Add(new ReadTestMarkBitOperator());
            _cmds.Add(new WriteTestMarkBitOperator());
            _cmds.Add(new SetDeviceStateOperator());
            _cmds.Add(new SetChannelSwichOperator());
            _cmds.Add(new ReadModemVersionOperator());
            _cmds.Add(new WiFiEnterLeaveOperator());
            _cmds.Add(new WiFiTXTestOperator());
            _cmds.Add(new WiFiRXTestOperator());
        }

        public byte[] Do(byte[] receiveBytes, int receiveCount)
        {
            IBusinessOperator businessOperator = GetCmd(receiveBytes, receiveCount);
            return businessOperator.Do(receiveBytes, receiveCount);           
        }

        private IBusinessOperator GetCmd(byte[] receiveBytes, int receiveCount)
        {
            foreach (var cmd in _cmds)
            {
                if (ArrayTool.CompareByteArray(receiveBytes, cmd.CmdHead, cmd.CmdHead.Length))
                    return cmd;
            }
            throw new InvalidOperationException("Command not support!");
        }
    }
}
