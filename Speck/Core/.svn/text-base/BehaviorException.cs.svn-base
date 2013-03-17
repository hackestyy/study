using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZteApp.ProductService.Core
{
    public class BehaviorException:Exception
    {
        private Behavior mBehavior;

        public BehaviorException(Behavior behavior)
        {
            mBehavior = behavior;
        }

        public BehaviorException(Behavior behavior,string message):base(message)
        {
            mBehavior = behavior;
        }

        public Behavior Behavior
        {
            get
            {
                return mBehavior;
            }
        }
    }
}
