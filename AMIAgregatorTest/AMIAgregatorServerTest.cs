using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using AMIAgregator;
using Moq;
using NUnit.Framework;

namespace AMIAgregatorTest
{
    [TestFixture]
    public class AMIAgregatorServerTest
    {

        private AMIAgregatorServer server;
        private AMIAgregatorServer server2;

        [SetUp]
        public void SetUp()
        {
            server = new AMIAgregatorServer();
            server2 = new AMIAgregatorServer();
        }

        [Test]
        public void AMIAgregatorServerOpenVerify()
        {
            bool pov = server.Open();
            Assert.AreEqual(true, pov);
        }

        [Test]
        public void AMIAgregatorServerCloseVerify()
        {
            bool pov = server.Close();
            Assert.AreEqual(true, pov);
        }

        [Test]
        public void AMIAgregatorServerOpenException()
        {
            server2.Open();
            bool pov = server2.Open();
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
