using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

using ZK_Lymytz.BLL;
using ZK_Lymytz.ENTITE;
using ZK_Lymytz.TOOLS;

namespace ZK_Lymytz.TOOLS
{
    class JobScheduler
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

        public void Start()
        {
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(Run));
            t.Start();
        }

        private void Run()
        {
            bool init_ = false;
            DateTime d = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
            while (!init_)
            {
                if (DateTime.Now.ToLongTimeString() == d.ToLongTimeString())
                {
                    System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(Fonctions.BackupLogDataDevice));
                    t.Start();

                    Scheduler.Instance(1, 0, 0, 0).Start();
                    init_ = true;
                }
            }
        }
    }

    class Scheduler
    {
        int _day = 0;
        int _hour = 0;
        int _min = 0;
        int _sec = 0;
        double _milli = 0;

        Timer _timer;
        static Scheduler instance;

        private Scheduler(double milli)
        {
            this._milli = milli;
        }

        public static Scheduler Instance(double milli)
        {
            if (instance == null)
            {
                instance = new Scheduler(milli);
            }
            else
            {
                instance._milli = milli;
            }
            return instance;
        }

        private Scheduler(int hour, int min, int sec) : this(0, hour, min, sec) { }

        public static Scheduler Instance(int hour, int min, int sec)
        {
            if (instance == null)
            {
                instance = new Scheduler(hour, min, sec);
            }
            else
            {
                instance._day = 0;
                instance._hour = hour;
                instance._min = min;
                instance._sec = sec;
                instance._milli = 0;
            }
            return instance;
        }

        private Scheduler(int day, int hour, int min, int sec)
        {
            this._day = day;
            this._hour = hour;
            this._min = min;
            this._sec = sec;
            this._milli = 0;
        }

        public static Scheduler Instance(int day, int hour, int min, int sec)
        {
            if (instance == null)
            {
                instance = new Scheduler(day, hour, min, sec);
            }
            else
            {
                instance._day = day;
                instance._hour = hour;
                instance._min = min;
                instance._sec = sec;
                instance._milli = 0;
            }
            return instance;
        }

        public void Start()
        {
            _timer = SetTimer(_day, _hour, _min, _sec);
            _timer.Start();
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
            t.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
            t.AutoReset = true;
            t.Enabled = true;
            return t;
        }

        private void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(Fonctions.BackupLogDataDevice));
            t.Start();
        }

        public void Stop()
        {
            instance._timer.Stop();
            instance._timer.Dispose();
            instance = null;
        }
    }
}
