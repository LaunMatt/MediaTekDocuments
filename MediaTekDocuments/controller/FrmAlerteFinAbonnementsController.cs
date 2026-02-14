using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaTekDocuments.model;
using MediaTekDocuments.dal;
using Newtonsoft.Json;

namespace MediaTekDocuments.controller
{
    /// <summary>
    /// Contrôleur lié à FrmAlerteFinAbonnements
    /// </summary>
    public class FrmAlerteFinAbonnementsController
    {
        /// <summary>
        /// Objet d'accès aux données
        /// </summary>
        private readonly Access access;

        /// <summary>
        /// Récupération de l'instance unique d'accès aux données
        /// </summary>
        public FrmAlerteFinAbonnementsController()
        {
            access = Access.GetInstance();
        }

        /// <summary>
        /// getter sur la liste des abonnements se finissant dans moins de 30 jours
        /// </summary>
        /// <returns></returns>
        public List<AbonnementFinissant> GetAbonnementsFinissant()
        {
            return access.GetAbonnementsFinissant();
        }
    }
}
