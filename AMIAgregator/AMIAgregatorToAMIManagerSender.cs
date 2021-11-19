using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using AMICommon2;

namespace AMIAgregator
{
    public class AMIAgregatorToAMIManagerSender
    {
        XMLSerializeWorker worker = new XMLSerializeWorker();
        TimeIntervalReader timeReader = new TimeIntervalReader();
  
        public void SlanjeUBazu(IAgregatorAndManagementJob proxy)
        {
            //citanje vremenskog intervala iz XML fajla
            //nakon koliko vremena Agregator salje podatke SystemManager-u
            int timeInterval = timeReader.ReadTimeInterval("TimeInterval.xml");

            int length = 7;
            StringBuilder str_build = new StringBuilder();
            Random random = new Random();
            char letter;

            for (int i = 0; i < length; i++)
            {
                double flt = random.NextDouble();
                int shift = Convert.ToInt32(Math.Floor(25 * flt));
                letter = Convert.ToChar(shift + 65);
                str_build.Append(letter);
            }

            //formiranje imena AMI Agregatora
            string agregatorName = str_build.ToString();
            int agregatorCode = agregatorName.GetHashCode();

            while (true)
            {
                //pakovanje podataka u strukturu i slanje SystemManageru
                DateTime date = DateTime.Now;
                long unixTime = ((DateTimeOffset)date).ToUnixTimeSeconds();
                AMIAgregatorStruct struktura = new AMIAgregatorStruct(agregatorCode, unixTime);
                struktura.Structures = worker.ReadMeasurements(xmlFajl.imeFajla);

                XmlDocument doc = new XmlDocument();
                doc.Load(xmlFajl.imeFajla);
                doc.DocumentElement.RemoveAll();
                doc.Save(xmlFajl.imeFajla);
              
                proxy.Posalji(struktura);
                    
                Thread.Sleep(timeInterval * 1000);        
            }

        }

    }
}
