using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using AMICommon2;

namespace AMISystemManagement
{
    public class AMISystemManagementServer
    {

        private ServiceHost serviceHost;
        private String serviceName = "AMISystemManagementService";


        public AMISystemManagementServer()
        {
            //kreiranje endpoint-a i definisanje AMI agregator host-a
            string endpoint = "net.tcp://localhost:10100/AMISystemManagementService";
            serviceHost = new ServiceHost(typeof(AMISystemManagementService));
            NetTcpBinding binding = new NetTcpBinding();

            serviceHost.AddServiceEndpoint(typeof(IAgregatorAndManagementJob), binding, endpoint);
        }

        //POKRETANJE HOST-a
        public bool Open()
        {
            try
            {
                serviceHost.Open();
                Console.WriteLine(String.Format("Host for {0} endpoint type opened successfully at {1}", serviceName, DateTime.Now));
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
