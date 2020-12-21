using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;

using ZK_Lymytz.BLL;
using ZK_Lymytz.TOOLS;
using ZK_Lymytz.ENTITE;

namespace ZK_Lymytz.TOOLS
{
    public class Logs
    {
        public static void WriteCsv(IOEMDevice iO)
        {
            string fileName = TOOLS.Chemins.CheminDatabase() + "LogRecord.csv";
            WriteCsv(fileName, iO);
        }

        public static void WriteCsv(string fileName, IOEMDevice iO)
        {
            if (!File.Exists(fileName))
            {
                SaveCsv(fileName, iO);
            }
            else
            {
                UpdateCsv(fileName, iO);
            }
        }

        public static void SaveCsv(string fileName, IOEMDevice iO)
        {
            try
            {
                // Write sample data to CSV file
                using (CsvFileWriter writer = new CsvFileWriter(fileName, true, Encoding.UTF8))
                {
                    List<string> row = new List<string>();
                    row.Add(iO.iMachineNumber.ToString());
                    row.Add(iO.idwSEnrollNumber.ToString());
                    row.Add(iO.idwVerifyMode.ToString());
                    row.Add(iO.idwInOutMode.ToString());
                    row.Add(iO.idwWorkCode.ToString());
                    row.Add(iO.idwReserved.ToString());
                    row.Add(iO.date_action.ToString("dd-MM-yyyy"));
                    row.Add(iO.time_action.ToString("HH:mm:ss"));
                    row.Add(iO.date_time_action.ToString("dd-MM-yyyy HH:mm:ss"));
                    row.Add(iO.pointeuse.Id.ToString());

                    writer.WriteRow(row);
                }
            }
            catch (Exception ex)
            {
                Utils.Exception(ex);
            }
        }

