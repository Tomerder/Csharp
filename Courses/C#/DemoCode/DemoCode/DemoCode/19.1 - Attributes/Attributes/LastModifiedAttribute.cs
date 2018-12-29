using System;

namespace Attributes
{
    // It's a convention to have the attribute class name end
    //  in 'Attribute'.  Note that we use the [AttributeUsage]
    //  attribute on our attribute class to specify how it can
    //  be used.
    //
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = false)]
    public class LastModifiedAttribute : Attribute
    {
        private string _developerName;
        private string _dateModified;

        // The constructor allows us to use Positional parameters
        //  when specifying the attribute on some code element, e.g.:
        //      [LastModified("A", "9/1/06")]

        public LastModifiedAttribute(string developerName, string dateModified)
        {
            _developerName = developerName;
            _dateModified = dateModified;
        }

        public LastModifiedAttribute()
        {
        }

        // The properties allow us to use Named parameters when
        //  specifying the attribute on some code element, e.g.:
        //      [LastModified(DeveloperName = "A", DateModified = "9/1/06")]

        public string DeveloperName
        {
            get { return _developerName; }
            set { _developerName = value; }
        }

        public string DateModified
        {
            get { return _dateModified; }
            set { _dateModified = value; }
        }
    }
}
