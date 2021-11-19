using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using AMICommon;
using AMIDevice;
using Moq;
using NUnit.Framework;

namespace AMIDeviceTest
{
    [TestFixture]
    public class TimeIntervalWorkerTest
    {

        [Test]
        public void ReadTimeIntervalVerify()
        {
            TimeIntervalWorker worker = new TimeIntervalWorker();
            int interval = worker.ReadTimeInterval("TimeInterval.xml");
            Assert.AreEqual(interval, 3);
        }

        [Test]
        public void ReadTimeIntervalVerifyException()
        {
            TimeIntervalWorker worker = new TimeIntervalWorker();
            int interval = worker.ReadTimeInterval("NepostojeciFajl.xml");
            Assert.AreNotEqual(interval, 3);
            Assert.AreEqual(interval, 1);
            //bacice izuzetak jer ne postoji fajl sa takvim nazivom
            //ako baci izuzetak, po default-u interval iznosi 1 sekundu
        }

        [Test]
        public void ReadTimeIntervalIsExecuted()
        {
            Mock<ITimeInterval> workerMock = new Mock<ITimeInterval>();
            workerMock.Setup(timeWorker => timeWorker.ReadTimeInterval("TimeInterval.xml")).Verifiable();
            workerMock.Object.ReadTimeInterval("TimeInterval.xml");
            workerMock.Verify(timeWorker => timeWorker.ReadTimeInterval("TimeInterval.xml"), Times.Once);

        }

    }
}
