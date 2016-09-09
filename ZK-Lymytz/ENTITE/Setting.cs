using System;
using System.Collections.Generic;
using System.Text;

namespace ZK_Lymytz.ENTITE
{
    [Serializable]
    class Setting
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private bool autoRun;
        public bool Autorun
        {
            get { return autoRun; }
            set { autoRun = value; }
        }

        private bool autoSynchro;
        public bool AutoSynchro
        {
            get { return autoSynchro; }
            set { autoSynchro = value; }
        }

        private bool autoClearAndBackup;
        public bool AutoClearAndBackup
        {
            get { return autoClearAndBackup; }
            set { autoClearAndBackup = value; }
        }

        private bool autoRattach;
        public bool AutoRattach
        {
            get { return autoRattach; }
            set { autoRattach = value; }
        }

        private bool addEnrollAuto;
        public bool AddEnrollAuto
        {
            get { return addEnrollAuto; }
            set { addEnrollAuto = value; }
        }

        private bool autoBackupDevice;
        public bool AutoBackupDevice
        {
            get { return autoBackupDevice; }
            set { autoBackupDevice = value; }
        }

        private bool checkConnect;
        public bool CheckConnect
        {
            get { return checkConnect; }
            set { checkConnect = value; }
        }

        private string cheminPersonal;
        public string CheminPersonal
        {
            get { return cheminPersonal != null ? cheminPersonal : ""; }
            set { cheminPersonal = value; }
        }

        private string cheminStartup;
        public string CheminStartup
        {
            get { return cheminStartup != null ? cheminStartup : ""; }
            set { cheminStartup = value; }
        }

        private string cheminPhoto;
        public string CheminPhoto
        {
            get { return cheminPhoto != null ? cheminPhoto : ""; }
            set { cheminPhoto = value; }
        }
    }
}
