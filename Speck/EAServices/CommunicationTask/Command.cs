using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace ZteApp.ProductService.EAServices.CommunicationTask
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class Command:ICommand
    {
        public event CommandEventHandler CommandReceived;
        //private const int mResultCacheSize=500;
        //public static byte[] mResultCache = new byte[mResultCacheSize]; 
        public byte[] mResultCache;
        private CommandEventArgs mCommandEventArgs = new CommandEventArgs(null);

        public Command()
        {
            //this.CommandReceived += new BTCommandInterpretion.BTCommandInterpreter().CommandEventHandler;
        }
        public void Send(string command)
        {
            //Do nothing but notify other faculties
            OnCommandReceived(command);
            if(mCommandEventArgs!=null)
            {
                mResultCache = mCommandEventArgs.CommandResult;
            }
        }

        public byte[] Receive()
        {
            return mResultCache;
        }

        protected virtual void OnCommandReceived(string commandString)  
        {
            if (CommandReceived != null)
            {
                mCommandEventArgs.CommandString=commandString;
                CommandReceived(this,mCommandEventArgs);
            }
        }

        public byte[] Result
        {
            get
            {
                return mResultCache;
            }
            set
            {
                //Array.Clear(mResultCache, 0, mResultCacheSize);
                //Array.Copy(value, mResultCache, value.Length);
                mResultCache = value;
            }
        }
    }

    public delegate void CommandEventHandler(object sender,CommandEventArgs e);
}
