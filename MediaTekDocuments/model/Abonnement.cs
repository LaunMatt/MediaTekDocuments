using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier Abonnement hérite de Commande : contient des propriétés spécifiques aux commandes de revues
    /// </summary>
    public class Abonnement : Commande
    {
        public DateTime DateFinAbonnement { get; }
        public string IdRevue { get; }

        public Abonnement(string id, DateTime dateCommande, double montant, DateTime dateFinAbonnement, string idRevue) :
            base(id, dateCommande, montant)
        {
            this.DateFinAbonnement = dateFinAbonnement;
            this.IdRevue = idRevue;
        }
    }
}
