using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMIAgregator;
using NUnit.Framework;

namespace AMIAgregatorTest
{
    [TestFixture]
    public class TimeIntervalKeeperTest
    {

        private const int _DEFAULT_VALUE = 1;

        [Test]
        public void TimeIntervalKeeperKonstruktor()
        {
            TimeIntervalKeeper intervalKeeper = new TimeIntervalKeeper();
            Assert.AreEqual(intervalKeeper.Interval, _DEFAULT_VALUE);
        }




    }
}
