using System;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Timers;

using Microsoft.Win32;

using ZK_Lymytz.BLL;
using ZK_Lymytz.IHM;
using ZK_Lymytz.ENTITE;

namespace ZK_Lymytz.TOOLS
{
    public class Appareil
    {
        public zkemkeeper.CZKEMClass axCZKEM = new zkemkeeper.CZKEMClass();

        public Pointeuse _POINTEUSE = new Pointeuse();
        public Employe _EMPLOYE = new Employe();
        public Finger _FINGER = new Finger();
        public Presence _PRESENCE = new Presence();
        public Planning _PLANNING = new Planning();
        public DateTime _CURRENT_DATE = new DateTime();
        public DateTime _CURRENT_TIME = new DateTime();

        public int _FINGER_IN = 3;
        public int _FINGER_ID = -1;
        public int _FLAG = 3;
        public string _S_TEMPLATE = "";
        public int _LONG_TMPL = 0;
        public int _TIME_INTERVAL = 100; //100ms

        public bool _CONNEXION_RUNNING = false;
        public bool _BIS_CONNECTED = false;
        public int _I_MACHINE_NUMBER = 1;
        public bool _VALIDER;
        public string _IP;


        private Form_Add_Empreinte _FORM_ADD_EMPREINTE = new Form_Add_Empreinte();
        string _message = "";
        int _iCol = 0, _iRow = 0;
        bool _bIsConnected = false;
        int _error = 3;


        public Appareil()
        {

        }

        public Appareil(Pointeuse pointeuse)
        {
            this._POINTEUSE = pointeuse;
        }

        public Appareil(Form_Add_Empreinte form_)
        {
            this._FORM_ADD_EMPREINTE = form_;
        }

        public Appareil(Form_Add_Empreinte form_, Pointeuse pointeuse)
        {
            this._FORM_ADD_EMPREINTE = form_;
            this._POINTEUSE = pointeuse;
        }

        public Form_Add_Empreinte FORM_ADD_EMPREINTE
        {
            get { return _FORM_ADD_EMPREINTE; }
            set { _FORM_ADD_EMPREINTE = value; }
        }

