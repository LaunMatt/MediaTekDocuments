using System;
using System.Collections.Generic;
using MediaTekDocuments.model;
using MediaTekDocuments.manager;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;
using Serilog;

namespace MediaTekDocuments.dal
{
    /// <summary>
    /// Classe d'accès aux données
    /// </summary>
    public class Access
    {
        /// <summary>
        /// adresse de l'API
        /// </summary>
        private static readonly string uriApi = "http://localhost/rest_mediatekdocuments/";
        /// <summary>
        /// Login et mot de passe permettant d'accéder à l'API
        /// </summary>
        private static readonly string authenticationName = "MediaTekDocuments.Properties.Settings.mediatekAuthenticationString";
        /// <summary>
        /// instance unique de la classe
        /// </summary>
        private static Access instance = null;
        /// <summary>
        /// instance de ApiRest pour envoyer des demandes vers l'api et recevoir la réponse
        /// </summary>
        private readonly ApiRest api = null;
        /// <summary>
        /// méthode HTTP pour select
        /// </summary>
        private const string GET = "GET";
        /// <summary>
        /// méthode HTTP pour insert
        /// </summary>
        private const string POST = "POST";
        /// <summary>
        /// méthode HTTP pour update
        /// <summary>
        private const string PUT = "PUT";
        /// <summary>
        /// méthode HTTP pour delete
        /// </summary>
        private const string DELETE = "DELETE";

