using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using AMICommon;

namespace AMIDevice
{
    [ExcludeFromCodeCoverage]
    class Program
    {
        private static IDeviceAndAgregatorJob proxy;
        private static TimeIntervalWorker timeWorker = new TimeIntervalWorker();
        private static TimeSimulatorWorker timeSimulatorWorker = new TimeSimulatorWorker();

        //KONEKTOVANJE NA NEKI OD AKTIVNIH AGREGATORA
        public static void Connect()
        {
            //preuzimanje adresa(salt-ova) trenutno aktivnih agregatora iz Agregators XML datoteke
            //salt - dio adrese
            List<ushort> salts = new List<ushort>();
            string path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\"))
                + "AMIAgregator\\bin\\Debug\\Agregators.xml";

            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            XmlNodeList elemList = doc.GetElementsByTagName("Salt");

            for (int i = 0; i < elemList.Count; i++)
            {
                salts.Add(ushort.Parse(elemList[i].InnerXml));
            }

            int broj;

            //odabir aktivnog agregatora kroz konzolu
            do
            {
                Console.WriteLine("Odaberi broj aktivnog agregatora za konekciju : ");
                for (int i = 0; i < salts.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. Agregator [{salts[i]}]");
                }
                int.TryParse(Console.ReadLine(), out broj);
            }
            while (broj < 1 || broj > salts.Count);

            ushort salt = salts[broj - 1];

            //definisanje adrese za konekciju
            string endpoint = $"net.tcp://localhost:9000/AMIAgregatorService/{salt}";

            //konektovanje, kreiranje kanala za komunikaciju
            var binding = new NetTcpBinding();
            ChannelFactory<IDeviceAndAgregatorJob> factory = new ChannelFactory<IDeviceAndAgregatorJob>
                (binding, new EndpointAddress(endpoint));
            proxy = factory.CreateChannel();

        }

        static void Main(string[] args)
        {

            Connect();

            //unos naziva AMI uredjaja i provjera jedinstvenosti
            string deviceName; 
            do
            {                                                                       
                Console.WriteLine("Unesite naziv AMI uredjaja: ");
                deviceName = Console.ReadLine();
            }
            while (proxy.ProveraJedinstvenogImena(deviceName.GetHashCode()) == false);

            //definisanje potrebnih podatak za slanje 
            TimeIntervalKeeper keeper = new TimeIntervalKeeper();
            keeper.Interval = timeWorker.ReadTimeInterval("TimeInterval.xml");
            int timeInterval = keeper.Interval;
            int timeSimulation = timeSimulatorWorker.ReadTimeInterval("SimulacijaVremena.xml");
            var deviceCode = deviceName.GetHashCode();
            bool prviProlaz = true;
            long unixTime2 = 0;
            while (true)
            {
                Random r = new Random();
                double napon = r.Next(110, 380);
                double struja = r.Next(5, 25);
                double aktivnaSnaga = r.Next(5, 30);
                double reaktivnaSnaga = r.Next(0, 2);
                AMIDeviceStruct struktura = new AMIDeviceStruct();

                if (prviProlaz == true)
                {
                    prviProlaz = false;
                    DateTime date = DateTime.Now;
                    long unixTime = ((DateTimeOffset)date).ToUnixTimeSeconds();
                    struktura = new AMIDeviceStruct(deviceCode, unixTime);
                }
                else if(prviProlaz == false)
                {
                    long unixTime1 = unixTime2 + timeInterval * timeSimulation; 
                    struktura = new AMIDeviceStruct(deviceCode, unixTime1);
                }

                struktura.Measures.Add(new MeasureStruct(MeasurementType.Napon, napon));
                struktura.Measures.Add(new MeasureStruct(MeasurementType.Struja, struja));
                struktura.Measures.Add(new MeasureStruct(MeasurementType.AktivnaSnaga, aktivnaSnaga));
                struktura.Measures.Add(new MeasureStruct(MeasurementType.ReaktivnaSnaga, reaktivnaSnaga));
                try
                {
                    unixTime2 = proxy.Posalji(struktura);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    //Console.ReadLine();
                    return;
                }
               
                Console.WriteLine("Podaci uspjesno poslati.");
                Thread.Sleep(timeInterval * 1000);

            }


        }
    }
}
