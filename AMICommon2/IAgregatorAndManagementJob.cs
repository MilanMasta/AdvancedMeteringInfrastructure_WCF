using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AMICommon2
{
    [ServiceContract]
    public interface IAgregatorAndManagementJob
    {

        [OperationContract]
        bool Posalji(AMIAgregatorStruct struktura);

    }
}
