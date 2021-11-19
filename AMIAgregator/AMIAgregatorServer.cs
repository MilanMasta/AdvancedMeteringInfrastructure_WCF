using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AMICommon;

namespace AMIAgregator
{
    public class AMIAgregatorServer
    {
        private ServiceHost serviceHost;
        private String serviceName = "AMIAgregatorService";
        private ushort salt = (ushort)new Random().Next();

        public AMIAgregatorServer()
        {
            //dodavanje novog salt-a u Agregators XML datoteku
            //nju ce citati AMIDevice kako bi se povezao na neki od agregatora
            var doc = XElement.Load("Agregators.xml");
            var target = doc.Descendants("Salts").First();
            target.Add(new XElement("Salt", salt));
            doc.Save("Agregators.xml");

            //kreiranje endpoint-a i definisanje AMI agregator host-a
            string endpoint = $"net.tcp://localhost:9000/AMIAgregatorService/{salt}";
            serviceHost = new ServiceHost(typeof(AMIAgregatorService));
            NetTcpBinding binding = new NetTcpBinding();
            binding.PortSharingEnabled = true;

            serviceHost.AddServiceEndpoint(typeof(IDeviceAndAgregatorJob), binding, endpoint);
        }

        //POKRETANJE HOST-a
        public bool Open()
        {
            try
            {
                serviceHost.Open();
                Console.WriteLine(String.Format("Host for {0} endpoint [{1}] type opened successfully at {2}", serviceName, salt, DateTime.Now));
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Host open error for {0} endpoint type. Error message is: {1}. ", serviceName, e.Message);
                return false;
            }
        }

        //GASENJE HOST-a
        public bool Close()
        {
            try
            {
                serviceHost.Close();
                Console.WriteLine(String.Format("Host for {0} endpoint type closed successfully at {1}", serviceName, DateTime.Now));
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Host close error for {0} endpoint type. Error message is: {1}. ", serviceName, e.Message);
                return false;
            }
        }


    }
}
