using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace ZteApp.ProductService.EAServices.CommunicationTask
{
    [ServiceContract(SessionMode=SessionMode.Required)]
    public interface INetworkInterfaceInfo
    {
        double CurrentSentRate
        {
            [OperationContract]
            get;
        }

        double CurrentReceivedRate
        {
            [OperationContract]
            get;
        }

        double BandWidth
        {
            [OperationContract]
            get;
        }

        double CurrentUtilization
        {
            [OperationContract]
            get;
        }
    }
}
