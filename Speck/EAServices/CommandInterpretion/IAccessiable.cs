using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZteApp.ProductService.EAServices.CommandInterpretion
{
    public interface IAccessiable
    {
        void Write(object param);
        object Read(object param);
        object Check(object param);

    }
}
