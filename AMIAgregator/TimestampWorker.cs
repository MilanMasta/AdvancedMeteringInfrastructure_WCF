using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMIAgregator
{
    public class TimestampWorker
    {
        //PRETVARANJE DATUMA I VREMENA IZ UNIX TIMESTAMP FORMATA U DATETIME
        public DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            if(unixTimeStamp < 0)
            {
                throw new ArgumentException("Parametar unixTimeStamp ne moze biti negativan broj.");
            }

            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

    }
}
