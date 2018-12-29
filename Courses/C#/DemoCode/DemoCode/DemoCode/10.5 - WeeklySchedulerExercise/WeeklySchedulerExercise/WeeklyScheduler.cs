using System;

namespace WeeklySchedulerExercise
{
    class WeeklyScheduler
    {
        // This is a so-called "jagged" array.  It's essentially an
        //  array of arrays.
        private string[][] _workingHours = new string[5][];
        private int _hoursPerDay;
        private int _startingHour;

        public WeeklyScheduler(int hoursPerDay, int startingHour)
        {
            _hoursPerDay = hoursPerDay;
            _startingHour = startingHour;
            for (int i = 0; i < _workingHours.Length; ++i)
            {
                // Since it's an array of arrays, each item
                //  in it must be initialized.
                _workingHours[i] = new string[_hoursPerDay];
            }
        }

        public string this[DayOfWeek day, int hour]
        {
            get
            {
                ValidateIndexerParameters(day, hour - _startingHour);
                return _workingHours[(int)day][hour - _startingHour];
            }
            set
            {
                ValidateIndexerParameters(day, hour - _startingHour);
                _workingHours[(int)day][hour - _startingHour] = value;
            }
        }

        private void ValidateIndexerParameters(DayOfWeek day, int hour)
        {
            // We're not working on Fridays and Saturdays.
            if (day == DayOfWeek.Friday ||
                day == DayOfWeek.Saturday)
            {
                throw new IndexOutOfRangeException("week day out of range");
            }
            if (hour < 0 || hour >= _hoursPerDay)
            {
                throw new IndexOutOfRangeException("hour out of range");
            }
        }

        public void Print()
        {
            Console.WriteLine("Schedule is {0}", this.IsScheduleFull ? "FULL" : "NOT FULL");
            Console.WriteLine("Weekly schedule print-out:");
            for (int day = 0; day < _workingHours.Length; ++day)
            {
                Console.WriteLine("Details for {0}", (DayOfWeek)day);
                for (int hour = 0; hour < _hoursPerDay; ++hour)
                {
                    Console.WriteLine("{0:D2} : {1}", _startingHour + hour,
                                      _workingHours[day][hour] ?? "-Empty-");
                }
            }
        }

        public bool IsScheduleFull
        {
            get
            {
                foreach (string[] dayArr in _workingHours)
                {
                    foreach (string secretary in dayArr)
                    {
                        if (String.IsNullOrEmpty(secretary))
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
        }
    }
}
