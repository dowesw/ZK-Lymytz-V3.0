using System;
using System.Collections.Generic;
using System.Text;

namespace ZK_Lymytz.ENTITE
{
    [Serializable]
    public class Setting
    {
        private Boolean vide;// Verifie si le fichier des parametres existe
        public Boolean Vide
        {
            get { return vide; }
            set { vide = value; }
        }

        private bool autoRun;// Demarre les services des pointeuses au démarrage de l'application
        public bool Autorun
        {
            get { return autoRun; }
            set { autoRun = value; }
        }

        private bool autoSynchro;// Synchronise les pointages au démarrage de l'application
        public bool AutoSynchro
        {
            get { return autoSynchro; }
            set { autoSynchro = value; }
        }

        private bool autoClearAndBackup;// Efface les logs des appareils après sauvegarde de ceux-ci
        public bool AutoClearAndBackup
        {
            get { return autoClearAndBackup; }
            set { autoClearAndBackup = value; }
        }

        private bool autoRattach;// Rattacher automatiquement tous les appareils
        public bool AutoRattach
        {
            get { return autoRattach; }
            set { autoRattach = value; }
        }

        private bool useFileTamponLog;// Charger un fichier tampon lors de l'ouverture de l'application
        public bool UseFileTamponLog
        {
            get { return useFileTamponLog; }
            set { useFileTamponLog = value; }
        }

        private bool addEnrollAuto;// Insere automatiquement l'empreinte enroller dans les autres appareils
        public bool AddEnrollAuto
        {
            get { return addEnrollAuto; }
            set { addEnrollAuto = value; }
        }

        private bool autoBackupDevice;// Sauvegarde les pointages au demarrage de l'application
        public bool AutoBackupDevice
        {
            get { return autoBackupDevice; }
            set { autoBackupDevice = value; }
        }

        private bool autoCheckConnectAndSynchro;// Demarre le service qui verifie la connexion des appareils
        public bool AutoCheckConnectAndSynchro
        {
            get { return autoCheckConnectAndSynchro; }
            set { autoCheckConnectAndSynchro = value; }
        }

        private DateTime timeSynchroAuto;// Heure de la synchronisation automatique
        public DateTime TimeSynchroAuto
        {
            get { return timeSynchroAuto; }
            set { timeSynchroAuto = value; }
        }

        private bool checkConnect;// Verifie l'authentification du User avant toute action
        public bool CheckConnect
        {
            get { return checkConnect; }
            set { checkConnect = value; }
        }

        private string cheminPersonal;// Chemin du dossier Users
        public string CheminPersonal
        {
            get { return cheminPersonal != null ? cheminPersonal : ""; }
            set { cheminPersonal = value; }
        }

        private string cheminStartup;// Chemin du dossier de l'application
        public string CheminStartup
        {
            get { return cheminStartup != null ? cheminStartup : ""; }
            set { cheminStartup = value; }
        }

        private string cheminPhoto;// Chemin du dossier des photos
        public string CheminPhoto
        {
            get { return cheminPhoto != null ? cheminPhoto : ""; }
            set { cheminPhoto = value; }
        }

        private string cheminSetup;// Chemin du dossier des mise a jours
        public string CheminSetup
        {
            get { return cheminSetup != null ? cheminSetup : ""; }
            set { cheminSetup = value; }
        }

        private DateTime dateInstall;// Date de l'installation
        public DateTime DateInstall
        {
            get { return  dateInstall; }
            set { dateInstall = value; }
        }

        private bool createService;
        public bool CreateService
        {
            get { return createService; }
            set { createService = value; }
        }
    }
}
