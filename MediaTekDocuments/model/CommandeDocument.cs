using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier CommandeDocument hérite de Commande : contient des propriétés spécifiques aux commandes de livres et dvd
    /// </summary>
    public class CommandeDocument : Commande
    {
        public int NbExemplaire { get; }
        public string IdLivreDvd { get; }
        public string IdSuivi { get; }
        public string Libelle { get; }

        public CommandeDocument(string id, DateTime dateCommande, double montant, int nbExemplaire,
            string idLivreDvd, string idSuivi, string libelle) :
            base(id, dateCommande, montant)
        {
            this.NbExemplaire = nbExemplaire;
            this.IdLivreDvd = idLivreDvd;
            this.IdSuivi = idSuivi;
            this.Libelle = libelle;
        }
    }
}
