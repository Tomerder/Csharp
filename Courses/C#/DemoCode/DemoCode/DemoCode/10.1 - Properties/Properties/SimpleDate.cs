using System;

namespace Properties
{
    class SimpleDate
    {
        // These fields hold the actual data for this class, but
        //  we do not want to make them public, because then we
        //  would have no control over the field access.
        //
        private int _day;
        private int _month;
        private int _year;

        public SimpleDate(int day, int month, int year)
        {
            _day = day;
            _month = month;
            _year = year;
        }

        // This is the implementation of the get/set accessors
        //  using methods.  We specify a method for each get/set
        //  operation, for each private field.  This allows us
        //  to control field access, and support the principle
        //  of encapsulation (data hiding).
        //
        public int GetDay()             { return _day; }
        public int GetMonth()           { return _month; }
        public int GetYear()            { return _year; }
        public void SetDay(int day)     { AssertValidDay(day);_day = day; }
        public void SetMonth(int month) { AssertValidMonth(month);  _month = month; }
        public void SetYear(int year)   { _year = year; }

        private void AssertValidDay(int day)
        {
            // Implementation omitted for brevity.
        }

        private void AssertValidMonth(int month)
        {
            // Implementation omitted for brevity.
        }
    }
}
