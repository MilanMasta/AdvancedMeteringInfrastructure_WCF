using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMIAgregator;
using AMICommon;
using NUnit.Framework;

namespace AMIAgregatorTest
{
    [TestFixture]
    public class AMIAgregatorServiceTest
    {
        private AMIDeviceStruct struktura;
        private AMIAgregatorService service;
        private string filename = "XJYLI.xml";
        private string devicesfilename = "XJYLIDevices.xml";
        private Random rnd = new Random();

        [SetUp]
        public void SetUp()
        {
            struktura = new AMIDeviceStruct(232323, 3005280223);
            struktura.Measures = new List<MeasureStruct> { new MeasureStruct(MeasurementType.Napon, 220) };
            service = new AMIAgregatorService(filename, devicesfilename);
        }

        [Test]
        public void PosaljiVerify()
        {
            long pov = service.Posalji(struktura);
            Assert.AreEqual(struktura.Timestamp, pov);
        }

        [Test]
        public void ProveraJedinstvenogImenaVerify()
        {
            int br = rnd.Next(1, 100000);
            bool pov = service.ProveraJedinstvenogImena(br);
            Assert.AreEqual(true, pov);
        }

        [Test]
        public void ProveraJedinstvenogImenaVerifyFalse()
        {
            bool pov = service.ProveraJedinstvenogImena(232323);
            Assert.AreEqual(false, pov);
        }

        [Test]
        public void PrazanKonstruktorVerify()
        {
            AMIAgregatorService servis = new AMIAgregatorService();
            Assert.AreNotEqual(null, servis.serializeWorker);
            Assert.AreNotEqual(null, servis.timestampWorker);
        }

        [TearDown]
        public void TearDown()
        {
            struktura = null;
            service = null;
        }


    }
}
