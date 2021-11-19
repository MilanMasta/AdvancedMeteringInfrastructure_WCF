using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using AMICommon;

namespace AMIDevice
{
    public class TimeIntervalWorker : ITimeInterval
    {

        //METODA ZA CITANJE VREMENSKOG INTERVALA ZA SLANJE PODATAKA AGREGATORU (citanje iz TimeInterval XML fajla)
        public int ReadTimeInterval(string filename)
        {
            try
            {
                TimeIntervalKeeper time = new TimeIntervalKeeper();
                XmlSerializer serializer = new XmlSerializer(typeof(TimeIntervalKeeper));
                using (TextReader textReader = new StreamReader(filename))
                {
                    // ispis u XML datoteku
                    time = (TimeIntervalKeeper)serializer.Deserialize(textReader);
                }
                return time.Interval;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 1;
            }

        }

    }
}
