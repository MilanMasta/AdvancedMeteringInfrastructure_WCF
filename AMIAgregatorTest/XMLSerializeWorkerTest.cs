using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMIAgregator;
using AMICommon;
using Moq;
using NUnit.Framework;

namespace AMIAgregatorTest
{
    [TestFixture]
    public class XMLSerializeWorkerTest
    {

        [Test]
        public void AddNewMeasurementVerify()
        {
            XMLSerializeWorker worker = new XMLSerializeWorker();
            AMIDeviceStruct str = new AMIDeviceStruct(312, 342567646666);
            string filename = "XJYLI.xml";
            bool pov = worker.AddNewMeasurement(str, filename);
            Assert.AreEqual(true, pov);

        }

        [Test]
        public void AddNewMeasurementException()
        {
            XMLSerializeWorker worker = new XMLSerializeWorker();
            Mock<AMIDeviceStruct> struckMock = new Mock<AMIDeviceStruct>();
            // ne bi trebalo da snimi u xml fajl mock.Object jer je prazan
            // pa ce baciti exception i vratiti false
            string filename = "XJYLI.xml";
            bool pov = worker.AddNewMeasurement(struckMock.Object, filename);
            Assert.AreEqual(false, pov);

        }

    }
}
