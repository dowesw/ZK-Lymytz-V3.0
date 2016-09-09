using System;
using System.Collections.Generic;
using System.Text;

namespace ZK_LymytzService.ENTITE
{
    public class IOEMDevice
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

        public IOEMDevice(int iMachineNumber, int idwTMachineNumber, int idwSEnrollNumber, int idwYear, int idwMonth, int idwDay, int idwHour, int idwMinute, int idwSecond)
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
        }

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
    }
}
