using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier Suivi (état d'avancée d'un commande)
    /// </summary>
    public class Suivi
    {
        public string Id { get; set; }
        public string Libelle { get; set; }

        public Suivi(string id, string libelle)
        {
            this.Id = id;
            this.Libelle = libelle;
        }
    }
}
