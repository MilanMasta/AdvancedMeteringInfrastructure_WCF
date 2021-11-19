using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AMICommon
{
    [DataContract]
    public class MeasureStruct
    {
        [DataMember]
        public MeasurementType Type { get; set; }

        [DataMember]
        public double Value { get; set; }

        public MeasureStruct(MeasurementType type, double value)
        {
            if (type == MeasurementType.Napon && (value < 110 || value > 380))
            {
                throw new ArgumentException("Napon nije u dozvoljenim granicama.");
            }

            if (type == MeasurementType.Struja && (value < 5 || value > 25))
            {
                throw new ArgumentException("Jacina struje nije u dozvoljenim granicama.");
            }

            if (type == MeasurementType.AktivnaSnaga && (value < 5 || value > 30))
            {
                throw new ArgumentException("Aktivna snaga nije u dozvoljenim granicama.");
            }

            if (type == MeasurementType.ReaktivnaSnaga && (value < 0 || value > 2))
            {
                throw new ArgumentException("Reaktivna snaga nije u dozvoljenim granicama.");
            }

            this.Type = type;
            this.Value = value;
        }

        public MeasureStruct() 
        {
            this.Type = MeasurementType.Napon;
            this.Value = 220;
        }

    }
}
