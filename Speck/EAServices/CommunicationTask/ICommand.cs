using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace ZteApp.ProductService.EAServices.CommunicationTask
{
    [ServiceContract(SessionMode=SessionMode.Required)]
    public interface ICommand
    {
        [OperationContract]
        void Send(string command);

        [OperationContract]
        byte[] Receive();

        byte[] Result
        {
            get;
            set;
        }
    }
}
