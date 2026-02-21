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
    public class GenreTests
    {
        private const string id = "00001";
        private const string libelle = "genre";
        private static readonly Genre genre = new Genre(id, libelle);

        [TestMethod()]
        public void GenreTest()
        {
            Assert.AreEqual(id, genre.Id, "devrait réussir : id valorisé");
            Assert.AreEqual(libelle, genre.Libelle, "devrait réussir : libelle valorisé");
        }

        [TestMethod()]
        public void ToStringTest()
        {
            Assert.AreEqual(libelle, genre.ToString(), "devrait réussir : libellé retourné");
        }
    }
}