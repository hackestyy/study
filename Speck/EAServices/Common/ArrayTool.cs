using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zte.Manufacture.Service.Common
{
    public class ArrayTool
    {
        public static bool CompareByteArray(byte[] a, byte[] b, int compareSize)
        {
            if (a.Length != b.Length)
                return false;
            if (a.Length < compareSize || b.Length < compareSize)
            {
                return false;
            }
            for (int i = 0; i < compareSize; i++)
            {
                if (a[i] != b[i])
                {
                    return false;
                }
            }
            return true;
        }
        public static byte[] JoinTwoByteArray(byte[] a, byte[] b)
        {
            byte[] result = new byte[a.Length + b.Length];
            Array.Copy(a, result, a.Length);
            Array.Copy(b, 0, result, a.Length, b.Length);
            return result;
        }
    }
}
