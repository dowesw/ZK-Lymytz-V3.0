using System;
using System.Collections.Generic;
using System.Text;

using ZK_Lymytz.ENTITE;
using ZK_Lymytz.TOOLS;
using ZK_Lymytz.DAO;

namespace ZK_Lymytz.BLL
{
    class IOEMDeviceBLL
    {

        public static bool Insert(IOEMDevice bean, string adresse)
        {
            try
            {
                return IOEMDeviceDAO.getInsert(bean, adresse);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static bool Delete(Pointeuse pointeuse, string adresse)
        {
            try
            {
                return IOEMDeviceDAO.getDelete(pointeuse, adresse);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static bool Delete(Pointeuse pointeuse, Employe employe, DateTime debut, DateTime fin, string adresse)
        {
            try
            {
                return IOEMDeviceDAO.getDelete(pointeuse, employe, debut, fin, adresse);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static IOEMDevice One(Pointeuse pointeuse, Employe employe, DateTime date, string adresse)
        {
            try
            {
                return IOEMDeviceDAO.getOne(pointeuse, employe, date, adresse);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<IOEMDevice> List(Pointeuse pointeuse, Employe employe, string adresse)
        {
            try
            {
                return IOEMDeviceDAO.getListByEmploye(pointeuse, employe, adresse);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<IOEMDevice> List(Pointeuse pointeuse, Employe employe, DateTime debut, DateTime fin, string adresse)
        {
            try
            {
                return IOEMDeviceDAO.getListByEmployeDates(pointeuse, employe, debut, fin, adresse);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<IOEMDevice> ListEx(Pointeuse pointeuse, List<Employe> employes, DateTime[] dates, string adresse)
        {
            try
            {
                return IOEMDeviceDAO.getListNoByEmployesDates(pointeuse, employes, dates, adresse);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
