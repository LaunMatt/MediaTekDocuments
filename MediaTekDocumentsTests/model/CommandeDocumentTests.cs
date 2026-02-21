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
    public class CommandeDocumentTests
    {
        private const string id = "00001";
        private static readonly DateTime dateCommande = new DateTime(2026, 1, 1);
        private const double montant = 1;
        private const int nbExemplaire = 1;
        private const string idLivreDvd = "00001";
        private const string idSuivi = "00001";
        private const string libelle = "suivi";
        private static readonly CommandeDocument commandeDocument = new CommandeDocument(id, dateCommande, montant, nbExemplaire, idLivreDvd, idSuivi, libelle);

        [TestMethod()]
        public void CommandeDocumentTest()
        {
            Assert.AreEqual(id, commandeDocument.Id, "devrait réussir : id valorisé");
            Assert.AreEqual(dateCommande, commandeDocument.DateCommande, "devrait réussir : dateCommande valorisée");
            Assert.AreEqual(montant, commandeDocument.Montant, "devrait réussir : montant valorisé");
            Assert.AreEqual(nbExemplaire, commandeDocument.NbExemplaire, "devrait réussir : nbExemplaire valorisé");
            Assert.AreEqual(idLivreDvd, commandeDocument.IdLivreDvd, "devrait réussir : idLivreDvd valorisé");
            Assert.AreEqual(idSuivi, commandeDocument.IdSuivi, "devrait réussir : idSuivi valorisé");
            Assert.AreEqual(libelle, commandeDocument.Libelle, "devrait réussir : libelle valorisé");
        }
    }
}