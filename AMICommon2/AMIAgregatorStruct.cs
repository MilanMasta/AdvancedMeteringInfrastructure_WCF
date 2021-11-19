using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using AMICommon;

namespace AMICommon2
{
    [DataContract]
    public class AMIAgregatorStruct
    {

        [DataMember]
        public int AMIAgregatorCode { get; set; }

        [DataMember]
        public long Timestamp { get; set; }

        [DataMember]
        public List<AMIDeviceStruct> Structures { get; set; }

        public AMIAgregatorStruct()
        {
            this.Structures = new List<AMIDeviceStruct>();
        }

        public AMIAgregatorStruct(int AmiAgregatorCode, long Timestamp)
        {
            if (Timestamp < 1622758694)
            {
                throw new ArgumentException("Vreme nije validno.");
            }

            this.AMIAgregatorCode = AmiAgregatorCode;
            this.Timestamp = Timestamp;
            this.Structures = new List<AMIDeviceStruct>();
        }



    }
}
