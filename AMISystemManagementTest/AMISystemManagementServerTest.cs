using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMISystemManagement;
using NUnit.Framework;

namespace AMIAgregatorTest
{
    [TestFixture]
    public class AMISystemManagementServerTest
    {

        private AMISystemManagementServer server;
        private AMISystemManagementServer server2;

        [SetUp]
        public void SetUp()
        {
            server = new AMISystemManagementServer();
            server2 = new AMISystemManagementServer();
        }


        [Test]
        public void AMISystemManagementServerOpenVerify1()
        {
            bool pov = server.Open();
            Assert.AreEqual(true, pov);
        }

        [Test]
        public void AMISystemManagementServerCloseVerify()
        {
            bool pov = server.Close();
            Assert.AreEqual(true, pov);
        }

        [Test]
        public void AMISystemManagementServerOpenVerify2()
        {
            bool pov = server.Open();
            Assert.AreEqual(false, pov);
        }

        [TearDown]
        public void TearDown()
        {
            server = null;
            server2 = null;
        }




    }
}
