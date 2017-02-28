using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.Diagnostics;

using ZK_Lymytz.BLL;
using ZK_Lymytz.ENTITE;
using ZK_Lymytz.TOOLS;

namespace ZK_Lymytz.TOOLS
{
    public class JobScheduler
    {
        int _day = 0;
        int _hour = 0;
        int _min = 0;
        int _sec = 0;

        public JobScheduler() { }

        public JobScheduler(int day, int hour, int min, int sec)
        {
            this._day = day;
            this._hour = hour;
            this._min = min;
            this._sec = sec;
        }

        public void StartBackupDataDevice()
        {
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(RunBackupDataDevice));
            t.Start();
        }

        private void RunBackupDataDevice()
        {
            bool init_ = false;
            DateTime d = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
            while (!init_)
            {
                if (DateTime.Now.ToLongTimeString() == d.ToLongTimeString())
                {
                    System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(Fonctions.BackupLogDataDevice));
                    t.Start();

                    Constantes.JOB_BACKUPDEVICE = new Scheduler(1, 0, 0, 0);
                    Constantes.JOB_BACKUPDEVICE.StartBackupDataDevice();
                    init_ = true;
                }
            }
        }

        public void StartSynchroDevice()
        {
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(RunSynchroDevice));
            t.Start();
        }

        private void RunSynchroDevice()
        {
            bool init_ = false;
            Setting s = SettingBLL.ReturnSetting();
            DateTime d = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, s.TimeSynchroAuto.Hour, s.TimeSynchroAuto.Minute, s.TimeSynchroAuto.Second);
            while (!init_)
            {
                if (DateTime.Now.ToLongTimeString() == d.ToLongTimeString())
                {
                    System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(Fonctions.CheckPingAndSynchro));
                    t.Start();

                    Constantes.JOB_SYNCHRODEVICE = new Scheduler(0, d.Hour, d.Minute, d.Second);
                    Constantes.JOB_SYNCHRODEVICE.StartSynchroDevice();
                    init_ = true;
                }
            }
        }

        public static void Stop(Scheduler scheduler)
        {
            if (scheduler != null)
            {
                scheduler.Stop();
                scheduler = null;
            }
        }
    }

    public class Scheduler
    {
        int _day = 0;
        int _hour = 0;
        int _min = 0;
        int _sec = 0;
        double _milli = 0;

        Timer _timer;

        public Scheduler(double milli) : this(0, 0, 0, 0, milli) { }

        public Scheduler(int hour, int min, int sec) : this(0, hour, min, sec, 0) { }

        public Scheduler(int day, int hour, int min, int sec) : this(day, hour, min, sec, 0) { }

        public Scheduler(int day, int hour, int min, int sec, double milli)
        {
            this._day = day;
            this._hour = hour;
            this._min = min;
            this._sec = sec;
            this._milli = milli;
        }

        private Timer SetTimer(int day, int hour, int min, int sec)
        {
            if (_milli == 0)
            {
                int mDay = day * 24 * 60 * 60 * 1000;
                int mHour = hour * 60 * 60 * 1000;
                int mMin = min * 60 * 1000;
                int mSec = sec * 1000;
                _milli = mDay + mHour + mMin + mSec; ;
            }

            Timer t = new Timer(_milli);
            return t;
        }

        public void Stop()
        {
            this._timer.Stop();
            this._timer.Dispose();
        }

        public void StartBackupDataDevice()
        {
            _timer = SetTimer(_day, _hour, _min, _sec);
            _timer.Elapsed += new System.Timers.ElapsedEventHandler(OnTimerBackupDataDevice);
            _timer.AutoReset = true;
            _timer.Enabled = true;
            _timer.Start();
        }

        private void OnTimerBackupDataDevice(object sender, System.Timers.ElapsedEventArgs e)
        {
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(Fonctions.BackupLogDataDevice));
            t.Start();
        }

        public void StartSynchroDevice()
        {
            _timer = SetTimer(_day, _hour, _min, _sec);
            _timer.Elapsed += new System.Timers.ElapsedEventHandler(OnTimerSynchroDevice);
            _timer.AutoReset = true;
            _timer.Enabled = true;
            _timer.Start();
        }

        private void OnTimerSynchroDevice(object sender, System.Timers.ElapsedEventArgs e)
        {
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(Fonctions.CheckPingAndSynchro));
            t.Start();            
        }
    }
}