        public static bool Verify()
        {
            try
            {
                zkemkeeper.CZKEMClass z = new zkemkeeper.CZKEMClass();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool CancelOperation()
        {
            return axCZKEM.CancelOperation();
        }

        public bool StartIdentify()
        {
            return axCZKEM.StartIdentify();
        }

        public void Deconnect()
        {
            axCZKEM.Disconnect();
        }

        public bool ReadAllTemplate(int iMachineNumber)
        {
            return axCZKEM.ReadAllTemplate(iMachineNumber);
        }

        public bool GetSDKVersion(ref string strVersion)
        {
            return axCZKEM.GetSDKVersion(ref strVersion);
        }

        public bool GetVendor(ref string strVendor)
        {
            return axCZKEM.GetVendor(ref strVendor);
        }

        public bool QueryState(ref int State)
        {
            return axCZKEM.QueryState(ref State);
        }

        public bool GetFirmwareVersion(int dwMachineNumber, ref string strVersion)
        {
            return axCZKEM.GetFirmwareVersion(dwMachineNumber, ref strVersion);
        }

        public bool GetCardFun(int dwMachineNumber, ref int CardFun)
        {
            return axCZKEM.GetCardFun(dwMachineNumber, ref CardFun);
        }

        public bool GetPlatform(int dwMachineNumber, ref string Platform)
        {
            return axCZKEM.GetPlatform(dwMachineNumber, ref Platform);
        }

        public bool GetProductCode(int dwMachineNumber, out string lpszProductCode)
        {
            return axCZKEM.GetProductCode(dwMachineNumber, out lpszProductCode);
        }

        public bool GetSysOption(int dwMachineNumber, string Option, out string Value)
        {
            return axCZKEM.GetSysOption(dwMachineNumber, Option, out Value);
        }

        public bool GetSerialNumber(int dwMachineNumber, out string dwSerialNumber)
        {
            return axCZKEM.GetSerialNumber(dwMachineNumber, out dwSerialNumber);
        }

        public bool GetDeviceStrInfo(int dwMachineNumber, int dwInfo, out string Value)
        {
            return axCZKEM.GetDeviceStrInfo(dwMachineNumber, dwInfo, out Value);
        }

        public bool GetDeviceMAC(int dwMachineNumber, ref string sMAC)
        {
            return axCZKEM.GetDeviceMAC(dwMachineNumber, ref sMAC);
        }

        public bool GetDeviceIP(int dwMachineNumber, ref string IPAddr)
        {
            return axCZKEM.GetDeviceIP(dwMachineNumber, ref IPAddr);
        }

        public bool ReadAllUserID(int iMachineNumber)
        {
            return axCZKEM.ReadAllUserID(iMachineNumber);
        }
        public bool ConnectNet()
        {
            if (_POINTEUSE != null ? _POINTEUSE.Ip != null : false)
            {
                return ConnectNet(_POINTEUSE.Ip, 4370);
            }
            return false;
        }

        public bool ConnectNet(string ip)
        {
            return ConnectNet(ip, 4370);
        }

        public bool ConnectNet(string ip, int port)
        {
            return ConnectNet(ip, port, !Constantes._FIRST_OPEN);
        }

        public bool ConnectNet(string ip, int port, bool view)
        {
            Form_Wait w = new Form_Wait();
            try
            {
                if (view)
                    w.Open();
                return axCZKEM.Connect_Net(ip, port);
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (view)
                    w.Fermer();
            }
        }

        public bool ConnectCom(int port)
        {
            return ConnectCom(port, 1);
        }

        public bool ConnectCom(int port, int ImachineNumber)
        {
            return ConnectCom(port, ImachineNumber, 115200);
        }

        public bool ConnectCom(int port, int ImachineNumber, int protocole)
        {
            return ConnectCom(port, ImachineNumber, protocole, !Constantes._FIRST_OPEN);
        }

        public bool ConnectCom(int port, int ImachineNumber, int protocole, bool view)
        {
            Form_Wait w = new Form_Wait();
            try
            {
                if (view)
                    w.Open();
                return axCZKEM.Connect_Com(port, ImachineNumber, protocole);
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (view)
                    w.Fermer();
            }
        }

        public bool GetAllUserID(int iMachineNumber, ref int iEnrollNumber, ref int iEMachineNumber, ref int iBackupNumber, ref int iPrivilege, ref int iEnabled)
        {
            return axCZKEM.GetAllUserID(iMachineNumber, ref iEnrollNumber, ref iEMachineNumber, ref iBackupNumber, ref iPrivilege, ref iEnabled);
        }

        public bool StartEnrollEx(string id, int fingerID, int iFlag)
        {
            return axCZKEM.StartEnrollEx(id, fingerID, iFlag);
        }

        public bool GetDeviceTime(int iMachineNumber, ref int idwYear, ref int idwMonth, ref int idwDay, ref int idwHour, ref int idwMinute, ref int idwSecond)
        {
            return axCZKEM.GetDeviceTime(iMachineNumber, ref idwYear, ref idwMonth, ref idwDay, ref idwHour, ref idwMinute, ref idwSecond);
        }

        public bool SetDeviceTime(int iMachineNumber)
        {
            return axCZKEM.SetDeviceTime(iMachineNumber);
        }

        public bool SetDeviceTime(int iMachineNumber, int idwYear, int idwMonth, int idwDay, int idwHour, int idwMinute, int idwSecond)
        {
            return axCZKEM.SetDeviceTime2(iMachineNumber, idwYear, idwMonth, idwDay, idwHour, idwMinute, idwSecond);
        }

        public bool DisableDeviceWithTimeOut(int iMachineNumber, int seconde)
        {
            return axCZKEM.DisableDeviceWithTimeOut(iMachineNumber, seconde);
        }

        public void GetLastError(ref int idError)
        {
            axCZKEM.GetLastError(idError);
        }

        public void RefreshData(int ImachineNumber)
        {
            axCZKEM.RefreshData(ImachineNumber);
        }

        public bool RegEvent(int ImachineNumbre)
        {
            return axCZKEM.RegEvent(ImachineNumbre, 65535);
        }

        public bool RegEvent(int ImachineNumbre, int protocole)
        {
            return axCZKEM.RegEvent(ImachineNumbre, protocole);
        }

        public bool EnableDevice(int ImachineNumbre, bool bEnabled)
        {
            return axCZKEM.EnableDevice(ImachineNumbre, bEnabled);
        }

        public bool SetDeviceIP(int ImachineNumbre, string ip)
        {
            return axCZKEM.SetDeviceIP(ImachineNumbre, ip);
        }

        public bool GetUserInfo(int ImachineNumbre, int id, ref string names, ref string password, ref int privilege, ref bool bEnable)
        {
            return axCZKEM.GetUserInfo(ImachineNumbre, id, ref names, ref password, ref privilege, ref bEnable);
        }

        public bool SetUserInfo(int ImachineNumbre, int id, string names, string password, int privilege, bool bEnable)
        {
            return axCZKEM.SetUserInfo(ImachineNumbre, id, names, password, privilege, bEnable);
        }

        public bool GetUserTmpExStr(int ImachineNumbre, string id, int iDfinger, out int flag, out string sTmpData, out int longTmp)
        {
            return axCZKEM.GetUserTmpExStr(ImachineNumbre, id, iDfinger, out flag, out sTmpData, out longTmp);
        }

        public bool SSR_DelUserTmpExt(int iMachineNumber, string id, int fingerID)
        {
            return axCZKEM.SSR_DelUserTmpExt(iMachineNumber, id, fingerID);
        }

        public bool DelUserTmp(int ImachineNumbre, int id, int fingerID)
        {
            return axCZKEM.DelUserTmp(ImachineNumbre, id, fingerID);
        }

        public bool DeleteUserInfoEx(int ImachineNumbre, int id)
        {
            return axCZKEM.DeleteUserInfoEx(ImachineNumbre, id);
        }

        public bool Restart(int iMachineNumber)
        {
            int idwErrorCode = 0;

            Cursor c = Cursors.WaitCursor;
            if (!axCZKEM.RestartDevice(iMachineNumber))
            {
                c = Cursors.Default;
                return true;
            }
            else
            {
                axCZKEM.GetLastError(ref idwErrorCode);
                c = Cursors.Default;
                return false;
            }
        }

        public bool Restart(bool bIsConnected, int iMachineNumber)
        {
            if (bIsConnected == false)
            {
                Messages.Erreur_Oui_Non("Voud devez connecter la pointeuse");
                return false;
            }
            return Restart(iMachineNumber);
        }

        public bool Stop(int iMachineNumber)
        {
            int idwErrorCode = 0;

            Cursor c = Cursors.WaitCursor;
            if (!axCZKEM.PowerOffDevice(iMachineNumber))
            {
                return true;
            }
            else
            {
                axCZKEM.GetLastError(ref idwErrorCode);
                return false;
            }
            c = Cursors.Default;
        }

        public bool Stop(bool bIsConnected, int iMachineNumber)
        {
            if (bIsConnected == false)
            {
                Messages.Erreur_Oui_Non("Voud devez connecter la pointeuse");
                return false;
            }
            return Stop(iMachineNumber);
        }

        public void ClearLCD(bool bIsConnected)
        {
            Cursor c;
            if (bIsConnected == false)
            {
                Messages.Erreur_Oui_Non("Voud devez connecter la pointeuse");
                return;
            }

            c = Cursors.WaitCursor;
            if (axCZKEM.ClearLCD())
            {
                axCZKEM.RefreshData(_I_MACHINE_NUMBER);//the data in the device should be refreshed
            }
            c = Cursors.Default;
        }

        public void WriteLCD(bool bIsConnected, int iRow, int iCol, string message)
        {
            _bIsConnected = bIsConnected;
            _iRow = iRow;
            _iCol = iCol;
            _message = message;
            Thread t = new Thread(new ThreadStart(WriteLCD));
            t.Start();
        }

        public void WriteLCD()
        {
            Cursor c;
            if (_bIsConnected == false)
            {
                Messages.Erreur_Oui_Non("Voud devez connecter la pointeuse");
                return;
            }

            if (_message.Trim() == "")
            {
                Messages.Erreur_Oui_Non("Le message ne peut pas etre null");
                return;
            }

            c = Cursors.WaitCursor;
            if (axCZKEM.WriteLCD(_iRow, _iCol, _message))
            {
                axCZKEM.RefreshData(_I_MACHINE_NUMBER);//the data in the device should be refreshed
            }
            c = Cursors.Default;
        }

        public List<IOEMDevice> GetAllAttentdData(int iMachineNumber, bool bIsConnected)
        {
            return GetAllAttentdData(iMachineNumber, bIsConnected, null, false, new DateTime(), new DateTime());
        }

        public List<IOEMDevice> GetAllAttentdData(int iMachineNumber, bool bIsConnected, Employe e, bool date, DateTime d, DateTime f)
        {
            try
            {
                if (bIsConnected == false)
                {
                    Messages.ShowErreur("Please connect the device first!");
                    return new List<IOEMDevice>();
                }
                string hd = d.ToShortTimeString();
                string hf = f.ToShortTimeString();
                bool heure_ = !(hd.Equals("00:00:00:000") || hd.Equals("00:00:00") || hd.Equals("00:00") || hd.Equals("00") || hf.Equals("00:00:00:000") || hf.Equals("00:00:00") || hf.Equals("00:00") || hf.Equals("00"));

                List<IOEMDevice> trans = new List<IOEMDevice>();
                if (axCZKEM.RegEvent(iMachineNumber, 65535))
                {
                    Cursor c = Cursors.WaitCursor;
                    axCZKEM.EnableDevice(iMachineNumber, false);//disable the device
                    if (axCZKEM.ReadGeneralLogData(iMachineNumber)) //read the logs from the memory.(the same as axCZKEM.ReadAllGLogData(iMachineNumber))
                    {
                        int idwTMachineNumber = 0;
                        int idwEnrollNumber = 0;
                        int idwVerifyMode = 0;
                        int idwInOutMode = 0;
                        int idwWorkCode = 0;
                        int idwReserved = 0;
                        int idwYear = 0;
                        int idwMonth = 0;
                        int idwDay = 0;
                        int idwHour = 0;
                        int idwMinute = 0;
                        int idwSecond = 0;
                        while (axCZKEM.GetGeneralExtLogData(iMachineNumber, ref idwEnrollNumber, ref idwVerifyMode, ref idwInOutMode,
                                 ref idwYear, ref idwMonth, ref idwDay, ref idwHour, ref idwMinute, ref idwSecond, ref idwWorkCode, ref idwReserved))//get records from the memory
                        {
                            DateTime h = new DateTime(idwYear, idwMonth, idwDay, 0, 0, 0);
                            if (heure_)
                            {
                                h = new DateTime(idwYear, idwMonth, idwDay, idwHour, idwMinute, 0);
                            }
                            if (h > Convert.ToDateTime("31/05/2016 17:59:59"))
                            {
                                ENTITE.IOEMDevice iO = new ENTITE.IOEMDevice(_POINTEUSE, iMachineNumber, idwTMachineNumber, idwEnrollNumber, idwYear, idwMonth, idwDay, idwHour, idwMinute, idwSecond);
                                if (e != null ? e.Id > 0 : false)
                                {
                                    if (date)
                                    {
                                        if (idwEnrollNumber == e.Id && (d <= h && h <= f))
                                        {
                                            trans.Add(iO);
                                        }
                                    }
                                    else
                                    {
                                        if (idwEnrollNumber == e.Id)
                                        {
                                            trans.Add(iO);
                                        }
                                    }
                                }
                                else
                                {
                                    if (date)
                                    {
                                        if (d <= h && h <= f)
                                        {
                                            trans.Add(iO);
                                        }
                                    }
                                    else
                                    {
                                        trans.Add(iO);
                                    }
                                }
                                Constantes.LoadPatience(false);
                            }
                        }
                    }
                    axCZKEM.RefreshData(iMachineNumber);//the data in the device should be refreshed
                    c = Cursors.Default;
                    axCZKEM.EnableDevice(iMachineNumber, true);//disable the device
                }
                return trans;
            }
            catch (Exception ex)
            {
                Messages.Exception("Appareil (GetAllAttentdData) ", ex);
                return null;
            }
        }

        public List<IOEMDevice> GetAllAttentdDataEx(int iMachineNumber, bool bIsConnected, List<Employe> le, DateTime[] dates)
        {
            try
            {
                if (bIsConnected == false)
                {
                    Messages.ShowErreur("Please connect the device first!");
                    return new List<IOEMDevice>();
                }

                List<IOEMDevice> trans = new List<IOEMDevice>();
                if (axCZKEM.RegEvent(iMachineNumber, 65535))
                {
                    Cursor c = Cursors.WaitCursor;
                    axCZKEM.EnableDevice(iMachineNumber, false);//disable the device
                    if (axCZKEM.ReadGeneralLogData(iMachineNumber)) //read the logs from the memory.(the same as axCZKEM.ReadAllGLogData(iMachineNumber))
                    {
                        int idwTMachineNumber = 0;
                        int idwEnrollNumber = 0;
                        int idwVerifyMode = 0;
                        int idwInOutMode = 0;
                        int idwWorkCode = 0;
                        int idwReserved = 0;
                        int idwYear = 0;
                        int idwMonth = 0;
                        int idwDay = 0;
                        int idwHour = 0;
                        int idwMinute = 0;
                        int idwSecond = 0;
                        while (axCZKEM.GetGeneralExtLogData(iMachineNumber, ref idwEnrollNumber, ref idwVerifyMode, ref idwInOutMode,
                                 ref idwYear, ref idwMonth, ref idwDay, ref idwHour, ref idwMinute, ref idwSecond, ref idwWorkCode, ref idwReserved))//get records from the memory
                        {

                            DateTime h = new DateTime(idwYear, idwMonth, idwDay, idwHour, idwMinute, idwSecond);
                            DateTime _d_ = Convert.ToDateTime("31/05/2016 17:59:59");
                            if (h > _d_)
                            {
                                bool deja = false;
                                ENTITE.IOEMDevice iO = new ENTITE.IOEMDevice(_POINTEUSE, iMachineNumber, idwTMachineNumber, idwEnrollNumber, idwYear, idwMonth, idwDay, idwHour, idwMinute, idwSecond);
                                DateTime _h = new DateTime(h.Year, h.Month, h.Day, 0, 0, 0);
                                foreach (Employe e in le)
                                {
                                    if (e != null ? e.Id > 0 : false)
                                    {
                                        if (idwEnrollNumber == e.Id)
                                        {
                                            deja = true;
                                            break;
                                        }
                                    }
                                }

                                if (!deja)
                                {
                                    foreach (DateTime d in dates)
                                    {
                                        DateTime _d = new DateTime(d.Year, d.Month, d.Day, 0, 0, 0);
                                        if (_d == _h)
                                        {
                                            deja = true;
                                            break;
                                        }
                                    }
                                }
                                if (!deja)
                                {
                                    Employe e = EmployeBLL.OneById(iO.idwSEnrollNumber, Constantes.SOCIETE.Id);
                                    if (e != null ? e.Id > 0 : false)
                                    {
                                        trans.Add(iO);
                                    }
                                }
                                Constantes.LoadPatience(false);
                            }
                        }
                    }
                    axCZKEM.RefreshData(iMachineNumber);//the data in the device should be refreshed
                    c = Cursors.Default;
                    axCZKEM.EnableDevice(iMachineNumber, true);//disable the device
                }
                return trans;
            }
            catch (Exception ex)
            {
                Messages.Exception("Appareil (GetAllAttentdDataEx) ", ex);
                return null;
            }
        }

        public List<Empreinte> GetAllTemplate(int iMachineNumber, bool bIsConnected)
        {
            try
            {
                if (bIsConnected == false)
                {
                    Messages.ShowErreur("Please connect the device first!");
                    return new List<Empreinte>();
                }

                List<Empreinte> l = new List<Empreinte>();
                int idwEnrollNumber = 0;
                string sName = "";
                string sPassword = "";
                int iPrivilege = 0;
                bool bEnabled = false;
                int idwFigerIndex;
                string sTmpData = "";
                int iTmpLength = 0;

                axCZKEM.EnableDevice(iMachineNumber, false);
                Cursor c = Cursors.WaitCursor;

                axCZKEM.BeginBatchUpdate(iMachineNumber, 1);//create memory space for batching data
                axCZKEM.ReadAllUserID(iMachineNumber);//read all the user information to the memory
                axCZKEM.ReadAllTemplate(iMachineNumber);//read all the users' fingerprint templates to the memory
                int i = 0;

                while (axCZKEM.GetAllUserInfo(iMachineNumber, ref idwEnrollNumber, ref sName, ref sPassword, ref iPrivilege, ref bEnabled))//get all the users' information from the memory
                {
                    for (idwFigerIndex = 0; idwFigerIndex < 10; idwFigerIndex++)
                    {
                        int iFlag = 0;
                        if (axCZKEM.GetUserTmpExStr(iMachineNumber, idwEnrollNumber.ToString(), idwFigerIndex, out iFlag, out sTmpData, out iTmpLength))//get the corresponding templates string and length from the memory
                        {
                            ++i;
                            Empreinte e = new Empreinte();
                            e.Id = i;
                            e.Employe = new Employe(idwEnrollNumber, sName);
                            e.Digital = idwFigerIndex;
                            e.STemplate = sTmpData;
                            e.Privilege = iPrivilege;
                            e.Longueur = iTmpLength;
                            e.Flag = iFlag;
                            l.Add(e);
                        }
                        Constantes.LoadPatience(false);
                    }
                }
                axCZKEM.BatchUpdate(iMachineNumber);//download all the information in the memory
                axCZKEM.EnableDevice(iMachineNumber, true);
                c = Cursors.Default;
                return l;
            }
            catch (Exception ex)
            {
                Messages.Exception("Appareil (GetAllTemplate) ", ex);
                return null;
            }
        }

        public List<Empreinte> GetAllTemplate(int iMachineNumber, int idEmploye, bool bIsConnected)
        {
            try
            {
                if (bIsConnected == false)
                {
                    Messages.ShowErreur("Please connect the device first!");
                    return new List<Empreinte>();
                }

                List<Empreinte> l = new List<Empreinte>();
                int idwEnrollNumber = 0;
                string sName = "";
                string sPassword = "";
                int iPrivilege = 0;
                bool bEnabled = false;
                int idwFigerIndex;
                string sTmpData = "";
                int iTmpLength = 0;

                axCZKEM.EnableDevice(iMachineNumber, false);
                Cursor c = Cursors.WaitCursor;

                axCZKEM.BeginBatchUpdate(iMachineNumber, 1);//create memory space for batching data
                axCZKEM.ReadAllUserID(iMachineNumber);//read all the user information to the memory
                axCZKEM.ReadAllTemplate(iMachineNumber);//read all the users' fingerprint templates to the memory
                int i = 0;

                while (axCZKEM.GetAllUserInfo(iMachineNumber, ref idwEnrollNumber, ref sName, ref sPassword, ref iPrivilege, ref bEnabled))//get all the users' information from the memory
                {
                    if (idwEnrollNumber == idEmploye)
                    {
                        for (idwFigerIndex = 0; idwFigerIndex < 10; idwFigerIndex++)
                        {
                            int iFlag = 0;
                            if (axCZKEM.GetUserTmpExStr(iMachineNumber, idwEnrollNumber.ToString(), idwFigerIndex, out iFlag, out sTmpData, out iTmpLength))//get the corresponding templates string and length from the memory
                            {
                                ++i;
                                Empreinte e = new Empreinte();
                                e.Id = i;
                                e.Employe = new Employe(idwEnrollNumber, sName);
                                e.Digital = idwFigerIndex;
                                e.STemplate = sTmpData;
                                e.Privilege = iPrivilege;
                                e.Longueur = iTmpLength;
                                e.Flag = iFlag;
                                l.Add(e);
                            }
                            Constantes.LoadPatience(false);
                        }
                    }
                }
                axCZKEM.BatchUpdate(iMachineNumber);//download all the information in the memory
                axCZKEM.EnableDevice(iMachineNumber, true);
                c = Cursors.Default;
                return l;
            }
            catch (Exception ex)
            {
                Messages.Exception("Appareil (GetAllTemplate) ", ex);
                return null;
            }
        }

        public int SetAllTemplate(List<Empreinte> l, int iMachineNumber, bool bIsConnected)
        {
            try
            {
                if (bIsConnected == false)
                {
                    Messages.ShowErreur("Please connect the device first!");
                    return 0;
                }
                int i = 0;
                if (l.Count > 0)
                {
                    ObjectThread o = new ObjectThread(Constantes.PBAR_WAIT);
                    o.UpdateMaxBar(Constantes.PBAR_WAIT.Maximum + l.Count);

                    int idwEnrollNumber = 0;
                    string sName = "";
                    int idwFingerIndex = 0;
                    string sTmpData = "";
                    int iPrivilege = 0;
                    string sPassword = null;
                    int longueur = 0;
                    bool bEnabled = true;
                    int iFlag = 0;

                    Cursor c = Cursors.WaitCursor;
                    axCZKEM.EnableDevice(iMachineNumber, false);
                    axCZKEM.RefreshData(iMachineNumber);//the data in the device should be refreshed
                    axCZKEM.ReadAllTemplate(iMachineNumber);//read template in devise
                    foreach (Empreinte e in l)
                    {
                        idwEnrollNumber = (int)(e.Employe != null ? e.Employe.Id : 0);
                        sName = (e.Employe != null ? e.Employe.Nom : "");
                        idwFingerIndex = e.Digital;
                        sTmpData = e.STemplate;
                        iPrivilege = e.Privilege;
                        longueur = e.Longueur;
                        iFlag = e.Flag;
                        if (sTmpData != null ? sTmpData.Trim() != "" : false)
                        {
                            if (axCZKEM.SetUserInfo(iMachineNumber, idwEnrollNumber, sName, sPassword, iPrivilege, bEnabled))//upload user information to the memory
                            {
                                if (axCZKEM.SetUserTmpExStr(iMachineNumber, idwEnrollNumber.ToString(), idwFingerIndex, iFlag, sTmpData))//upload tempates information to the memory
                                {
                                    Finger d = (Finger)Finger.Get(idwFingerIndex);
                                    Utils.WriteLog("---- Ajout de l'empreinte de l'employé " + sName + " du doigt (" + d.Doigt + ") de la main (" + d.Main + ") effectue!");
                                    ++i;
                                }
                                else
                                {
                                    Utils.WriteLog("---- Ajout de l'empreinte de l'employé " + sName + " echoué!");
                                }
                            }
                            else
                            {
                                Utils.WriteLog("---- Ajout de l'employé " + sName + " echoue!");
                            }
                        }
                        Constantes.LoadPatience(false);
                    }
                    c = Cursors.Default;
                }
                return i;
            }
            catch (Exception ex)
            {
                Messages.Exception("Zkemkeeper (SetAllTemplate) ", ex);
                return 0;
            }
            finally
            {
                axCZKEM.RefreshData(iMachineNumber);//the data in the device should be refreshed
                axCZKEM.EnableDevice(iMachineNumber, true);
            }
        }

        public bool OnEmpreintTesting(bool starting)
        {
            try
            {
                _EMPLOYE = null;
                if (starting)
                {
                    this.axCZKEM.OnVerify += new zkemkeeper._IZKEMEvents_OnVerifyEventHandler(axCZKEM1_OnVerifyOne);
                    this.axCZKEM.OnVerify -= new zkemkeeper._IZKEMEvents_OnVerifyEventHandler(axCZKEM1_OnVerify);
                }
                else
                {
                    this.axCZKEM.OnVerify -= new zkemkeeper._IZKEMEvents_OnVerifyEventHandler(axCZKEM1_OnVerifyOne);
                    this.axCZKEM.OnVerify += new zkemkeeper._IZKEMEvents_OnVerifyEventHandler(axCZKEM1_OnVerify);
                }
                return true;
            }
            catch (Exception ex)
            {
                Messages.Exception("Zkemkeeper (OnEmpreintTesting) ", ex);
                return false;
            }
        }

        //Clear all the operation logs in the device.
        public bool ClearSLog(int iMachineNumber)
        {
            axCZKEM.EnableDevice(iMachineNumber, false);//disable the device
            if (axCZKEM.ClearSLog(iMachineNumber))
            {
                axCZKEM.RefreshData(iMachineNumber);//the data in the device should be refreshed
                axCZKEM.EnableDevice(iMachineNumber, true);//enable the device
                return true;
            }
            else
            {
                axCZKEM.EnableDevice(iMachineNumber, true);//enable the device
                return false;
            }
        }

        public bool ClearGLog(int iMachineNumber)
        {
            axCZKEM.EnableDevice(iMachineNumber, false);//disable the device
            if (axCZKEM.ClearGLog(iMachineNumber))
            {
                axCZKEM.RefreshData(iMachineNumber);//the data in the device should be refreshed
                axCZKEM.EnableDevice(iMachineNumber, true);//enable the device
                return true;
            }
            else
            {
                axCZKEM.EnableDevice(iMachineNumber, true);//enable the device
                return false;
            }
        }

        public bool ClearAdministrators(int iMachineNumber)
        {
            axCZKEM.EnableDevice(iMachineNumber, false);//disable the device
            if (axCZKEM.ClearAdministrators(iMachineNumber))
            {
                axCZKEM.RefreshData(iMachineNumber);//the data in the device should be refreshed
                axCZKEM.EnableDevice(iMachineNumber, true);//enable the device
                return true;
            }
            else
            {
                axCZKEM.EnableDevice(iMachineNumber, true);//enable the device
                return false;
            }
        }

        public bool ClearTmp(int iMachineNumber)
        {
            int iDataFlag = 2;
            return ClearData(iMachineNumber, iDataFlag);
        }

        public bool ClearUsers(int iMachineNumber)
        {
            int iDataFlag = 5;
            return ClearData(iMachineNumber, iDataFlag);
        }

        public bool ClearData(int iMachineNumber, int iDataFlag)
        {
            axCZKEM.EnableDevice(iMachineNumber, false);//disable the device
            if (axCZKEM.ClearData(iMachineNumber, iDataFlag))
            {
                axCZKEM.RefreshData(iMachineNumber);//the data in the device should be refreshed
                axCZKEM.EnableDevice(iMachineNumber, true);//enable the device
                return true;
            }
            else
            {
                axCZKEM.EnableDevice(iMachineNumber, true);//enable the device
                return false;
            }
        }

        private void zkTimer1_Tick(Object source, ElapsedEventArgs e)
        {
            try
            {
                _I_MACHINE_NUMBER = 1;//In fact,when you are using the tcp/ip communication,this parameter will be ignored,that is any integer will all right.Here we use 1.
                if (this.axCZKEM.RegEvent(_I_MACHINE_NUMBER, 65535))//Here you can register the realtime events that you want to be triggered(the parameters 65535 means registering all)
                {
                    this.axCZKEM.OnVerify += new zkemkeeper._IZKEMEvents_OnVerifyEventHandler(axCZKEM1_OnVerify);
                    this.axCZKEM.OnAttTransactionEx += new zkemkeeper._IZKEMEvents_OnAttTransactionExEventHandler(axCZKEM1_OnAttTransactionEx);
                }
            }
            catch (Exception ex)
            {
                Messages.Exception("Zkemkeeper (zkTimer1_Tick) ", ex);
            }
        }

        public bool StopOneDirect()
        {
            try
            {
                this.axCZKEM.OnFinger -= new zkemkeeper._IZKEMEvents_OnFingerEventHandler(axCZKEM1_OnFinger);
                this.axCZKEM.OnVerify -= new zkemkeeper._IZKEMEvents_OnVerifyEventHandler(axCZKEM1_OnVerify);
                this.axCZKEM.OnAttTransactionEx -= new zkemkeeper._IZKEMEvents_OnAttTransactionExEventHandler(axCZKEM1_OnAttTransactionEx);
                this.axCZKEM.OnFingerFeature -= new zkemkeeper._IZKEMEvents_OnFingerFeatureEventHandler(axCZKEM1_OnFingerFeature);
                this.axCZKEM.OnEnrollFingerEx -= new zkemkeeper._IZKEMEvents_OnEnrollFingerExEventHandler(axCZKEM1_OnEnrollFingerEx);
                this.axCZKEM.OnDeleteTemplate -= new zkemkeeper._IZKEMEvents_OnDeleteTemplateEventHandler(axCZKEM1_OnDeleteTemplate);
                this.axCZKEM.OnNewUser -= new zkemkeeper._IZKEMEvents_OnNewUserEventHandler(axCZKEM1_OnNewUser);
                this.axCZKEM.OnHIDNum -= new zkemkeeper._IZKEMEvents_OnHIDNumEventHandler(axCZKEM1_OnHIDNum);
                this.axCZKEM.OnAlarm -= new zkemkeeper._IZKEMEvents_OnAlarmEventHandler(axCZKEM1_OnAlarm);
                this.axCZKEM.OnDoor -= new zkemkeeper._IZKEMEvents_OnDoorEventHandler(axCZKEM1_OnDoor);
                this.axCZKEM.OnWriteCard -= new zkemkeeper._IZKEMEvents_OnWriteCardEventHandler(axCZKEM1_OnWriteCard);
                this.axCZKEM.OnEmptyCard -= new zkemkeeper._IZKEMEvents_OnEmptyCardEventHandler(axCZKEM1_OnEmptyCard);
                this.axCZKEM.OnKeyPress -= new zkemkeeper._IZKEMEvents_OnKeyPressEventHandler(axCZKEM1_OnKeyPress);
                return true;
            }
            catch (Exception ex)
            {
                Messages.Exception("Zkemkeeper (StopOneDirect) ", ex);
                return false;
            }
        }

        public bool StartOneDirect()
        {
            try
            {
                _I_MACHINE_NUMBER = 1;//In fact,when you are using the tcp/ip communication,this parameter will be ignored,that is any integer will all right.Here we use 1.
                if (this.axCZKEM.RegEvent(_I_MACHINE_NUMBER, 65535))//Here you can register the realtime events that you want to be triggered(the parameters 65535 means registering all)
                {
                    this.axCZKEM.OnFinger += new zkemkeeper._IZKEMEvents_OnFingerEventHandler(axCZKEM1_OnFinger);
                    this.axCZKEM.OnVerify += new zkemkeeper._IZKEMEvents_OnVerifyEventHandler(axCZKEM1_OnVerify);
                    this.axCZKEM.OnAttTransactionEx += new zkemkeeper._IZKEMEvents_OnAttTransactionExEventHandler(axCZKEM1_OnAttTransactionEx);
                    this.axCZKEM.OnFingerFeature += new zkemkeeper._IZKEMEvents_OnFingerFeatureEventHandler(axCZKEM1_OnFingerFeature);
                    this.axCZKEM.OnEnrollFingerEx += new zkemkeeper._IZKEMEvents_OnEnrollFingerExEventHandler(axCZKEM1_OnEnrollFingerEx);
                    this.axCZKEM.OnDeleteTemplate += new zkemkeeper._IZKEMEvents_OnDeleteTemplateEventHandler(axCZKEM1_OnDeleteTemplate);
                    this.axCZKEM.OnNewUser += new zkemkeeper._IZKEMEvents_OnNewUserEventHandler(axCZKEM1_OnNewUser);
                    this.axCZKEM.OnHIDNum += new zkemkeeper._IZKEMEvents_OnHIDNumEventHandler(axCZKEM1_OnHIDNum);
                    this.axCZKEM.OnAlarm += new zkemkeeper._IZKEMEvents_OnAlarmEventHandler(axCZKEM1_OnAlarm);
                    this.axCZKEM.OnDoor += new zkemkeeper._IZKEMEvents_OnDoorEventHandler(axCZKEM1_OnDoor);
                    this.axCZKEM.OnWriteCard += new zkemkeeper._IZKEMEvents_OnWriteCardEventHandler(axCZKEM1_OnWriteCard);
                    this.axCZKEM.OnEmptyCard += new zkemkeeper._IZKEMEvents_OnEmptyCardEventHandler(axCZKEM1_OnEmptyCard);
                    this.axCZKEM.OnKeyPress += new zkemkeeper._IZKEMEvents_OnKeyPressEventHandler(axCZKEM1_OnKeyPress);

                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Messages.Exception("Zkemkeeper (StartOneDirect) ", ex);
                return false;
            }
        }

        public bool StartOne(int id, String ip)
        {
            try
            {
                int idwErrorCode = 0;
                bool bIsConnected = axCZKEM.Connect_Net(ip, 4370);
                if (bIsConnected == true)
                {
                    _I_MACHINE_NUMBER = 1;//In fact,when you are using the tcp/ip communication,this parameter will be ignored,that is any integer will all right.Here we use 1.
                    if (this.axCZKEM.RegEvent(_I_MACHINE_NUMBER, 65535))//Here you can register the realtime events that you want to be triggered(the parameters 65535 means registering all)
                    {
                        if (PointeuseBLL.Connect(id, _I_MACHINE_NUMBER))
                        {
                            this.axCZKEM.OnFinger += new zkemkeeper._IZKEMEvents_OnFingerEventHandler(axCZKEM1_OnFinger);
                            this.axCZKEM.OnVerify += new zkemkeeper._IZKEMEvents_OnVerifyEventHandler(axCZKEM1_OnVerify);
                            this.axCZKEM.OnAttTransactionEx += new zkemkeeper._IZKEMEvents_OnAttTransactionExEventHandler(axCZKEM1_OnAttTransactionEx);
                            this.axCZKEM.OnFingerFeature += new zkemkeeper._IZKEMEvents_OnFingerFeatureEventHandler(axCZKEM1_OnFingerFeature);
                            this.axCZKEM.OnEnrollFingerEx += new zkemkeeper._IZKEMEvents_OnEnrollFingerExEventHandler(axCZKEM1_OnEnrollFingerEx);
                            this.axCZKEM.OnDeleteTemplate += new zkemkeeper._IZKEMEvents_OnDeleteTemplateEventHandler(axCZKEM1_OnDeleteTemplate);
                            this.axCZKEM.OnNewUser += new zkemkeeper._IZKEMEvents_OnNewUserEventHandler(axCZKEM1_OnNewUser);
                            this.axCZKEM.OnHIDNum += new zkemkeeper._IZKEMEvents_OnHIDNumEventHandler(axCZKEM1_OnHIDNum);
                            this.axCZKEM.OnAlarm += new zkemkeeper._IZKEMEvents_OnAlarmEventHandler(axCZKEM1_OnAlarm);
                            this.axCZKEM.OnDoor += new zkemkeeper._IZKEMEvents_OnDoorEventHandler(axCZKEM1_OnDoor);
                            this.axCZKEM.OnWriteCard += new zkemkeeper._IZKEMEvents_OnWriteCardEventHandler(axCZKEM1_OnWriteCard);
                            this.axCZKEM.OnEmptyCard += new zkemkeeper._IZKEMEvents_OnEmptyCardEventHandler(axCZKEM1_OnEmptyCard);
                            this.axCZKEM.OnKeyPress += new zkemkeeper._IZKEMEvents_OnKeyPressEventHandler(axCZKEM1_OnKeyPress);

                            Utils.WriteLog("Connexion à l'appareil ip " + ip + " établie");

                            return true;
                        }
                    }
                }
                else
                {
                    axCZKEM.GetLastError(ref idwErrorCode);
                    Utils.WriteLog("Impossible de se connecter à l'appareil " + ip + ", ErrorCode=" + idwErrorCode.ToString());

                }
                return false;
            }
            catch (Exception ex)
            {
                Messages.Exception("Zkemkeeper (StartOne) ", ex);
                return false;
            }
        }

        #region RealTime Events

        //When you place your finger on sensor of the device,this event will be triggered
        private void axCZKEM1_OnFinger()
        {
            Utils.WriteLog("Identification demandée sur la pointeuse (" + _POINTEUSE.Ip + ") ...");
        }

        //After you have placed your finger on the sensor(or swipe your card to the device),this event will be triggered.
        //If you passes the verification,the returned value userid will be the user enrollnumber,or else the value will be -1;
        private void axCZKEM1_OnVerify(int iUserID)
        {
            _EMPLOYE = EmployeBLL.OneById(Convert.ToInt32(iUserID));
            Utils.WriteLog("-- Vérification identitée demandée...");
            if (iUserID != -1)
            {
                Utils.WriteLog("--- Vérification reussie, l'employé est " + _EMPLOYE.Nom + " " + _EMPLOYE.Prenom);
            }
            else
            {
                Utils.WriteLog("--- Vérification echouée... ");

            }
        }
        private void axCZKEM1_OnVerifyOne(int iUserID)
        {
            _EMPLOYE = EmployeBLL.OneById(Convert.ToInt32(iUserID));
            Utils.WriteLog("-- Vérification identitée demandée...");
            if (iUserID < 0 || (_EMPLOYE != null ? _EMPLOYE.Id < 1 : true))
            {
                Utils.WriteLog("--- Vérification echouée... ");
                return;
            }
            Utils.WriteLog("--- Vérification reussie, l'employé est " + _EMPLOYE.Nom + " " + _EMPLOYE.Prenom);
            if (Constantes.FORM_EMPLOYE != null)
            {
                Form_Employe f = Constantes.FORM_EMPLOYE;
                f.LoadInfosEmploye(_EMPLOYE);
            }
        }

        //If your fingerprint(or your card) passes the verification,this event will be triggered
        private void axCZKEM1_OnAttTransactionEx(string sEnrollNumber, int iIsInValid, int iAttState, int iVerifyMethod, int iYear, int iMonth, int iDay, int iHour, int iMinute, int iSecond, int iWorkCode)
        {
            if (_EMPLOYE != null ? _EMPLOYE.Id > 0 : false)
            {
                Cursor c;
                Utils.WriteLog("Identification reussie...");
                Utils.WriteLog("... pour l'employé : " + _EMPLOYE.Nom + " " + _EMPLOYE.Prenom);
                Utils.WriteLog("... pour la date : " + iYear.ToString() + "-" + iMonth.ToString() + "-" + iDay.ToString() + " " + iHour.ToString() + ":" + iMinute.ToString() + ":" + iSecond.ToString());
                DateTime current_time = new DateTime(iYear, iMonth, iDay, iHour, iMinute, iSecond);
                DateTime current_date = new DateTime(iYear, iMonth, iDay, iHour, iMinute, iSecond);
                if (Fonctions.OnSavePointage(_EMPLOYE, current_time, current_date, _POINTEUSE))
                {
                    c = Cursors.WaitCursor;
                    Utils.WriteLog("Pointage enregistré avec succes...");

                    IOEMDevice iO = new IOEMDevice();
                    iO.iMachineNumber = _I_MACHINE_NUMBER;
                    iO.iParams1 = (int)_EMPLOYE.Id;
                    iO.iParams2 = 0;
                    iO.iParams3 = 0;
                    iO.iParams4 = 0;
                    iO.idwManipulation = 6;
                    iO.idwYear = iYear;
                    iO.idwMonth = iMonth;
                    iO.idwDay = iDay;
                    iO.idwHour = iHour;
                    iO.idwMinute = iMinute;
                    iO.idwSecond = iSecond;
                    Logs.WriteCsv((IOEMDevice)iO);

                    ClearLCD(true);
                    if (_PLANNING.Valide)
                    {
                        WriteLCD(true, 0, 4, iHour > 13 ? "Bonsoir" : "Bonjour");
                        WriteLCD(true, 1, 0, _EMPLOYE.Nom + " " + _EMPLOYE.Prenom);
                    }
                    else
                    {
                        WriteLCD(true, 0, 4, iHour > 13 ? "Bonsoir" : "Bonjour");
                        WriteLCD(true, 1, 0, _EMPLOYE.Nom + " " + _EMPLOYE.Prenom);
                        WriteLCD(true, 2, 0, "Vous n'êtes pas programmé");
                    }
                    Fonctions.DefaultLCD(true);
                }
                c = Cursors.Default;
            }
        }

        //When you have enrolled your finger,this event will be triggered and return the quality of the fingerprint you have enrolled
        public void axCZKEM1_OnFingerFeature(int iScore)
        {
            if (_EMPLOYE != null ? _EMPLOYE.Id > 0 : false)
            {
                _FINGER_IN -= 1;
                if (iScore < 0)
                {
                    Utils.WriteLog("---- La qualité de l'empreinte digitale de l'employe " + _EMPLOYE.NomPrenom + " est mauvaise");
                    return;
                }
                else
                {
                    Utils.WriteLog("---- Empreinte de l'employe " + _EMPLOYE.NomPrenom + " en cours de traitement.... (" + _FINGER_IN + " essai(e) restant)");
                }
                if (_FINGER_IN == 2)
                {
                    _FORM_ADD_EMPREINTE.box_doigt.Image = global::ZK_Lymytz.Properties.Resources.empreinte02;
                }
                else if (_FINGER_IN == 1)
                {
                    _FORM_ADD_EMPREINTE.box_doigt.Image = global::ZK_Lymytz.Properties.Resources.empreinte03;
                }
                else
                {
                    _FORM_ADD_EMPREINTE.box_doigt.Image = global::ZK_Lymytz.Properties.Resources.empreinte;
                }

                if (_FINGER_IN == 0 && iScore > -1)
                {
                    if (Fonctions.StartSave(this, _POINTEUSE.Ip))
                    {
                        _FORM_ADD_EMPREINTE.ResetEmploye();
                        _FORM_ADD_EMPREINTE.ResetDoigt();
                        _FORM_ADD_EMPREINTE.txt_result.BackColor = Color.Green;
                        _FORM_ADD_EMPREINTE.txt_result.Text = "Correct";
                    }
                    else
                    {
                        _FORM_ADD_EMPREINTE.txt_result.BackColor = Color.Red;
                        _FORM_ADD_EMPREINTE.txt_result.Text = "Incorrect";
                    }
                    _FORM_ADD_EMPREINTE.box_doigt.Image = global::ZK_Lymytz.Properties.Resources.empreinte;
                    _FINGER_IN = 3;
                    _FLAG = 3;
                    Utils.WriteLog("Fin de l'enregistrement de l'empreinte, Employe " + _EMPLOYE.NomPrenom + ", Doigt (" + _FINGER.Doigt + ") - Main (" + _FINGER.Main + ")");
                }
            }
        }

        //When you are enrolling your finger,this event will be triggered.
        private void axCZKEM1_OnEnrollFingerEx(string sEnrollNumber, int iFingerIndex, int iActionResult, int iTemplateLength)
        {
            if (iActionResult == 0)
            {
                Utils.WriteLog("RTEvent OnEnrollFigerEx Has been Triggered....");
                Utils.WriteLog(".....UserID: " + sEnrollNumber + " Index: " + iFingerIndex.ToString() + " tmpLen: " + iTemplateLength.ToString());
            }
            else
            {
                Utils.WriteLog("RTEvent OnEnrollFigerEx Has been Triggered Error,actionResult=" + iActionResult.ToString());
            }
        }

        //When you have deleted one one fingerprint template,this event will be triggered.
        private void axCZKEM1_OnDeleteTemplate(int iEnrollNumber, int iFingerIndex)
        {
            Utils.WriteLog("RTEvent OnDeleteTemplate Has been Triggered...");
            Utils.WriteLog("...UserID=" + iEnrollNumber.ToString() + " FingerIndex=" + iFingerIndex.ToString());
        }

        //When you have enrolled a new user,this event will be triggered.
        private void axCZKEM1_OnNewUser(int iEnrollNumber)
        {
            Utils.WriteLog("RTEvent OnNewUser Has been Triggered...");
            Utils.WriteLog("...NewUserID=" + iEnrollNumber.ToString());
        }

        //When you swipe a card to the device, this event will be triggered to show you the card number.
        private void axCZKEM1_OnHIDNum(int iCardNumber)
        {
            Utils.WriteLog("RTEvent OnHIDNum Has been Triggered...");
            Utils.WriteLog("...Cardnumber=" + iCardNumber.ToString());
        }

        //When the dismantling machine or duress alarm occurs, trigger this event.
        private void axCZKEM1_OnAlarm(int iAlarmType, int iEnrollNumber, int iVerified)
        {
            //Evenement pour l'alarme ou le message à envoyer

            //Utils.WriteLog("RTEvent OnAlarm Has been Triggered...");
            //Utils.WriteLog("...AlarmType=" + iAlarmType.ToString());
            //Utils.WriteLog("...EnrollNumber=" + iEnrollNumber.ToString());
            //Utils.WriteLog("...Verified=" + iVerified.ToString());
        }

        //Door sensor event
        private void axCZKEM1_OnDoor(int iEventType)
        {
            Utils.WriteLog("RTEvent Ondoor Has been Triggered...");
            Utils.WriteLog("...EventType=" + iEventType.ToString());
        }

        //When you have emptyed the Mifare card,this event will be triggered.
        private void axCZKEM1_OnEmptyCard(int iActionResult)
        {
            Utils.WriteLog("RTEvent OnEmptyCard Has been Triggered...");
            if (iActionResult == 0)
            {
                Utils.WriteLog("...Empty Mifare Card OK");
            }
            else
            {
                Utils.WriteLog("...Empty Failed");

            }
        }

        //When you have written into the Mifare card ,this event will be triggered.
        private void axCZKEM1_OnWriteCard(int iEnrollNumber, int iActionResult, int iLength)
        {
            Utils.WriteLog("RTEvent OnWriteCard Has been Triggered...");
            if (iActionResult == 0)
            {
                Utils.WriteLog("...Write Mifare Card OK");
                Utils.WriteLog("...EnrollNumber=" + iEnrollNumber.ToString());
                Utils.WriteLog("...TmpLength=" + iLength.ToString());
            }
            else
            {
                Utils.WriteLog("...Write Failed");

            }
        }


        private void axCZKEM1_OnKeyPress(int Key)
        {
            string keyStr = null;
            if (47 < Key && Key < 58)
            {
                int key = Key - 48;
                keyStr = key.ToString();
            }
            else
            {
                switch (Key)
                {
                    case 10:
                        keyStr = "Menu";
                        break;
                    case 11:
                        keyStr = "Ok";
                        break;
                    case 12:
                        keyStr = "Esc";
                        break;
                    case 13:
                        keyStr = "Haut";
                        break;
                    case 14:
                        keyStr = "Bas";
                        break;
                    case 15:
                        keyStr = "Arret Systeme";
                        break;
                    default:
                        break;
                }
            }
        }

        //After function GetRTLog() is called ,RealTime Events will be triggered. 
        //When you are using these two functions, it will request data from the device forwardly.
        private void rtTimer_Tick(object sender, EventArgs e)
        {
            if (axCZKEM.ReadRTLog(_I_MACHINE_NUMBER))
            {
                while (axCZKEM.GetRTLog(_I_MACHINE_NUMBER))
                {

                }
            }
        }

        #endregion

    }
}
