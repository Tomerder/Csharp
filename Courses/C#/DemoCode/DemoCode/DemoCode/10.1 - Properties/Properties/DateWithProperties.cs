using System;
using System.Collections.Generic;
using System.Text;

namespace Properties
{
    // This class contains the exact same implementation as the
    //  SimpleDate class, but with C# properties instead of the
    //  manual get/set accessor syntax.
    //
    class DateWithProperties
    {
        private int _day;
        private int _month;
        private int _year;

        public DateWithProperties(int day, int month, int year)
        {
            _day = day;
            _month = month;
            _year = year;
        }

        // This is the syntax for a C# property.  The get
        //  accessor simply returns the value of the field.
        //  It has an implicit return value of the property
        //  itself.  The set accessor accepts a parameter
        //  named 'value' that has the type of the property
        //  itself.  We can still perform validation, since
        //  the property get/set methods can contain any code
        //  we should like.
        // Note that as a general rule of thumb, property
        //  get/set accessors should not perform 'heavy'
        //  processing, or throw exceptions that are not related
        //  to the field itself.  For example, it is unreasonable
        //  to have a property set accessor to save the changed
        //  property to a database, as this is a relatively
        //  lengthy operation, which may result in various
        //  exceptions (e.g., if the database connection
        //  becomes unavailable).
        //
        public int Day
        {
            get { return _day; }
            set { AssertValidDay(value); _day = value; }
        }

        public int Month
        {
            get { return _month; }
            set { AssertValidMonth(value); _month = value; }
        }

        public int Year
        {
            get { return _year; }
            set { _year = value; }
        }

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
