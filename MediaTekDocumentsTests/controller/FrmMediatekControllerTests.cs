using Microsoft.VisualStudio.TestTools.UnitTesting;
using MediaTekDocuments.controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTekDocuments.controller.Tests
{
    [TestClass()]
    public class FrmMediatekControllerTests
    {
        [TestMethod()]
        public void ParutionDansAbonnementTest()
        {
            FrmMediatekController controller = new FrmMediatekController();

            DateTime dateCommande = new DateTime(2026, 2, 1);
            DateTime dateFinAbonnement = new DateTime(2026, 2, 28);

            // Test avec une date de parution située entre la date de commande et la date de fin d'abonnement
            DateTime dateParution1 = new DateTime(2026, 2, 15);
            Assert.IsTrue(controller.ParutionDansAbonnement(dateParution1, dateCommande, dateFinAbonnement));

            // Test avec une date de parution égale à la date de commande
            Assert.IsTrue(controller.ParutionDansAbonnement(dateCommande, dateCommande, dateFinAbonnement));

            // Test avec une date de parution égale à la date de fin de l'abonnement
            Assert.IsTrue(controller.ParutionDansAbonnement(dateFinAbonnement, dateCommande, dateFinAbonnement));

            // Test avec une date de parution située avant la date de commande
            DateTime dateParution2 = new DateTime(2026, 1, 1);
            Assert.IsFalse(controller.ParutionDansAbonnement(dateParution2, dateCommande, dateFinAbonnement));

            // Test avec une date de parution située après la date de fin de l'abonnement
            DateTime dateParution3 = new DateTime(2026, 3, 1);
            Assert.IsFalse(controller.ParutionDansAbonnement(dateParution3, dateCommande, dateFinAbonnement));
        }
    }
}