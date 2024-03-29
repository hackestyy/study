///////////////////////////////////////////////////////////
//  Behavior.cs
//  Implementation of the Class Behavior
//  Generated by Enterprise Architect
//  Created on:      20-十二月-2011 10:13:32
//  Original author: zhanghao
///////////////////////////////////////////////////////////


/*===========================================================================
                             Edit History
    when       who     what, where, why
    --------   ---     ----------------------------------------------------------
 *  
===========================================================================*/
using ZteApp.ProductService.Core;
using System;
using System.Threading;
using System.Collections.Generic;

namespace ZteApp.ProductService.Core {
	public class Behavior : Component, IDisposable {
        private readonly static object mSyncRoot = new object();
        private Thread mAsynchronousWorkThread;
        protected ManualResetEvent mManualEvent;
        protected BehaviorExecutePattern mBehaviorExecutePattern;
        private EventArgs mDummyEventArgs;
		private Dictionary<Component,BehaviorEntityType> mBehaviorEntities;
        private BehaviorStatus mStatus;
        protected bool mShouldPause = false;
        protected bool mShouldAbort = false;
        private BehaviorExecutePattern mPlannedWorkPattern;
        private ProgressChangedEventArgs mProgressChangedEventArgs;
        
        //Indicates the finished ratio of the operation,100 indicates 100% of completion and 0 indicates no work has been done
        private int mProcessRate;

        //make sure to set this flag true to enable the work done process
        protected bool mIsWorkFinished=false;

        // Track whether Dispose has been called.
        private bool mDisposed = false;
		private List<Behavior> mDependentBehaviors;

        public Behavior()
        {
            Initialize();
        }

        ~Behavior()
        {
            Dispose(false);
        }


        public virtual BehaviorStatus Status
        {
            get
            {
                return mStatus;
            }
            protected set
            {
                BehaviorStatus oldStatus = mStatus;
                mStatus = value;
                if (mStatus != oldStatus)
                {
                    OnStatusChanged();
                }
            }
        }

        public BehaviorExecutePattern BehaviorExecutePattern
        {
            get
            {
                return mBehaviorExecutePattern;
            }
        }

        /// <summary>
        /// Indicate the finished ratio of the operation,100 indicates 100% of completion and 0 indicates no work has been done
        /// </summary>
        public virtual int ProcessRate
        {
            get
            {
                return mProcessRate;
            }
            protected set
            {
                int odlProcessRate = mProcessRate;
                mProcessRate = value;
                if (odlProcessRate != mProcessRate)
                {
                    OnProgressChanged();
                }
            }
        }

        public BehaviorExecutePattern PlannedWorkPattern
        {
            get
            {
                return mPlannedWorkPattern;
            }
            set
            {
                mPlannedWorkPattern = value;
            }
        }

        public bool IsWorkFinished
        {
            get
            {
                return mIsWorkFinished;
            }
        }

		public virtual void Abort()
        {
            mShouldAbort = true;
            //Status = BehaviorStatus.Closed;
            mManualEvent.Set();
		}

		protected virtual void ExecuteAsynchronously()
        {
            if (mAsynchronousWorkThread == null)
            {
                lock (mSyncRoot)
                {
                    if (mAsynchronousWorkThread == null)
                    {
                        mAsynchronousWorkThread = new Thread(Work);
                    }
                }
            }
            mBehaviorExecutePattern = BehaviorExecutePattern.Asynchronization;
            mAsynchronousWorkThread.Start();
		}

		protected virtual void ExecuteSynchronously()
        {
            mBehaviorExecutePattern = BehaviorExecutePattern.Synchronization;
            Work();
		}

		protected virtual void OnAborted(){
            if (Aborted != null)
            {
                Aborted(this, mDummyEventArgs);
            }
		}

		protected virtual void OnBeforeStart()
        {
            if (BeforeStart != null)
            {
                BeforeStart(this, mDummyEventArgs);
            }
		}

		protected virtual void OnClose(){
            Status = BehaviorStatus.Closed;
            if(Close!=null)
            {
                Close(this, mDummyEventArgs);
            }
		}

		/// <summary>
		/// Rewrite to provide the behavior of customization
		/// </summary>
		protected virtual void OnPaused(){
            Status = BehaviorStatus.Paused;
            if (Paused != null)
            {
                Paused(this, mDummyEventArgs);
            }
		}

		protected virtual void OnResumed(){
            Status = BehaviorStatus.Working;
            if (Resumed != null)
            {
                Resumed(this,mDummyEventArgs);
            }
		}

		public virtual void Pause(){
            mManualEvent.Reset();
            mShouldPause = true;
		}

		public virtual void Resume()
        {
            mShouldPause = false;
            mManualEvent.Set();
		}


        /// <summary>
        /// Starts the behavior with the specified synchronization pattern.
        /// </summary>
        /// <param name="synchronizationPattern">The synchronization pattern.</param>
		public virtual void Start(BehaviorExecutePattern synchronizationPattern)
        {
            if (mIsWorkFinished)
                return;
            switch (synchronizationPattern)
            {
                case BehaviorExecutePattern.Synchronization:
                    ExecuteSynchronously();
                    break;
                case BehaviorExecutePattern.Asynchronization:
                    ExecuteAsynchronously();
                    break;
            }
		}

