using System.Collections.Generic;
using MediaTekDocuments.model;
using MediaTekDocuments.dal;
using Newtonsoft.Json;

namespace MediaTekDocuments.controller
{
    /// <summary>
    /// Contrôleur lié à FrmMediatek
    /// </summary>
    class FrmMediatekController
    {
        /// <summary>
        /// Objet d'accès aux données
        /// </summary>
        private readonly Access access;

        /// <summary>
        /// Récupération de l'instance unique d'accès aux données
        /// </summary>
        public FrmMediatekController()
        {
            access = Access.GetInstance();
        }

        /// <summary>
        /// getter sur la liste des genres
        /// </summary>
        /// <returns>Liste d'objets Genre</returns>
        public List<Categorie> GetAllGenres()
        {
            return access.GetAllGenres();
        }

        /// <summary>
        /// getter sur la liste des livres
        /// </summary>
        /// <returns>Liste d'objets Livre</returns>
        public List<Livre> GetAllLivres()
        {
            return access.GetAllLivres();
        }

        /// <summary>
        /// getter sur la liste des Dvd
        /// </summary>
        /// <returns>Liste d'objets dvd</returns>
        public List<Dvd> GetAllDvd()
        {
            return access.GetAllDvd();
        }

        /// <summary>
        /// getter sur la liste des revues
        /// </summary>
        /// <returns>Liste d'objets Revue</returns>
        public List<Revue> GetAllRevues()
        {
            return access.GetAllRevues();
        }

        /// <summary>
        /// getter sur les rayons
        /// </summary>
        /// <returns>Liste d'objets Rayon</returns>
        public List<Categorie> GetAllRayons()
        {
            return access.GetAllRayons();
        }

        /// <summary>
        /// getter sur les publics
        /// </summary>
        /// <returns>Liste d'objets Public</returns>
        public List<Categorie> GetAllPublics()
        {
            return access.GetAllPublics();
        }


        /// <summary>
        /// récupère les exemplaires d'un document
        /// </summary>
        /// <param name="idDocument">id du document concerné</param>
        /// <returns>Liste d'objets Exemplaire</returns>
        public List<Exemplaire> GetExemplaires(string idDocument)
        {
            return access.GetExemplaires(idDocument);
        }

        /// <summary>
        /// Crée un exemplaire d'une revue dans la bdd
        /// </summary>
        /// <param name="exemplaire">L'objet Exemplaire concerné</param>
        /// <returns>True si la création a pu se faire</returns>
        public bool CreerExemplaire(Exemplaire exemplaire)
        {
            return access.CreerExemplaire(exemplaire);
        }

        /// <summary>
        /// Ajoute un livre dans la bdd
        /// </summary>
        /// <param name="livre">L'objet Livre concerné</param>
        /// <returns>True si l'ajout a pu se faire</returns>
        public bool AjouterLivre(Livre livre)
        {
            return access.AjouterElement("livre", livre);
        }

        /// <summary>
        /// Modifie un livre dans la bdd
        /// </summary>
        /// <param name="livre">L'objet Livre concerné</param>
        /// <returns>True si la modification a pu se faire</returns>
        public bool ModifierLivre(Livre livre)
        {
            return access.ModifierElement("livre", livre.Id, livre);
        }

        /// <summary>
        /// Supprime un livre dans la bdd
        /// </summary>
        /// <param name="livre">L'objet Livre concerné</param>
        /// <returns>True si la suppression a pu se faire</returns>
        public bool SupprimerLivre(Livre livre)
        {
            return access.SupprimerElement("livre", livre.Id);
        }


        /// <summary>
        /// Ajoute un dvd dans la bdd
        /// </summary>
        /// <param name="dvd">L'objet Dvd concerné</param>
        /// <returns>True si l'ajout a pu se faire</returns>
        public bool AjouterDvd(Dvd dvd)
        {
            return access.AjouterElement("dvd", dvd);
        }

        /// <summary>
        /// Modifie un dvd dans la bdd
        /// </summary>
        /// <param name="dvd">L'objet Dvd concerné</param>
        /// <returns>True si la modification a pu se faire</returns>
        public bool ModifierDvd(Dvd dvd)
        {
            return access.ModifierElement("dvd", dvd.Id, dvd);
        }

        /// <summary>
        /// Supprime un dvd dans la bdd
        /// </summary>
        /// <param name="dvd">L'objet Dvd concerné</param>
        /// <returns>True si la suppression a pu se faire</returns>
        public bool SupprimerDvd(Dvd dvd)
        {
            return access.SupprimerElement("dvd", dvd.Id);
        }


        /// <summary>
        /// Ajoute une revue dans la bdd
        /// </summary>
        /// <param name="revue">L'objet Revue concerné</param>
        /// <returns>True si l'ajout a pu se faire</returns>
        public bool AjouterRevue(Revue revue)
        {
            return access.AjouterElement("revue", revue);
        }

        /// <summary>
        /// Modifie une revue dans la bdd
        /// </summary>
        /// <param name="revue">L'objet Revue concerné</param>
        /// <returns>True si la modification a pu se faire</returns>
        public bool ModifierRevue(Revue revue)
        {
            return access.ModifierElement("revue", revue.Id, revue);
        }

        /// <summary>
        /// Supprime une revue dans la bdd
        /// </summary>
        /// <param name="revue">L'objet Revue concerné</param>
        /// <returns>True si la suppression a pu se faire</returns>
        public bool SupprimerRevue(Revue revue)
        {
            return access.SupprimerElement("revue", revue.Id);
        }
    }
}
