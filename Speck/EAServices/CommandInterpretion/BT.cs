using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZteApp.ProductService.EAServicesProvision;
using ZteApp.ProductService.EAServices.Helper;

namespace ZteApp.ProductService.EAServices.CommandInterpretion
{
    public class BT:IAccessiable
    {
        
        public void Write(object param)
        {
            var obj=param as string;
            if (obj == null)
            {
                throw new InvalidCastException("param is not type of string!");
            }

            //TODO:add write address here
            ZTEFactoryTool zTEFactoryTool = new ZTEFactoryTool();
            zTEFactoryTool.WriteBTAddress(obj);

        }

        public object Read(object param)
        {
            //TODO:add read code here
            //return new byte[]{1,2,3,4,5,6};
            ZTEFactoryTool zTEFactoryTool = new ZTEFactoryTool();
            return ASCIIRepresentor.String2ByteArray(zTEFactoryTool.GetBTAddress());
        }

        public object Check(object param)
        {
            Control controlParam = param as Control;
            //if parm is op type of control, then we do it
            if (controlParam != null)
            {
                if (controlParam.ControlType == Ability.QueryID)
                { 

                }
            }
            return null;
        }

        public class Control
        {
            private Ability mControlType;
            private byte[] mControlCode;

            public Ability ControlType
            {
                get
                {
                    return mControlType;
                }
                set
                {
                    mControlType = value;
                }
            }

            public byte[] ControlCode
            {
                get
                {
                    return mControlCode;
                }
                set
                {
                    mControlCode=value;
                }
            }

        }

        public enum Ability
        {
            QueryID,
            SetTxRadio,
            SetRxRadio,
            ReceivedInfo
        }
    }
}
