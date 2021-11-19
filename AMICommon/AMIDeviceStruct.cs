using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AMICommon
{
    [DataContract]
    public class AMIDeviceStruct
    {
        [DataMember]
        public int AMIDeviceCode { get; set; }

        [DataMember]
        public long Timestamp { get; set; }

        [DataMember]
        public List<MeasureStruct> Measures { get; set; }

        public AMIDeviceStruct()
        {        
            this.Measures = new List<MeasureStruct>();
        }

        public AMIDeviceStruct(int AmiDeviceCode, long Timestamp)
        {
            if (Timestamp < 1622758694)
            {
                throw new ArgumentException("Vreme nije validno.");
            }

            this.AMIDeviceCode = AmiDeviceCode;
            this.Timestamp = Timestamp;
            this.Measures = new List<MeasureStruct>();
        }

    }
}
