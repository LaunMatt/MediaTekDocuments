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
    public class DocumentTests
    {
        private const string id = "00001";
        private const string titre = "titre";
        private const string image = "image";
        private const string idGenre = "00001";
        private const string genre = "genre";
        private const string idPublic = "00001";
        private const string lePublic = "public";
        private const string idRayon = "00001";
        private const string rayon = "rayon";
        private static readonly Document document = new Document(id, titre, image, idGenre, genre, idPublic, lePublic, idRayon, rayon);

        [TestMethod()]
        public void DocumentTest()
        {
            Assert.AreEqual(id, document.Id, "devrait réussir : id valorisé");
            Assert.AreEqual(titre, document.Titre, "devrait réussir : titre valorisé");
            Assert.AreEqual(image, document.Image, "devrait réussir : image valorisée");
            Assert.AreEqual(idGenre, document.IdGenre, "devrait réussir : idGenre valorisé");
            Assert.AreEqual(genre, document.Genre, "devrait réussir : genre valorisé");
            Assert.AreEqual(idPublic, document.IdPublic, "devrait réussir : idPublic valorisé");
            Assert.AreEqual(lePublic, document.Public, "devrait réussir : lePublic valorisé");
            Assert.AreEqual(idRayon, document.IdRayon, "devrait réussir : idRayon valorisé");
            Assert.AreEqual(rayon, document.Rayon, "devrait réussir : rayon valorisé");
        }
    }
}