using MediaTekDocuments.controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MediaTekDocuments.model;
using Newtonsoft.Json;
using System.Security.Cryptography;

namespace MediaTekDocuments.view
{
    /// <summary>
    /// Classe d'affichage de la fenêtre d'authentification de l'application
    /// </summary>
    public partial class FrmAuthentification : Form
    {
        private readonly FrmAuthentificationController controller;

        /// <summary>
        /// Constructeur : création du contrôleur lié à ce formulaire
        /// </summary>
        public FrmAuthentification()
        {
            InitializeComponent();
            this.controller = new FrmAuthentificationController();
        }

        /// <summary>
        /// Permet le hachage d'une chaîne de caractères avec l'algorithme SHA256 (comme les mots de passe)
        /// </summary>
        /// <param name="rawData"></param>
        /// <returns></returns>
        public static string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        /// <summary>
        /// Demande de connexion d'un utilisateur et vérification de l'authentification
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSeConnecter_Click(object sender, EventArgs e)
        {
            string login = txbLogin.Text;
            string password = ComputeSha256Hash(txbPwd.Text);

            if (string.IsNullOrEmpty(txbLogin.Text) || string.IsNullOrEmpty(txbPwd.Text))
            {
                MessageBox.Show("Tous les champs doivent être remplis", "Information");
            }
            else
            {
                Utilisateur utilisateurConnexionEnCours = controller.ControleAuthentification(login, password);
                if (utilisateurConnexionEnCours != null)
                {
                    Service.Id = utilisateurConnexionEnCours.IdService;
                    if (Service.Id.Equals("00003"))
                    {
                        MessageBox.Show("Vous n'avez pas les droits suffisants pour accéder à cette application", "Information");
                        this.Close();
                    }
                    else
                    {
                        FrmMediatek frmMediatek = new FrmMediatek();
                        frmMediatek.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("Identifiant et/ou mot de passe incorrectes", "Information");
                }
            }
        }
    }
}
