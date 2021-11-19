using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMIDevice;
using NUnit.Framework;

namespace AMIDeviceTest
{
    public class TimeSimulatorWorkerTest
    {

        [Test]
        public void ReadTimeSimulatorVerify()
        {
            TimeSimulatorWorker worker = new TimeSimulatorWorker();
            int interval = worker.ReadTimeInterval("SimulacijaVremena.xml");
            Assert.AreEqual(interval, 18000);
        }

        [Test]
        public void ReadTimeSimulatorVerifyException()
        {
            TimeSimulatorWorker worker = new TimeSimulatorWorker();
            int interval = worker.ReadTimeInterval("NepostojeciFajl.xml");
            Assert.AreNotEqual(interval, 18000);
            //bacice izuzetak jer ne postoji fajl sa takvim nazivom
        }


    }
}
