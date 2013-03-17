///////////////////////////////////////////////////////////
//  Task.cs
//  Implementation of the Class Task
//  Generated by Enterprise Architect
//  Created on:      20-十二月-2011 11:32:33
//  Original author: zhanghao
///////////////////////////////////////////////////////////


/*===========================================================================
                             Edit History
    when       who     what, where, why
    --------   ---     ----------------------------------------------------------
 *  
===========================================================================*/

using System;
using log4net;
using ZteApp.ProductService.Core;
using System.Collections.Generic;
using System.Threading;
using QuickGraph;
using QuickGraph.Algorithms;

namespace ZteApp.ProductService.Core {
	public class Task : Component,IDisposable
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // Track whether Dispose has been called.
        private bool mDisposed = false;
        //Represents a graph, where the keys are the vertices and the value is a collection of out-edges. 
        private Dictionary<Behavior, Behavior[]> mBehaviorDependencys;

        private AdjacencyGraph<Behavior, SEquatableEdge<Behavior>> mBehaviorDenpendencyGraph;
        private IEnumerable<Behavior> mSynchronizedExecutionEnumerable;
		private Priority mTaskPriority;
        private WorkStatus mTaskStatus;
        private ManualResetEvent mWorkSpinDoor;
        private Thread mTaskThread;
        private int mAsynchronousWorksCount = 0;
        private int mWorksCountInExecution = 0;
        //Indicates the finished ratio of the operation,100 indicates 100% of completion and 0 indicates no work has been done
        private int mProcessRate = 0;
        //private int mTotalBehaviors = 0;
        //private int mFinishedBehaviorsCount = 0;

        private bool mIsInitialized = false;

        public event BehaviorEventHandler BehaviorAdded;
        public event BehaviorEventHandler BehaviorRemoved;
        public event EventHandler StatusChanged;

        private ProgressChangedEventArgs mProgressChangedEventArgs;

        //Task guard woker
        private Thread mGuardThread;
        private Action mGuardAction;
        private ManualResetEvent mGuardSpinDoor;
        private bool mIsGuardWorkFinished;
        private const int mGuardWorkerSleepInterval = 500;  //in  millisecond
        private EventArgs mDummyEventArgs;

        public Task()
        {
            Initialize();
        }

        ~Task()
        {
            Dispose(false);
        }

        /// <summary>
        /// Indicates the finished ratio of the operation,100 indicates 100% of completion
        /// and 0 indicates no work has been done.
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

        public virtual WorkStatus Status
        {
            get
            {
                return mTaskStatus;
            }
            protected set
            {
                WorkStatus oldStatus = mTaskStatus;
                mTaskStatus = value;
                if (mTaskStatus != oldStatus)
                {
                    OnStatusChanged();
                }
            }
        }

        public Dictionary<Behavior, Behavior[]> BehaviorDependencys
        {
            get
            {
                return mBehaviorDependencys;
            }
        }

		public virtual void Abort(){
            mGuardAction = GuardWork4Abort;
            foreach (var pair in mBehaviorDependencys)
            {
                pair.Key.Abort();
            }
            mGuardSpinDoor.Set();
		}

        protected virtual void GuardWork4Abort()
        {
            //wait for all work to be done
            Thread.Sleep(mGuardWorkerSleepInterval);
            
            //Suppose all work has been aborted
            bool isAllWorkClosed = true;
            foreach (var pair in mBehaviorDependencys)
            {
                isAllWorkClosed = isAllWorkClosed && (pair.Key.Status == BehaviorStatus.Closed);
            }
            //then tell me if all work has been aborted
            if (isAllWorkClosed)
            {
                OnAborted();
                //tell guard to end its work
                mIsGuardWorkFinished = true;
            }
        }

        public event AbortedEventHandler Aborted;

        public event BeforeStartEventHandler BeforeStart;


        public event CloseEventHandler Closed;

		protected virtual void EnterPauseState(){

		}

		protected virtual void OnAborted(){
            if (Aborted != null)
            {
                Aborted(this,mDummyEventArgs);
            }
		}

        protected virtual void OnBehaviorAdded(Behavior addedBehavior)
        {
            if(BehaviorAdded!=null)
            {
                BehaviorAdded(this, new BehaviorEventArgs(addedBehavior));
            }
        }

        protected virtual void OnStatusChanged()
        {
            if (StatusChanged != null)
            {
                StatusChanged(this, mDummyEventArgs);
            }
        }

        protected virtual void OnBehaviorRemoved(Behavior removedBehavior)
        {
            if (BehaviorRemoved != null)
            {
                BehaviorRemoved(this,new BehaviorEventArgs(removedBehavior));
            }
        }

		protected virtual void OnBeforeStart(){
            if (BeforeStart != null)
            {
                BeforeStart(this,null);
            }
		}

