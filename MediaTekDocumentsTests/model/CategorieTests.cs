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
    public class CategorieTests
    {
        private const string id = "00001";
        private const string libelle = "catégorie";
        private static readonly Categorie categorie = new Categorie(id, libelle);

        [TestMethod()]
        public void CategorieTest()
        {
            Assert.AreEqual(id, categorie.Id, "devrait réussir : id valorisé");
            Assert.AreEqual(libelle, categorie.Libelle, "devrait réussir : libelle valorisé");
        }

        [TestMethod()]
        public void ToStringTest()
        {
            Assert.AreEqual(libelle, categorie.ToString(), "devrait réussir : libellé retourné");
        }
    }
}