using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AMICommon
{
    [ServiceContract]
    public interface ITimeInterval
    {
        [OperationContract]
        int ReadTimeInterval(string filename);

    }
}
