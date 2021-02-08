using System;
using System.Collections.Generic;
using System.Text;

namespace Lib
{
    public class TimeTI
    {
        public int Hours { get; private set; }

        public int Minutes { get; private set; }

        public int Seconds { get; private set; }

        public TimeTI()
        {
            Hours = 0;
            Minutes = 0;
            Seconds = 0;
        }

        public TimeTI(int hours, int minutes, int seconds)
        {
            if (hours > 23 || hours < 0)
                throw new Exception("Wrong hours value");

            if (minutes >= 60 || minutes < 0)
                throw new Exception("Wrong minutes value");

            if (seconds >= 60 || seconds < 0)
                throw new Exception("Wrong seconds value");

            Hours = hours;

            Minutes = minutes;

            Seconds = seconds;
        }

        public TimeTI(DateTime dt)
        {
            Hours = dt.Hour;
            Minutes = dt.Minute;
            Seconds = dt.Second;
        }

        public override string ToString()
        {
            return $"{Hours}:{Minutes}:{Seconds}";
        }

        public static TimeTI Sum(TimeTI first, TimeTI second)
        {
            var sec = first.Seconds + second.Seconds;
            var min = 0;
            var hour = 0;
            while (sec >= 60)
            {
                min += 1;
                sec -= 60;
            }

            min += first.Minutes + second.Minutes;
            while (min >= 60)
            {
                hour += 1;
                min -= 60;
            }
            hour += first.Hours + second.Hours;
            while (hour > 23)
                hour -= 23;

            return new TimeTI(hour, min, sec);
        }

        public static TimeTI Diff(TimeTI first, TimeTI second)
        {
            if (first.Hours < second.Hours)
            {
                var temp = first;
                first = second;
                second = temp;
            }
            var hours = first.Hours - second.Hours;
            var minutes = first.Minutes - second.Minutes;
            var seconds = first.Seconds - second.Seconds;
            while (seconds < 0)
            {
                seconds += 60;
                minutes -= 1;
            }
            while (minutes < 0)
            {
                minutes += 60;
                hours -= 1;
            }
            while (hours < 0)
            {
                hours += 23;
            }

            return new TimeTI(hours, minutes, seconds);
        }

        public void Add(TimeTI time)
        {
            var res = Sum(this, time);
            Hours = res.Hours;
            Minutes = res.Minutes;
            Seconds = res.Seconds;
        }

        public void Sub(TimeTI time)
        {
            var res = Diff(this, time);
            Hours = res.Hours;
            Minutes = res.Minutes;
            Seconds = res.Seconds;
        }
    }
}
