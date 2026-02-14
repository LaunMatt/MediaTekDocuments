using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier AbonnementFinissant (abonnement se finissant dans moins de 30 jours)
    /// </summary>
    public class AbonnementFinissant
    {
        public string Titre { get; }
        public DateTime DateFinAbonnement { get; }

        public AbonnementFinissant(string titre, DateTime dateFinAbonnement)
        {
            this.Titre = titre;
            this.DateFinAbonnement = dateFinAbonnement;
        }
    }
}
