using System;
using System.Collections.Generic;
using System.Text;

namespace ZK_Lymytz.TOOLS
{
    class Mots
    {
        public static string Fichier = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "File" : "Fichier";
        public static string Affichage = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Display" : "Affichage";
        public static string Outil = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Tools" : "Outils";
        public static string Gestion = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Management" : "Gestion";
        public static string Aide = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Help" : "Aide";
        public static string Etat = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Status" : "Etat";
        public static string Nouveau = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "New" : "Nouveau";
        public static string Actualiser = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Actualize" : "Actualiser";
        public static string Quitter = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Exit" : "Quitter";
        public static string BarOutil = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Toolbar" : "Barre d'outil";
        public static string BarEtat = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Status bar" : "Barre d'état";
        public static string Parametre = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Settings" : "Paramètres";
        public static string Nouv_Fenetre = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "New window" : "Nouvelle fenêtre";
        public static string Cascade = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Cascade" : "Cascade";
        public static string Mosaique = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Tile" : "Mosaïque";
        public static string Detail = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Details" : "Détails";
        public static string Vue = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "View" : "Vue";
        public static string A_Propos = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "About ZK_Lymytz" : "A propos de ZK_Lymytz";
        public static string Fermer_Tout = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Close all" : "Fermer tout";
        public static string Reorganiser_Fenetre = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Reorganize icons" : "Réorganiser les icônes";

        public static string Enregistrer = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Save" : "Enregistrer";
        public static string Supprimer = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Delete" : "Supprimer";
        public static string Annuler = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Cancel" : "Annuler";
        public static string Modifier = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Update" : "Modifier";

        public static string Afficher = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Show" : "Afficher";
        public static string Cacher = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Hide" : "Cacher";

        public static string Restart = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Restart" : "Redémarrer";
        public static string Restart_Now = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Restart now" : "Redémarrer maintenant";
        public static string Licence = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Key" : "Licence";
        public static string Contenu = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Contains" : "Contenus";
        public static string Activer = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Activate" : "Activer";
        public static string Delai = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? " day(s) remaining" : " jour(s) restants";
        public static string Formulaire = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Forms" : "Formulaires";
        public static string Ressource = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Resources" : "Ressources";
        public static string Niveau = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Level" : "Niveau";
        public static string Code = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Code" : "Code";
        public static string Libelle = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Wording" : "Libelle";
        public static string Acces = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Access" : "Acces";
        public static string Reference = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Reference" : "Référence";
        public static string Designation = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Designation" : "Désignation";
        public static string Famille = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Familly" : "Famille";
        public static string Marque = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Mark" : "Marque";
        public static string Date_Creation = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Date Save" : "Date Création";
        public static string Description = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Description" : "Description";
        public static string Stock = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Stock" : "Stock";
        public static string Prix = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Price" : "Prix";
        public static string Prix_Vente = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Sale price" : "Prix Vente";
        public static string Prix_Achat = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Buying price" : "Prix Achat";
        public static string Date_Modifier = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Date Update" : "Date Modification";
        public static string Recherche = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Research" : "Recherche";
        public static string Liste = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "List" : "Liste";
        public static string Informations = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Informations" : "Informations";
        public static string Actions = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Actions" : "Actions";
        public static string Photos = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Pictures" : "Photos";
        public static string Document = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Document" : "Document";
        public static string Entree = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Input" : "Entrée";
        public static string Sortie = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Output" : "Sortie";
        public static string Article = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Item" : "Article";
        public static string Quantite = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Quantity" : "Quantité";
        public static string Type = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Type" : "Type";
        public static string Utilisateurs = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Users" : "Utilisateurs";
        public static string Nom = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Name" : "Nom";
        public static string Noms = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Name" : "Nom(s)";
        public static string Prenoms = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Surname" : "Prénom(s)";
        public static string Identifiant = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Login" : "Identifiant";
        public static string Password = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Password" : "Mot de passe";
        public static string Temps_Connexion = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Time of connection" : "Temps de connexion";
        public static string Connexion = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "CONNECTION" : "CONNEXION";
        public static string Login = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Login" : "Connexion";
        public static string Confirmer = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Confirm" : "Confirmer";
        public static string General = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "General" : "Général";
        public static string Actif = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Active" : "Actif";
        public static string Photo = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Picture" : "Photo";
        public static string Langue = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Languague" : "Langue";
        public static string Serveur = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Server" : "Serveur";
        public static string Adresse_IP = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "IP Adress" : "Adresse IP";
        public static string Port = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Port" : "Port";
        public static string Database = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Database" : "Base de données";
        public static string Utilisateur = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "User" : "Utilisateur";
        public static string Inventaire = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Inventory" : "Inventaire";
        public static string Propos = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "About" : "A Propos";
        public static string Gestion_Article = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Management articles" : "Gestion articles";
        public static string Gestion_Stock = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Management stocks" : "Gestion stocks";
        public static string Gestion_Utilisateur = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Management users" : "Gestion utilisateur";
        public static string Autorisation = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Authorization" : "Autorisation";
        public static string Mouvement_Stock = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Stock movements" : "Mouvements stock";
        public static string Document_Stock = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Stock documents" : "Documents stock";
        public static string Famille_Article = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Family articles" : "Famille articles";
        public static string Marque_Article = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Mark articles" : "Marque articles";
        public static string Vue_Article = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Item Details" : "Détails Article";

        public static string Accessoire_Article = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Accessory Section" : "Accessoire article";
        public static string Par = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "By" : "Par";
        public static string Save_Famille = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Save Familly Article" : "Création Famille Article";
        public static string Save_Marque = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Save Mark Article" : "Création Marque Article";
        public static string Les_Informations = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "the informations" : " les informations";

        public static string Msg_ChampsVides = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Fields must contains a value" : "Les champs doivent contenir une valeur";
        public static string Msg_ChampsVide = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Please enter the value of" : "Veuillez entrer la valeur de";
        public static string Msg_Confirmation = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Are you sure you want " : "Etes sûr de vouloir";
        public static string Msg_Annulation = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Do you want to cancel the action?" : "Voulez-vous annuler l'action?";
        public static string Msg_FermerApplication = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Do you want to close the software?" : "Voulez-vous fermer l'application?";
        public static string Msg_Exception = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "The following error was detected :" : "L'erreur suivante a été detectée :";
        public static string Msg_Succes = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Success" : "Succès";
        public static string Msg_Avec_Succes = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "with success" : "avec succès";
        public static string Msg_Inexistant = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "does not exist" : "n'existe pas";

        public static string Msg_Photo_exist = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "You have already associated this picture" : "Vous avez déja associé cette photo";
        public static string Msg_Select_Article = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "You should select item" : "Vous devez sélectionner l'article";
        public static string Msg_Licence_Erreur = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Incorrect licence" : "Licence Incorrecte";
        public static string Msg_Compte_Erreur = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Your account is disabled... Please contact your administrator" : "Votre compte est désactiver...Veuillez contacter votre administrateur!";
        public static string Msg_identfiant_Erreur = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "Code or password incorrect!" : "Code ou mot de passe incorrect!";
        public static string Msg_Activer_Licence_Erreur = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "You have already activated your account" : "Vous avez déja activer votre compte";

        public static string Msg_Copyrigth = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "You have already activated your account" : "Tout droit reservé au propriétaire à la limite définie dans le contrat";
        public static string Msg_A_Propos = Configuration.langue.Equals(Constantes.LANGUE_ANGLAIS) ? "You have already activated your account" : "Vous avez déja activer votre compte";
    }
}
