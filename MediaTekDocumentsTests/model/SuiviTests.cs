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
    public class SuiviTests
    {
        private const string id = "00001";
        private const string libelle = "suivi";
        private static readonly Suivi suivi = new Suivi(id, libelle);

        [TestMethod()]
        public void SuiviTest()
        {
            Assert.AreEqual(id, suivi.Id, "devrait réussir : id valorisé");
            Assert.AreEqual(libelle, suivi.Libelle, "devrait réussir : libelle valorisé");
        }
    }
}