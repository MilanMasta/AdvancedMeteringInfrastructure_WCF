using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AMIDevice
{
    public class TimeSimulatorWorker
    {

        public int ReadTimeInterval(string filepath)
        {
            try
            {
                TimeSimulatorKeeper time = new TimeSimulatorKeeper();
                XmlSerializer serializer = new XmlSerializer(typeof(TimeSimulatorKeeper));
                using (TextReader textReader = new StreamReader(filepath))
                {
                    // ispis u XML datoteku
                    time = (TimeSimulatorKeeper)serializer.Deserialize(textReader);
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
