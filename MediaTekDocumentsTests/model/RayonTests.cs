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
    public class RayonTests
    {
        private const string id = "00001";
        private const string libelle = "rayon";
        private static readonly Rayon rayon = new Rayon(id, libelle);

        [TestMethod()]
        public void RayonTest()
        {
            Assert.AreEqual(id, rayon.Id, "devrait réussir : id valorisé");
            Assert.AreEqual(libelle, rayon.Libelle, "devrait réussir : libelle valorisé");
        }

        [TestMethod()]
        public void ToStringTest()
        {
            Assert.AreEqual(libelle, rayon.ToString(), "devrait réussir : libellé retourné");
        }
    }
}