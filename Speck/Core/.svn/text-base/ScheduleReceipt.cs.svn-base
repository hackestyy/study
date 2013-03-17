using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZteApp.ProductService.Core
{
    internal class ScheduleReceipt<T>
    {
        private ScheduleStatus mStatus;
        private T mScheduledObject;
        private bool mIsReady = false;
        private bool mNeedRemoved=false;
        private bool mNeedCancel = false;

        private Action<ISchduler<T>> mSchduleAction;

        public ScheduleReceipt(T schduledObj,bool isReady)
        {
            mScheduledObject = schduledObj;
            mIsReady = isReady;
        }


        public Action<ISchduler<T>> ScheduleAction
        {
            get
            {
                return mSchduleAction;
            }
            set
            {
                mSchduleAction = value;
            }
        }

        public bool NeedRemoved
        {
            get
            {
                return mNeedRemoved;
            }
            set
            {
                mNeedRemoved = value;
            }
        }

        public bool IsReady
        {
            get {
                return mIsReady;
            }
            set
            {
                mIsReady = value;
            }
        }

        public bool NeedCancel
        {
            get
            {
                return mNeedCancel;
            }
            set
            {
                throw new Exception("Currently do not support remove task from execution queue!");
                //mNeedCancel = value;
            }
        }
        /// <summary>
        /// Internally used by scheduler
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public  ScheduleStatus Status
        {
            get
            {
                return mStatus;
            }
            set
            {
                mStatus = value;
            }
        }

        public T ScheduleObject
        {
            get
            {
                return mScheduledObject;
            }
            set
            {
                mScheduledObject = value;
            }
        }
    }
}
