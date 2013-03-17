using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZteApp.ProductService.Core
{
    class TaskExecuteFaculty
    {
    }

    public class TaskExecuteEventArgs : EventArgs
    {
        private Task mCurrentTask;
        public TaskExecuteEventArgs(Task currentTask)
        {
            mCurrentTask = currentTask;
        }

        public Task CurrentTask
        {
            get {
                return mCurrentTask;
            }
        }
    }
}
