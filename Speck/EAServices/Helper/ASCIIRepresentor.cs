using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZteApp.ProductService.EAServices.Helper
{
    internal class ASCIIRepresentor
    {
        public static string ASCIIByteArray2String(byte[] byteArray)
        {
            return ASCIIByteArray2String(byteArray,0,byteArray.Length);
        }

        public static string ASCIIByteArray2String(byte[] byteArray, int index, int count)
        {
            StringBuilder sb = new StringBuilder();
            int endIndex=index + count;
            for (int i = index; i < endIndex; i++)
            {
                sb.Append(byteArray[i].ToString("x2"));
            }
            return sb.ToString();
        }

        public static byte[] String2ByteArray(string str)
        {
            if (str.Length % 2 != 0)
            {
                throw new Exception("Invalid ascii representation!");
            }

            int byteArrayLength = str.Length / 2;
            byte[] byteArray = new byte[byteArrayLength];
            for (int i = 0; i < byteArrayLength; i++)
            {
                byteArray[i] = byte.Parse(str.Substring(2*i,2),System.Globalization.NumberStyles.HexNumber);
            }
            return byteArray;
        }
    }
}
