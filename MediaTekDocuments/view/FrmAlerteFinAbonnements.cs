using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MediaTekDocuments.controller;
using MediaTekDocuments.model;

namespace MediaTekDocuments.view
{
    /// <summary>
    /// Classe d'affichage de la liste des abonnements se finissant dans moins de 30 jours
    /// </summary>
    public partial class FrmAlerteFinAbonnements : Form
    {
        private readonly FrmAlerteFinAbonnementsController controller;
        private readonly BindingSource bdgAbonnementsFinissantListe = new BindingSource();
        private List<AbonnementFinissant> lesAbonnementsFinissant = new List<AbonnementFinissant>();
        private bool presenceAbonnementsFinissant;

        /// <summary>
        /// Constructeur : création du contrôleur lié à ce formulaire
        /// </summary>
        public FrmAlerteFinAbonnements()
        {
            InitializeComponent();
            this.controller = new FrmAlerteFinAbonnementsController();
        }

        /// <summary>
        /// Affichage de la liste des abonnements se finissant dans moins de 30 jours au chargement de la fenêtre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmAlerteFinAbonnements_Load(object sender, EventArgs e)
        {
            lesAbonnementsFinissant = controller.GetAbonnementsFinissant();
            RemplirAbonnementsFinissantListe(lesAbonnementsFinissant);
            AfficheAbonnementsFinissant();
        }

        /// <summary>
        /// Remplit le datagrid des abonnements se finissant dans moins de 30 jours avec la liste reçue en paramètre
        /// </summary>
        /// <param name="abonnementsFinissant"></param>
        public void RemplirAbonnementsFinissantListe(List<AbonnementFinissant> abonnementsFinissant)
        {
            if (abonnementsFinissant != null)
            {
                bdgAbonnementsFinissantListe.DataSource = abonnementsFinissant;
                dgvAlerteFinAbonnementsListe.DataSource = bdgAbonnementsFinissantListe;
                dgvAlerteFinAbonnementsListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgvAlerteFinAbonnementsListe.Columns["titre"].DisplayIndex = 0;
                dgvAlerteFinAbonnementsListe.Columns["dateFinAbonnement"].DisplayIndex = 1;
                presenceAbonnementsFinissant = true;
            }
            else
            {
                dgvAlerteFinAbonnementsListe.DataSource = null;
                presenceAbonnementsFinissant = false;
            }
        }

        /// <summary>
        /// Modification de l'affichage suivant si des abonnements de revues se finissent dans moins de 30 jours ou non
        /// </summary>
        public void AfficheAbonnementsFinissant()
        {
            if (presenceAbonnementsFinissant)
            {
                label6.Text = "Liste des revues dont un abonnement se finit dans moins de 30 jours :";
                dgvAlerteFinAbonnementsListe.Visible = true;
            }
            else
            {
                label6.Text = "Aucune revue ne possède un abonnement se finissant dans moins de 30 jours";
                dgvAlerteFinAbonnementsListe.Visible = false;
            }
        }

        /// <summary>
        /// Ferme la fenêtre actuelle
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAlerteFinAbonnementsFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Tri sur une colonne
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvAlerteFinAbonnementsListe_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string titreColonne = dgvAlerteFinAbonnementsListe.Columns[e.ColumnIndex].HeaderText;
            List<AbonnementFinissant> sortedList = new List<AbonnementFinissant>();
            switch (titreColonne)
            {
                case "Titre":
                    sortedList = lesAbonnementsFinissant.OrderBy(o => o.Titre).Reverse().ToList();
                    break;
                case "DateFinAbonnement":
                    sortedList = lesAbonnementsFinissant.OrderBy(o => o.DateFinAbonnement).Reverse().ToList();
                    break;
            }
            RemplirAbonnementsFinissantListe(sortedList);
        }

    }
}
