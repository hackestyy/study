using System;
using MacAddress.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTestMacAddress
    {
        private INetCard _netCard = new NetCard();
        [TestMethod]
        public void TestMethodGetMacAddress()
        {
            Assert.AreEqual("1078d290a1cc", _netCard.GetLocalMac());
        }
    }
}
