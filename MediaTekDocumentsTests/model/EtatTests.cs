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
    public class EtatTests
    {
        private const string id = "00001";
        private const string libelle = "etat";
        private static readonly Etat etat = new Etat(id, libelle);

        [TestMethod()]
        public void EtatTest()
        {
            Assert.AreEqual(id, etat.Id, "devrait réussir : id valorisé");
            Assert.AreEqual(libelle, etat.Libelle, "devrait réussir : libelle valorisé");
        }

        [TestMethod()]
        public void ToStringTest()
        {
            Assert.AreEqual(libelle, etat.ToString(), "devrait réussir : libellé retourné");
        }
    }
}