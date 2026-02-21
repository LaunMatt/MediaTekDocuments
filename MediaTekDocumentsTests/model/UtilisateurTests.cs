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
    public class UtilisateurTests
    {
        private const string id = "00001";
        private const string login = "login";
        private const string password = "password";
        private const string idService = "00001";
        private const string libelle = "service";
        private static readonly Utilisateur utilisateur = new Utilisateur(id, login, password, idService, libelle);

        [TestMethod()]
        public void UtilisateurTest()
        {
            Assert.AreEqual(id, utilisateur.Id,"devrait réussir : id valorisé");
            Assert.AreEqual(login, utilisateur.Login, "devrait réussir : login valorisé");
            Assert.AreEqual(password, utilisateur.Password, "devrait réussir : password valorisé");
            Assert.AreEqual(idService, utilisateur.IdService, "devrait réussir : idService valorisé");
            Assert.AreEqual(libelle, utilisateur.Libelle, "devrait réussir : libelle valorisé");
        }
    }
}