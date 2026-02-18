using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier utilisateur (utilisateurs de l'application appartenant à un service)
    /// </summary>
    public class Utilisateur
    {
        public string Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string IdService { get; set; }
        [JsonIgnore] public string Libelle { get; set; }

        public Utilisateur(string id, string login, string password, string idService, string libelle)
        {
            this.Id = id;
            this.Login = login;
            this.Password = password;
            this.IdService = idService;
            this.Libelle = libelle;
        }
    }
}
