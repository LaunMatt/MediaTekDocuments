# MediatekDocuments
Lien vers le dépôt d'origine dont le "README" contient la présentation de l'application d'origine :<br>
https://github.com/CNED-SLAM/MediaTekDocuments
## Les fonctionnalités ajoutées à l'application
Voici l'ensemble des fonctionnalités ajoutées à l'application :
### Fenêtre 1 : Authentification
![0](https://github.com/user-attachments/assets/a46c8d07-fd51-43f5-a7b1-f2e3bb28a72d)<br>
Cette nouvelle fenêtre de l'application permet aux différents utilisateurs de s'authentifier en saisissant leur login, ainsi que leur mot de passe. Si les informations inscrites sont correcte, l'utilisateur peut alors accéder aux fonctionnalités de l'application qui lui sont autorisées selon son service.
### Fenêtre 2 : Alerte concernant les abonnements se finissant
![1](https://github.com/user-attachments/assets/6a999c26-4112-444d-94a4-72c319c3efb7)<br>
Cette nouvelle fenêtre de l'application s'affiche uniquement pour les utilisateurs appartenant au service "administratif" ou au service "administrateur". Elle affiche la liste des abonnements de revues se finissant dans moins de 30 jours avec le titre de la revue auxquels ils sont rattachés et leur date de fin.
### Fenêtre principale
### Onglet 1 : Livres
![2](https://github.com/user-attachments/assets/59b02584-522d-416f-90d0-9cdfb931359f)<br>
#### Ajout d'un livre
Il est possible d'ajouter un livre à la liste des livres après avoir rempli l'ensemble de ses informations dans les différents champs vides de la zone d'ajout de la fenêtre et cliqué sur le bouton "Valider" de cette zone. Un message de confirmation s'affiche alors et il est possible de confirmer ou d'annuler. Il est par ailleurs possible d'annuler un ajout en cours en cliquant sur le bouton "Annuler" de la zone d'ajout, qui vide alors les différents champs de cette même zone.
#### Modification d'un livre
Si un livre est sélectionné dans la liste des livres, il est possible de le modifier après avoir cliqué sur le bouton "Modifier". La zone d'ajout d'un livre devient alors la zone de modification d'un livre et ses différents champs sont remplis avec les informations du livre en cours de modification. Il est alors possible de modifier des informations du livre en question et de cliquer sur le bouton "Valider" pour valider les modifications, alors répercutées dans la liste des livres. Un message de confirmation s'affiche alors et il est possible de confirmer ou d'annuler. Il est par ailleurs possible d'annuler une modification en cours en cliquant sur le bouton "Annuler" de la zone de modification, qui vide alors les différents champs de cette même zone et la fait redevenir zone d'ajout.
#### Suppression d'un livre
Si un livre est sélectionné dans la liste des livres, il est possible de le supprimer après avoir cliqué sur le bouton "Supprimer". Un message de confirmation s'affiche alors et il est possible de confirmer ou d'annuler. Si le livre que l'on souhaite supprimer est lié à des exemplaires ou commandes, un message d'information s'affiche pour informer l'utilisateur que la suppression n'est alors pas possible et la suppression est annulée.
#### Consultation des exemplaires d'un livre
Si un livre est sélectionné dans la liste des livres et qu'il est lié à des exemplaires, ceux-ci s'affichent dans la liste des exemplaires présente en bas de la fenêtre avec leur numéro, leur date d'achat et leur état. Il est par ailleurs possible de trier la liste sur chacune de ses colonnes.
#### Modification de l'état d'un exemplaire de livre
Si un exemplaire de livre est sélectionné dans la liste des exemplaires d'un livre, il est possible modifier son état après avoir cliqué sur le bouton "Modifier l'état de l'exemplaire". La zone de modification de l'état d'un exemplaire de livre devient alors accessible, affiche l'état actuel de l'exemplaire et il est alors possible de modifier l'état de l'exemplaire, puis de valider la modification en cliquant sur le bouton "Valider" de cette même zone qui permet d'afficher le nouvel état de l'exemplaire dans la liste des exemplaires et de rendre la zone de modification de l'état d'un exemplaire à nouveau inaccessible. Un message de confirmation s'affiche alors et il est possible de confirmer ou d'annuler. Il est par ailleurs possible d'annuler une modification en cours en cliquant sur le bouton "Annuler" de la zone de modification de l'état d'un exemplaire.
#### Suppression d'un exemplaire de livre
Si un exemplaire de livre est sélectionné dans la liste des exemplaires d'un livre, il est possible de le supprimer après avoir cliqué sur le bouton "Supprimer l'exemplaire". Un message de confirmation s'affiche alors et il est possible de confirmer ou d'annuler.
### Onglet 2 : DVD
![3](https://github.com/user-attachments/assets/2a8ac534-48ec-4e72-a6a4-3da7f7c2f93a)<br>
#### Ajout d'un DVD
Il est possible d'ajouter un DVD à la liste des DVD après avoir rempli l'ensemble de ses informations dans les différents champs vides de la zone d'ajout de la fenêtre et cliqué sur le bouton "Valider" de cette zone. Un message de confirmation s'affiche alors et il est possible de confirmer ou d'annuler. Il est par ailleurs possible d'annuler un ajout en cours en cliquant sur le bouton "Annuler" de la zone d'ajout, qui vide alors les différents champs de cette même zone.
#### Modification d'un DVD
Si un DVD est sélectionné dans la liste des DVD, il est possible de le modifier après avoir cliqué sur le bouton "Modifier". La zone d'ajout d'un DVD devient alors la zone de modification d'un DVD et ses différents champs sont remplis avec les informations du DVD en cours de modification. Il est alors possible de modifier des informations du DVD en question et de cliquer sur le bouton "Valider" pour valider les modifications, alors répercutées dans la liste des DVD. Un message de confirmation s'affiche alors et il est possible de confirmer ou d'annuler. Il est par ailleurs possible d'annuler une modification en cours en cliquant sur le bouton "Annuler" de la zone de modification, qui vide alors les différents champs de cette même zone et la fait redevenir zone d'ajout.
#### Suppression d'un DVD
Si un DVD est sélectionné dans la liste des DVD, il est possible de le supprimer après avoir cliqué sur le bouton "Supprimer". Un message de confirmation s'affiche alors et il est possible de confirmer ou d'annuler. Si le DVD que l'on souhaite supprimer est lié à des exemplaires ou commandes, un message d'information s'affiche pour informer l'utilisateur que la suppression n'est alors pas possible et la suppression est annulée.
#### Consultation des exemplaires d'un DVD
Si un DVD est sélectionné dans la liste des DVD et qu'il est lié à des exemplaires, ceux-ci s'affichent dans la liste des exemplaires présente en bas de la fenêtre avec leur numéro, leur date d'achat et leur état. Il est par ailleurs possible de trier la liste sur chacune de ses colonnes.
#### Modification de l'état d'un exemplaire de DVD
Si un exemplaire de DVD est sélectionné dans la liste des exemplaires d'un DVD, il est possible modifier son état après avoir cliqué sur le bouton "Modifier l'état de l'exemplaire". La zone de modification de l'état d'un exemplaire de DVD devient alors accessible, affiche l'état actuel de l'exemplaire et il est alors possible de modifier l'état de l'exemplaire, puis de valider la modification en cliquant sur le bouton "Valider" de cette même zone qui permet d'afficher le nouvel état de l'exemplaire dans la liste des exemplaires et de rendre la zone de modification de l'état d'un exemplaire à nouveau inaccessible. Un message de confirmation s'affiche alors et il est possible de confirmer ou d'annuler. Il est par ailleurs possible d'annuler une modification en cours en cliquant sur le bouton "Annuler" de la zone de modification de l'état d'un exemplaire.
#### Suppression d'un exemplaire de DVD
Si un exemplaire de DVD est sélectionné dans la liste des exemplaires d'un DVD, il est possible de le supprimer après avoir cliqué sur le bouton "Supprimer l'exemplaire". Un message de confirmation s'affiche alors et il est possible de confirmer ou d'annuler.
### Onglet 3 : Revues
![4](https://github.com/user-attachments/assets/9f18471a-3a8a-4153-9713-a9de9504dd02)<br>
#### Ajout d'une revue
Il est possible d'ajouter une revue à la liste des revues après avoir rempli l'ensemble de ses informations dans les différents champs vides de la zone d'ajout de la fenêtre et cliqué sur le bouton "Valider" de cette zone. Un message de confirmation s'affiche alors et il est possible de confirmer ou d'annuler. Il est par ailleurs possible d'annuler un ajout en cours en cliquant sur le bouton "Annuler" de la zone d'ajout, qui vide alors les différents champs de cette même zone.
#### Modification d'une revue
Si une revue est sélectionnée dans la liste des revues, il est possible de la modifier après avoir cliqué sur le bouton "Modifier". La zone d'ajout d'une revue devient alors la zone de modification d'une revue et ses différents champs sont remplis avec les informations de la revue en cours de modification. Il est alors possible de modifier des informations de la revue en question et de cliquer sur le bouton "Valider" pour valider les modifications, alors répercutées dans la liste des revues. Un message de confirmation s'affiche alors et il est possible de confirmer ou d'annuler. Il est par ailleurs possible d'annuler une modification en cours en cliquant sur le bouton "Annuler" de la zone de modification, qui vide alors les différents champs de cette même zone et la fait redevenir zone d'ajout.
#### Suppression d'une revue
Si une revue est sélectionnée dans la liste des revues, il est possible de la supprimer après avoir cliqué sur le bouton "Supprimer". Un message de confirmation s'affiche alors et il est possible de confirmer ou d'annuler. Si la revue que l'on souhaite supprimer est liée à des exemplaires ou abonnements, un message d'information s'affiche pour informer l'utilisateur que la suppression n'est alors pas possible et la suppression est annulée.
### Onglet 4 : Parutions des revues
![5](https://github.com/user-attachments/assets/797ebb28-c30f-4eb6-85fb-3b8166a0a769)<br>
#### Modification de l'état d'un exemplaire (parution) de revue
Si un exemplaire (parution) de revue est sélectionné dans la liste des exemplaires (parutions) d'une revue, il est possible modifier son état après avoir cliqué sur le bouton "Modifier l'état de la parution". La zone de modification de l'état d'un exemplaire (parution) de revue devient alors accessible, affiche l'état actuel de l'exemplaire (parution) et il est alors possible de modifier l'état de l'exemplaire (parution), puis de valider la modification en cliquant sur le bouton "Valider" de cette même zone qui permet d'afficher le nouvel état de l'exemplaire (parution) dans la liste des exemplaires (parutions) et de rendre la zone de modification de l'état d'un exemplaire (parution) à nouveau inaccessible. Un message de confirmation s'affiche alors et il est possible de confirmer ou d'annuler. Il est par ailleurs possible d'annuler une modification en cours en cliquant sur le bouton "Annuler" de la zone de modification de l'état d'un exemplaire (parution).
#### Suppression d'un exemplaire (parution) de revue
Si un exemplaire (parution) de revue est sélectionné dans la liste des exemplaires (parutions) d'une revue, il est possible de le supprimer après avoir cliqué sur le bouton "Supprimer la parution". Un message de confirmation s'affiche alors et il est possible de confirmer ou d'annuler.
### Onglet 5 : Commandes des livres
![6](https://github.com/user-attachments/assets/ca4e1c05-2847-4e54-a590-951623b7ba29)<br>
Cet onglet permet d'enregistrer l'ajout de nouvelles commandes d'un livre et la modification de l'étape de suivi de celles-ci.<br>
Il se décompose en 3 parties (groupbox).
#### Partie "Recherche livre"
Cette partie permet, à partir de la saisie d'un numéro de livre (puis en cliquant sur le bouton "Rechercher"), d'afficher toutes les informations du livre (comme dans l'onglet des livres), ainsi que son image principale en petit, avec en plus la liste des commandes existantes (date de commande, montant, nombre d'exemplaires, suivi).<br>
Dès qu'un numéro de livre est reconnu et ses informations affichées, la seconde partie ("Nouvelle commande pour ce livre") devient accessible.<br>
Si une modification est apportée au numéro du livre, toutes les zones sont réinitialisées et la seconde partie est rendue inaccessible, tant que le bouton "Rechercher" n'est pas utilisé.
#### Partie "Nouvelle commande pour ce livre"
Cette partie n'est accessible que si un livre a bien été trouvé dans la première partie.<br>
Il est possible alors d'ajouter une nouvelle commande en saisissant son numéro, en sélectionnant une date (date du jour proposée par défaut), en indiquant son montant et en saisissant le nombre d'exemplaires que comporte la commande.<br>
Le clic sur "Valider la commande" va permettre d'ajouter un tuple dans les tables commandeDocument et commande de la base de données. La commande correspondante apparaîtra alors automatiquement dans la liste des commandes et les zones de la partie "Nouvelle commande pour ce livre" seront réinitialisées.<br>
Si le numéro de la commande existe déjà, elle n’est pas ajoutée et un message est affiché.
#### Partie "Modifier l'étape de suivi de la commande"
Cette partie n'est accessible que si une commande a été sélectionnée dans la première partie et après avoir cliqué sur le bouton "Modifier le suivi de la commande".<br>
Il est possible alors de modifier l'étape de suivi de la commande vers "livrée" ou "relancée" grâce aux boutons respectifs réservés à ces effets, si l'étape de suivi actuelle de la commande est "en cours", seulement vers "livrée" grâce au bouton réservé à cet effet, si l'étape de suivi actuelle de la commande est "relancée" et seulement vers "réglée" grâce au bouton réservé à cet effet, si l'étape de suivi actuelle de la commande est "livrée". Si l'étape de suivi de la commande est "réglée", il n'est alors pas possible de la modifier et un message d'information s'affiche à l'utilisateur lors du clic sur le bouton "Modifier le suivi de la commande". Lorsque l'étape de suivi d'une commande passe à "livrée", autant d'exemplaires liés au livre de la commande que le nombre d'exemplaires de la commande en question sont créés dans la table exemplaire de la base de données. Par ailleurs, il est possible d'annuler une modification en cours de l'étape de suivi d'une commande grâce au bouton "Annuler".<br>
#### Suppression d'une commande de livre
Si une commande de livre est sélectionnée dans la liste des commandes d'un livre, il est possible de la supprimer après avoir cliqué sur le bouton "Supprimer la commande" et uniquement si l'étape de suivi de la commande en question n'est pas "livrée" ou "réglée" (dans le cas contraire, un message d'information s'affiche à l'utilisateur). Un message de confirmation s'affiche alors et il est possible de confirmer ou d'annuler.
### Onglet 6 : Commandes des DVD
![7](https://github.com/user-attachments/assets/13d3b0e3-12e3-4672-81ff-77ca0c14db03)<br>
Cet onglet permet d'enregistrer l'ajout de nouvelles commandes d'un DVD et la modification de l'étape de suivi de celles-ci.<br>
Il se décompose en 3 parties (groupbox).
#### Partie "Recherche DVD"
Cette partie permet, à partir de la saisie d'un numéro de DVD (puis en cliquant sur le bouton "Rechercher"), d'afficher toutes les informations du DVD (comme dans l'onglet des DVD), ainsi que son image principale en petit, avec en plus la liste des commandes existantes (date de commande, montant, nombre d'exemplaires, suivi).<br>
Dès qu'un numéro de DVD est reconnu et ses informations affichées, la seconde partie ("Nouvelle commande pour ce DVD") devient accessible.<br>
Si une modification est apportée au numéro du DVD, toutes les zones sont réinitialisées et la seconde partie est rendue inaccessible, tant que le bouton "Rechercher" n'est pas utilisé.
#### Partie "Nouvelle commande pour ce DVD"
Cette partie n'est accessible que si un DVD a bien été trouvé dans la première partie.<br>
Il est possible alors d'ajouter une nouvelle commande en saisissant son numéro, en sélectionnant une date (date du jour proposée par défaut), en indiquant son montant et en saisissant le nombre d'exemplaires que comporte la commande.<br>
Le clic sur "Valider la commande" va permettre d'ajouter un tuple dans les tables commandeDocument et commande de la base de données. La commande correspondante apparaîtra alors automatiquement dans la liste des commandes et les zones de la partie "Nouvelle commande pour ce DVD" seront réinitialisées.<br>
Si le numéro de la commande existe déjà, elle n’est pas ajoutée et un message est affiché.
#### Partie "Modifier l'étape de suivi de la commande"
Cette partie n'est accessible que si une commande a été sélectionnée dans la première partie et après avoir cliqué sur la bouton "Modifier le suivi de la commande".<br>
Il est possible alors de modifier l'étape de suivi de la commande vers "livrée" ou "relancée" grâce aux boutons respectifs réservés à ces effets, si l'étape de suivi actuelle de la commande est "en cours", seulement vers "livrée" grâce au bouton réservé à cet effet, si l'étape de suivi actuelle de la commande est "relancée" et seulement vers "réglée" grâce au bouton réservé à cet effet, si l'étape de suivi actuelle de la commande est "livrée". Si l'étape de suivi de la commande est "réglée", il n'est alors pas possible de la modifier et un message d'information s'affiche à l'utilisateur lors du clic sur le bouton "Modifier le suivi de la commande". Lorsque l'étape de suivi d'une commande passe à "livrée", autant d'exemplaires liés au DVD de la commande que le nombre d'exemplaires de la commande en question sont créés dans la table exemplaire de la base de données. Par ailleurs, il est possible d'annuler une modification en cours de l'étape de suivi d'une commande grâce au bouton "Annuler".<br>
#### Suppression d'une commande de DVD
Si une commande de DVD est sélectionnée dans la liste des commandes d'un DVD, il est possible de la supprimer après avoir cliqué sur le bouton "Supprimer la commande" et uniquement si l'étape de suivi de la commande en question n'est pas "livrée" ou "réglée" (dans le cas contraire, un message d'information s'affiche à l'utilisateur). Un message de confirmation s'affiche alors et il est possible de confirmer ou d'annuler.
### Onglet 7 : Commandes des revues
![8](https://github.com/user-attachments/assets/eecbb748-bdb9-45a8-870f-0a3656eeb70a)<br>
Cet onglet permet d'enregistrer l'ajout de nouvelles commandes (abonnements) d'une revue et la modification de l'étape de suivi de celles-ci.<br>
Il se décompose en 2 parties (groupbox).
#### Partie "Recherche revue"
Cette partie permet, à partir de la saisie d'un numéro de revue (puis en cliquant sur le bouton "Rechercher"), d'afficher toutes les informations de la revue (comme dans l'onglet des revues), ainsi que son image principale en petit, avec en plus la liste des commandes (abonnements) existantes (date de commande, montant, date de fin de l'abonnement).<br>
Dès qu'un numéro de revue est reconnu et ses informations affichées, la seconde partie ("Nouvelle commande / renouvellement de commande pour cette revue") devient accessible.<br>
Si une modification est apportée au numéro de la revue, toutes les zones sont réinitialisées et la seconde partie est rendue inaccessible, tant que le bouton "Rechercher" n'est pas utilisé.
#### Partie "Nouvelle commande / renouvellement de commande pour cette revue"
Cette partie n'est accessible que si une revue a bien été trouvée dans la première partie.<br>
Il est possible alors d'ajouter une nouvelle commande (abonnement) en saisissant son numéro, en sélectionnant une date (date du jour proposée par défaut), en indiquant son montant et en sélectionnant une date de fin.<br>
Le clic sur "Valider" va permettre d'ajouter un tuple dans les tables abonnement et commande de la base de données. La commande (abonnement) correspondante apparaîtra alors automatiquement dans la liste des commandes et les zones de la partie "Nouvelle commande / renouvellement de commande pour cette revue" seront réinitialisées.<br>
Si le numéro de la commande (abonnement) existe déjà, elle n’est pas ajoutée et un message est affiché.
#### Suppression d'une commande de revue
Si une commande (abonnement) de revue est sélectionnée dans la liste des commandes (abonnements) d'une revue, il est possible de la supprimer après avoir cliqué sur le bouton "Supprimer la commande" et uniquement si un exemplaire de revue n'a pas été reçu entre la date de début et la date de fin de l'abonnement (dans le cas contraire, un message d'information s'affiche à l'utilisateur). Un message de confirmation s'affiche alors et il est possible de confirmer ou d'annuler.
## La base de données
La base de données 'mediatek86 ' est au format MySQL.<br>
Voici sa structure :<br>
![MCD final mediatekdocuments](https://github.com/user-attachments/assets/f08a8ae9-ac1a-405d-8b21-fe38aa323d5b)<br>
## Les éléments ajoutés à la base de données
Voici l'ensemble des éléments ajoutés à la base de données :<br>
<ul>
   <li><strong>suivi : </strong>table contenant les différentes étapes de suivi possible d'une commande de livre ou de DVD (commandeDocument) qui peuvent être "en cours", "livrée", "réglée" ou "relancée".</li>
   <li><strong>utilisateur : </strong>table contenant les utilisateurs de l'application qui peuvent s'authentifier sur cette dernière et appartiennent à un service.</li>
   <li><strong>service : </strong>table contenant les services auxquels peuvent appartenir les utilisateurs, c'est-à-dire "administratif", "prêts", "culture" et "administrateur".</li>
</ul>

## L'API REST
L'accès à la BDD se fait à travers une API REST protégée par une authentification basique.<br>
Le code de l'API REST se trouve ici :<br>
https://github.com/LaunMatt/rest_mediatekdocuments<br>
avec les explications pour l'utiliser (dans le readme).
## Installation de l'application
Ce mode opératoire permet d'installer l'application pour pouvoir l'utiliser.<br>
- Installer Visual Studio 2019 entreprise et l'extension newtonsoft.json.<br>
- Télécharger le code et le dézipper puis renommer le dossier en "MediatekDocuments".<br>
- Récupérer et installer l'API REST nécessaire (https://github.com/LaunMatt/rest_mediatekdocuments) ainsi que la base de données (les explications sont données dans le readme correspondant).<br>
- Pour utiliser l'application avec l'API REST locale, modifier la valeur de "uriApi" dans la classe "Access.cs" du package "dal" pour l'url de l'API REST locale :<br>
http://localhost/rest_mediatekdocuments/<br>
- Pour utiliser l'application avec l'API REST en ligne, modifier la valeur de "uriApi" dans la classe "Access.cs" du package "dal" pour l'url de l'API REST en ligne :<br>
http://restmediatekdocumentsml.atwebpages.com/<br>
- Pour utiliser l'application créée par l'installateur et qui exploite l'API REST en ligne, lancer l'installateur "MediaTekDocumentsInstalleur" présent dans le dossier du projet téléchargé, suivre les étapes d'installation affichées, puis lancer l'application à l'aide du raccourci créé sur le bureau (ou celui présent dans le menu "Démarrer" de l'ordinateur).