        /// <summary>
        /// Starts  work synchronously.
        /// </summary>
        public virtual void Start()
        {
            Start(BehaviorExecutePattern.Synchronization);
        }

        public event BeforeStartEventHandler BeforeStart;

        public event PausedEventHandler Paused;

        public event ResumeEventHandler Resumed;

        public event AbortedEventHandler Aborted;

        public event CloseEventHandler Close;

        public event EntityEventHandler EntityAdded;

        public event EntityEventHandler EntityRemoved;

        public event ProgressChangedEventHandler ProgressChanged;

        public event EventHandler StatusChanged;


        /// <summary>
        /// Called when progress changed. If you want to display the progress, you should change the ProcessRate member
        /// correspondly
        /// </summary>
        protected virtual void OnProgressChanged()
        {
            if (ProgressChanged != null)
            {
                mProgressChangedEventArgs.ProgressPercentage = mProcessRate;
                ProgressChanged(this, mProgressChangedEventArgs);
            }
        }

        protected virtual void OnStatusChanged()
        {
            if (StatusChanged != null)
            {
                StatusChanged(this,mDummyEventArgs);
            }
        }


        public override void Add(Component component)
        {
            InsertEntity(component);
        }

        /// <summary>
        /// Does the work. Need to be override to provide the context meaning functionality
        /// if you want the work to get done, finally you should set the mIsWorkFinished = true here OR
        ///do your logic but not forget to set the flag or you will be hang here
        /// </summary>
        protected override void DoWork()
        {
            

            //if you want the work to get done, finally you should set the mIsWorkFinished = true here OR
            //do your logic but not forget to set the flag or you will be hang here
            mIsWorkFinished = true;
        }

        public override void Initialize()
        {
            mDummyEventArgs = new EventArgs();
            mPlannedWorkPattern = BehaviorExecutePattern.Synchronization;
            Status = BehaviorStatus.Closed;
            mDependentBehaviors = new List<Behavior>();
            //initialize the event state to allow thread to continue
            mManualEvent = new ManualResetEvent(true);
            mBehaviorEntities = new Dictionary<Component, BehaviorEntityType>();
            mShouldPause = false;
            mShouldAbort = false;
            mIsWorkFinished=false;
            mDisposed = false;
            ProcessRate = 0;
            mProgressChangedEventArgs = new ProgressChangedEventArgs(mProcessRate);
        }

        public override void Remove(Component component)
        {
            RemoveEntity(component);
        }

        public override void Work()
        {
            OnBeforeStart();
            while (!mIsWorkFinished)
            {
                if (mShouldPause)
                {
                    OnPaused();
                    mManualEvent.WaitOne();
                    if (mStatus == BehaviorStatus.Paused)
                    {
                        //is resume from the pause state
                        OnResumed();
                    }
                    mShouldPause = false;
                }
                if (mShouldAbort)
                {
                    //do other abort activity here

                    OnAborted();
                    mShouldAbort = false;
                    mIsWorkFinished = true;
                    continue;
                }
                Status = BehaviorStatus.Working;
                DoWork();
            }
            OnClose();
        }

		public Dictionary<Component,BehaviorEntityType> BehaviorEntities{
			get{
				return mBehaviorEntities;
			}
		}

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            //weather need avoid thread self join here
            if (!this.mDisposed)
            {
                if (disposing)
                {
                    // Free other state (managed objects).

                }
                // Free your own state (unmanaged objects).

                
                if (mStatus != BehaviorStatus.Closed)
                {
                    this.Abort();
                }

                ////close the work thread if it had
                if (mAsynchronousWorkThread != null)
                {
                    mAsynchronousWorkThread.Join();
                }

                
                // Note disposing has been done.
                mDisposed = true;

            }
        }

        public virtual void InsertEntity(Component entity,BehaviorEntityType entityType)
        {
            mBehaviorEntities.Add(entity, entityType);
            OnEntityAdded(entity);
        }

        public virtual void InsertEntity(Component entity)
        {
            InsertEntity(entity,BehaviorEntityType.Object);
        }

        public virtual void RemoveEntity(Component entity)
        {
            mBehaviorEntities.Remove(entity);
            OnEntityRemoved(entity);
        }

        protected virtual void OnEntityAdded(Component addedEntity)
        {
            if (EntityAdded != null)
            {
                EntityEventArgs e=new EntityEventArgs(addedEntity);
                EntityAdded(this, e);
            }
        }

        protected virtual void OnEntityRemoved(Component entity)
        {
            if (EntityRemoved != null)
            {
                EntityEventArgs e = new EntityEventArgs(entity);
                EntityRemoved(this,e);
            }
        }


		public List<Behavior> DependendentBehaviors{
			get{
				return mDependentBehaviors;
			}
			set{
				mDependentBehaviors = value;
			}
		}

    }//end Behavior

    

    public delegate void BeforeStartEventHandler(object sender,EventArgs e);
    public delegate void PausedEventHandler(object sender,EventArgs e);
    public delegate void ResumeEventHandler(object sender,EventArgs e);
    public delegate void AbortedEventHandler(object sender,EventArgs e);
    public delegate void CloseEventHandler(object sender,EventArgs e);
    public delegate void EntityEventHandler(object sender,EntityEventArgs e);
    public enum BehaviorStatus
    { 
        Working,
        Paused,
        Closed
    }//end BehaviorStatuss
}//end namespace Core