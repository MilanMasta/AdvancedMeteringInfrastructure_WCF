using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMICommon2;
using NUnit.Framework;

namespace AMICommon2Test
{
    [TestFixture]
    public class AMIAgregatorStructTest
    {

        [Test]
        [TestCase(212121, 1622758695)]
        [TestCase(313131, 1622758696)]
        public void AMIAgregatorStructDobriParametri(int AmiAgregatorCode, long Timestamp)
        {
            AMIAgregatorStruct agregatorStruct = new AMIAgregatorStruct(AmiAgregatorCode, Timestamp);

            Assert.AreEqual(agregatorStruct.AMIAgregatorCode, AmiAgregatorCode);
            Assert.AreEqual(agregatorStruct.Timestamp, Timestamp);
            Assert.AreNotEqual(agregatorStruct.Structures, null);
        }

        [Test]
        [TestCase(345654, 1622758693)]
        public void AMIAgregatorStructLosiParametri(int AmiAgregatorCode, long Timestamp)
        {
            Assert.Throws<ArgumentException>
                (() =>
                {
                    AMIAgregatorStruct agregatorStruct = new AMIAgregatorStruct(AmiAgregatorCode, Timestamp);
                }
                );
        }

        [Test]
        public void AMIAgregatorStructBezParametara()
        {
            AMIAgregatorStruct amiAgregatorStruct = new AMIAgregatorStruct();
            Assert.AreNotEqual(amiAgregatorStruct.Structures, null);
        }

    }
}
