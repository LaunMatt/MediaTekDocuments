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
    public class AbonnementFinissantTests
    {
        private const string titre = "titre";
        private static readonly DateTime dateFinAbonnement = new DateTime(2026, 1, 1);
        private static readonly AbonnementFinissant abonnementFinissant = new AbonnementFinissant(titre, dateFinAbonnement);

        [TestMethod()]
        public void AbonnementFinissantTest()
        {
            Assert.AreEqual(titre, abonnementFinissant.Titre, "devrait réussir : titre valorisé");
            Assert.AreEqual(dateFinAbonnement, abonnementFinissant.DateFinAbonnement, "devrait réussir : dateFinAbonnement valorisée");
        }
    }
}