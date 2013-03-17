using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Speck.EAServices.CommandInterpretion
{
    internal interface ITerm
    {
        void Parse(Context context);
        void Interprete();
    }
}
