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
    public class AbonnementTests
    {
        private const string id = "00001";
        private static readonly DateTime dateCommande = new DateTime(2026, 1, 1);
        private const double montant = 1;
        private static readonly DateTime dateFinAbonnement = new DateTime(2026, 1, 2);
        private const string idRevue = "00001";
        private static readonly Abonnement abonnement = new Abonnement(id, dateCommande, montant, dateFinAbonnement, idRevue);

        [TestMethod()]
        public void AbonnementTest()
        {
            Assert.AreEqual(id, abonnement.Id, "devrait réussir : id valorisé");
            Assert.AreEqual(dateCommande, abonnement.DateCommande, "devrait réussir : dateCommande valorisée");
            Assert.AreEqual(montant, abonnement.Montant, "devrait réussir : montant valorisé");
            Assert.AreEqual(dateFinAbonnement, abonnement.DateFinAbonnement, "devrait réussir : dateFinAbonnement valorisée");
            Assert.AreEqual(idRevue, abonnement.IdRevue, "devrait réussir : idRevue valorisé");
        }
    }
}