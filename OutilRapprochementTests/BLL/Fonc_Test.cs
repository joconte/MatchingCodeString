using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RecodageList;
using RecodageList.BLL;
using RecodageList.DAL;

namespace OutilRapprochementTests
{
    [TestClass]
    public class Fonc_Test
    {
        Fonc fonc = new Fonc();

        [TestMethod]
        public void TestCanonicalFonc()
        {
            Assert.AreEqual("TEST1234", fonc.CanonicalString("Test1234"));

            Assert.AreEqual("E", fonc.CanonicalString("<>é"));
        }

        [TestMethod]
        public void iLD_Test()
        {
            Assert.AreEqual(0, fonc.iLD("Test1234", "Test1234"));
            Assert.AreEqual(100, fonc.iLD("E", "<>é"));
        }


    }
}
