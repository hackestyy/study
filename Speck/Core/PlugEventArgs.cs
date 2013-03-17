using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZteApp.ProductService.Core
{
    public class PlugEventArgs : EventArgs
    {
        private object mPluggedObject;

        public object PluggedObject
        {
            get { return mPluggedObject; }
            set { mPluggedObject = value; }
        }
    }
}
