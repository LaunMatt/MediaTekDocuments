using System;
using System.Windows.Forms;
using MediaTekDocuments.model;
using MediaTekDocuments.controller;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.IO;

namespace MediaTekDocuments.view

{
    /// <summary>
    /// Classe d'affichage
    /// </summary>
    public partial class FrmMediatek : Form
    {
        #region Commun
        private readonly FrmMediatekController controller;
        private readonly BindingSource bdgGenres = new BindingSource();
        private readonly BindingSource bdgPublics = new BindingSource();
        private readonly BindingSource bdgRayons = new BindingSource();

        const string SUIVIENCOURS = "00001";
        const string SUIVIENCOURSLIBELLE = "en cours";
        const string SUIVILIVREE = "00002";
        const string SUIVILIVREELIBELLE = "livrée";
        const string SUIVIREGLEE = "00003";
        const string SUIVIREGLEELIBELLE = "réglée";
        const string SUIVIRELANCEE = "00004";
        const string SUIVIRELANCEELIBELLE = "relancée";

        /// <summary>
        /// Constructeur : création du contrôleur lié à ce formulaire
        /// </summary>
        internal FrmMediatek()
        {
            InitializeComponent();
            this.controller = new FrmMediatekController();
        }

        /// <summary>
        /// Rempli un des 3 combo (genre, public, rayon)
        /// </summary>
        /// <param name="lesCategories">liste des objets de type Genre ou Public ou Rayon</param>
        /// <param name="bdg">bindingsource contenant les informations</param>
        /// <param name="cbx">combobox à remplir</param>
        public void RemplirComboCategorie(List<Categorie> lesCategories, BindingSource bdg, ComboBox cbx)
        {
            bdg.DataSource = lesCategories;
            cbx.DataSource = bdg;
            if (cbx.Items.Count > 0)
            {
                cbx.SelectedIndex = -1;
            }
        }
        #endregion

        #region Onglet Livres
        private readonly BindingSource bdgLivresListe = new BindingSource();
        private List<Livre> lesLivres = new List<Livre>();

        /// <summary>
        /// Ouverture de l'onglet Livres : 
        /// appel des méthodes pour remplir le datagrid des livres et des combos (genre, rayon, public)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabLivres_Enter(object sender, EventArgs e)
        {
            lesLivres = controller.GetAllLivres();
            RemplirComboCategorie(controller.GetAllGenres(), bdgGenres, cbxLivresGenres);
            RemplirComboCategorie(controller.GetAllPublics(), bdgPublics, cbxLivresPublics);
            RemplirComboCategorie(controller.GetAllRayons(), bdgRayons, cbxLivresRayons);

            RemplirComboCategorie(controller.GetAllGenres(), bdgGenresLivres, cbxLivresGenresActions);
            RemplirComboCategorie(controller.GetAllPublics(), bdgPublicsLivres, cbxLivresPublicsActions);
            RemplirComboCategorie(controller.GetAllRayons(), bdgRayonsLivres, cbxLivresRayonsActions);

            RemplirLivresListeComplete();
        }

        /// <summary>
        /// Remplit le dategrid avec la liste reçue en paramètre
        /// </summary>
        /// <param name="livres">liste de livres</param>
        private void RemplirLivresListe(List<Livre> livres)
        {
            bdgLivresListe.DataSource = livres;
            dgvLivresListe.DataSource = bdgLivresListe;
            dgvLivresListe.Columns["isbn"].Visible = false;
            dgvLivresListe.Columns["idRayon"].Visible = false;
            dgvLivresListe.Columns["idGenre"].Visible = false;
            dgvLivresListe.Columns["idPublic"].Visible = false;
            dgvLivresListe.Columns["image"].Visible = false;
            dgvLivresListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvLivresListe.Columns["id"].DisplayIndex = 0;
            dgvLivresListe.Columns["titre"].DisplayIndex = 1;
        }

        /// <summary>
        /// Recherche et affichage du livre dont on a saisi le numéro.
        /// Si non trouvé, affichage d'un MessageBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLivresNumRecherche_Click(object sender, EventArgs e)
        {
            if (!txbLivresNumRecherche.Text.Equals(""))
            {
                txbLivresTitreRecherche.Text = "";
                cbxLivresGenres.SelectedIndex = -1;
                cbxLivresRayons.SelectedIndex = -1;
                cbxLivresPublics.SelectedIndex = -1;
                Livre livre = lesLivres.Find(x => x.Id.Equals(txbLivresNumRecherche.Text));
                if (livre != null)
                {
                    List<Livre> livres = new List<Livre>() { livre };
                    RemplirLivresListe(livres);
                }
                else
                {
                    MessageBox.Show("numéro introuvable");
                    RemplirLivresListeComplete();
                }
            }
            else
            {
                RemplirLivresListeComplete();
            }
        }

        /// <summary>
        /// Recherche et affichage des livres dont le titre matche acec la saisie.
        /// Cette procédure est exécutée à chaque ajout ou suppression de caractère
        /// dans le textBox de saisie.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxbLivresTitreRecherche_TextChanged(object sender, EventArgs e)
        {
            if (!txbLivresTitreRecherche.Text.Equals(""))
            {
                cbxLivresGenres.SelectedIndex = -1;
                cbxLivresRayons.SelectedIndex = -1;
                cbxLivresPublics.SelectedIndex = -1;
                txbLivresNumRecherche.Text = "";
                List<Livre> lesLivresParTitre;
                lesLivresParTitre = lesLivres.FindAll(x => x.Titre.ToLower().Contains(txbLivresTitreRecherche.Text.ToLower()));
                RemplirLivresListe(lesLivresParTitre);
            }
            else
            {
                // si la zone de saisie est vide et aucun élément combo sélectionné, réaffichage de la liste complète
                if (cbxLivresGenres.SelectedIndex < 0 && cbxLivresPublics.SelectedIndex < 0 && cbxLivresRayons.SelectedIndex < 0
                    && txbLivresNumRecherche.Text.Equals(""))
                {
                    RemplirLivresListeComplete();
                }
            }
        }

        /// <summary>
        /// Affichage des informations du livre sélectionné
        /// </summary>
        /// <param name="livre">le livre</param>
        private void AfficheLivresInfos(Livre livre)
        {
            txbLivresAuteur.Text = livre.Auteur;
            txbLivresCollection.Text = livre.Collection;
            txbLivresImage.Text = livre.Image;
            txbLivresIsbn.Text = livre.Isbn;
            txbLivresNumero.Text = livre.Id;
            txbLivresGenre.Text = livre.Genre;
            txbLivresPublic.Text = livre.Public;
            txbLivresRayon.Text = livre.Rayon;
            txbLivresTitre.Text = livre.Titre;
            string image = livre.Image;
            try
            {
                pcbLivresImage.Image = Image.FromFile(image);
            }
            catch
            {
                pcbLivresImage.Image = null;
            }
        }

        /// <summary>
        /// Vide les zones d'affichage des informations du livre
        /// </summary>
        private void VideLivresInfos()
        {
            txbLivresAuteur.Text = "";
            txbLivresCollection.Text = "";
            txbLivresImage.Text = "";
            txbLivresIsbn.Text = "";
            txbLivresNumero.Text = "";
            txbLivresGenre.Text = "";
            txbLivresPublic.Text = "";
            txbLivresRayon.Text = "";
            txbLivresTitre.Text = "";
            pcbLivresImage.Image = null;
        }

