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
    public class DvdTests
    {
        private const string id = "00001";
        private const string titre = "titre";
        private const string image = "image";
        private const int duree = 60;
        private const string realisateur = "realisateur";
        private const string synopsis = "synopsis";
        private const string idGenre = "00001";
        private const string genre = "genre";
        private const string idPublic = "00001";
        private const string lePublic = "public";
        private const string idRayon = "00001";
        private const string rayon = "rayon";
        private static readonly Dvd dvd = new Dvd(id, titre, image, duree, realisateur, synopsis, idGenre, genre, idPublic, lePublic, idRayon, rayon);

        [TestMethod()]
        public void DvdTest()
        {
            Assert.AreEqual(id, dvd.Id, "devrait réussir : id valorisé");
            Assert.AreEqual(titre, dvd.Titre, "devrait réussir : titre valorisé");
            Assert.AreEqual(image, dvd.Image, "devrait réussir : image valorisée");
            Assert.AreEqual(duree, dvd.Duree, "devrait réussir : duree valorisée");
            Assert.AreEqual(realisateur, dvd.Realisateur, "devrait réussir : realisateur valorisé");
            Assert.AreEqual(synopsis, dvd.Synopsis, "devrait réussir : synopsis valorisé");
            Assert.AreEqual(idGenre, dvd.IdGenre, "devrait réussir : idGenre valorisé");
            Assert.AreEqual(genre, dvd.Genre, "devrait réussir : genre valorisé");
            Assert.AreEqual(idPublic, dvd.IdPublic, "devrait réussir : idPublic valorisé");
            Assert.AreEqual(lePublic, dvd.Public, "devrait réussir : lePublic valorisé");
            Assert.AreEqual(idRayon, dvd.IdRayon, "devrait réussir : idRayon valorisé");
            Assert.AreEqual(rayon, dvd.Rayon, "devrait réussir : rayon valorisé");
        }
    }
}