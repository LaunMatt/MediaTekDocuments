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
    public class ExemplaireTests
    {
        private const int numero = 1;
        private static readonly DateTime dateAchat = new DateTime(2026, 1, 1);
        private const string photo = "photo";
        private const string idEtat = "00001";
        private const string idDocument = "00001";
        private const string libelle = "état";
        private static readonly Exemplaire exemplaire = new Exemplaire(numero, dateAchat, photo, idEtat, idDocument, libelle);

        [TestMethod()]
        public void ExemplaireTest()
        {
            Assert.AreEqual(numero, exemplaire.Numero, "devrait réussir : numero valorisé");
            Assert.AreEqual(dateAchat, exemplaire.DateAchat, "devrait réussir : dateAchat valorisée");
            Assert.AreEqual(photo, exemplaire.Photo, "devrait réussir : photo valorisée");
            Assert.AreEqual(idEtat, exemplaire.IdEtat, "devrait réussir : idEtat valorisé");
            Assert.AreEqual(idDocument, exemplaire.Id, "devrait réussir : id valorisé");
            Assert.AreEqual(libelle, exemplaire.Libelle, "devrait réussir : libelle valorisé");
        }
    }
}