using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMIDevice;
using NUnit.Framework;

namespace AMIDeviceTest
{
    [TestFixture]
    public class TimeSimulatorKeeperTest
    {
        private const int _DEFAULT_VALUE = 1;

        [Test]
        public void TimeSimulatorKeeperKonstruktor()
        {
            TimeSimulatorKeeper simulatorKeeper = new TimeSimulatorKeeper();
            Assert.AreEqual(simulatorKeeper.Interval, _DEFAULT_VALUE);
        }



    }
}
