using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier Comande (réunit les infomations communes à toutes les commandes : CommandeDocument, Abonnement)
    /// </summary>
    public class Commande
    {
        public string Id { get; }
        public DateTime DateCommande { get; set; }
        public double Montant { get; }


        public Commande(string id, DateTime dateCommande, double montant)
        {
            Id = id;
            DateCommande = dateCommande;
            Montant = montant;
        }
    }
}
