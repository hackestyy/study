using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZteApp.ProductService.EAServicesProvision
{
    internal class ArrayWrapper<T>
    {
        private int mArrayLength=0;
        T[] mArray;
        public ArrayWrapper(int arrayLength)
        {
            mArrayLength = arrayLength;
            mArray = new T[mArrayLength];
        }

        public T this[int index]
        {
            get
            {
                return mArray[index];
            }
            set
            {
                mArray[index] = value;
            }
        }

        public T[] Array
        {
            get
            {
                return mArray;
            }
        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (T element in mArray)
            {
                sb.Append(element.ToString());
            }
            return sb.ToString();
        }
    }
}