        /// <summary>
        /// Filtre sur le genre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbxLivresGenres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxLivresGenres.SelectedIndex >= 0)
            {
                txbLivresTitreRecherche.Text = "";
                txbLivresNumRecherche.Text = "";
                Genre genre = (Genre)cbxLivresGenres.SelectedItem;
                List<Livre> livres = lesLivres.FindAll(x => x.Genre.Equals(genre.Libelle));
                RemplirLivresListe(livres);
                cbxLivresRayons.SelectedIndex = -1;
                cbxLivresPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur la catégorie de public
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbxLivresPublics_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxLivresPublics.SelectedIndex >= 0)
            {
                txbLivresTitreRecherche.Text = "";
                txbLivresNumRecherche.Text = "";
                Public lePublic = (Public)cbxLivresPublics.SelectedItem;
                List<Livre> livres = lesLivres.FindAll(x => x.Public.Equals(lePublic.Libelle));
                RemplirLivresListe(livres);
                cbxLivresRayons.SelectedIndex = -1;
                cbxLivresGenres.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur le rayon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbxLivresRayons_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxLivresRayons.SelectedIndex >= 0)
            {
                txbLivresTitreRecherche.Text = "";
                txbLivresNumRecherche.Text = "";
                Rayon rayon = (Rayon)cbxLivresRayons.SelectedItem;
                List<Livre> livres = lesLivres.FindAll(x => x.Rayon.Equals(rayon.Libelle));
                RemplirLivresListe(livres);
                cbxLivresGenres.SelectedIndex = -1;
                cbxLivresPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Sur la sélection d'une ligne ou cellule dans le grid
        /// affichage des informations du livre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvLivresListe_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvLivresListe.CurrentCell != null)
            {
                try
                {
                    Livre livre = (Livre)bdgLivresListe.List[bdgLivresListe.Position];
                    AfficheLivresInfos(livre);
                }
                catch
                {
                    VideLivresZones();
                }
            }
            else
            {
                VideLivresInfos();
            }
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des livres
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLivresAnnulPublics_Click(object sender, EventArgs e)
        {
            RemplirLivresListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des livres
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLivresAnnulRayons_Click(object sender, EventArgs e)
        {
            RemplirLivresListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des livres
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLivresAnnulGenres_Click(object sender, EventArgs e)
        {
            RemplirLivresListeComplete();
        }

        /// <summary>
        /// Affichage de la liste complète des livres
        /// et annulation de toutes les recherches et filtres
        /// </summary>
        private void RemplirLivresListeComplete()
        {
            RemplirLivresListe(lesLivres);
            VideLivresZones();
        }

        /// <summary>
        /// vide les zones de recherche et de filtre
        /// </summary>
        private void VideLivresZones()
        {
            cbxLivresGenres.SelectedIndex = -1;
            cbxLivresRayons.SelectedIndex = -1;
            cbxLivresPublics.SelectedIndex = -1;
            txbLivresNumRecherche.Text = "";
            txbLivresTitreRecherche.Text = "";
        }

        /// <summary>
        /// Tri sur les colonnes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvLivresListe_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            VideLivresZones();
            string titreColonne = dgvLivresListe.Columns[e.ColumnIndex].HeaderText;
            List<Livre> sortedList = new List<Livre>();
            switch (titreColonne)
            {
                case "Id":
                    sortedList = lesLivres.OrderBy(o => o.Id).ToList();
                    break;
                case "Titre":
                    sortedList = lesLivres.OrderBy(o => o.Titre).ToList();
                    break;
                case "Collection":
                    sortedList = lesLivres.OrderBy(o => o.Collection).ToList();
                    break;
                case "Auteur":
                    sortedList = lesLivres.OrderBy(o => o.Auteur).ToList();
                    break;
                case "Genre":
                    sortedList = lesLivres.OrderBy(o => o.Genre).ToList();
                    break;
                case "Public":
                    sortedList = lesLivres.OrderBy(o => o.Public).ToList();
                    break;
                case "Rayon":
                    sortedList = lesLivres.OrderBy(o => o.Rayon).ToList();
                    break;
            }
            RemplirLivresListe(sortedList);
        }

        // Sources permettant de remplir les combos d'ajout ou de modification d'un livre (genre, public, rayon)
        private readonly BindingSource bdgGenresLivres = new BindingSource();
        private readonly BindingSource bdgPublicsLivres = new BindingSource();
        private readonly BindingSource bdgRayonsLivres = new BindingSource();

        /// <summary>
        /// Booléen pour savoir si une modification d'un livre est demandée
        /// </summary>
        private Boolean enCoursDeModificationLivre = false;

        /// <summary>
        /// Modification d'affichage suivant si on est en cours de modification ou d'ajout d'un livre
        /// </summary>
        /// <param name="modification">modification en cours ou non</param>
        private void EnCoursDeModificationLivre(Boolean modification) 
        {
            enCoursDeModificationLivre = modification;
            grpLivresRecherche.Enabled = !modification;
            grpLivresInfos.Enabled = !modification;
            if (modification)
            {
                grpLivresActions.Text = "Modifier un livre";
                txbLivresNumeroActions.Enabled = false;
            }
            else
            {
                grpLivresActions.Text = "Ajouter un livre";
                txbLivresNumeroActions.Text = "";
                txbLivresIsbnActions.Text = "";
                txbLivresTitreActions.Text = "";
                txbLivresAuteurActions.Text = "";
                txbLivresCollectionActions.Text = "";
                txbLivresImageActions.Text = "";
                pcbLivresImageActions.Image = null;
                txbLivresNumeroActions.Enabled = true;
            }
        }

        /// <summary>
        /// Vide les zones d'affichage des informations du livre dans la zone d'ajout ou de modification
        /// </summary>
        private void VideLivresInfosActions()
        {
            txbLivresAuteurActions.Text = "";
            txbLivresCollectionActions.Text = "";
            txbLivresImageActions.Text = "";
            txbLivresIsbnActions.Text = "";
            txbLivresNumeroActions.Text = "";
            txbLivresTitreActions.Text = "";
            pcbLivresImageActions.Image = null;
        }

        /// <summary>
        /// Recherche image sur disque (pour le livre à insérer)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRechercheLivresImage_Click(object sender, EventArgs e)
        {
            string filePath = "";
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                // positionnement à la racine du disque où se trouve le dossier actuel
                InitialDirectory = Path.GetPathRoot(Environment.CurrentDirectory),
                Filter = "Files|*.jpg;*.bmp;*.jpeg;*.png;*.gif"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;
            }
            txbLivresImageActions.Text = filePath;
            try
            {
                pcbLivresImageActions.Image = Image.FromFile(filePath);
            }
            catch
            {
                pcbLivresImageActions.Image = null;
            }
        }

        /// <summary>
        /// Demande de modification d'un livre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnModificationLivres_Click(object sender, EventArgs e)
        {
            if (dgvLivresListe.SelectedRows.Count > 0)
            {
                EnCoursDeModificationLivre(true);
                Livre livre = (Livre)bdgLivresListe.List[bdgLivresListe.Position];
                txbLivresNumeroActions.Text = livre.Id;
                txbLivresIsbnActions.Text = livre.Isbn;
                txbLivresTitreActions.Text = livre.Titre;
                txbLivresAuteurActions.Text = livre.Auteur;
                txbLivresCollectionActions.Text = livre.Collection;
                cbxLivresGenresActions.SelectedIndex = cbxLivresGenresActions.FindStringExact(livre.Genre);
                cbxLivresPublicsActions.SelectedIndex = cbxLivresPublicsActions.FindStringExact(livre.Public);
                cbxLivresRayonsActions.SelectedIndex = cbxLivresRayonsActions.FindStringExact(livre.Rayon);
                txbLivresImageActions.Text = livre.Image;
                if (!txbLivresImageActions.Text.Equals(""))
                {
                    pcbLivresImageActions.Image = Image.FromFile(txbLivresImageActions.Text);
                }
            }
            else
            {
                MessageBox.Show("Une ligne parmi la liste des livres doit être sélectionnée.", "Information");
            }
        }

        /// <summary>
        /// Demande de suppression d'un livre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSuppressionLivres_Click(object sender, EventArgs e)
        {
            if (dgvLivresListe.SelectedRows.Count > 0)
            {
                Livre livre = (Livre)bdgLivresListe.List[bdgLivresListe.Position];
                lesExemplaires = controller.GetExemplaires(livre.Id);
                lesCommandesLivre = controller.GetCommandeDocuments(livre.Id);

                int nbExemplaires = 0;
                foreach (Exemplaire exemplaire in lesExemplaires)
                {
                    if (exemplaire.Id.Equals(livre.Id))
                    {
                        nbExemplaires++;
                    }
                }

                int nbCommandes = 0;
                foreach (CommandeDocument commandeLivre in lesCommandesLivre)
                {
                    if (commandeLivre.IdLivreDvd.Equals(livre.Id))
                    {
                        nbCommandes++;
                    }
                }

                if (nbExemplaires == 0 && nbCommandes == 0)
                {
                    if (MessageBox.Show("Souhaitez-vous vraiment supprimer le livre " + livre.Titre + " ?", "Confirmation de suppression", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        if (controller.SupprimerLivre(livre))
                        {
                            lesLivres = controller.GetAllLivres();
                            RemplirLivresListeComplete();
                        }
                        else
                        {
                            MessageBox.Show("problème rencontré lors de la suppression", "Erreur");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Veiller à ce qu'aucun exemplaire et qu'aucune commande ne soit rattaché à ce livre pour pouvoir le supprimer", "Information");
                }
            }
            else
            {
                MessageBox.Show("Une ligne parmi la liste des livres doit être sélectionnée.", "Information");
            }
        }

        /// <summary>
        /// Demande d'enregistrement de l'ajout ou de la modification d'un livre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLivresActionsValider_Click(object sender, EventArgs e)
        {
            if (!txbLivresNumeroActions.Text.Equals("") && cbxLivresGenresActions.SelectedIndex != -1 && cbxLivresPublicsActions.SelectedIndex != -1 && cbxLivresRayonsActions.SelectedIndex != -1)
            {
                try
                {
                    if (MessageBox.Show("Voulez-vous vraiment valider ces informations ?", "Confirmation de validation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        Genre unGenre = (Genre)cbxLivresGenresActions.SelectedItem;
                        Public unPublic = (Public)cbxLivresPublicsActions.SelectedItem;
                        Rayon unRayon = (Rayon)cbxLivresRayonsActions.SelectedItem;
                        string id = txbLivresNumeroActions.Text;
                        string isbn = txbLivresIsbnActions.Text;
                        string titre = txbLivresTitreActions.Text;
                        string auteur = txbLivresAuteurActions.Text;
                        string collection = txbLivresCollectionActions.Text;
                        string idGenre = unGenre.Id;
                        string genre = unGenre.Libelle;
                        string idPublic = unPublic.Id;
                        string lePublic = unPublic.Libelle;
                        string idRayon = unRayon.Id;
                        string rayon = unRayon.Libelle;
                        string image = txbLivresImageActions.Text;

                        lesLivres = controller.GetAllLivres();
                        int nbIdLivres = 0;
                        foreach (Livre leLivre in lesLivres)
                        {
                            if (leLivre.Id.Equals(id))
                            {
                                nbIdLivres++;
                            }
                        }

                        Livre livre = new Livre(id, titre, image, isbn, auteur, collection, idGenre, genre, idPublic, lePublic, idRayon, rayon);

                        if (enCoursDeModificationLivre)
                        {
                            if (controller.ModifierLivre(livre))
                            {
                                EnCoursDeModificationLivre(false);
                                lesLivres = controller.GetAllLivres();
                                RemplirLivresListeComplete();
                                VideLivresInfosActions();

                            }
                            else
                            {
                                MessageBox.Show("problème rencontré lors de la modification", "Erreur");
                            }
                        }
                        else
                        {
                            if (nbIdLivres == 0)
                            {
                                if (controller.AjouterLivre(livre))
                                {
                                    lesLivres = controller.GetAllLivres();
                                    RemplirLivresListeComplete();
                                    VideLivresInfosActions();
                                }
                                else
                                {
                                    MessageBox.Show("problème rencontré lors de l'ajout", "Erreur");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Veuillez saisir un numéro de document qui n'est pas déjà existant", "Information");
                            }
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("problème lors de la validation", "Erreur");
                    EnCoursDeModificationLivre(false);
                    VideLivresInfosActions();
                }
            }
            else
            {
                MessageBox.Show("Veuillez remplir au minimum le numéro de document, le genre, le public et le rayon", "Information");
            }
        }

        /// <summary>
        /// Demande d'annulation de l'ajout ou de la modification d'un livre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLivresActionsAnnuler_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Voulez-vous vraiment annuler ?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                EnCoursDeModificationLivre(false);
            }
        }

        #endregion

        #region Onglet Dvd
        private readonly BindingSource bdgDvdListe = new BindingSource();
        private List<Dvd> lesDvd = new List<Dvd>();

        /// <summary>
        /// Ouverture de l'onglet Dvds : 
        /// appel des méthodes pour remplir le datagrid des dvd et des combos (genre, rayon, public)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabDvd_Enter(object sender, EventArgs e)
        {
            lesDvd = controller.GetAllDvd();
            RemplirComboCategorie(controller.GetAllGenres(), bdgGenres, cbxDvdGenres);
            RemplirComboCategorie(controller.GetAllPublics(), bdgPublics, cbxDvdPublics);
            RemplirComboCategorie(controller.GetAllRayons(), bdgRayons, cbxDvdRayons);

            RemplirComboCategorie(controller.GetAllGenres(), bdgGenresLivres, cbxDvdGenresActions);
            RemplirComboCategorie(controller.GetAllPublics(), bdgPublicsLivres, cbxDvdPublicsActions);
            RemplirComboCategorie(controller.GetAllRayons(), bdgRayonsLivres, cbxDvdRayonsActions);

            RemplirDvdListeComplete();
        }

        /// <summary>
        /// Remplit le dategrid avec la liste reçue en paramètre
        /// </summary>
        /// <param name="Dvds">liste de dvd</param>
        private void RemplirDvdListe(List<Dvd> Dvds)
        {
            bdgDvdListe.DataSource = Dvds;
            dgvDvdListe.DataSource = bdgDvdListe;
            dgvDvdListe.Columns["idRayon"].Visible = false;
            dgvDvdListe.Columns["idGenre"].Visible = false;
            dgvDvdListe.Columns["idPublic"].Visible = false;
            dgvDvdListe.Columns["image"].Visible = false;
            dgvDvdListe.Columns["synopsis"].Visible = false;
            dgvDvdListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvDvdListe.Columns["id"].DisplayIndex = 0;
            dgvDvdListe.Columns["titre"].DisplayIndex = 1;
        }

        /// <summary>
        /// Recherche et affichage du Dvd dont on a saisi le numéro.
        /// Si non trouvé, affichage d'un MessageBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDvdNumRecherche_Click(object sender, EventArgs e)
        {
            if (!txbDvdNumRecherche.Text.Equals(""))
            {
                txbDvdTitreRecherche.Text = "";
                cbxDvdGenres.SelectedIndex = -1;
                cbxDvdRayons.SelectedIndex = -1;
                cbxDvdPublics.SelectedIndex = -1;
                Dvd dvd = lesDvd.Find(x => x.Id.Equals(txbDvdNumRecherche.Text));
                if (dvd != null)
                {
                    List<Dvd> Dvd = new List<Dvd>() { dvd };
                    RemplirDvdListe(Dvd);
                }
                else
                {
                    MessageBox.Show("numéro introuvable");
                    RemplirDvdListeComplete();
                }
            }
            else
            {
                RemplirDvdListeComplete();
            }
        }

        /// <summary>
        /// Recherche et affichage des Dvd dont le titre matche acec la saisie.
        /// Cette procédure est exécutée à chaque ajout ou suppression de caractère
        /// dans le textBox de saisie.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txbDvdTitreRecherche_TextChanged(object sender, EventArgs e)
        {
            if (!txbDvdTitreRecherche.Text.Equals(""))
            {
                cbxDvdGenres.SelectedIndex = -1;
                cbxDvdRayons.SelectedIndex = -1;
                cbxDvdPublics.SelectedIndex = -1;
                txbDvdNumRecherche.Text = "";
                List<Dvd> lesDvdParTitre;
                lesDvdParTitre = lesDvd.FindAll(x => x.Titre.ToLower().Contains(txbDvdTitreRecherche.Text.ToLower()));
                RemplirDvdListe(lesDvdParTitre);
            }
            else
            {
                // si la zone de saisie est vide et aucun élément combo sélectionné, réaffichage de la liste complète
                if (cbxDvdGenres.SelectedIndex < 0 && cbxDvdPublics.SelectedIndex < 0 && cbxDvdRayons.SelectedIndex < 0
                    && txbDvdNumRecherche.Text.Equals(""))
                {
                    RemplirDvdListeComplete();
                }
            }
        }

        /// <summary>
        /// Affichage des informations du dvd sélectionné
        /// </summary>
        /// <param name="dvd">le dvd</param>
        private void AfficheDvdInfos(Dvd dvd)
        {
            txbDvdRealisateur.Text = dvd.Realisateur;
            txbDvdSynopsis.Text = dvd.Synopsis;
            txbDvdImage.Text = dvd.Image;
            txbDvdDuree.Text = dvd.Duree.ToString();
            txbDvdNumero.Text = dvd.Id;
            txbDvdGenre.Text = dvd.Genre;
            txbDvdPublic.Text = dvd.Public;
            txbDvdRayon.Text = dvd.Rayon;
            txbDvdTitre.Text = dvd.Titre;
            string image = dvd.Image;
            try
            {
                pcbDvdImage.Image = Image.FromFile(image);
            }
            catch
            {
                pcbDvdImage.Image = null;
            }
        }

        /// <summary>
        /// Vide les zones d'affichage des informations du dvd
        /// </summary>
        private void VideDvdInfos()
        {
            txbDvdRealisateur.Text = "";
            txbDvdSynopsis.Text = "";
            txbDvdImage.Text = "";
            txbDvdDuree.Text = "";
            txbDvdNumero.Text = "";
            txbDvdGenre.Text = "";
            txbDvdPublic.Text = "";
            txbDvdRayon.Text = "";
            txbDvdTitre.Text = "";
            pcbDvdImage.Image = null;
        }

        /// <summary>
        /// Filtre sur le genre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxDvdGenres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxDvdGenres.SelectedIndex >= 0)
            {
                txbDvdTitreRecherche.Text = "";
                txbDvdNumRecherche.Text = "";
                Genre genre = (Genre)cbxDvdGenres.SelectedItem;
                List<Dvd> Dvd = lesDvd.FindAll(x => x.Genre.Equals(genre.Libelle));
                RemplirDvdListe(Dvd);
                cbxDvdRayons.SelectedIndex = -1;
                cbxDvdPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur la catégorie de public
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxDvdPublics_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxDvdPublics.SelectedIndex >= 0)
            {
                txbDvdTitreRecherche.Text = "";
                txbDvdNumRecherche.Text = "";
                Public lePublic = (Public)cbxDvdPublics.SelectedItem;
                List<Dvd> Dvd = lesDvd.FindAll(x => x.Public.Equals(lePublic.Libelle));
                RemplirDvdListe(Dvd);
                cbxDvdRayons.SelectedIndex = -1;
                cbxDvdGenres.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur le rayon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxDvdRayons_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxDvdRayons.SelectedIndex >= 0)
            {
                txbDvdTitreRecherche.Text = "";
                txbDvdNumRecherche.Text = "";
                Rayon rayon = (Rayon)cbxDvdRayons.SelectedItem;
                List<Dvd> Dvd = lesDvd.FindAll(x => x.Rayon.Equals(rayon.Libelle));
                RemplirDvdListe(Dvd);
                cbxDvdGenres.SelectedIndex = -1;
                cbxDvdPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Sur la sélection d'une ligne ou cellule dans le grid
        /// affichage des informations du dvd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvDvdListe_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDvdListe.CurrentCell != null)
            {
                try
                {
                    Dvd dvd = (Dvd)bdgDvdListe.List[bdgDvdListe.Position];
                    AfficheDvdInfos(dvd);
                }
                catch
                {
                    VideDvdZones();
                }
            }
            else
            {
                VideDvdInfos();
            }
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des Dvd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDvdAnnulPublics_Click(object sender, EventArgs e)
        {
            RemplirDvdListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des Dvd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDvdAnnulRayons_Click(object sender, EventArgs e)
        {
            RemplirDvdListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des Dvd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDvdAnnulGenres_Click(object sender, EventArgs e)
        {
            RemplirDvdListeComplete();
        }

        /// <summary>
        /// Affichage de la liste complète des Dvd
        /// et annulation de toutes les recherches et filtres
        /// </summary>
        private void RemplirDvdListeComplete()
        {
            RemplirDvdListe(lesDvd);
            VideDvdZones();
        }

        /// <summary>
        /// vide les zones de recherche et de filtre
        /// </summary>
        private void VideDvdZones()
        {
            cbxDvdGenres.SelectedIndex = -1;
            cbxDvdRayons.SelectedIndex = -1;
            cbxDvdPublics.SelectedIndex = -1;
            txbDvdNumRecherche.Text = "";
            txbDvdTitreRecherche.Text = "";
        }

        /// <summary>
        /// Tri sur les colonnes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvDvdListe_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            VideDvdZones();
            string titreColonne = dgvDvdListe.Columns[e.ColumnIndex].HeaderText;
            List<Dvd> sortedList = new List<Dvd>();
            switch (titreColonne)
            {
                case "Id":
                    sortedList = lesDvd.OrderBy(o => o.Id).ToList();
                    break;
                case "Titre":
                    sortedList = lesDvd.OrderBy(o => o.Titre).ToList();
                    break;
                case "Duree":
                    sortedList = lesDvd.OrderBy(o => o.Duree).ToList();
                    break;
                case "Realisateur":
                    sortedList = lesDvd.OrderBy(o => o.Realisateur).ToList();
                    break;
                case "Genre":
                    sortedList = lesDvd.OrderBy(o => o.Genre).ToList();
                    break;
                case "Public":
                    sortedList = lesDvd.OrderBy(o => o.Public).ToList();
                    break;
                case "Rayon":
                    sortedList = lesDvd.OrderBy(o => o.Rayon).ToList();
                    break;
            }
            RemplirDvdListe(sortedList);
        }

        // Sources permettant de remplir les combos d'ajout ou de modification d'un dvd (genre, public, rayon)
        private readonly BindingSource bdgGenresDvd = new BindingSource();
        private readonly BindingSource bdgPublicsDvd = new BindingSource();
        private readonly BindingSource bdgRayonsDvd = new BindingSource();

        /// <summary>
        /// Booléen pour savoir si une modification d'un dvd est demandée
        /// </summary>
        private Boolean enCoursDeModificationDvd = false;

        /// <summary>
        /// Modification d'affichage suivant si on est en cours de modification ou d'ajout d'un dvd
        /// </summary>
        /// <param name="modification">modification en cours ou non</param>
        private void EnCoursDeModificationDvd(Boolean modification)
        {
            enCoursDeModificationDvd = modification;
            grpDvdRecherche.Enabled = !modification;
            grpDvdInfos.Enabled = !modification;
            if (modification)
            {
                grpDvdActions.Text = "Modifier un dvd";
                txbDvdNumeroActions.Enabled = false;
            }
            else
            {
                grpDvdActions.Text = "Ajouter un dvd";
                txbDvdNumeroActions.Text = "";
                txbDvdDureeActions.Text = "";
                txbDvdTitreActions.Text = "";
                txbDvdRealisateurActions.Text = "";
                txbDvdSynopsisActions.Text = "";
                txbDvdImageActions.Text = "";
                pcbDvdImageActions.Image = null;
                txbDvdNumeroActions.Enabled = true;
            }
        }

        /// <summary>
        /// Vide les zones d'affichage des informations du dvd dans la zone d'ajout ou de modification
        /// </summary>
        private void VideDvdInfosActions()
        {
            txbDvdRealisateurActions.Text = "";
            txbDvdSynopsisActions.Text = "";
            txbDvdImageActions.Text = "";
            txbDvdDureeActions.Text = "";
            txbDvdNumeroActions.Text = "";
            txbDvdTitreActions.Text = "";
            pcbDvdImageActions.Image = null;
        }

        /// <summary>
        /// Recherche image sur disque (pour le dvd à insérer)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRechercheDvdImage_Click(object sender, EventArgs e)
        {
            string filePath = "";
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                // positionnement à la racine du disque où se trouve le dossier actuel
                InitialDirectory = Path.GetPathRoot(Environment.CurrentDirectory),
                Filter = "Files|*.jpg;*.bmp;*.jpeg;*.png;*.gif"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;
            }
            txbDvdImageActions.Text = filePath;
            try
            {
                pcbDvdImageActions.Image = Image.FromFile(filePath);
            }
            catch
            {
                pcbDvdImageActions.Image = null;
            }
        }

        /// <summary>
        /// Demande de modification d'un dvd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnModificationDvd_Click(object sender, EventArgs e)
        {
            if (dgvDvdListe.SelectedRows.Count > 0)
            {
                EnCoursDeModificationDvd(true);
                Dvd dvd = (Dvd)bdgDvdListe.List[bdgDvdListe.Position];
                txbDvdNumeroActions.Text = dvd.Id;
                txbDvdDureeActions.Text = dvd.Duree.ToString();
                txbDvdTitreActions.Text = dvd.Titre;
                txbDvdRealisateurActions.Text = dvd.Realisateur;
                txbDvdSynopsisActions.Text = dvd.Synopsis;
                cbxDvdGenresActions.SelectedIndex = cbxDvdGenresActions.FindStringExact(dvd.Genre);
                cbxDvdPublicsActions.SelectedIndex = cbxDvdPublicsActions.FindStringExact(dvd.Public);
                cbxDvdRayonsActions.SelectedIndex = cbxDvdRayonsActions.FindStringExact(dvd.Rayon);
                txbDvdImageActions.Text = dvd.Image;
                if (!txbDvdImageActions.Text.Equals(""))
                {
                    pcbDvdImageActions.Image = Image.FromFile(txbDvdImageActions.Text);
                }
            }
            else
            {
                MessageBox.Show("Une ligne parmi la liste des dvd doit être sélectionnée.", "Information");
            }
        }

        /// <summary>
        /// Demande de suppression d'un dvd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSuppressionDvd_Click(object sender, EventArgs e)
        {
            if (dgvDvdListe.SelectedRows.Count > 0)
            {
                Dvd dvd = (Dvd)bdgDvdListe.List[bdgDvdListe.Position];
                lesExemplaires = controller.GetExemplaires(dvd.Id);
                lesCommandesDvd = controller.GetCommandeDocuments(dvd.Id);
                int nbExemplaires = 0;
                foreach (Exemplaire exemplaire in lesExemplaires)
                {
                    if (exemplaire.Id.Equals(dvd.Id))
                    {
                        nbExemplaires++;
                    }
                }

                int nbCommandes = 0;
                foreach (CommandeDocument commandeDvd in lesCommandesDvd)
                {
                    if (commandeDvd.IdLivreDvd.Equals(dvd.Id))
                    {
                        nbCommandes++;
                    }
                }

                if (nbExemplaires == 0 && nbCommandes == 0)
                {
                    if (MessageBox.Show("Souhaitez-vous vraiment supprimer le dvd " + dvd.Titre + " ?", "Confirmation de suppression", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        if (controller.SupprimerDvd(dvd))
                        {
                            lesDvd = controller.GetAllDvd();
                            RemplirDvdListeComplete();
                        }
                        else
                        {
                            MessageBox.Show("problème rencontré lors de la suppression", "Erreur");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Veiller à ce qu'aucun exemplaire et qu'aucune commande ne soit rattaché à ce dvd pour pouvoir le supprimer", "Information");
                }
            }
            else
            {
                MessageBox.Show("Une ligne parmi la liste des dvd doit être sélectionnée.", "Information");
            }
        }

        /// <summary>
        /// Demande d'enregistrement de l'ajout ou de la modification d'un dvd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDvdActionsValider_Click(object sender, EventArgs e)
        {
            if (!txbDvdNumeroActions.Text.Equals("") && !txbDvdDureeActions.Text.Equals("") && cbxDvdGenresActions.SelectedIndex != -1 && cbxDvdPublicsActions.SelectedIndex != -1 && cbxDvdRayonsActions.SelectedIndex != -1)
            {
                try
                {
                    if (MessageBox.Show("Voulez-vous vraiment valider ces informations ?", "Confirmation de validation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        Genre unGenre = (Genre)cbxDvdGenresActions.SelectedItem;
                        Public unPublic = (Public)cbxDvdPublicsActions.SelectedItem;
                        Rayon unRayon = (Rayon)cbxDvdRayonsActions.SelectedItem;
                        string id = txbDvdNumeroActions.Text;
                        int duree = int.Parse(txbDvdDureeActions.Text);
                        string titre = txbDvdTitreActions.Text;
                        string realisateur = txbDvdRealisateurActions.Text;
                        string synopsis = txbDvdSynopsisActions.Text;
                        string idGenre = unGenre.Id;
                        string genre = unGenre.Libelle;
                        string idPublic = unPublic.Id;
                        string lePublic = unPublic.Libelle;
                        string idRayon = unRayon.Id;
                        string rayon = unRayon.Libelle;
                        string image = txbDvdImageActions.Text;

                        lesDvd = controller.GetAllDvd();
                        int nbIdDvd = 0;
                        foreach (Dvd leDvd in lesDvd)
                        {
                            if (leDvd.Id.Equals(id))
                            {
                                nbIdDvd++;
                            }
                        }

                        Dvd dvd = new Dvd(id, titre, image, duree, realisateur, synopsis, idGenre, genre, idPublic, lePublic, idRayon, rayon);

                        if (enCoursDeModificationDvd)
                        {
                            if (controller.ModifierDvd(dvd))
                            {
                                EnCoursDeModificationDvd(false);
                                lesDvd = controller.GetAllDvd();
                                RemplirDvdListeComplete();
                                VideDvdInfosActions();

                            }
                            else
                            {
                                MessageBox.Show("problème rencontré lors de la modification", "Erreur");
                            }
                        }
                        else
                        {
                            if (nbIdDvd == 0)
                            {
                                if (controller.AjouterDvd(dvd))
                                {
                                    lesDvd = controller.GetAllDvd();
                                    RemplirDvdListeComplete();
                                    VideDvdInfosActions();
                                }
                                else
                                {
                                    MessageBox.Show("problème rencontré lors de l'ajout", "Erreur");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Veuillez saisir un numéro de document qui n'est pas déjà existant", "Information");
                            }
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("problème lors de la validation (veillez à bien entrer une valeur numérique pour la durée)", "Erreur");
                    EnCoursDeModificationDvd(false);
                    VideDvdInfosActions();
                }
            }
            else
            {
                MessageBox.Show("Veuillez remplir au minimum le numéro de document, la durée, le genre, le public et le rayon", "Information");
            }
        }
        /// <summary>
        /// Demande d'annulation de l'ajout ou de la modification d'un dvd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDvdActionsAnnuler_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Voulez-vous vraiment annuler ?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                EnCoursDeModificationDvd(false);
            }
        }

        #endregion

        #region Onglet Revues
        private readonly BindingSource bdgRevuesListe = new BindingSource();
        private List<Revue> lesRevues = new List<Revue>();

        /// <summary>
        /// Ouverture de l'onglet Revues : 
        /// appel des méthodes pour remplir le datagrid des revues et des combos (genre, rayon, public)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabRevues_Enter(object sender, EventArgs e)
        {
            lesRevues = controller.GetAllRevues();
            RemplirComboCategorie(controller.GetAllGenres(), bdgGenres, cbxRevuesGenres);
            RemplirComboCategorie(controller.GetAllPublics(), bdgPublics, cbxRevuesPublics);
            RemplirComboCategorie(controller.GetAllRayons(), bdgRayons, cbxRevuesRayons);

            RemplirComboCategorie(controller.GetAllGenres(), bdgGenresRevues, cbxRevuesGenresActions);
            RemplirComboCategorie(controller.GetAllPublics(), bdgPublicsRevues, cbxRevuesPublicsActions);
            RemplirComboCategorie(controller.GetAllRayons(), bdgRayonsRevues, cbxRevuesRayonsActions);

            RemplirRevuesListeComplete();
        }

        /// <summary>
        /// Remplit le dategrid avec la liste reçue en paramètre
        /// </summary>
        /// <param name="revues"></param>
        private void RemplirRevuesListe(List<Revue> revues)
        {
            bdgRevuesListe.DataSource = revues;
            dgvRevuesListe.DataSource = bdgRevuesListe;
            dgvRevuesListe.Columns["idRayon"].Visible = false;
            dgvRevuesListe.Columns["idGenre"].Visible = false;
            dgvRevuesListe.Columns["idPublic"].Visible = false;
            dgvRevuesListe.Columns["image"].Visible = false;
            dgvRevuesListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvRevuesListe.Columns["id"].DisplayIndex = 0;
            dgvRevuesListe.Columns["titre"].DisplayIndex = 1;
        }

        /// <summary>
        /// Recherche et affichage de la revue dont on a saisi le numéro.
        /// Si non trouvé, affichage d'un MessageBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRevuesNumRecherche_Click(object sender, EventArgs e)
        {
            if (!txbRevuesNumRecherche.Text.Equals(""))
            {
                txbRevuesTitreRecherche.Text = "";
                cbxRevuesGenres.SelectedIndex = -1;
                cbxRevuesRayons.SelectedIndex = -1;
                cbxRevuesPublics.SelectedIndex = -1;
                Revue revue = lesRevues.Find(x => x.Id.Equals(txbRevuesNumRecherche.Text));
                if (revue != null)
                {
                    List<Revue> revues = new List<Revue>() { revue };
                    RemplirRevuesListe(revues);
                }
                else
                {
                    MessageBox.Show("numéro introuvable");
                    RemplirRevuesListeComplete();
                }
            }
            else
            {
                RemplirRevuesListeComplete();
            }
        }

        /// <summary>
        /// Recherche et affichage des revues dont le titre matche acec la saisie.
        /// Cette procédure est exécutée à chaque ajout ou suppression de caractère
        /// dans le textBox de saisie.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txbRevuesTitreRecherche_TextChanged(object sender, EventArgs e)
        {
            if (!txbRevuesTitreRecherche.Text.Equals(""))
            {
                cbxRevuesGenres.SelectedIndex = -1;
                cbxRevuesRayons.SelectedIndex = -1;
                cbxRevuesPublics.SelectedIndex = -1;
                txbRevuesNumRecherche.Text = "";
                List<Revue> lesRevuesParTitre;
                lesRevuesParTitre = lesRevues.FindAll(x => x.Titre.ToLower().Contains(txbRevuesTitreRecherche.Text.ToLower()));
                RemplirRevuesListe(lesRevuesParTitre);
            }
            else
            {
                // si la zone de saisie est vide et aucun élément combo sélectionné, réaffichage de la liste complète
                if (cbxRevuesGenres.SelectedIndex < 0 && cbxRevuesPublics.SelectedIndex < 0 && cbxRevuesRayons.SelectedIndex < 0
                    && txbRevuesNumRecherche.Text.Equals(""))
                {
                    RemplirRevuesListeComplete();
                }
            }
        }

        /// <summary>
        /// Affichage des informations de la revue sélectionné
        /// </summary>
        /// <param name="revue">la revue</param>
        private void AfficheRevuesInfos(Revue revue)
        {
            txbRevuesPeriodicite.Text = revue.Periodicite;
            txbRevuesImage.Text = revue.Image;
            txbRevuesDateMiseADispo.Text = revue.DelaiMiseADispo.ToString();
            txbRevuesNumero.Text = revue.Id;
            txbRevuesGenre.Text = revue.Genre;
            txbRevuesPublic.Text = revue.Public;
            txbRevuesRayon.Text = revue.Rayon;
            txbRevuesTitre.Text = revue.Titre;
            string image = revue.Image;
            try
            {
                pcbRevuesImage.Image = Image.FromFile(image);
            }
            catch
            {
                pcbRevuesImage.Image = null;
            }
        }

        /// <summary>
        /// Vide les zones d'affichage des informations de la reuve
        /// </summary>
        private void VideRevuesInfos()
        {
            txbRevuesPeriodicite.Text = "";
            txbRevuesImage.Text = "";
            txbRevuesDateMiseADispo.Text = "";
            txbRevuesNumero.Text = "";
            txbRevuesGenre.Text = "";
            txbRevuesPublic.Text = "";
            txbRevuesRayon.Text = "";
            txbRevuesTitre.Text = "";
            pcbRevuesImage.Image = null;
        }

        /// <summary>
        /// Filtre sur le genre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxRevuesGenres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxRevuesGenres.SelectedIndex >= 0)
            {
                txbRevuesTitreRecherche.Text = "";
                txbRevuesNumRecherche.Text = "";
                Genre genre = (Genre)cbxRevuesGenres.SelectedItem;
                List<Revue> revues = lesRevues.FindAll(x => x.Genre.Equals(genre.Libelle));
                RemplirRevuesListe(revues);
                cbxRevuesRayons.SelectedIndex = -1;
                cbxRevuesPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur la catégorie de public
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxRevuesPublics_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxRevuesPublics.SelectedIndex >= 0)
            {
                txbRevuesTitreRecherche.Text = "";
                txbRevuesNumRecherche.Text = "";
                Public lePublic = (Public)cbxRevuesPublics.SelectedItem;
                List<Revue> revues = lesRevues.FindAll(x => x.Public.Equals(lePublic.Libelle));
                RemplirRevuesListe(revues);
                cbxRevuesRayons.SelectedIndex = -1;
                cbxRevuesGenres.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur le rayon
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxRevuesRayons_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxRevuesRayons.SelectedIndex >= 0)
            {
                txbRevuesTitreRecherche.Text = "";
                txbRevuesNumRecherche.Text = "";
                Rayon rayon = (Rayon)cbxRevuesRayons.SelectedItem;
                List<Revue> revues = lesRevues.FindAll(x => x.Rayon.Equals(rayon.Libelle));
                RemplirRevuesListe(revues);
                cbxRevuesGenres.SelectedIndex = -1;
                cbxRevuesPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Sur la sélection d'une ligne ou cellule dans le grid
        /// affichage des informations de la revue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvRevuesListe_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvRevuesListe.CurrentCell != null)
            {
                try
                {
                    Revue revue = (Revue)bdgRevuesListe.List[bdgRevuesListe.Position];
                    AfficheRevuesInfos(revue);
                }
                catch
                {
                    VideRevuesZones();
                }
            }
            else
            {
                VideRevuesInfos();
            }
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des revues
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRevuesAnnulPublics_Click(object sender, EventArgs e)
        {
            RemplirRevuesListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des revues
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRevuesAnnulRayons_Click(object sender, EventArgs e)
        {
            RemplirRevuesListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des revues
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRevuesAnnulGenres_Click(object sender, EventArgs e)
        {
            RemplirRevuesListeComplete();
        }

        /// <summary>
        /// Affichage de la liste complète des revues
        /// et annulation de toutes les recherches et filtres
        /// </summary>
        private void RemplirRevuesListeComplete()
        {
            RemplirRevuesListe(lesRevues);
            VideRevuesZones();
        }

        /// <summary>
        /// vide les zones de recherche et de filtre
        /// </summary>
        private void VideRevuesZones()
        {
            cbxRevuesGenres.SelectedIndex = -1;
            cbxRevuesRayons.SelectedIndex = -1;
            cbxRevuesPublics.SelectedIndex = -1;
            txbRevuesNumRecherche.Text = "";
            txbRevuesTitreRecherche.Text = "";
        }

        /// <summary>
        /// Tri sur les colonnes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvRevuesListe_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            VideRevuesZones();
            string titreColonne = dgvRevuesListe.Columns[e.ColumnIndex].HeaderText;
            List<Revue> sortedList = new List<Revue>();
            switch (titreColonne)
            {
                case "Id":
                    sortedList = lesRevues.OrderBy(o => o.Id).ToList();
                    break;
                case "Titre":
                    sortedList = lesRevues.OrderBy(o => o.Titre).ToList();
                    break;
                case "Periodicite":
                    sortedList = lesRevues.OrderBy(o => o.Periodicite).ToList();
                    break;
                case "DelaiMiseADispo":
                    sortedList = lesRevues.OrderBy(o => o.DelaiMiseADispo).ToList();
                    break;
                case "Genre":
                    sortedList = lesRevues.OrderBy(o => o.Genre).ToList();
                    break;
                case "Public":
                    sortedList = lesRevues.OrderBy(o => o.Public).ToList();
                    break;
                case "Rayon":
                    sortedList = lesRevues.OrderBy(o => o.Rayon).ToList();
                    break;
            }
            RemplirRevuesListe(sortedList);
        }

        // Sources permettant de remplir les combos d'ajout ou de modification d'une revue (genre, public, rayon)
        private readonly BindingSource bdgGenresRevues = new BindingSource();
        private readonly BindingSource bdgPublicsRevues = new BindingSource();
        private readonly BindingSource bdgRayonsRevues = new BindingSource();

        /// <summary>
        /// Booléen pour savoir si une modification d'une revue est demandée
        /// </summary>
        private Boolean enCoursDeModificationRevue = false;

        /// <summary>
        /// Modification d'affichage suivant si on est en cours de modification ou d'ajout d'une revue
        /// </summary>
        /// <param name="modification">modification en cours ou non</param>
        private void EnCoursDeModificationRevue(Boolean modification)
        {
            enCoursDeModificationRevue = modification;
            grpRevuesRecherche.Enabled = !modification;
            grpRevuesInfos.Enabled = !modification;
            if (modification)
            {
                grpRevuesActions.Text = "Modifier une revue";
                txbRevuesNumeroActions.Enabled = false;
            }
            else
            {
                grpRevuesActions.Text = "Ajouter une revue";
                txbRevuesNumeroActions.Text = "";
                txbRevuesPeriodiciteActions.Text = "";
                txbRevuesTitreActions.Text = "";
                txbRevuesDateMiseADispoActions.Text = "";
                txbRevuesImageActions.Text = "";
                pcbRevuesImageActions.Image = null;
                txbRevuesNumeroActions.Enabled = true;
            }
        }

        /// <summary>
        /// Vide les zones d'affichage des informations de la revue dans la zone d'ajout ou de modification
        /// </summary>
        private void VideRevuesInfosActions()
        {
            txbRevuesDateMiseADispoActions.Text = "";
            txbRevuesImageActions.Text = "";
            txbRevuesPeriodiciteActions.Text = "";
            txbRevuesNumeroActions.Text = "";
            txbRevuesTitreActions.Text = "";
            pcbRevuesImageActions.Image = null;
        }

        /// <summary>
        /// Recherche image sur disque (pour la revue à insérer)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRechercheRevuesImage_Click(object sender, EventArgs e)
        {
            string filePath = "";
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                // positionnement à la racine du disque où se trouve le dossier actuel
                InitialDirectory = Path.GetPathRoot(Environment.CurrentDirectory),
                Filter = "Files|*.jpg;*.bmp;*.jpeg;*.png;*.gif"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;
            }
            txbRevuesImageActions.Text = filePath;
            try
            {
                pcbRevuesImageActions.Image = Image.FromFile(filePath);
            }
            catch
            {
                pcbRevuesImageActions.Image = null;
            }
        }

        /// <summary>
        /// Demande de modification d'une revue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnModificationRevues_Click(object sender, EventArgs e)
        {
            if (dgvRevuesListe.SelectedRows.Count > 0)
            {
                EnCoursDeModificationRevue(true);
                Revue revue = (Revue)bdgRevuesListe.List[bdgRevuesListe.Position];
                txbRevuesNumeroActions.Text = revue.Id;
                txbRevuesPeriodiciteActions.Text = revue.Periodicite;
                txbRevuesTitreActions.Text = revue.Titre;
                txbRevuesDateMiseADispoActions.Text = revue.DelaiMiseADispo.ToString();
                cbxRevuesGenresActions.SelectedIndex = cbxRevuesGenresActions.FindStringExact(revue.Genre);
                cbxRevuesPublicsActions.SelectedIndex = cbxRevuesPublicsActions.FindStringExact(revue.Public);
                cbxRevuesRayonsActions.SelectedIndex = cbxRevuesRayonsActions.FindStringExact(revue.Rayon);
                txbRevuesImageActions.Text = revue.Image;
                if (!txbRevuesImageActions.Text.Equals(""))
                {
                    pcbRevuesImageActions.Image = Image.FromFile(txbRevuesImageActions.Text);
                }
            }
            else
            {
                MessageBox.Show("Une ligne parmi la liste des revues doit être sélectionnée.", "Information");
            }
        }

        /// <summary>
        /// Demande de suppression d'une revue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSuppressionRevues_Click(object sender, EventArgs e)
        {
            if (dgvRevuesListe.SelectedRows.Count > 0)
            {
                Revue revue = (Revue)bdgRevuesListe.List[bdgRevuesListe.Position];
                lesExemplaires = controller.GetExemplaires(revue.Id);
                // Début de l'emplacement pour la future initilisation de la liste des commandes

                // Fin de l'emplacement pour la future initilisation de la liste des commandes
                int nbExemplaires = 0;
                foreach (Exemplaire exemplaire in lesExemplaires)
                {
                    if (exemplaire.Id.Equals(revue.Id))
                    {
                        nbExemplaires++;
                    }
                }
                // Début de l'emplacement pour la future vérification des potentielles commandes rattachées à la revue

                // Fin de l'emplacement pour la future vérification des potentielles commandes rattachées à la revue
                if (nbExemplaires == 0)
                {
                    if (MessageBox.Show("Souhaitez-vous vraiment supprimer la revue " + revue.Titre + " ?", "Confirmation de suppression", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        if (controller.SupprimerRevue(revue))
                        {
                            lesRevues = controller.GetAllRevues();
                            RemplirRevuesListeComplete();
                        }
                        else
                        {
                            MessageBox.Show("problème rencontré lors de la suppression", "Erreur");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Veiller à ce qu'aucun exemplaire ne soit rattaché à cette revue pour pouvoir la supprimer", "Information");
                }
            }
            else
            {
                MessageBox.Show("Une ligne parmi la liste des revues doit être sélectionnée.", "Information");
            }
        }

        /// <summary>
        /// Demande d'enregistrement de l'ajout ou de la modification d'une revue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRevuesActionsValider_Click(object sender, EventArgs e)
        {
            if (!txbRevuesNumeroActions.Text.Equals("") && !txbRevuesDateMiseADispoActions.Text.Equals("") && cbxRevuesGenresActions.SelectedIndex != -1 && cbxRevuesPublicsActions.SelectedIndex != -1 && cbxRevuesRayonsActions.SelectedIndex != -1)
            {
                try
                {
                    if (MessageBox.Show("Voulez-vous vraiment valider ces informations ?", "Confirmation de validation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        Genre unGenre = (Genre)cbxRevuesGenresActions.SelectedItem;
                        Public unPublic = (Public)cbxRevuesPublicsActions.SelectedItem;
                        Rayon unRayon = (Rayon)cbxRevuesRayonsActions.SelectedItem;
                        string id = txbRevuesNumeroActions.Text;
                        string periodicite = txbRevuesPeriodiciteActions.Text;
                        string titre = txbRevuesTitreActions.Text;
                        int delaiMiseADispo = int.Parse(txbRevuesDateMiseADispoActions.Text);
                        string idGenre = unGenre.Id;
                        string genre = unGenre.Libelle;
                        string idPublic = unPublic.Id;
                        string lePublic = unPublic.Libelle;
                        string idRayon = unRayon.Id;
                        string rayon = unRayon.Libelle;
                        string image = txbRevuesImageActions.Text;

                        lesRevues = controller.GetAllRevues();
                        int nbIdRevues = 0;
                        foreach (Revue laRevue in lesRevues)
                        {
                            if (laRevue.Id.Equals(id))
                            {
                                nbIdRevues++;
                            }
                        }

                        Revue revue = new Revue(id, titre, image, idGenre, genre, idPublic, lePublic, idRayon, rayon, periodicite, delaiMiseADispo);

                        if (enCoursDeModificationRevue)
                        {
                            if (controller.ModifierRevue(revue))
                            {
                                EnCoursDeModificationRevue(false);
                                lesRevues = controller.GetAllRevues();
                                RemplirRevuesListeComplete();
                                VideRevuesInfosActions();

                            }
                            else
                            {
                                MessageBox.Show("problème rencontré lors de la modification", "Erreur");
                            }
                        }
                        else
                        {
                            if (nbIdRevues == 0)
                            {
                                if (controller.AjouterRevue(revue))
                                {
                                    lesRevues = controller.GetAllRevues();
                                    RemplirRevuesListeComplete();
                                    VideRevuesInfosActions();
                                }
                                else
                                {
                                    MessageBox.Show("problème rencontré lors de l'ajout", "Erreur");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Veuillez saisir un numéro de document qui n'est pas déjà existant", "Information");
                            }
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("problème lors de la validation (veillez à bien entrer une valeur numérique pour le délai de mise à dispo)", "Erreur");
                    EnCoursDeModificationRevue(false);
                    VideRevuesInfosActions();
                }
            }
            else
            {
                MessageBox.Show("Veuillez remplir au minimum le numéro de document, le délai de mise à dispo, le genre, le public et le rayon", "Information");
            }
        }

        /// <summary>
        /// Demande d'annulation de l'ajout ou de la modification d'une revue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRevuesActionsAnnuler_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Voulez-vous vraiment annuler ?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                EnCoursDeModificationRevue(false);
            }
        }

        #endregion

        #region Onglet Parutions
        private readonly BindingSource bdgExemplairesListe = new BindingSource();
        private List<Exemplaire> lesExemplaires = new List<Exemplaire>();
        const string ETATNEUF = "00001";

        /// <summary>
        /// Ouverture de l'onglet : récupère le revues et vide tous les champs.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabReceptionRevue_Enter(object sender, EventArgs e)
        {
            lesRevues = controller.GetAllRevues();
            txbReceptionRevueNumero.Text = "";
        }

        /// <summary>
        /// Remplit le dategrid des exemplaires avec la liste reçue en paramètre
        /// </summary>
        /// <param name="exemplaires">liste d'exemplaires</param>
        private void RemplirReceptionExemplairesListe(List<Exemplaire> exemplaires)
        {
            if (exemplaires != null)
            {
                bdgExemplairesListe.DataSource = exemplaires;
                dgvReceptionExemplairesListe.DataSource = bdgExemplairesListe;
                dgvReceptionExemplairesListe.Columns["idEtat"].Visible = false;
                dgvReceptionExemplairesListe.Columns["id"].Visible = false;
                dgvReceptionExemplairesListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgvReceptionExemplairesListe.Columns["numero"].DisplayIndex = 0;
                dgvReceptionExemplairesListe.Columns["dateAchat"].DisplayIndex = 1;
            }
            else
            {
                bdgExemplairesListe.DataSource = null;
            }
        }

        /// <summary>
        /// Recherche d'un numéro de revue et affiche ses informations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReceptionRechercher_Click(object sender, EventArgs e)
        {
            if (!txbReceptionRevueNumero.Text.Equals(""))
            {
                Revue revue = lesRevues.Find(x => x.Id.Equals(txbReceptionRevueNumero.Text));
                if (revue != null)
                {
                    AfficheReceptionRevueInfos(revue);
                }
                else
                {
                    MessageBox.Show("numéro introuvable");
                }
            }
        }

        /// <summary>
        /// Si le numéro de revue est modifié, la zone de l'exemplaire est vidée et inactive
        /// les informations de la revue son aussi effacées
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txbReceptionRevueNumero_TextChanged(object sender, EventArgs e)
        {
            txbReceptionRevuePeriodicite.Text = "";
            txbReceptionRevueImage.Text = "";
            txbReceptionRevueDelaiMiseADispo.Text = "";
            txbReceptionRevueGenre.Text = "";
            txbReceptionRevuePublic.Text = "";
            txbReceptionRevueRayon.Text = "";
            txbReceptionRevueTitre.Text = "";
            pcbReceptionRevueImage.Image = null;
            RemplirReceptionExemplairesListe(null);
            AccesReceptionExemplaireGroupBox(false);
        }

        /// <summary>
        /// Affichage des informations de la revue sélectionnée et les exemplaires
        /// </summary>
        /// <param name="revue">la revue</param>
        private void AfficheReceptionRevueInfos(Revue revue)
        {
            // informations sur la revue
            txbReceptionRevuePeriodicite.Text = revue.Periodicite;
            txbReceptionRevueImage.Text = revue.Image;
            txbReceptionRevueDelaiMiseADispo.Text = revue.DelaiMiseADispo.ToString();
            txbReceptionRevueNumero.Text = revue.Id;
            txbReceptionRevueGenre.Text = revue.Genre;
            txbReceptionRevuePublic.Text = revue.Public;
            txbReceptionRevueRayon.Text = revue.Rayon;
            txbReceptionRevueTitre.Text = revue.Titre;
            string image = revue.Image;
            try
            {
                pcbReceptionRevueImage.Image = Image.FromFile(image);
            }
            catch
            {
                pcbReceptionRevueImage.Image = null;
            }
            // affiche la liste des exemplaires de la revue
            AfficheReceptionExemplairesRevue();
        }

        /// <summary>
        /// Récupère et affiche les exemplaires d'une revue
        /// </summary>
        private void AfficheReceptionExemplairesRevue()
        {
            string idDocuement = txbReceptionRevueNumero.Text;
            lesExemplaires = controller.GetExemplaires(idDocuement);
            RemplirReceptionExemplairesListe(lesExemplaires);
            AccesReceptionExemplaireGroupBox(true);
        }

        /// <summary>
        /// Permet ou interdit l'accès à la gestion de la réception d'un exemplaire
        /// et vide les objets graphiques
        /// </summary>
        /// <param name="acces">true ou false</param>
        private void AccesReceptionExemplaireGroupBox(bool acces)
        {
            grpReceptionExemplaire.Enabled = acces;
            txbReceptionExemplaireImage.Text = "";
            txbReceptionExemplaireNumero.Text = "";
            pcbReceptionExemplaireImage.Image = null;
            dtpReceptionExemplaireDate.Value = DateTime.Now;
        }

        /// <summary>
        /// Recherche image sur disque (pour l'exemplaire à insérer)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReceptionExemplaireImage_Click(object sender, EventArgs e)
        {
            string filePath = "";
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                // positionnement à la racine du disque où se trouve le dossier actuel
                InitialDirectory = Path.GetPathRoot(Environment.CurrentDirectory),
                Filter = "Files|*.jpg;*.bmp;*.jpeg;*.png;*.gif"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;
            }
            txbReceptionExemplaireImage.Text = filePath;
            try
            {
                pcbReceptionExemplaireImage.Image = Image.FromFile(filePath);
            }
            catch
            {
                pcbReceptionExemplaireImage.Image = null;
            }
        }

        /// <summary>
        /// Enregistrement du nouvel exemplaire
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReceptionExemplaireValider_Click(object sender, EventArgs e)
        {
            if (!txbReceptionExemplaireNumero.Text.Equals(""))
            {
                try
                {
                    int numero = int.Parse(txbReceptionExemplaireNumero.Text);
                    DateTime dateAchat = dtpReceptionExemplaireDate.Value;
                    string photo = txbReceptionExemplaireImage.Text;
                    string idEtat = ETATNEUF;
                    string idDocument = txbReceptionRevueNumero.Text;
                    Exemplaire exemplaire = new Exemplaire(numero, dateAchat, photo, idEtat, idDocument);
                    if (controller.CreerExemplaire(exemplaire))
                    {
                        AfficheReceptionExemplairesRevue();
                    }
                    else
                    {
                        MessageBox.Show("numéro de publication déjà existant", "Erreur");
                    }
                }
                catch
                {
                    MessageBox.Show("le numéro de parution doit être numérique", "Information");
                    txbReceptionExemplaireNumero.Text = "";
                    txbReceptionExemplaireNumero.Focus();
                }
            }
            else
            {
                MessageBox.Show("numéro de parution obligatoire", "Information");
            }
        }

        /// <summary>
        /// Tri sur une colonne
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvExemplairesListe_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string titreColonne = dgvReceptionExemplairesListe.Columns[e.ColumnIndex].HeaderText;
            List<Exemplaire> sortedList = new List<Exemplaire>();
            switch (titreColonne)
            {
                case "Numero":
                    sortedList = lesExemplaires.OrderBy(o => o.Numero).Reverse().ToList();
                    break;
                case "DateAchat":
                    sortedList = lesExemplaires.OrderBy(o => o.DateAchat).Reverse().ToList();
                    break;
                case "Photo":
                    sortedList = lesExemplaires.OrderBy(o => o.Photo).ToList();
                    break;
            }
            RemplirReceptionExemplairesListe(sortedList);
        }

        /// <summary>
        /// affichage de l'image de l'exemplaire suite à la sélection d'un exemplaire dans la liste
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvReceptionExemplairesListe_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvReceptionExemplairesListe.CurrentCell != null)
            {
                Exemplaire exemplaire = (Exemplaire)bdgExemplairesListe.List[bdgExemplairesListe.Position];
                string image = exemplaire.Photo;
                try
                {
                    pcbReceptionExemplaireRevueImage.Image = Image.FromFile(image);
                }
                catch
                {
                    pcbReceptionExemplaireRevueImage.Image = null;
                }
            }
            else
            {
                pcbReceptionExemplaireRevueImage.Image = null;
            }
        }

        #endregion

        #region Onglet Commandes Livres
        private readonly BindingSource bdgCommandesLivresListe = new BindingSource();
        private List<CommandeDocument> lesCommandesLivre = new List<CommandeDocument>();

        /// <summary>
        /// Ouverture de l'onglet : récupère les livres et vide tous les champs.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabCommandesLivres_Enter(object sender, EventArgs e)
        {
            lesLivres = controller.GetAllLivres();
            txbCommandesLivresNumero.Text = "";
            EnCoursDeModificationCommandeLivre(false);
            
        }

        /// <summary>
        /// Remplit le dategrid des commandes avec la liste reçue en paramètre
        /// </summary>
        /// <param name="commandesLivres">liste de commandes</param>
        private void RemplirCommandesLivreListe(List<CommandeDocument> commandesLivres)
        {
            if (commandesLivres != null)
            {
                bdgCommandesLivresListe.DataSource = commandesLivres;
                dgvReceptionCommandesLivresListe.DataSource = bdgCommandesLivresListe;
                dgvReceptionCommandesLivresListe.Columns["id"].Visible = false;
                dgvReceptionCommandesLivresListe.Columns["idLivreDvd"].Visible = false;
                dgvReceptionCommandesLivresListe.Columns["idSuivi"].Visible = false;
                dgvReceptionCommandesLivresListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgvReceptionCommandesLivresListe.Columns["dateCommande"].DisplayIndex = 0;
                dgvReceptionCommandesLivresListe.Columns["montant"].DisplayIndex = 1;
                dgvReceptionCommandesLivresListe.Columns["nbExemplaire"].DisplayIndex = 2;
                dgvReceptionCommandesLivresListe.Columns["libelle"].DisplayIndex = 3;
                dgvReceptionCommandesLivresListe.Columns["libelle"].HeaderText = "Suivi";
            }
            else
            {
                bdgCommandesLivresListe.DataSource = null;
            }
        }

        /// <summary>
        /// Recherche d'un numéro de livre et affiche ses informations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCommandesLivresRecherche_Click(object sender, EventArgs e)
        {
            if (!txbCommandesLivresNumero.Text.Equals(""))
            {
                Livre livre = lesLivres.Find(x => x.Id.Equals(txbCommandesLivresNumero.Text));
                if (livre != null)
                {
                    AfficheCommandesLivresInfos(livre);
                }
                else
                {
                    MessageBox.Show("numéro introuvable");
                }
            }
        }

        /// <summary>
        /// Si le numéro de livre est modifié, la zone de la commande est vidée et inactive
        /// les informations du livre sont aussi effacées
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txbCommandesLivresNumero_TextChanged(object sender, EventArgs e)
        {
            txbCommandesLivresIsbn.Text = "";
            txbCommandesLivresTitre.Text = "";
            txbCommandesLivresAuteur.Text = "";
            txbCommandesLivresCollection.Text = "";
            txbCommandesLivresGenre.Text = "";
            txbCommandesLivresPublic.Text = "";
            txbCommandesLivresRayon.Text = "";
            txbCommandesLivresImage.Text = "";
            pcbCommandesLivresImage.Image = null;
            RemplirCommandesLivreListe(null);
            AccesCommandesLivreGroupBox(false);
        }

        /// <summary>
        /// Affichage des informations du livre sélectionné et les commandes
        /// </summary>
        /// <param name="livre">le livre</param>
        private void AfficheCommandesLivresInfos(Livre livre)
        {
            // informations sur le livre
            txbCommandesLivresNumero.Text = livre.Id;
            txbCommandesLivresIsbn.Text = livre.Isbn;
            txbCommandesLivresTitre.Text = livre.Titre;
            txbCommandesLivresAuteur.Text = livre.Auteur;
            txbCommandesLivresCollection.Text = livre.Collection;
            txbCommandesLivresGenre.Text = livre.Genre;
            txbCommandesLivresPublic.Text = livre.Public;
            txbCommandesLivresRayon.Text = livre.Rayon;
            txbCommandesLivresImage.Text = livre.Image;
            string image = livre.Image;
            try
            {
                pcbCommandesLivresImage.Image = Image.FromFile(image);
            }
            catch
            {
                pcbCommandesLivresImage.Image = null;
            }
            // affiche la liste des commandes du livre
            AfficheCommandesLivre();
        }

        /// <summary>
        /// Récupère et affiche les commandes d'un livre
        /// </summary>
        private void AfficheCommandesLivre()
        {
            string idDocument = txbCommandesLivresNumero.Text;
            lesCommandesLivre = controller.GetCommandeDocuments(idDocument);
            RemplirCommandesLivreListe(lesCommandesLivre);
            AccesCommandesLivreGroupBox(true);
        }

        /// <summary>
        /// Permet ou interdit l'accès à la gestion de la réception d'une commande
        /// et vide les objets graphiques
        /// </summary>
        /// <param name="acces">true ou false</param>
        private void AccesCommandesLivreGroupBox(bool acces)
        {
            grpCommandesLivresCommande.Enabled = acces;
            txbCommandesLivresCommandeNumero.Text = "";
            txbCommandesLivresCommandeMontant.Text = "";
            txbCommandesLivresCommandeNbExemplaires.Text = "";
            dtpCommandesLivresCommandeDate.Value = DateTime.Now;
        }

        /// <summary>
        /// Enregistrement de la nouvelle commande
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCommandesLivresCommandeValider_Click(object sender, EventArgs e)
        {
            if (!txbCommandesLivresCommandeNumero.Text.Equals("") && !txbCommandesLivresCommandeMontant.Text.Equals("") && !txbCommandesLivresCommandeNbExemplaires.Text.Equals(""))
            {
                try
                {
                    string id = txbCommandesLivresCommandeNumero.Text;
                    DateTime dateCommande = dtpCommandesLivresCommandeDate.Value;
                    double montant = double.Parse(txbCommandesLivresCommandeMontant.Text);
                    int nbExemplaire = int.Parse(txbCommandesLivresCommandeNbExemplaires.Text);
                    string idLivreDvd = txbCommandesLivresNumero.Text;
                    string idSuivi = SUIVIENCOURS;
                    string libelleSuivi = SUIVIENCOURSLIBELLE;

                    lesCommandesLivre = controller.GetCommandeDocuments(idLivreDvd);
                    int nbIdCommandes = 0;
                    foreach (CommandeDocument laCommandeLivre in lesCommandesLivre)
                    {
                        if (laCommandeLivre.Id.Equals(id))
                        {
                            nbIdCommandes++;
                        }
                    }

                    CommandeDocument commandeDocument = new CommandeDocument(id, dateCommande, montant, nbExemplaire, idLivreDvd, idSuivi, libelleSuivi);
                    if (nbIdCommandes == 0)
                    {
                        if (controller.CreerCommandeDocument(commandeDocument))
                        {
                            AfficheCommandesLivre();
                        }
                        else
                        {
                            MessageBox.Show("problème rencontré lors de l'ajout", "Erreur");
                        }
                    }
                    else
                    {
                        MessageBox.Show("numéro de commande déjà existant", "Erreur");
                    }
                }
                catch
                {
                    MessageBox.Show("Le numéro de commande, le montant et le nombre d'exemplaires doivent être numérique", "Information");
                    txbCommandesLivresCommandeNumero.Text = "";
                    txbCommandesLivresCommandeMontant.Text = "";
                    txbCommandesLivresCommandeNbExemplaires.Text = "";
                    txbCommandesLivresCommandeNumero.Focus();
                }
            }
            else
            {
                MessageBox.Show("Veuillez remplir tous les champs", "Information");
            }
        }

        /// <summary>
        /// Tri sur une colonne
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvReceptionCommandesLivresListe_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string titreColonne = dgvReceptionCommandesLivresListe.Columns[e.ColumnIndex].HeaderText;
            List<CommandeDocument> sortedList = new List<CommandeDocument>();
            switch (titreColonne)
            {
                case "DateCommande":
                    sortedList = lesCommandesLivre.OrderBy(o => o.DateCommande).Reverse().ToList();
                    break;
                case "Montant":
                    sortedList = lesCommandesLivre.OrderBy(o => o.Montant).Reverse().ToList();
                    break;
                case "NbExemplaire":
                    sortedList = lesCommandesLivre.OrderBy(o => o.NbExemplaire).Reverse().ToList();
                    break;
                case "Suivi":
                    sortedList = lesCommandesLivre.OrderBy(o => o.Libelle).Reverse().ToList();
                    break;
            }
            RemplirCommandesLivreListe(sortedList);
        }

        /// <summary>
        /// Modification d'affichage suivant si on est en cours de modification de l'étape de suivi d'une commande de livre ou non
        /// </summary>
        /// <param name="modification">modification en cours ou non</param>
        private void EnCoursDeModificationCommandeLivre(Boolean modification)
        {
            grpCommandesLivres.Enabled = !modification;
            grpCommandesLivresCommande.Enabled = !modification;
            grpCommandesLivresModificationSuivi.Enabled = modification;
            if (!modification)
            {
                txbCommandesLivresModificationSuiviNumero.Text = "";
                txbCommandesLivresModificationSuiviEtapeActuelle.Text = "";
            }
        }

        /// <summary>
        /// Demande de modification de l'étape de suivi d'une commande de livre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCommandesLivresModifierSuivi_Click(object sender, EventArgs e)
        {
            if (dgvReceptionCommandesLivresListe.SelectedRows.Count > 0)
            {
                CommandeDocument commandeDocument = (CommandeDocument)bdgCommandesLivresListe.List[bdgCommandesLivresListe.Position];
                if (commandeDocument.IdSuivi.Equals(SUIVIREGLEE))
                {
                    MessageBox.Show("Cette commande a déjà été réglée et son suivi ne peut donc plus être modifié.", "Information");
                }
                else
                {
                    EnCoursDeModificationCommandeLivre(true);
                    txbCommandesLivresModificationSuiviNumero.Text = commandeDocument.Id;
                    txbCommandesLivresModificationSuiviEtapeActuelle.Text = commandeDocument.Libelle;
                    if (commandeDocument.IdSuivi.Equals(SUIVIENCOURS))
                    {
                        btnCommandesLivresModificationSuiviEtapeClassique.Text = "Passer à l'étape de suivi \"livrée\"";
                        btnCommandesLivresModificationSuiviEtapeRelancee.Enabled = true;

                    }
                    else if (commandeDocument.IdSuivi.Equals(SUIVILIVREE))
                    {
                        btnCommandesLivresModificationSuiviEtapeClassique.Text = "Passer à l'étape de suivi \"réglée\"";
                        btnCommandesLivresModificationSuiviEtapeRelancee.Enabled = false;
                    }
                    else if (commandeDocument.IdSuivi.Equals(SUIVIRELANCEE))
                    {
                        btnCommandesLivresModificationSuiviEtapeClassique.Text = "Passer à l'étape de suivi \"livrée\"";
                        btnCommandesLivresModificationSuiviEtapeRelancee.Enabled = false;
                    }
                }
            }
            else
            {
                MessageBox.Show("Une ligne parmi la liste des commandes doit être sélectionnée.", "Information");
            }
        }

        /// <summary>
        /// Demande d'enregistrement de la modification de l'étape de suivi d'une commande de livre lors d'une modification vers une étape de suivi classique
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCommandesLivresModificationSuiviEtapeClassique_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Voulez-vous vraiment valider la modification ?", "Confirmation de validation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    CommandeDocument commandeDocument = (CommandeDocument)bdgCommandesLivresListe.List[bdgCommandesLivresListe.Position];
                    string idSuivi = "";
                    string libelleSuivi = "";

                    if (commandeDocument.IdSuivi.Equals(SUIVIENCOURS) || commandeDocument.IdSuivi.Equals(SUIVIRELANCEE))
                    {
                        idSuivi = SUIVILIVREE;
                        libelleSuivi = SUIVILIVREELIBELLE;
                    }
                    else if (commandeDocument.IdSuivi.Equals(SUIVILIVREE))
                    {
                        idSuivi = SUIVIREGLEE;
                        libelleSuivi = SUIVIREGLEELIBELLE;
                    }

                    CommandeDocument commandeDocumentModifiee = new CommandeDocument(commandeDocument.Id, commandeDocument.DateCommande, commandeDocument.Montant, commandeDocument.NbExemplaire, commandeDocument.IdLivreDvd, idSuivi, libelleSuivi);

                    if (controller.ModifierCommandeDocument(commandeDocumentModifiee))
                    {
                        EnCoursDeModificationCommandeLivre(false);
                        lesCommandesLivre = controller.GetCommandeDocuments(commandeDocumentModifiee.IdLivreDvd);
                        AfficheCommandesLivre();

                    }
                    else
                    {
                        MessageBox.Show("problème rencontré lors de la modification", "Erreur");
                    }
                }
            }
            catch
            {
                MessageBox.Show("problème lors de la validation", "Erreur");
                EnCoursDeModificationCommandeLivre(false);
            }
        }

        /// <summary>
        /// Demande d'enregistrement de la modification de l'étape de suivi d'une commande de livre lors d'une modification vers l'étape de suivi "relancée"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCommandesLivresModificationSuiviEtapeRelancee_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Voulez-vous vraiment valider la modification ?", "Confirmation de validation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    CommandeDocument commandeDocument = (CommandeDocument)bdgCommandesLivresListe.List[bdgCommandesLivresListe.Position];
                    string idSuivi = SUIVIRELANCEE;
                    string libelleSuivi = SUIVIRELANCEELIBELLE;

                    CommandeDocument commandeDocumentModifiee = new CommandeDocument(commandeDocument.Id, commandeDocument.DateCommande, commandeDocument.Montant, commandeDocument.NbExemplaire, commandeDocument.IdLivreDvd, idSuivi, libelleSuivi);

                    if (controller.ModifierCommandeDocument(commandeDocumentModifiee))
                    {
                        EnCoursDeModificationCommandeLivre(false);
                        lesCommandesLivre = controller.GetCommandeDocuments(commandeDocumentModifiee.IdLivreDvd);
                        AfficheCommandesLivre();

                    }
                    else
                    {
                        MessageBox.Show("problème rencontré lors de la modification", "Erreur");
                    }
                }
            }
            catch
            {
                MessageBox.Show("problème lors de la validation", "Erreur");
                EnCoursDeModificationCommandeLivre(false);
            }
        }

        /// <summary>
        /// Demande d'annulation de la modification de l'étape de suivi d'une commande de livre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCommandesLivresModificationSuiviAnnuler_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Voulez-vous vraiment annuler ?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                EnCoursDeModificationCommandeLivre(false);
            }
        }

        /// <summary>
        /// Demande de suppression d'une commande de livre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCommandesLivresSupprimerCommande_Click(object sender, EventArgs e)
        {
            if (dgvReceptionCommandesLivresListe.SelectedRows.Count > 0)
            {
                CommandeDocument commandeDocument = (CommandeDocument)bdgCommandesLivresListe.List[bdgCommandesLivresListe.Position];
                if (!commandeDocument.IdSuivi.Equals(SUIVILIVREE) && !commandeDocument.IdSuivi.Equals(SUIVIREGLEE))
                {
                    if (MessageBox.Show("Souhaitez-vous vraiment supprimer cette commande ?", "Confirmation de suppression", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        if (controller.SupprimerCommandeDocument(commandeDocument))
                        {
                            lesCommandesLivre = controller.GetCommandeDocuments(commandeDocument.IdLivreDvd);
                            AfficheCommandesLivre();
                        }
                        else
                        {
                            MessageBox.Show("problème rencontré lors de la suppression", "Erreur");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Une commande déjà livrée ou réglée ne peut pas être supprimée", "Information");
                }
            }
            else
            {
                MessageBox.Show("Une ligne parmi la liste des commandes doit être sélectionnée.", "Information");
            }
        }

        #endregion

        #region Onglet Commandes Dvd
        private readonly BindingSource bdgCommandesDvdListe = new BindingSource();
        private List<CommandeDocument> lesCommandesDvd = new List<CommandeDocument>();

        /// <summary>
        /// Ouverture de l'onglet : récupère les dvd et vide tous les champs.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabCommandesDvd_Enter(object sender, EventArgs e)
        {
            lesDvd = controller.GetAllDvd();
            txbCommandesDvdNumero.Text = "";
            EnCoursDeModificationCommandeDvd(false);
        }

        /// <summary>
        /// Remplit le dategrid des commandes avec la liste reçue en paramètre
        /// </summary>
        /// <param name="commandesDvd">liste de commandes</param>
        private void RemplirCommandesDvdListe(List<CommandeDocument> commandesDvd)
        {
            if (commandesDvd != null)
            {
                bdgCommandesDvdListe.DataSource = commandesDvd;
                dgvReceptionCommandesDvdListe.DataSource = bdgCommandesDvdListe;
                dgvReceptionCommandesDvdListe.Columns["id"].Visible = false;
                dgvReceptionCommandesDvdListe.Columns["idLivreDvd"].Visible = false;
                dgvReceptionCommandesDvdListe.Columns["idSuivi"].Visible = false;
                dgvReceptionCommandesDvdListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgvReceptionCommandesDvdListe.Columns["dateCommande"].DisplayIndex = 0;
                dgvReceptionCommandesDvdListe.Columns["montant"].DisplayIndex = 1;
                dgvReceptionCommandesDvdListe.Columns["nbExemplaire"].DisplayIndex = 2;
                dgvReceptionCommandesDvdListe.Columns["libelle"].DisplayIndex = 3;
                dgvReceptionCommandesDvdListe.Columns["libelle"].HeaderText = "Suivi";
            }
            else
            {
                bdgCommandesDvdListe.DataSource = null;
            }
        }


        /// <summary>
        /// Recherche d'un numéro de dvd et affiche ses informations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCommandesDvdRecherche_Click(object sender, EventArgs e)
        {
            if (!txbCommandesDvdNumero.Text.Equals(""))
            {
                Dvd dvd = lesDvd.Find(x => x.Id.Equals(txbCommandesDvdNumero.Text));
                if (dvd != null)
                {
                    AfficheCommandesDvdInfos(dvd);
                }
                else
                {
                    MessageBox.Show("numéro introuvable");
                }
            }
        }


        /// <summary>
        /// Si le numéro de dvd est modifié, la zone de la commande est vidée et inactive
        /// les informations du dvd sont aussi effacées
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txbCommandesDvdNumero_TextChanged(object sender, EventArgs e)
        {
            txbCommandesDvdDuree.Text = "";
            txbCommandesDvdTitre.Text = "";
            txbCommandesDvdRealisateur.Text = "";
            txbCommandesDvdSynopsis.Text = "";
            txbCommandesDvdGenre.Text = "";
            txbCommandesDvdPublic.Text = "";
            txbCommandesDvdRayon.Text = "";
            txbCommandesDvdImage.Text = "";
            pcbCommandesDvdImage.Image = null;
            RemplirCommandesDvdListe(null);
            AccesCommandesDvdGroupBox(false);
        }

        /// <summary>
        /// Affichage des informations du dvd sélectionné et les commandes
        /// </summary>
        /// <param name="dvd">le dvd</param>
        private void AfficheCommandesDvdInfos(Dvd dvd)
        {
            // informations sur le dvd
            txbCommandesDvdNumero.Text = dvd.Id;
            txbCommandesDvdDuree.Text = dvd.Duree.ToString();
            txbCommandesDvdTitre.Text = dvd.Titre;
            txbCommandesDvdRealisateur.Text = dvd.Realisateur;
            txbCommandesDvdSynopsis.Text = dvd.Synopsis;
            txbCommandesDvdGenre.Text = dvd.Genre;
            txbCommandesDvdPublic.Text = dvd.Public;
            txbCommandesDvdRayon.Text = dvd.Rayon;
            txbCommandesDvdImage.Text = dvd.Image;
            string image = dvd.Image;
            try
            {
                pcbCommandesDvdImage.Image = Image.FromFile(image);
            }
            catch
            {
                pcbCommandesDvdImage.Image = null;
            }
            // affiche la liste des commandes du dvd
            AfficheCommandesDvd();
        }

        /// <summary>
        /// Récupère et affiche les commandes d'un dvd
        /// </summary>
        private void AfficheCommandesDvd()
        {
            string idDocument = txbCommandesDvdNumero.Text;
            lesCommandesDvd = controller.GetCommandeDocuments(idDocument);
            RemplirCommandesDvdListe(lesCommandesDvd);
            AccesCommandesDvdGroupBox(true);
        }

        /// <summary>
        /// Permet ou interdit l'accès à la gestion de la réception d'une commande
        /// et vide les objets graphiques
        /// </summary>
        /// <param name="acces">true ou false</param>
        private void AccesCommandesDvdGroupBox(bool acces)
        {
            grpCommandesDvdCommande.Enabled = acces;
            txbCommandesDvdCommandeNumero.Text = "";
            txbCommandesDvdCommandeMontant.Text = "";
            txbCommandesDvdCommandeNbExemplaires.Text = "";
            dtpCommandesDvdCommandeDate.Value = DateTime.Now;
        }


        /// <summary>
        /// Enregistrement de la nouvelle commande
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCommandesDvdCommandeValider_Click(object sender, EventArgs e)
        {
            if (!txbCommandesDvdCommandeNumero.Text.Equals("") && !txbCommandesDvdCommandeMontant.Text.Equals("") && !txbCommandesDvdCommandeNbExemplaires.Text.Equals(""))
            {
                try
                {
                    string id = txbCommandesDvdCommandeNumero.Text;
                    DateTime dateCommande = dtpCommandesDvdCommandeDate.Value;
                    double montant = double.Parse(txbCommandesDvdCommandeMontant.Text);
                    int nbExemplaire = int.Parse(txbCommandesDvdCommandeNbExemplaires.Text);
                    string idLivreDvd = txbCommandesDvdNumero.Text;
                    string idSuivi = SUIVIENCOURS;
                    string libelleSuivi = SUIVIENCOURSLIBELLE;

                    lesCommandesDvd = controller.GetCommandeDocuments(idLivreDvd);
                    int nbIdCommandes = 0;
                    foreach (CommandeDocument laCommandeDvd in lesCommandesDvd)
                    {
                        if (laCommandeDvd.Id.Equals(id))
                        {
                            nbIdCommandes++;
                        }
                    }

                    CommandeDocument commandeDocument = new CommandeDocument(id, dateCommande, montant, nbExemplaire, idLivreDvd, idSuivi, libelleSuivi);
                    if (nbIdCommandes == 0)
                    {
                        if (controller.CreerCommandeDocument(commandeDocument))
                        {
                            AfficheCommandesDvd();
                        }
                        else
                        {
                            MessageBox.Show("problème rencontré lors de l'ajout", "Erreur");
                        }
                    }
                    else
                    {
                        MessageBox.Show("numéro de commande déjà existant", "Erreur");
                    }
                }
                catch
                {
                    MessageBox.Show("Le numéro de commande, le montant et le nombre d'exemplaires doivent être numérique", "Information");
                    txbCommandesDvdCommandeNumero.Text = "";
                    txbCommandesDvdCommandeMontant.Text = "";
                    txbCommandesDvdCommandeNbExemplaires.Text = "";
                    txbCommandesDvdCommandeNumero.Focus();
                }
            }
            else
            {
                MessageBox.Show("Veuillez remplir tous les champs", "Information");
            }
        }


        /// <summary>
        /// Tri sur une colonne
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvReceptionCommandesDvdListe_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string titreColonne = dgvReceptionCommandesDvdListe.Columns[e.ColumnIndex].HeaderText;
            List<CommandeDocument> sortedList = new List<CommandeDocument>();
            switch (titreColonne)
            {
                case "DateCommande":
                    sortedList = lesCommandesDvd.OrderBy(o => o.DateCommande).Reverse().ToList();
                    break;
                case "Montant":
                    sortedList = lesCommandesDvd.OrderBy(o => o.Montant).Reverse().ToList();
                    break;
                case "NbExemplaire":
                    sortedList = lesCommandesDvd.OrderBy(o => o.NbExemplaire).Reverse().ToList();
                    break;
                case "Suivi":
                    sortedList = lesCommandesDvd.OrderBy(o => o.Libelle).Reverse().ToList();
                    break;
            }
            RemplirCommandesDvdListe(sortedList);
        }

        /// <summary>
        /// Modification d'affichage suivant si on est en cours de modification de l'étape de suivi d'une commande de dvd ou non
        /// </summary>
        /// <param name="modification">modification en cours ou non</param>
        private void EnCoursDeModificationCommandeDvd(Boolean modification)
        {
            grpCommandesDvd.Enabled = !modification;
            grpCommandesDvdCommande.Enabled = !modification;
            grpCommandesDvdModificationSuivi.Enabled = modification;
            if (!modification)
            {
                txbCommandesDvdModificationSuiviNumero.Text = "";
                txbCommandesDvdModificationSuiviEtapeActuelle.Text = "";
            }
        }


        /// <summary>
        /// Demande de modification de l'étape de suivi d'une commande de dvd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCommandesDvdModifierSuivi_Click(object sender, EventArgs e)
        {
            if (dgvReceptionCommandesDvdListe.SelectedRows.Count > 0)
            {
                CommandeDocument commandeDocument = (CommandeDocument)bdgCommandesDvdListe.List[bdgCommandesDvdListe.Position];
                if (commandeDocument.IdSuivi.Equals(SUIVIREGLEE))
                {
                    MessageBox.Show("Cette commande a déjà été réglée et son suivi ne peut donc plus être modifié.", "Information");
                }
                else
                {
                    EnCoursDeModificationCommandeDvd(true);
                    txbCommandesDvdModificationSuiviNumero.Text = commandeDocument.Id;
                    txbCommandesDvdModificationSuiviEtapeActuelle.Text = commandeDocument.Libelle;
                    if (commandeDocument.IdSuivi.Equals(SUIVIENCOURS))
                    {
                        btnCommandesDvdModificationSuiviEtapeClassique.Text = "Passer à l'étape de suivi \"livrée\"";
                        btnCommandesDvdModificationSuiviEtapeRelancee.Enabled = true;

                    }
                    else if (commandeDocument.IdSuivi.Equals(SUIVILIVREE))
                    {
                        btnCommandesDvdModificationSuiviEtapeClassique.Text = "Passer à l'étape de suivi \"réglée\"";
                        btnCommandesDvdModificationSuiviEtapeRelancee.Enabled = false;
                    }
                    else if (commandeDocument.IdSuivi.Equals(SUIVIRELANCEE))
                    {
                        btnCommandesDvdModificationSuiviEtapeClassique.Text = "Passer à l'étape de suivi \"livrée\"";
                        btnCommandesDvdModificationSuiviEtapeRelancee.Enabled = false;
                    }
                }
            }
            else
            {
                MessageBox.Show("Une ligne parmi la liste des commandes doit être sélectionnée.", "Information");
            }
        }


        /// <summary>
        /// Demande d'enregistrement de la modification de l'étape de suivi d'une commande de dvd lors d'une modification vers une étape de suivi classique
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCommandesDvdModificationSuiviEtapeClassique_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Voulez-vous vraiment valider la modification ?", "Confirmation de validation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    CommandeDocument commandeDocument = (CommandeDocument)bdgCommandesDvdListe.List[bdgCommandesDvdListe.Position];
                    string idSuivi = "";
                    string libelleSuivi = "";

                    if (commandeDocument.IdSuivi.Equals(SUIVIENCOURS) || commandeDocument.IdSuivi.Equals(SUIVIRELANCEE))
                    {
                        idSuivi = SUIVILIVREE;
                        libelleSuivi = SUIVILIVREELIBELLE;
                    }
                    else if (commandeDocument.IdSuivi.Equals(SUIVILIVREE))
                    {
                        idSuivi = SUIVIREGLEE;
                        libelleSuivi = SUIVIREGLEELIBELLE;
                    }

                    CommandeDocument commandeDocumentModifiee = new CommandeDocument(commandeDocument.Id, commandeDocument.DateCommande, commandeDocument.Montant, commandeDocument.NbExemplaire, commandeDocument.IdLivreDvd, idSuivi, libelleSuivi);

                    if (controller.ModifierCommandeDocument(commandeDocumentModifiee))
                    {
                        EnCoursDeModificationCommandeDvd(false);
                        lesCommandesDvd = controller.GetCommandeDocuments(commandeDocumentModifiee.IdLivreDvd);
                        AfficheCommandesDvd();

                    }
                    else
                    {
                        MessageBox.Show("problème rencontré lors de la modification", "Erreur");
                    }
                }
            }
            catch
            {
                MessageBox.Show("problème lors de la validation", "Erreur");
                EnCoursDeModificationCommandeDvd(false);
            }
        }


        /// <summary>
        /// Demande d'enregistrement de la modification de l'étape de suivi d'une commande de dvd lors d'une modification vers l'étape de suivi "relancée"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCommandesDvdModificationSuiviEtapeRelancee_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Voulez-vous vraiment valider la modification ?", "Confirmation de validation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    CommandeDocument commandeDocument = (CommandeDocument)bdgCommandesDvdListe.List[bdgCommandesDvdListe.Position];
                    string idSuivi = SUIVIRELANCEE;
                    string libelleSuivi = SUIVIRELANCEELIBELLE;

                    CommandeDocument commandeDocumentModifiee = new CommandeDocument(commandeDocument.Id, commandeDocument.DateCommande, commandeDocument.Montant, commandeDocument.NbExemplaire, commandeDocument.IdLivreDvd, idSuivi, libelleSuivi);

                    if (controller.ModifierCommandeDocument(commandeDocumentModifiee))
                    {
                        EnCoursDeModificationCommandeDvd(false);
                        lesCommandesDvd = controller.GetCommandeDocuments(commandeDocumentModifiee.IdLivreDvd);
                        AfficheCommandesDvd();

                    }
                    else
                    {
                        MessageBox.Show("problème rencontré lors de la modification", "Erreur");
                    }
                }
            }
            catch
            {
                MessageBox.Show("problème lors de la validation", "Erreur");
                EnCoursDeModificationCommandeDvd(false);
            }
        }


        /// <summary>
        /// Demande d'annulation de la modification de l'étape de suivi d'une commande de dvd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCommandesDvdModificationSuiviAnnuler_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Voulez-vous vraiment annuler ?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                EnCoursDeModificationCommandeDvd(false);
            }
        }


        /// <summary>
        /// Demande de suppression d'une commande de dvd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCommandesDvdSupprimerCommande_Click(object sender, EventArgs e)
        {
            if (dgvReceptionCommandesDvdListe.SelectedRows.Count > 0)
            {
                CommandeDocument commandeDocument = (CommandeDocument)bdgCommandesDvdListe.List[bdgCommandesDvdListe.Position];
                if (!commandeDocument.IdSuivi.Equals(SUIVILIVREE) && !commandeDocument.IdSuivi.Equals(SUIVIREGLEE))
                {
                    if (MessageBox.Show("Souhaitez-vous vraiment supprimer cette commande ?", "Confirmation de suppression", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        if (controller.SupprimerCommandeDocument(commandeDocument))
                        {
                            lesCommandesDvd = controller.GetCommandeDocuments(commandeDocument.IdLivreDvd);
                            AfficheCommandesDvd();
                        }
                        else
                        {
                            MessageBox.Show("problème rencontré lors de la suppression", "Erreur");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Une commande déjà livrée ou réglée ne peut pas être supprimée", "Information");
                }
            }
            else
            {
                MessageBox.Show("Une ligne parmi la liste des commandes doit être sélectionnée.", "Information");
            }
        }


        #endregion


    }
}
