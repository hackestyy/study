using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
namespace WpfApplication1
{
    public class Win32
    {
        [DllImport("user32.dll", EntryPoint = "MessageBox")]

        public static extern int msgbox(int hwnd, string text, string caption, uint type);


    }
}
