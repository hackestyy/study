using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZteApp.ProductService.Core
{
    public class ProgressChangedEventArgs:EventArgs
    {
        private int mPercentage;


        public ProgressChangedEventArgs(int percentage)
        {
            mPercentage = percentage;
        }

        public int ProgressPercentage
        {
            get { return mPercentage; }
            set
            {
                mPercentage = value;
            }
        }

    }

    public delegate void ProgressChangedEventHandler(object sender,ProgressChangedEventArgs e);
}
