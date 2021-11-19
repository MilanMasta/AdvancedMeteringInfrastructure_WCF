using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AMIAgregator
{
    [DataContract]
    //KLASA ZA CUVANJE VRIJEDNOSTI VREMENSKOG INTERVALA PROCITANOG IZ XML FAJLA
    public class TimeIntervalKeeper
    {
        [DataMember]
        public int Interval { get; set; }

        public TimeIntervalKeeper() 
        {
            Interval = 1;
        }

    }
}
