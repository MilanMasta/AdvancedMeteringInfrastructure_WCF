using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using AMICommon;

namespace AMIAgregator
{
    public class AMIAgregatorService : IDeviceAndAgregatorJob
    {
        private string filename = xmlFajl.imeFajla;
        private string devicesFilename = xmlFajl.imeFajlaZaListuDevice;
        public static int brojMjerenja = 0;
        public TimestampWorker timestampWorker = new TimestampWorker();
        public XMLSerializeWorker serializeWorker = new XMLSerializeWorker();

        public AMIAgregatorService(string filename, string devicesfilename)
        {
            this.filename = filename;
            this.devicesFilename = devicesfilename;
        }

        public AMIAgregatorService() { }

        public long Posalji(AMIDeviceStruct struktura)
        {
            //ispis mjerenja u konzolu
            Console.WriteLine();
            Console.WriteLine($"############## MERENJE {++brojMjerenja} ##############");
            Console.WriteLine($"AMIDeviceCode : {struktura.AMIDeviceCode.ToString()}");
            Console.WriteLine($"Vreme : {timestampWorker.UnixTimeStampToDateTime(struktura.Timestamp)}");
            Console.WriteLine("---------------------------------------");
            foreach (MeasureStruct measure in struktura.Measures)
            {
                Console.WriteLine($"[{measure.Type} = {measure.Value}]");
            }
            Console.WriteLine("---------------------------------------");
            Console.WriteLine();

            //cuvanje  mjerenja u XML fajl
            serializeWorker.AddNewMeasurement(struktura, filename);
            return struktura.Timestamp;

        }

        public bool ProveraJedinstvenogImena(int AmiDeviceCode)                
        {
            List<int> deviceCodes = new List<int>();

            //preuzimanje liste imena AMI uredjaja iz XMl fajla
            XmlDocument doc = new XmlDocument();
            doc.Load(devicesFilename);
            XmlNodeList elemList = doc.GetElementsByTagName("Code");

            for (int i = 0; i < elemList.Count; i++)
            {
                deviceCodes.Add(int.Parse(elemList[i].InnerXml));
            }

            //ako ime nije jedinstveno, vraca se false
            foreach(int code in deviceCodes)
            {
                if(code == AmiDeviceCode)
                {
                    return false;
                }
            }

            //ako je ime jedinstveno, dodaje se u XML datoteku i vraca se true
            var doc2 = XElement.Load(devicesFilename);
            var target = doc2.Descendants("Codes").First();
            target.Add(new XElement("Code", AmiDeviceCode));
            doc2.Save(devicesFilename);

            return true;

        }
    }
}
