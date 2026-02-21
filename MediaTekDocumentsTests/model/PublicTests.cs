using Microsoft.VisualStudio.TestTools.UnitTesting;
using MediaTekDocuments.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTekDocuments.model.Tests
{
    [TestClass()]
    public class PublicTests
    {
        private const string id = "00001";
        private const string libelle = "public";
        private static readonly Public lePublic = new Public(id, libelle);

        [TestMethod()]
        public void PublicTest()
        {
            Assert.AreEqual(id, lePublic.Id, "devrait réussir : id valorisé");
            Assert.AreEqual(libelle, lePublic.Libelle, "devrait réussir : libelle valorisé");
        }

        [TestMethod()]
        public void ToStringTest()
        {
            Assert.AreEqual(libelle, lePublic.ToString(), "devrait réussir : libellé retourné");
        }
    }
}