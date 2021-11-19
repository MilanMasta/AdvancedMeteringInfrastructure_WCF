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
    public class MeasureStructTest
    {

        [Test]
        [TestCase(MeasurementType.Napon, 220)]
        [TestCase(MeasurementType.Struja, 20)]
        [TestCase(MeasurementType.AktivnaSnaga, 10)]
        [TestCase(MeasurementType.ReaktivnaSnaga, 1)]
        public void MeasureStructDobriParametri(MeasurementType type, double value)
        {

            MeasureStruct measure = new MeasureStruct(type, value);

            Assert.AreEqual(measure.Type, type);
            Assert.AreEqual(measure.Value, value);

        }

        [Test]
        [TestCase(MeasurementType.Napon, 380)]
        [TestCase(MeasurementType.Struja, 5)]
        [TestCase(MeasurementType.AktivnaSnaga, 30)]
        [TestCase(MeasurementType.ReaktivnaSnaga, 2)]
        public void MeasureStructGranicniParametri(MeasurementType type, double value)
        {

            MeasureStruct measure = new MeasureStruct(type, value);

            Assert.AreEqual(measure.Type, type);
            Assert.AreEqual(measure.Value, value);

        }

        [Test]
        [TestCase(MeasurementType.Napon, 1000)]
        [TestCase(MeasurementType.Struja, 50)]
        [TestCase(MeasurementType.AktivnaSnaga, 40)]
        [TestCase(MeasurementType.ReaktivnaSnaga, 10)]
        [TestCase(MeasurementType.Napon, 10)]
        [TestCase(MeasurementType.Struja, 2)]
        [TestCase(MeasurementType.AktivnaSnaga, 0)]
        [TestCase(MeasurementType.ReaktivnaSnaga, -3)]
        public void MeasureStructLosiParametri(MeasurementType type, double value)
        {

            Assert.Throws<ArgumentException>
                (() =>
                {
                    MeasureStruct measure = new MeasureStruct(type, value);
                });

        }

        [Test]
        public void MeasureStructPrazanKonstruktor()
        {

            MeasureStruct measure = new MeasureStruct();

            Assert.AreEqual(measure.Type, MeasurementType.Napon);
            Assert.AreEqual(measure.Value, 220);

        }



    }
}
