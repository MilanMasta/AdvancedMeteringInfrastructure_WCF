using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMISystemManagement;
using NUnit.Framework;

namespace AMISystemManagementTest
{
    [TestFixture]
    public class TimestampWorkerTest
    {

        [Test]
        [TestCase("1999-05-30")]
        [TestCase("2008-02-28")]
        public void TimestampWorkerVerify(DateTime dateTime)
        {
            TimestampWorker worker = new TimestampWorker();
            long unixTime = ((DateTimeOffset)dateTime).ToUnixTimeSeconds();
            DateTime resultDateTime = worker.UnixTimeStampToDateTime(unixTime);
            Assert.AreEqual(dateTime, resultDateTime);


        }

        [Test]
        [TestCase(-23)]
        public void TimestampWorkerVerifyNotValidParameter(double unixTimestamp)
        {
            TimestampWorker worker = new TimestampWorker();
            Assert.Throws<ArgumentException>
                (() =>
                {
                    worker.UnixTimeStampToDateTime(unixTimestamp);
                }
                );
        }



    }
}
