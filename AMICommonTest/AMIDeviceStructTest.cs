using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMICommon;
using NUnit.Framework;

namespace AMICommonTest
{
    [TestFixture]
    public class AMIDeviceStructTest
    {

        [Test]
        [TestCase(345654, 1622758695)]
        [TestCase(364382, 1622758696)]
        public void AMIDeviceStructDobriParametri(int AmiDeviceCode, long Timestamp)
        {
            AMIDeviceStruct deviceStruct = new AMIDeviceStruct(AmiDeviceCode, Timestamp);

            Assert.AreEqual(deviceStruct.AMIDeviceCode, AmiDeviceCode);
            Assert.AreEqual(deviceStruct.Timestamp, Timestamp);
            Assert.AreNotEqual(deviceStruct.Measures, null);
        }

        [Test]
        [TestCase(345654, 1622758693)]
        public void AMIDeviceStructLosiParametri(int AmiDeviceCode, long Timestamp)
        {
            Assert.Throws<ArgumentException>
                (() =>
                {
                    AMIDeviceStruct deviceStruct = new AMIDeviceStruct(AmiDeviceCode, Timestamp);
                }
                );
        }

        [Test]
        public void AMIDeviceStructBezParametara()
        {
            AMIDeviceStruct amiDeviceStruct = new AMIDeviceStruct();
            Assert.AreNotEqual(amiDeviceStruct.Measures, null);
        }



    }
}
