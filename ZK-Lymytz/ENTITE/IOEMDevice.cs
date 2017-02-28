using System;
using System.Collections.Generic;
using System.Text;

namespace ZK_Lymytz.ENTITE
{
    public class IOEMDevice : IComparable
    {

        public IOEMDevice() { }

        public IOEMDevice(int iMachineNumber)
        {
            this.iMachineNumber = iMachineNumber;
        }

        public IOEMDevice(int iMachineNumber, int idwTMachineNumber, int idwSEnrollNumber, int iParams4, int iParams1, int iParams2, int idwManipulation, int iParams3, int idwYear, int idwMonth, int idwDay, int idwHour, int idwMinute, int idwSecond)
        {
            this.iMachineNumber = iMachineNumber;
            this.idwTMachineNumber = idwTMachineNumber;
            this.idwSEnrollNumber = idwSEnrollNumber;
            this.iParams4 = iParams4;
            this.iParams1 = iParams1;
            this.iParams2 = iParams2;
            this.idwManipulation = idwManipulation;
            this.iParams3 = iParams3;
            this.idwYear = idwYear;
            this.idwMonth = idwMonth;
            this.idwDay = idwDay;
            this.idwHour = idwHour;
            this.idwMinute = idwMinute;
            this.idwSecond = idwSecond;
        }

        public IOEMDevice(Pointeuse iPointeuse, int iMachineNumber, int idwTMachineNumber, int idwSEnrollNumber, int idwYear, int idwMonth, int idwDay, int idwHour, int idwMinute, int idwSecond)
        {
            this.iMachineNumber = iMachineNumber;
            this.idwTMachineNumber = idwTMachineNumber;
            this.idwSEnrollNumber = idwSEnrollNumber;
            this.iParams1 = idwSEnrollNumber;
            this.idwYear = idwYear;
            this.idwMonth = idwMonth;
            this.idwDay = idwDay;
            this.idwHour = idwHour;
            this.idwMinute = idwMinute;
            this.idwSecond = idwSecond;
            this.pointeuse = iPointeuse;
        }

        public int id = 0;
        public int iMachineNumber = 0;
        public int idwTMachineNumber = 0;
        public int idwSEnrollNumber = 0;
        public int idwManipulation = 0;//Action --6 Register a fingerprint --7 Register a password --8 Register a ID Card
        public int iParams1 = 0; // ID Users
        public int iParams2 = 0; //Resutat od operation --0 Succes --#0 Echec
        public int iParams3 = 0; //Index fingerprint register
        public int iParams4 = 0; //Length of the fingerprint template
        public int idwYear = 0;
        public int idwMonth = 0;
        public int idwDay = 0;
        public int idwHour = 0;
        public int idwMinute = 0;
        public int idwSecond = 0;
        public bool exclure = false;
        public bool iCorrect = false;
        public Pointeuse pointeuse = new Pointeuse();

        public System.Drawing.Bitmap Icon()
        {
            if (iCorrect)
            {
                return new System.Drawing.Bitmap(global::ZK_Lymytz.Properties.Resources.vu, 16, 16);
            }
            return new System.Drawing.Bitmap(global::ZK_Lymytz.Properties.Resources.vu_non, 16, 16);
        }

        public DateTime CurrentDateTime
        {
            get { return new DateTime(idwYear, idwMonth, idwDay, idwHour, idwMinute, idwSecond); }
            set { }
        }

        public DateTime CurrentDate
        {
            get { return new DateTime(idwYear, idwMonth, idwDay, 0, 0, 0); }
            set { }
        }

        public int CompareTo(Object o)
        {
            IOEMDevice f = (IOEMDevice)o;
            if (idwYear.Equals(f.idwYear))
            {
                if (idwMonth.Equals(f.idwMonth))
                {
                    if (idwDay.Equals(f.idwDay))
                    {
                        if (idwHour.Equals(f.idwHour))
                        {
                            if (idwMinute.Equals(f.idwMinute))
                            {
                                if (idwSecond.Equals(f.idwSecond))
                                {
                                    return idwSEnrollNumber.CompareTo(f.idwSEnrollNumber);
                                }
                                return idwSecond.CompareTo(f.idwSecond);
                            }
                            return idwMinute.CompareTo(f.idwMinute);
                        }
                        return idwHour.CompareTo(f.idwHour);
                    }
                    return idwDay.CompareTo(f.idwDay);
                }
                return idwMonth.CompareTo(f.idwMonth);
            }
            return idwYear.CompareTo(f.idwYear);
        }
    }
}
