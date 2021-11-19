using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using AMICommon;

namespace AMIDevice
{
    //KLASA ZA CUVANJE VRIJEDNOSTI VREMENSKOG INTERVALA PROCITANOG IZ XML FAJLA
    [DataContract]
    public class TimeIntervalKeeper
    {
        [DataMember]
        public int Interval { get; set; }

        public TimeIntervalKeeper() 
        {
            Interval = 1;
            //default vrijednost
        }
     

    }
}
