using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZteApp.ProductService.Core
{
    public class TaskEventArgs:EventArgs
    {
        private Task mTask;

        public TaskEventArgs(Task task)
        {
            mTask = task;
        }

        public Task Task
        {
            get 
            {
                return mTask;
            }

            set 
            {
                mTask = value;
            }
        }
    }

    public delegate void TaskEventHandler(object sender,TaskEventArgs e);
}
