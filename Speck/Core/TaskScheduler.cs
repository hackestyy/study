using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZteApp.ProductService.Core
{
    internal class TaskScheduler:Component,ISchduler<Component>
    {
        private Queue<ScheduleReceipt<Task>> mExecutionQueue;
        private List<ScheduleReceipt<Task>> mRepository;

        public event TaskEventHandler TaskCancelled;

        public event TaskEventHandler TaskQueued;

        public event TaskEventHandler TaskDispatched;

        public event TaskEventHandler TaskRemoved;

        public TaskScheduler()
        {
            Initialize();
        }

        /// <summary>
        /// Gets the count of the  execution task in the queue.
        /// </summary>
        public int Count
        {
            get
            {
                return mExecutionQueue.Count;
            }
        }

        public Task this[int i]
        {
            get
            {
                return mExecutionQueue.ElementAt(i).ScheduleObject;
            }
            set
            {
                mExecutionQueue.ElementAt(i).ScheduleObject = value;
            }
        }

        protected virtual void OnTaskCancelled(Task cancelledTask)
        {
            if (TaskCancelled != null)
            {
                TaskCancelled(this, new TaskEventArgs(cancelledTask));
            }
        }

        protected virtual void OnTaskQueued(Task QueuedTask)
        {
            if (TaskQueued != null)
            {
                TaskQueued(this,new TaskEventArgs(QueuedTask));
            }
        }

        protected virtual void OnTaskDispatched(Task dispatchedTask)
        {
            if (TaskDispatched != null)
            {
                TaskDispatched(this, new TaskEventArgs(dispatchedTask));
            }
        }


        protected virtual void OnTaskRemoved(Task removedTask)
        {
            if (TaskRemoved != null)
            {
                TaskRemoved(this, new TaskEventArgs(removedTask));
            }
        }

        public override void Add(Component component)
        {
            Task task = component as Task;
            if (task == null)
            {
                return;
            }
            mRepository.Add(new ScheduleReceipt<Task>(task,true));
        }

        protected override void DoWork()
        {
            throw new NotImplementedException();
        }

        public override void Initialize()
        {
            mExecutionQueue = new Queue<ScheduleReceipt<Task>>();
            mRepository = new List<ScheduleReceipt<Task>>();
        }

        public override void Remove(Component component)
        {
            var receipt = mRepository.Find((ScheduleReceipt<Task> rpt) =>{
                if (rpt.ScheduleObject == component)
                    return true;
                else
                    return false;
            });
            if (receipt != null)
            {
                receipt.NeedRemoved = true;
            }
        }

        public void Clear()
        {
            mRepository.Clear();
            mExecutionQueue.Clear();
        }

        public override void Work()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Used by task execute engine, do not use this.
        /// </summary>
        /// <returns></returns>
        public Component Get()
        {
            var receipt=mExecutionQueue.Dequeue();
            OnTaskDispatched(receipt.ScheduleObject);
            return receipt.ScheduleObject;
        }


        public bool Cancel(Component scheduledObj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This is a simple schedule method, need more realistic implementation
        /// </summary>
        public void Schdule()
        {
            //simple schedule operation
            //ScheduleReceipt<Task> scheduleReceipt;
            ScheduleReceipt<Task> receipt;
            for (int i = 0; i < mRepository.Count;i++)
            {
                receipt = mRepository[i];
                //if ready ,queue tasks from repository
                if (receipt.IsReady)
                {
                    mRepository.Remove(receipt);
                    receipt.Status = ScheduleStatus.Queued;
                    mExecutionQueue.Enqueue(receipt);
                    OnTaskQueued(receipt.ScheduleObject);
                }
                //remove task if required
                if (receipt.NeedRemoved)
                {
                    mRepository.Remove(receipt);
                    OnTaskRemoved(receipt.ScheduleObject);
                }
            }

            ////Cancel any receipt if if need been cancelled
            //foreach (var receipt in mExecutionQueue)
            //{ 
      
            //}
        }
    }

    internal enum ScheduleStatus
    {
        Cancelled,
        Queued,
        NotReady,
        Ready,
        Dispatched
    }
}
