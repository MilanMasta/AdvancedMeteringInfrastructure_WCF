using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using AMICommon;

namespace AMIAgregator
{
    public class XMLSerializeWorker
    {
        //DODAVANJE NOVOG MJERENJA U XML FAJL
        public bool AddNewMeasurement(AMIDeviceStruct measurement, string fileName)
        {
            try
            {
                List<AMIDeviceStruct> measurements = ReadMeasurements(fileName);
                measurements.Add(measurement);

                XmlSerializer serializer = new XmlSerializer(typeof(List<AMIDeviceStruct>));
                using (TextWriter textWriter = new StreamWriter(fileName))
                {
                    // ispis u XML datoteku
                    serializer.Serialize(textWriter, measurements);
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        //CITANJE LISTE SVIH MJERENJA IZ XML FAJLA
        public List<AMIDeviceStruct> ReadMeasurements(string fileName)
        {
            try
            {
                List<AMIDeviceStruct> measurements = new List<AMIDeviceStruct>();
                XmlSerializer serializer = new XmlSerializer(typeof(List<AMIDeviceStruct>));
                using (TextReader textReader = new StreamReader(fileName))
                {
                    // ispis u XML datoteku
                    measurements = (List<AMIDeviceStruct>)serializer.Deserialize(textReader);
                }
                return measurements;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<AMIDeviceStruct>();
            }

        }






    }

}
