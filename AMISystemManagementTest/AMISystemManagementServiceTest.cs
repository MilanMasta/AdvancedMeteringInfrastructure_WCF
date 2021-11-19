using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMICommon;
using AMICommon2;
using AMISystemManagement;
using Moq;
using NUnit.Framework;

namespace AMISystemManagementTest
{
    [TestFixture]
    public class AMISystemManagementServiceTest
    {
  
        [Test]
        public void PosaljiException()
        {
            Mock<AMIAgregatorStruct> structMock = new Mock<AMIAgregatorStruct>();
            AMISystemManagementService service = new AMISystemManagementService();
            bool pov = service.Posalji(structMock.Object);
            // baca exception i vraca false jer objekat nije instanciran
            Assert.AreEqual(false, pov);

        }

    }
}
