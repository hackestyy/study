using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZteApp.ProductService.EAServices.CommunicationTask
{
    public class CommandEventArgs:EventArgs
    {
        private string mCommandString;
        private byte[] mCommandResult;

        public CommandEventArgs(string commandString)
        {
            mCommandString = commandString;
        }

        public string CommandString
        {
            get
            {
                return mCommandString;
            }
            set
            {
                mCommandString = value;
            }
        }

        public byte[] CommandResult
        {
            set
            {
                mCommandResult = value;
            }
            get
            {
                return mCommandResult;
            }
        }
    }
}