        /// <summary>
        /// Méthode privée pour créer un singleton
        /// initialise l'accès à l'API
        /// </summary>
        private Access()
        {
            String authenticationString = null;
            try
            {
                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Verbose()
                    .WriteTo.Console()
                    .WriteTo.File("logs/log.txt")
                    .CreateLogger();
                authenticationString = GetAuthenticationStringByName(authenticationName);
                api = ApiRest.GetInstance(uriApi, authenticationString);
            }
            catch (Exception e)
            {
                Log.Fatal("Access.Access catch authenticationString={0} erreur={1}", authenticationString, e.Message);
                Console.WriteLine(e.Message);
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Création et retour de l'instance unique de la classe
        /// </summary>
        /// <returns>instance unique de la classe</returns>
        public static Access GetInstance()
        {
            if(instance == null)
            {
                instance = new Access();
            }
            return instance;
        }

        /// <summary>
        /// Récupération de la chaîne de connexion
        /// </summary>
        /// <param name="name">chaîne de connexion dans App.config</param>
        /// <returns>chaîne de connexion</returns>
        static string GetAuthenticationStringByName(string name)
        {
            string returnValue = null;
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[name];
            if (settings != null)
                returnValue = settings.ConnectionString;
            return returnValue;
        }

        /// <summary>
        /// Retourne tous les genres à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Genre</returns>
        public List<Categorie> GetAllGenres()
        {
            IEnumerable<Genre> lesGenres = TraitementRecup<Genre>(GET, "genre", null);
            return new List<Categorie>(lesGenres);
        }

        /// <summary>
        /// Retourne tous les rayons à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Rayon</returns>
        public List<Categorie> GetAllRayons()
        {
            IEnumerable<Rayon> lesRayons = TraitementRecup<Rayon>(GET, "rayon", null);
            return new List<Categorie>(lesRayons);
        }

        /// <summary>
        /// Retourne toutes les catégories de public à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Public</returns>
        public List<Categorie> GetAllPublics()
        {
            IEnumerable<Public> lesPublics = TraitementRecup<Public>(GET, "public", null);
            return new List<Categorie>(lesPublics);
        }

        /// <summary>
        /// Retourne toutes les livres à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Livre</returns>
        public List<Livre> GetAllLivres()
        {
            List<Livre> lesLivres = TraitementRecup<Livre>(GET, "livre", null);
            return lesLivres;
        }

        /// <summary>
        /// Retourne toutes les dvd à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Dvd</returns>
        public List<Dvd> GetAllDvd()
        {
            List<Dvd> lesDvd = TraitementRecup<Dvd>(GET, "dvd", null);
            return lesDvd;
        }

        /// <summary>
        /// Retourne toutes les revues à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Revue</returns>
        public List<Revue> GetAllRevues()
        {
            List<Revue> lesRevues = TraitementRecup<Revue>(GET, "revue", null);
            return lesRevues;
        }

        /// <summary>
        /// Retourne les exemplaires d'un document
        /// </summary>
        /// <param name="idDocument">id du document concerné</param>
        /// <returns>Liste d'objets Exemplaire</returns>
        public List<Exemplaire> GetExemplaires(string idDocument)
        {
            String jsonIdDocument = convertToJson("id", idDocument);
            List<Exemplaire> lesExemplaires = TraitementRecup<Exemplaire>(GET, "exemplaire/" + jsonIdDocument, null);
            return lesExemplaires;
        }

        /// <summary>
        /// Retourne tous les suivis à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Suivi</returns>
        public List<Suivi> GetAllSuivis()
        {
            IEnumerable<Suivi> lesSuivis = TraitementRecup<Suivi>(GET, "suivi", null);
            return new List<Suivi>(lesSuivis);
        }

        /// <summary>
        /// Retourne tous les états à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Etat</returns>
        public List<Etat> GetAllEtats()
        {
            IEnumerable<Etat> lesEtats = TraitementRecup<Etat>(GET, "etat", null);
            return new List<Etat>(lesEtats);
        }

        /// <summary>
        /// Retourne les commandes d'un livre ou dvd
        /// </summary>
        /// <param name="idDocument">id du document concerné</param>
        /// <returns>Liste d'objets CommandeDocument</returns>
        public List<CommandeDocument> GetCommandeDocuments(string idDocument)
        {
            String jsonIdDocument = convertToJson("id", idDocument);
            List<CommandeDocument> lesCommandesDocument = TraitementRecup<CommandeDocument>(GET, "commandedocument/" + jsonIdDocument, null);
            return lesCommandesDocument;
        }

        /// <summary>
        /// Retourne les abonnements d'une revue
        /// </summary>
        /// <param name="idDocument">id du document concerné</param>
        /// <returns>Liste d'objets Abonnement</returns>
        public List<Abonnement> GetAbonnements(string idDocument)
        {
            String jsonIdDocument = convertToJson("id", idDocument);
            List<Abonnement> lesAbonnements = TraitementRecup<Abonnement>(GET, "abonnement/" + jsonIdDocument, null);
            return lesAbonnements;
        }

        /// <summary>
        /// Retourne tous les abonnements se finissant dans moins de 30 jours à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets AbonnementFinissant</returns>
        public List<AbonnementFinissant> GetAbonnementsFinissant()
        {
            List<AbonnementFinissant> lesAbonnementsFinissant = TraitementRecup<AbonnementFinissant>(GET, "abonnementfinissant", null);
            return lesAbonnementsFinissant;
        }

        /// <summary>
        /// écriture d'un exemplaire en base de données
        /// </summary>
        /// <param name="exemplaire">exemplaire à insérer</param>
        /// <returns>true si l'insertion a pu se faire (retour != null)</returns>
        public bool CreerExemplaire(Exemplaire exemplaire)
        {
            String jsonExemplaire = JsonConvert.SerializeObject(exemplaire, new CustomDateTimeConverter());
            try
            {
                List<Exemplaire> liste = TraitementRecup<Exemplaire>(POST, "exemplaire", "champs=" + jsonExemplaire);
                return (liste != null);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Access.CreerExemplaire catch type erreur={0} champs={1}", ex, jsonExemplaire);
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// modification de l'état d'un exemplaire en base de données
        /// </summary>
        /// <param name="idExemplaire">id de l'exemplaire à modifier</param>
        /// <param name="exemplaire">exemplaire à modifier</param>
        /// <returns>true si la modification a pu se faire (retour != null)</returns>
        public bool ModifierExemplaire(string idExemplaire, Exemplaire exemplaire)
        {
            String jsonExemplaire = JsonConvert.SerializeObject(exemplaire, new CustomDateTimeConverter());
            try
            {
                List<Exemplaire> liste = TraitementRecup<Exemplaire>(PUT, "exemplaire/" + idExemplaire, "champs=" + jsonExemplaire);
                return (liste != null);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Access.ModifierExemplaire catch type erreur={0} id={1} champs={2}", ex, idExemplaire, jsonExemplaire);
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// suppression d'un exemplaire en base de données
        /// </summary>
        /// <param name="idExemplaire">id de l'exemplaire à supprimer</param>
        /// <param name="numeroExemplaire">numero de l'exemplaire à supprimer</param>
        /// <returns>true si la suppression a pu se faire (retour != null)</returns>
        public bool SupprimerExemplaire(string idExemplaire, int numeroExemplaire)
        {
            string jsonExemplaire = "{\"Id\":\"" + idExemplaire + "\",\"Numero\":\"" + numeroExemplaire + "\"}";
            try
            {
                List<Exemplaire> liste = TraitementRecup<Exemplaire>(DELETE, "exemplaire/" + jsonExemplaire, null);
                return (liste != null);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Access.SupprimerExemplaire catch type erreur={0} champs={1}", ex, jsonExemplaire);
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// écriture d'une commande de livre ou dvd en base de données
        /// </summary>
        /// <param name="commandeDocument">commande à insérer</param>
        /// <returns>true si l'insertion a pu se faire (retour != null)</returns>
        public bool CreerCommandeDocument(CommandeDocument commandeDocument)
        {
            String jsonCommandeDocument = JsonConvert.SerializeObject(commandeDocument, new CustomDateTimeConverter());
            try
            {
                List<CommandeDocument> liste = TraitementRecup<CommandeDocument>(POST, "commandedocument", "champs=" + jsonCommandeDocument);
                return (liste != null);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Access.CreerCommandeDocument catch type erreur={0} champs={1}", ex, jsonCommandeDocument);
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// modification de l'étape de suivi d'une commande de livre ou dvd en base de données
        /// </summary>
        /// <param name="idCommande">id de la commande à modifier</param>
        /// <param name="commandeDocument">commande à modifier</param>
        /// <returns>true si la modification a pu se faire (retour != null)</returns>
        public bool ModifierCommandeDocument(string idCommande, CommandeDocument commandeDocument)
        {
            String jsonCommandeDocument = JsonConvert.SerializeObject(commandeDocument, new CustomDateTimeConverter());
            try
            {
                List<CommandeDocument> liste = TraitementRecup<CommandeDocument>(PUT, "commandedocument/" + idCommande, "champs=" + jsonCommandeDocument);
                return (liste != null);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Access.ModifierCommandeDocument catch type erreur={0} id={1} champs={2}", ex, idCommande, jsonCommandeDocument);
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// suppression d'une commande (abonnement compris) en base de données
        /// </summary>
        /// <param name="idCommande">commande à supprimer</param>
        /// <returns>true si la suppression a pu se faire (retour != null)</returns>
        public bool SupprimerCommande(string idCommande)
        {
            String jsonIdCommande = convertToJson("Id", idCommande);
            try
            {
                List<CommandeDocument> liste = TraitementRecup<CommandeDocument>(DELETE, "commande/" + jsonIdCommande, null);
                return (liste != null);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Access.SupprimerCommande catch type erreur={0} id={1}", ex, jsonIdCommande);
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// écriture d'un abonnement de revue en base de données
        /// </summary>
        /// <param name="abonnement">abonnement à insérer</param>
        /// <returns>true si l'insertion a pu se faire (retour != null)</returns>
        public bool CreerAbonnement(Abonnement abonnement)
        {
            String jsonAbonnement = JsonConvert.SerializeObject(abonnement, new CustomDateTimeConverter());
            try
            {
                List<Abonnement> liste = TraitementRecup<Abonnement>(POST, "abonnement", "champs=" + jsonAbonnement);
                return (liste != null);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Access.CreerAbonnement catch type erreur={0} champs={1}", ex, jsonAbonnement);
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// écriture d'un element en base de données
        /// </summary>
        /// <param name="typeElement">type de l'élément à ajouter</param>
        /// <param name="element">élement à ajouter</param>
        /// <returns>true si l'ajout a pu se faire (retour != null)</returns>
        public bool AjouterElement(string typeElement, Object element)
        {
            String jsonElement = JsonConvert.SerializeObject(element);
            try
            {
                List<Object> liste = TraitementRecup<Object>(POST, typeElement, "champs=" + jsonElement);
                return (liste != null);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Access.AjouterElement catch type erreur={0} type document={1} champs={2}", ex, typeElement, jsonElement);
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// modification d'un element en base de données
        /// </summary>
        /// <param name="typeElement">type de l'élément à modifier</param>
        /// <param name="idElement">id de l'élément à modifier</param>
        /// <param name="element">élément à modifier</param>
        /// <returns>true si la modification a pu se faire (retour != null)</returns>
        public bool ModifierElement(string typeElement, string idElement, Object element)
        {
            String jsonElement = JsonConvert.SerializeObject(element);
            try
            {
                List<Object> liste = TraitementRecup<Object>(PUT, typeElement + "/" + idElement, "champs=" + jsonElement);
                return (liste != null);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Access.ModifierElement catch type erreur={0} type document={1} id={2} champs={3}", ex, typeElement, idElement, jsonElement);
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// suppression d'un element en base de données
        /// </summary>
        /// <param name="typeElement">type de l'élément à supprimer</param>
        /// <param name="idElement">id de l'élément à supprimer</param>
        /// <returns>true si la suppression a pu se faire (retour != null)</returns>
        public bool SupprimerElement(string typeElement, string idElement)
        {
            String jsonIdElement = convertToJson("Id", idElement);
            try
            {
                List<Object> liste = TraitementRecup<Object>(DELETE, typeElement + "/" + jsonIdElement, null);
                return (liste != null);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Access.SupprimerElement catch type erreur={0} type document={1} id={2}", ex, typeElement, jsonIdElement);
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Vérifie l'authentification et récupère les informations d'un utilisateur dans la base de données si ses informations sont correctes
        /// </summary>
        /// <param name="login">login de l'utilisateur essayant de se connecter</param>
        /// <param name="password">mot de passe de l'utilisateur essayant de se connecter</param>
        /// <returns>Objet Utilisateur si les informations sont correctes, null si les informations sont incorrectes</returns>
        public Utilisateur ControleAuthentification(string login, string password)
        {
            String jsonLogin = convertToJson("login", login);
            List<Utilisateur> utilisateur = TraitementRecup<Utilisateur>(GET, "utilisateur/" + jsonLogin, null);
            if (utilisateur != null && utilisateur.Count > 0)
            {
                Utilisateur utilisateurVerifie = utilisateur[0];
                string utilisateurPassword = utilisateurVerifie.Password;
                if (utilisateurPassword.Equals(password))
                {
                    return utilisateurVerifie;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Traitement de la récupération du retour de l'api, avec conversion du json en liste pour les select (GET)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="methode">verbe HTTP (GET, POST, PUT, DELETE)</param>
        /// <param name="message">information envoyée dans l'url</param>
        /// <param name="parametres">paramètres à envoyer dans le body, au format "chp1=val1&chp2=val2&..."</param>
        /// <returns>liste d'objets récupérés (ou liste vide)</returns>
        private List<T> TraitementRecup<T> (String methode, String message, String parametres)
        {
            // trans
            List<T> liste = new List<T>();
            try
            {
                JObject retour = api.RecupDistant(methode, message, parametres);
                // extraction du code retourné
                String code = (String)retour["code"];
                if (code.Equals("200"))
                {
                    // dans le cas du GET (select), récupération de la liste d'objets
                    if (methode.Equals(GET))
                    {
                        String resultString = JsonConvert.SerializeObject(retour["result"]);
                        // construction de la liste d'objets à partir du retour de l'api
                        liste = JsonConvert.DeserializeObject<List<T>>(resultString, new CustomBooleanJsonConverter());
                    }
                }
                else
                {
                    Console.WriteLine("code erreur = " + code + " message = " + (String)retour["message"]);
                    Log.Error("Access.TraitementRecup code erreur={Message}", code, (String)retour["message"]);
                }
            }catch(Exception e)
            {
                Console.WriteLine("Erreur lors de l'accès à l'API : "+e.Message);
                Log.Fatal("Access.TraitementRecup Erreur lors de l'accès à l'API : {Message}", e.Message);
                Environment.Exit(0);
            }
            return liste;
        }

        /// <summary>
        /// Convertit en json un couple nom/valeur
        /// </summary>
        /// <param name="nom"></param>
        /// <param name="valeur"></param>
        /// <returns>couple au format json</returns>
        private String convertToJson(Object nom, Object valeur)
        {
            Dictionary<Object, Object> dictionary = new Dictionary<Object, Object>();
            dictionary.Add(nom, valeur);
            return JsonConvert.SerializeObject(dictionary);
        }

        /// <summary>
        /// Modification du convertisseur Json pour gérer le format de date
        /// </summary>
        private sealed class CustomDateTimeConverter : IsoDateTimeConverter
        {
            public CustomDateTimeConverter()
            {
                base.DateTimeFormat = "yyyy-MM-dd";
            }
        }

        /// <summary>
        /// Modification du convertisseur Json pour prendre en compte les booléens
        /// classe trouvée sur le site :
        /// https://www.thecodebuzz.com/newtonsoft-jsonreaderexception-could-not-convert-string-to-boolean/
        /// </summary>
        private sealed class CustomBooleanJsonConverter : JsonConverter<bool>
        {
            public override bool ReadJson(JsonReader reader, Type objectType, bool existingValue, bool hasExistingValue, JsonSerializer serializer)
            {
                return Convert.ToBoolean(reader.ValueType == typeof(string) ? Convert.ToByte(reader.Value) : reader.Value);
            }

            public override void WriteJson(JsonWriter writer, bool value, JsonSerializer serializer)
            {
                serializer.Serialize(writer, value);
            }
        }

    }
}