        public static void UpdateCsv(string fileName, IOEMDevice iO)
        {
            try
            {
                // Write sample data to CSV file
                FileInfo file = new FileInfo(fileName);
                if (file.Length == 8192)
                {
                    string destination = file.DirectoryName + Constantes.FILE_SEPARATOR + file.Name.Replace(file.Extension, "") + "_" + DateTime.Now.ToShortDateString().Replace("/", "-") + file.Extension;
                    file.MoveTo(destination);
                    SaveCsv(fileName, iO);
                }
                else
                {
                    using (CsvFileWriter writer = new CsvFileWriter(fileName, true, Encoding.UTF8))
                    {
                        List<string> row = new List<string>();
                        row.Add(iO.iMachineNumber.ToString());
                        row.Add(iO.idwSEnrollNumber.ToString());
                        row.Add(iO.idwVerifyMode.ToString());
                        row.Add(iO.idwInOutMode.ToString());
                        row.Add(iO.idwWorkCode.ToString());
                        row.Add(iO.idwReserved.ToString());
                        row.Add(iO.date_action.ToString("dd-MM-yyyy"));
                        row.Add(iO.time_action.ToString("HH:mm:ss"));
                        row.Add(iO.date_time_action.ToString("dd-MM-yyyy HH:mm:ss"));
                        row.Add(iO.pointeuse.Id.ToString());

                        writer.WriteRow(row);
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.Exception(ex);
            }
        }

        public static List<IOEMDevice> ReadCsv()
        {
            return ReadCsv(TOOLS.Chemins.CheminDatabase() + "LogRecord.csv");
        }

        public static List<IOEMDevice> ReadCsv(string fileName)
        {
            try
            {
                if (!File.Exists(fileName))
                {
                    return new List<IOEMDevice>();
                }

                List<IOEMDevice> l = new List<IOEMDevice>();
                // Read sample data from CSV file
                using (CsvFileReader reader = new CsvFileReader(fileName))
                {
                    List<string> row = new List<string>();
                    while (reader.ReadRow(row))
                    {
                        int i = 0;
                        IOEMDevice e = new IOEMDevice(Convert.ToInt32(row.Count > 0 ? row[i++] : "0"));
                        if (row.Count == 10)//Chargement de la version 2 du fichier 
                        {
                            e.idwSEnrollNumber = Convert.ToInt32(row.Count > 1 ? row[i++] : "0");
                            e.idwVerifyMode = Convert.ToInt32(row.Count > 2 ? row[i++] : "0");
                            e.idwInOutMode = Convert.ToInt32(row.Count > 3 ? row[i++] : "0");
                            e.idwWorkCode = Convert.ToInt32(row.Count > 4 ? row[i++] : "0");
                            e.idwReserved = Convert.ToInt32(row.Count > 5 ? row[i++] : "0");
                            e.date_action = row.Count > 6 ? Convert.ToDateTime(row[i++]) : DateTime.Now;
                            e.time_action = row.Count > 7 ? Convert.ToDateTime(row[i++]) : DateTime.Now; ;
                            e.date_time_action = row.Count > 8 ? Convert.ToDateTime(row[i++]) : DateTime.Now; ;
                            e.pointeuse = new Pointeuse(Convert.ToInt32(row.Count > 9 ? row[i++] : "0"));
                            e.idwYear = Convert.ToInt32(e.date_action.ToString("yyyy"));
                            e.idwMonth = Convert.ToInt32(e.date_action.ToString("MM"));
                            e.idwDay = Convert.ToInt32(e.date_action.ToString("dd"));
                            e.idwHour = Convert.ToInt32(e.time_action.ToString("HH"));
                            e.idwMinute = Convert.ToInt32(e.time_action.ToString("mm"));
                            e.idwSecond = Convert.ToInt32(e.time_action.ToString("ss"));
                        }
                        else//Chargement de la version 1 du fichier
                        {
                            e.idwTMachineNumber = Convert.ToInt32(row.Count > 1 ? row[i++] : "0");
                            e.idwSEnrollNumber = Convert.ToInt32(row.Count > 2 ? row[i++] : "0");
                            e.iParams4 = Convert.ToInt32(row.Count > 3 ? row[i++] : "0");
                            e.iParams1 = Convert.ToInt32(row.Count > 4 ? row[i++] : "0");
                            e.iParams2 = Convert.ToInt32(row.Count > 5 ? row[i++] : "0");
                            e.idwManipulation = Convert.ToInt32(row.Count > 6 ? row[i++] : "0");
                            e.iParams3 = Convert.ToInt32(row.Count > 7 ? row[i++] : "0");
                            e.idwYear = Convert.ToInt32(row.Count > 8 ? row[i++] : "0");
                            e.idwMonth = Convert.ToInt32(row.Count > 9 ? row[i++] : "0");
                            e.idwDay = Convert.ToInt32(row.Count > 10 ? row[i++] : "0");
                            e.idwHour = Convert.ToInt32(row.Count > 11 ? row[i++] : "0");
                            e.idwMinute = Convert.ToInt32(row.Count > 12 ? row[i++] : "0");
                            e.idwSecond = Convert.ToInt32(row.Count > 13 ? row[i++] : "0");
                            e.date_action = new DateTime(e.idwYear, e.idwMonth, e.idwDay, 0, 0, 0);
                            e.time_action = new DateTime(e.idwYear, e.idwMonth, e.idwDay, e.idwHour, e.idwMinute, 0);
                            e.date_time_action = new DateTime(e.idwYear, e.idwMonth, e.idwDay, e.idwHour, e.idwMinute, 0);
                        }
                        l.Add(e);
                        Constantes.LoadPatience(false);
                    }
                }
                return l;
            }
            catch (Exception ex)
            {
                Utils.Exception(ex);
            }
            return new List<IOEMDevice>();
        }

        public static void WriteTxt(string message)
        {
            string fileName = TOOLS.Chemins.CheminDatabase() + "Log.txt";
            WriteTxt(fileName, message);
        }

        public static void WriteTxt(string fileName, string message)
        {
            if (!File.Exists(fileName))
            {
                SaveTxt(fileName, message);
            }
            else
            {
                UpdateTxt(fileName, message);
            }
        }

        public static void SaveTxt(string fileName, string message)
        {
            using (TxtFileWriter writer = new TxtFileWriter(fileName))
            {
                writer.WriteRow(message);
            }
        }

        public static void UpdateTxt(string fileName, string message)
        {
            FileInfo file = new FileInfo(fileName);
            if (file.Length == 8192)
            {
                string destination = file.DirectoryName + Constantes.FILE_SEPARATOR + file.Name.Replace(file.Extension, "") + "_" + DateTime.Now.ToShortDateString().Replace("/", "-") + file.Extension;
                file.MoveTo(destination);
                SaveTxt(fileName, message);
            }
            else
            {
                using (TxtFileWriter writer = new TxtFileWriter(fileName, true, Encoding.UTF8))
                {
                    writer.WriteRow(message);
                }
            }
        }

        public static List<string> ReadTxt()
        {
            return ReadTxt(TOOLS.Chemins.CheminDatabase() + "Log.txt");
        }

        public static List<string> ReadTxt(DateTime dateDebut, DateTime dateFin)
        {
            return ReadTxt(TOOLS.Chemins.CheminDatabase() + "Log.txt", dateDebut, dateFin);
        }

        public static List<string> ReadTxt(string fileName)
        {
            if (!File.Exists(fileName))
            {
                return new List<string>();
            }

            List<string> l = new List<string>();
            using (TxtFileReader reader = new TxtFileReader(fileName))
            {
                l = reader.ReadRow();
            }
            return l;
        }

        public static List<string> ReadTxt(string fileName, DateTime dateDebut, DateTime dateFin)
        {
            if (!File.Exists(fileName))
            {
                return new List<string>();
            }

            List<string> l = new List<string>();
            using (TxtFileReader reader = new TxtFileReader(fileName))
            {
                l = reader.ReadRow(dateDebut, dateFin);
            }
            return l;
        }

        public static List<object[]> ReadEvenLog(string logName, string sourceName)
        {
            System.Diagnostics.EventLog events = new System.Diagnostics.EventLog();
            events.Log = logName;
            events.Source = sourceName;
            events.MachineName = Chemins.machineName;
            List<object[]> list = new List<object[]>();
            if (System.Diagnostics.EventLog.SourceExists(events.Source))
            {
                foreach (EventLogEntry record in events.Entries)
                {
                    if (record.Source.Equals(sourceName))
                    {
                        object[] r = new object[] { record.TimeGenerated, record.TimeWritten, record.Message };
                        list.Add(r);
                    }
                }
            }
            return list;
        }

    }
}
