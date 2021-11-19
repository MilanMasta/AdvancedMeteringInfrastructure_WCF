using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMIAgregator;
using AMICommon2;
using Moq;
using NUnit.Framework;

namespace AMIAgregatorTest
{
    [TestFixture]
    public class TimeIntervalReaderTest
    {

        [Test]
        public void ReadTimeIntervalVerify()
        {
            TimeIntervalReader reader = new TimeIntervalReader();
            int interval = reader.ReadTimeInterval("TimeInterval2.xml");
            Assert.AreEqual(interval, 10);
        }

        [Test]
        public void ReadTimeIntervalVerifyException()
        {
            TimeIntervalReader reader = new TimeIntervalReader();
            int interval = reader.ReadTimeInterval("NepostojeciFajl.xml");
            Assert.AreNotEqual(interval, 3);
            Assert.AreEqual(interval, 5);
            //bacice izuzetak jer ne postoji fajl sa takvim nazivom
        }

        [Test]
        public void ReadTimeIntervalIsExecuted()
        {
            Mock<ITimeInterval> workerMock = new Mock<ITimeInterval>();
            workerMock.Setup(timeWorker => timeWorker.ReadTimeInterval("TimeInterval2.xml")).Verifiable();
            workerMock.Object.ReadTimeInterval("TimeInterval2.xml");
            workerMock.Verify(timeWorker => timeWorker.ReadTimeInterval("TimeInterval2.xml"), Times.Once);

        }





    }
}
