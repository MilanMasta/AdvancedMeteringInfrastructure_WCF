using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using AMICommon2;

namespace AMIAgregator
{
    public static class xmlFajl
    {
        public static string imeFajla;
        public static string imeFajlaZaListuDevice;
    }

    [ExcludeFromCodeCoverage]
    class Program
    {
        private static IAgregatorAndManagementJob proxy;
        private static AMIAgregatorToAMIManagerSender sender = new AMIAgregatorToAMIManagerSender();

        //konektovanje na AMI System Management servis
        public static void Connect()
        {
            var binding = new NetTcpBinding();
            ChannelFactory<IAgregatorAndManagementJob> factory = new ChannelFactory<IAgregatorAndManagementJob>
                (binding, new EndpointAddress("net.tcp://localhost:10100/AMISystemManagementService"));
            proxy = factory.CreateChannel();
            Console.WriteLine("AMIAgregator has been conected to AMISystemManagementService.");

        }

        static void Main(string[] args)
        {
            //pozivanje metode za konekciju sa AMI System Manager-om
            Connect();

            //otvaranje sopstvenog servisa, upucenog ka AMI uredjajima
            AMIAgregatorServer server = new AMIAgregatorServer();
            server.Open();

            //generisanje Random string-a
            int length = 5;                                         
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
            string pomocni = str_build.ToString();
            xmlFajl.imeFajla = pomocni + ".xml";

            //kreiranje novog XML fajla za svaku instancu Agregatora
            //XML fajl sluzi kao privremeni buffer, za cuvanje podataka
            XmlWriter writer = XmlWriter.Create(xmlFajl.imeFajla);           
            writer.WriteStartElement("ArrayOfAMIDeviceStruct");            
            writer.WriteEndElement();
            writer.Close();

            //kreiranje drugog XML fajla koji cuva imena trenutno povezanih AMI uredjaja
            xmlFajl.imeFajlaZaListuDevice = pomocni + "Devices" + ".xml";
            XmlWriter writer2 = XmlWriter.Create(xmlFajl.imeFajlaZaListuDevice);
            writer2.WriteStartElement("ArrayOfAMIDevices");
            writer2.WriteStartElement("Codes");
            writer2.WriteEndElement();
            writer2.WriteEndElement();
            writer2.Close();

            Console.WriteLine("AMIAgregator has been started.");

            XmlDocument doc = new XmlDocument();
            doc.Load(xmlFajl.imeFajla);
            doc.DocumentElement.RemoveAll();
            doc.Save(xmlFajl.imeFajla);

            //slanje podataka AMI System Manageru radi kao odvojen proces
            new Thread(() => sender.SlanjeUBazu(proxy)) { IsBackground = true }.Start();

            Console.ReadLine();

            Console.WriteLine("AMIAgregator is stopping.");
            server.Close();

            Console.WriteLine("AMIAgregator has stopped.");
        }
    }
}
