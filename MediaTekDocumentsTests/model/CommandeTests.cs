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
    public class CommandeTests
    {
        private const string id = "00001";
        private static readonly DateTime dateCommande = new DateTime(2026, 1, 1);
        private const double montant = 1;
        private static readonly Commande commande = new Commande(id, dateCommande, montant);

        [TestMethod()]
        public void CommandeTest()
        {
            Assert.AreEqual(id, commande.Id, "devrait réussir : id valorisé");
            Assert.AreEqual(dateCommande, commande.DateCommande, "devrait réussir : dateCommande valorisée");
            Assert.AreEqual(montant, commande.Montant, "devrait réussir : montant valorisé");
        }
    }
}