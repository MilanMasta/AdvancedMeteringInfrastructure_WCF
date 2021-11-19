using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using AMICommon;

namespace AMIDevice
{
    
    [DataContract]
    public class TimeSimulatorKeeper
    {
        [DataMember]
        public int Interval { get; set; }

        public TimeSimulatorKeeper() 
        {
            Interval = 1;
        }

    }
}
