using System;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

using Microsoft.Win32;

using ZK_LymytzService.BLL;
using ZK_LymytzService.ENTITE;

namespace ZK_LymytzService.TOOLS
{
    public class Zkemkeeper
    {
        private Pointeuse pointeuse = new Pointeuse();
        public zkemkeeper.CZKEMClass axCZKEM = new zkemkeeper.CZKEMClass();

        public Zkemkeeper()
        {

        }

        public Zkemkeeper(Pointeuse pointeuse)
        {
            this.pointeuse = pointeuse;
        }


        public void Deconnect()
        {
            axCZKEM.Disconnect();
        }

        public bool CancelOperation()
        {
            return axCZKEM.CancelOperation();
        }

        public void GetLastError(ref int idError)
        {
            axCZKEM.GetLastError(idError);
        }

        public void RefreshData(int ImachineNumber)
        {
            axCZKEM.RefreshData(ImachineNumber);
        }

        public bool ConnectNet(string ip)
        {
            return axCZKEM.Connect_Net(ip, 4370);
        }

        public bool ConnectNet(string ip, int port)
        {
            return axCZKEM.Connect_Net(ip, port);
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

        public List<IOEMDevice> GetAllAttentdData(int iMachineNumber, bool bIsConnected)
        {
            if (bIsConnected == false)
            {
                return new List<IOEMDevice>();
            }

            List<IOEMDevice> trans = new List<IOEMDevice>();
            if (axCZKEM.RegEvent(iMachineNumber, 65535))
            {
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

                        DateTime d = new DateTime(idwYear, idwMonth, idwDay, idwHour, idwMinute, idwSecond);
                        DateTime d_ = Convert.ToDateTime("31/05/2016 17:59:59");
                        if (d > d_)
                        {
                            ENTITE.IOEMDevice iO = new ENTITE.IOEMDevice(iMachineNumber, idwTMachineNumber, idwEnrollNumber, idwYear, idwMonth, idwDay, idwHour, idwMinute, idwSecond);
                            trans.Add(iO);
                        }
                    }
                }
                axCZKEM.RefreshData(iMachineNumber);//the data in the device should be refreshed
                axCZKEM.EnableDevice(iMachineNumber, true);//disable the device
            }
            return trans;
        }

