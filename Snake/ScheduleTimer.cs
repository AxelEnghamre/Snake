﻿using System;
namespace Snake
{
    class ScheduleTimer : IDisposable
    {
        public bool Aborted { get; private set; }

        bool _active = true;

        long _start;
        long _time;

        System.Timers.Timer? _timer;

        readonly Action _action;

        public ScheduleTimer(int time, Action action)
        {
            _time = time;
            _action = () =>
            {
                _active = false;
                action();
            };

            Resume();
        }

        public void Abort()
        {
            Aborted = true;
            Invalidate();
        }

        public void Pause()
        {
            if (_active && !Aborted)
            {
                Invalidate();
                _time = Math.Max(1, _time - (DateTimeOffset.Now.ToUnixTimeMilliseconds() - _start));
            }
        }

        public void Resume()
        {
            if (_active && !Aborted)
            {
                _start = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                _timer = new System.Timers.Timer(_time);
                _timer.Elapsed += (sender, arg) => _action();
                _timer.AutoReset = false;
                _timer.Start();
            }
        }

        void Invalidate()
        {
            if (_timer != null)
            {
                _timer.Enabled = false;
                _timer.Close();
                _timer = null;
            }
        }

        public void Dispose() => Invalidate();
    }
}