		protected virtual void OnClosed(){
            Status = WorkStatus.Closed;
            if (Closed != null)
            {
                Closed(this, new EventArgs());
            }
		}

        protected virtual void OnPaused()
        {
            Status = WorkStatus.Pause;
            if (Paused != null)
            {
                Paused(this,new EventArgs());
            }

		}

		protected virtual void OnResumed(){
            Status = WorkStatus.Working;
            if (Resumed != null)
            {
                Resumed(this,new EventArgs());
            }
		}

		public virtual void Pause()
        {
            mGuardAction = GuardWork4Pause;
            foreach (var behaviorPair in mBehaviorDependencys)
            {
                behaviorPair.Key.Pause();
            }
            //activate guard worker
            mGuardSpinDoor.Set();
		}

        protected virtual void GuardWork4Pause()
        {
            //wait for all work to be done
            Thread.Sleep(mGuardWorkerSleepInterval);
            //Suppose work has been done
            bool isAllWorksDone=true;;
            foreach (var behaviorPair in mBehaviorDependencys)
            {
                isAllWorksDone = isAllWorksDone && (behaviorPair.Key.Status != BehaviorStatus.Working);
            }
            //then tell me if all works have been done
            if (isAllWorksDone)
            {
                OnPaused();
                mGuardSpinDoor.Reset();
            }
        }

        public event PausedEventHandler Paused;

        /// <summary>
        /// Resumes this task. If any behavior belong to it is resumed, we say the task was resumed
        /// </summary>
		public virtual void Resume(){
            mGuardAction = GuardWork4Resume;
            foreach(var behaviorPair in mBehaviorDependencys)
            {
                behaviorPair.Key.Resume();
            }

            //activate guard worker
            mGuardSpinDoor.Set();
		}

        protected void GuardWork4Resume()
        {
            Thread.Sleep(mGuardWorkerSleepInterval);
            //Suppose no work has been resumed
            bool isAnyWorkReusmed = false;
            foreach (var behaviorPair in mBehaviorDependencys)
            {
                isAnyWorkReusmed = isAnyWorkReusmed || (behaviorPair.Key.Status==BehaviorStatus.Working);
            }
            //then tell me if any work is reusmed
            if (isAnyWorkReusmed)
            {
                OnResumed();
                mGuardSpinDoor.Reset();
            }
        }

        public event ResumeEventHandler Resumed;
        
        public event ProgressChangedEventHandler ProgressChanged;

		private void SetAbortSignal(){

		}

		private void SetPauseSignal(){

		}

		private void SetResumeSignal(){

		}

		public virtual void Start(){
            log.Debug("Start");
            if (mTaskThread != null && mGuardThread!=null)
            {
                mTaskThread.Start();
                mGuardThread.Start();
            }
		}

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

        /// <summary>
        /// </summary>
        /// <param name="component"></param>
        public override void Add(Component component)
        {
            if(component is Behavior)
            {
                mBehaviorDependencys.Add((component as Behavior),(component as Behavior).DependendentBehaviors.ToArray());
                //mTotalBehaviors = mBehaviorDependencys.Count;
                OnBehaviorAdded(component as Behavior);
            }
        }

        protected override void DoWork()
        {
            throw new NotImplementedException();
        }

        public override void Initialize()
        {
            if (!mIsInitialized)
            {
                Status = WorkStatus.Inactive;
                mDisposed = false;
                mWorkSpinDoor = new ManualResetEvent(false);
                mDummyEventArgs = new EventArgs();
                mTaskThread = new Thread(Work);
                log.Debug("Initialize");
                mAsynchronousWorksCount = 0;
                mBehaviorDependencys = new Dictionary<Behavior, Behavior[]>();
                mWorksCountInExecution = 0;

                ProcessRate = 0;
                mProgressChangedEventArgs = new ProgressChangedEventArgs(mProcessRate);

                //Task guard woker
                mIsGuardWorkFinished = false;
                mGuardSpinDoor = new ManualResetEvent(false);
                mGuardThread = new Thread(() =>
                {
                    while (!mIsGuardWorkFinished)
                    {
                        mGuardSpinDoor.WaitOne();
                        if (mGuardAction != null)
                        {
                            mGuardAction();
                        }
                    }
                });
                mIsInitialized = true;
            }
        }

        public override void Remove(Component component)
        {
            if (component is Behavior)
            {
                mBehaviorDependencys.Remove(component as Behavior);
                //mTotalBehaviors = mBehaviorDependencys.Count;
                OnBehaviorRemoved(component as Behavior);
            }
        }

