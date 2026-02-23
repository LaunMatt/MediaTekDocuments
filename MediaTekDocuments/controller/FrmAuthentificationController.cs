using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaTekDocuments.dal;
using MediaTekDocuments.model;

namespace MediaTekDocuments.controller
{
    /// <summary>
    /// Contrôleur lié à FrmAuthentification
    /// </summary>
    public class FrmAuthentificationController
    {
        /// <summary>
        /// Objet d'accès aux données
        /// </summary>
        private readonly Access access;

        /// <summary>
        /// Récupération de l'instance unique d'accès aux données
        /// </summary>
        public FrmAuthentificationController()
        {
            access = Access.GetInstance();
        }

        /// <summary>
        /// Vérifie l'authentification et récupère un utilisateur en base de données si les informations de connexion sont correctes
        /// </summary>
        /// <param name="login">login de l'utilisateur essayant de se connecter</param>
        /// <param name="password">mot de passe de l'utilisateur essayant de se connecter</param>
        /// <returns>Objet Utilisateur</returns>
        public Utilisateur ControleAuthentification(string login, string password)
        {
            return access.ControleAuthentification(login, password);
        }
    }
}