        public bool StartOneDirect()
        {
            try
            {
                this.axCZKEM.OnFinger += new zkemkeeper._IZKEMEvents_OnFingerEventHandler(axCZKEM1_OnFinger);
                this.axCZKEM.OnVerify += new zkemkeeper._IZKEMEvents_OnVerifyEventHandler(axCZKEM1_OnVerify);
                this.axCZKEM.OnAttTransactionEx += new zkemkeeper._IZKEMEvents_OnAttTransactionExEventHandler(axCZKEM1_OnAttTransactionEx);
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
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool StartOne(String ip)
        {
            try
            {
                int idwErrorCode = 0;
                bool bIsConnected = axCZKEM.Connect_Net(ip, 4370);
                if (bIsConnected == true)
                {
                    Constantes.I_MACHINE_NUMBER = 1;//In fact,when you are using the tcp/ip communication,this parameter will be ignored,that is any integer will all right.Here we use 1.
                    if (axCZKEM.RegEvent(Constantes.I_MACHINE_NUMBER, 65535))//Here you can register the realtime events that you want to be triggered(the parameters 65535 means registering all)
                    {
                        this.axCZKEM.OnFinger += new zkemkeeper._IZKEMEvents_OnFingerEventHandler(axCZKEM1_OnFinger);
                        this.axCZKEM.OnVerify += new zkemkeeper._IZKEMEvents_OnVerifyEventHandler(axCZKEM1_OnVerify);
                        this.axCZKEM.OnAttTransactionEx += new zkemkeeper._IZKEMEvents_OnAttTransactionExEventHandler(axCZKEM1_OnAttTransactionEx);
                        this.axCZKEM.OnEnrollFingerEx += new zkemkeeper._IZKEMEvents_OnEnrollFingerExEventHandler(axCZKEM1_OnEnrollFingerEx);
                        this.axCZKEM.OnDeleteTemplate += new zkemkeeper._IZKEMEvents_OnDeleteTemplateEventHandler(axCZKEM1_OnDeleteTemplate);
                        this.axCZKEM.OnNewUser += new zkemkeeper._IZKEMEvents_OnNewUserEventHandler(axCZKEM1_OnNewUser);
                        this.axCZKEM.OnHIDNum += new zkemkeeper._IZKEMEvents_OnHIDNumEventHandler(axCZKEM1_OnHIDNum);
                        this.axCZKEM.OnAlarm += new zkemkeeper._IZKEMEvents_OnAlarmEventHandler(axCZKEM1_OnAlarm);
                        this.axCZKEM.OnDoor += new zkemkeeper._IZKEMEvents_OnDoorEventHandler(axCZKEM1_OnDoor);
                        this.axCZKEM.OnWriteCard += new zkemkeeper._IZKEMEvents_OnWriteCardEventHandler(axCZKEM1_OnWriteCard);
                        this.axCZKEM.OnEmptyCard += new zkemkeeper._IZKEMEvents_OnEmptyCardEventHandler(axCZKEM1_OnEmptyCard);
                        this.axCZKEM.OnKeyPress += new zkemkeeper._IZKEMEvents_OnKeyPressEventHandler(axCZKEM1_OnKeyPress);
                        //Utils.WriteLog("Connexion à l'appareil ip " + ip + " établie");
                        return true;
                    }
                }
                else
                {
                    axCZKEM.GetLastError(ref idwErrorCode);
                    //Utils.WriteLog("Impossible de se connecter à l'appareil " + ip + ", ErrorCode=" + idwErrorCode.ToString());
                }
                return false;
            }
            catch (Exception ex)
            {
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
                    Constantes.I_MACHINE_NUMBER = 1;//In fact,when you are using the tcp/ip communication,this parameter will be ignored,that is any integer will all right.Here we use 1.
                    if (axCZKEM.RegEvent(Constantes.I_MACHINE_NUMBER, 65535))//Here you can register the realtime events that you want to be triggered(the parameters 65535 means registering all)
                    {
                        if (PointeuseBLL.Connect(id, Constantes.I_MACHINE_NUMBER))
                        {
                            this.axCZKEM.OnFinger += new zkemkeeper._IZKEMEvents_OnFingerEventHandler(axCZKEM1_OnFinger);
                            this.axCZKEM.OnVerify += new zkemkeeper._IZKEMEvents_OnVerifyEventHandler(axCZKEM1_OnVerify);
                            this.axCZKEM.OnAttTransactionEx += new zkemkeeper._IZKEMEvents_OnAttTransactionExEventHandler(axCZKEM1_OnAttTransactionEx);
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
                return false;
            }
        }

        #region RealTime Events

        //When you place your finger on sensor of the device,this event will be triggered
        private void axCZKEM1_OnFinger()
        {
            Utils.WriteLog("Identification demandée...");
            ZK_LymytzService.WriteToFile("USERS : ");
        }

        //After you have placed your finger on the sensor(or swipe your card to the device),this event will be triggered.
        //If you passes the verification,the returned value userid will be the user enrollnumber,or else the value will be -1;
        private void axCZKEM1_OnVerify(int iUserID)
        {
            Constantes.iO.iParams1 = iUserID;

            Constantes.EMPLOYE = EmployeBLL.OneById(Convert.ToInt32(iUserID));
            ZK_LymytzService.WriteToFile("USERS : " + iUserID);
            Utils.WriteLog("-- Vérification identitée demandée...");
            if (iUserID != -1)
            {
                Utils.WriteLog("-- Vérification reussie, l'employé est " + Constantes.EMPLOYE.Nom + " " + Constantes.EMPLOYE.Prenom);
            }
            else
            {
                Utils.WriteLog("-- Vérification echouée... ");

            }
        }

        //If your fingerprint(or your card) passes the verification,this event will be triggered
        public void axCZKEM1_OnAttTransactionEx(string sEnrollNumber, int iIsInValid, int iAttState, int iVerifyMethod, int iYear, int iMonth, int iDay, int iHour, int iMinute, int iSecond, int iWorkCode)
        {
            Utils.WriteLog("Identification reussie...");
            Utils.WriteLog("... pour l'employé : " + Constantes.EMPLOYE.Nom + " " + Constantes.EMPLOYE.Prenom);
            Utils.WriteLog("... pour la date : " + iYear.ToString() + "-" + iMonth.ToString() + "-" + iDay.ToString() + " " + iHour.ToString() + ":" + iMinute.ToString() + ":" + iSecond.ToString());
            Constantes.CURRENT_TIME = Constantes.CURRENT_DATE = new DateTime(iYear, iMonth, iDay, iHour, iMinute, iSecond);
            if (ThreadRegEvent.OnSavePointage())
            {
                Utils.WriteLog("Pointage enregistré avec succes...");

                Constantes.iO.iMachineNumber = Constantes.I_MACHINE_NUMBER;
                Constantes.iO.iParams2 = 0;
                Constantes.iO.iParams3 = 0;
                Constantes.iO.iParams4 = 0;
                Constantes.iO.idwManipulation = 6;
                Constantes.iO.idwYear = iYear;
                Constantes.iO.idwMonth = iMonth;
                Constantes.iO.idwDay = iDay;
                Constantes.iO.idwHour = iHour;
                Constantes.iO.idwMinute = iMinute;
                Constantes.iO.idwSecond = iSecond;
                Logs.WriteCsv(Constantes.iO);
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
            if (axCZKEM.ReadRTLog(Constantes.I_MACHINE_NUMBER))
            {
                while (axCZKEM.GetRTLog(Constantes.I_MACHINE_NUMBER))
                {

                }
            }
        }

        #endregion

    }
}
