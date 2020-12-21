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
    public enum TypeScheduler
    {
        TYPE_INDEFINI = 1,
        TYPE_DEFINI_BY_NBRE = 2,
        TYPE_DEFINI_BY_DATE = 3,
        TYPE_DEFINI_BY_TIME = 4,
        TYPE_EVERY_TIME = 5,
    }

    public class Scheduler
    {
        int _day = 0;
        int _hour = 0;
        int _min = 0;
        int _sec = 0;
        double _milli = 0;

        TypeScheduler _type = TypeScheduler.TYPE_INDEFINI;
        public TypeScheduler Type
        {
            get { return _type; }
            set { _type = value; }
        }
        int _nombre = 1, _current_nombre = 0;
        public int Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
        DateTime _current_date = DateTime.Now;
        DateTime _date_end = DateTime.Now;
        public DateTime DateEnd
        {
            get { return _date_end; }
            set { _date_end = value; }
        }
        DateTime _time_execution = DateTime.Now;
        public DateTime TimeExecution
        {
            get { return _time_execution; }
            set { _time_execution = value; }
        }

        Timer _timer;

        public delegate void Fonction();
        Fonction _fonction;

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
                _milli = Utils.MilliSeconds(day, hour, min, sec);
            }
            Timer t = new Timer(_milli);
            return t;
        }

        public void Stop()
        {
            this._timer.Stop();
            this._timer.Dispose();
        }

        public void Start(Fonction target)
        {
            _fonction = target;
            if (_type == TypeScheduler.TYPE_EVERY_TIME)
            {
                _time_execution = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, _time_execution.Hour, _time_execution.Minute, _time_execution.Second);
                DateTime current = DateTime.Now;
                TimeSpan interval = _time_execution - current;
                long milliseconde = (long)interval.TotalMilliseconds;
                _milli = milliseconde >= 0 ? milliseconde : Utils.MilliSeconds(1, 0, 0, 0) + milliseconde;
            }
            _timer = SetTimer(_day, _hour, _min, _sec);
            _timer.Elapsed += new System.Timers.ElapsedEventHandler(OnTimer);
            _timer.Enabled = true;
            _timer.Start();
        }

        private void OnTimer(object sender, System.Timers.ElapsedEventArgs e)
        {
            new System.Threading.Thread(new System.Threading.ThreadStart(_fonction)).Start();
            if (_type == TypeScheduler.TYPE_DEFINI_BY_DATE)
            {
                if (!(_date_end.CompareTo(_current_date) == 1))
                    Stop();
            }
            else if (_type == TypeScheduler.TYPE_DEFINI_BY_NBRE)
            {
                if (!(_nombre > _current_nombre))
                    Stop();
            }
            else if (_type == TypeScheduler.TYPE_EVERY_TIME)
            {
                Stop();
                Scheduler job = new Scheduler(1, 0, 0, 0);
                job.Start(new Scheduler.Fonction(_fonction));
            }
            else if (_type == TypeScheduler.TYPE_DEFINI_BY_TIME)
            {
                Stop();
                Scheduler job = new Scheduler(_day, _hour, _min, _sec);
                job.Start(new Scheduler.Fonction(_fonction));
            }
            _current_date = DateTime.Now;
            _current_nombre += 1;
        }
    }
}