        public override void Work()
        {
            OnBeforeStart();
            Status = WorkStatus.Working;
            //get behavior from list
            foreach (var behaviorPair in mBehaviorDependencys)
            {
                switch (behaviorPair.Key.PlannedWorkPattern)
                {
                    case BehaviorExecutePattern.Asynchronization:
                        behaviorPair.Key.Close += OnBehaviorFinished;
                        behaviorPair.Key.StatusChanged += OnBehaviorStatusChanged;
                        mAsynchronousWorksCount++;
                        mWorksCountInExecution++;
                        behaviorPair.Key.Start(BehaviorExecutePattern.Asynchronization);
                        break;
                    case BehaviorExecutePattern.Synchronization:
                        behaviorPair.Key.Close += OnBehaviorFinished;
                        behaviorPair.Key.StatusChanged += OnBehaviorStatusChanged;
                        mWorksCountInExecution++;
                        break;
                }
            }
            SortExecutionList();

            //execute the synchronizd works
            if (mSynchronizedExecutionEnumerable != null)
            {
                foreach (var behavior in mSynchronizedExecutionEnumerable)
                {
                    behavior.Start();
                }
            }

            //block current thread until all work were done
            mWorkSpinDoor.WaitOne();
            //join all workerThread and resources recycling
            foreach (Behavior behavior in mBehaviorDependencys.Keys)
            {
                behavior.Dispose();
            }

            Status = WorkStatus.Closed;
            //End task
            OnClosed();
        }

        private void SortExecutionList()
        {
            //DAG topological sort the behavior dependency graph to get the work list
            if (mBehaviorDenpendencyGraph == null)
            {
                mBehaviorDenpendencyGraph = new AdjacencyGraph<Behavior, SEquatableEdge<Behavior>>();
                foreach (var kv in mBehaviorDependencys)
                {
                    if (kv.Key.PlannedWorkPattern == BehaviorExecutePattern.Asynchronization)
                    {
                        //ignore the indenpendent behavior
                        continue;
                    }
                    mBehaviorDenpendencyGraph.AddVertex(kv.Key);
                    foreach(var outEdgeVertex in kv.Value)
                    {
                        mBehaviorDenpendencyGraph.AddVertex(outEdgeVertex);
                        mBehaviorDenpendencyGraph.AddEdge(new SEquatableEdge<Behavior>(outEdgeVertex,kv.Key));
                        //mBehaviorDenpendencyGraph.AddEdge(new SEquatableEdge<Behavior>(kv.Key,outEdgeVertex));
                    }
                }
            }

            mSynchronizedExecutionEnumerable = mBehaviorDenpendencyGraph.TopologicalSort();
        }

        protected virtual void OnBehaviorFinished(object sender,EventArgs e)
        {
            Behavior behavior = sender as Behavior;
            if (behavior == null)
            {
                return;
            }
            behavior.Close -= OnBehaviorFinished;
            behavior.StatusChanged -= OnBehaviorStatusChanged;
            if (behavior.BehaviorExecutePattern == BehaviorExecutePattern.Asynchronization)
            {
                //decrease the asynchronous works count
                mAsynchronousWorksCount -= 1;
            }
            mWorksCountInExecution--;
            if (mWorksCountInExecution <= 0)
            {
                mIsGuardWorkFinished = true;
                mGuardAction = null;
                mGuardSpinDoor.Set();
                mWorkSpinDoor.Set();
            }
        }

        protected virtual void OnBehaviorStatusChanged(object sender,EventArgs e)
        {
            Behavior behavior = sender as Behavior;
            if (behavior == null)
            {
                return;
            }
            if (behavior.Status == BehaviorStatus.Working)
            {
                Status = WorkStatus.Working;
            }
            else if (behavior.Status == BehaviorStatus.Paused)
            {
                //suppose all behavior is paused
                bool isAllWorkPause = true;
                foreach (var pair in mBehaviorDependencys)
                {
                    isAllWorkPause=isAllWorkPause&&(pair.Key.Status!=BehaviorStatus.Working);
                }
                if (isAllWorkPause)
                {
                    OnPaused();
                }
            }
        }

		public Priority TaskPriority{
			get{
				return mTaskPriority;
			}
			set{
				mTaskPriority = value;
			}
		}

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!mDisposed)
            {
                if (disposing)
                {
                    //free managed resources
                }
                //free unmanaged resources
                if (mTaskStatus != WorkStatus.Closed)
                {
                    Abort();
                }

                //join the thread if it had\
                if (mGuardThread != null && mGuardThread.IsAlive)
                {
                    mIsGuardWorkFinished = true;
                    mGuardAction = null;
                    mGuardSpinDoor.Set();
                    mGuardThread.Join();
                }
                if (mTaskThread != null && mTaskThread.IsAlive)
                {
                    mTaskThread.Join();
                }
                mDisposed = true;
            }
        }
    }//end Task

    public delegate void BehaviorEventHandler(object sender,BehaviorEventArgs e);
}//end namespace Core