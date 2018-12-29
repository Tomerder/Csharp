using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FormatInterfaces
{
    enum PersonFormats
    {
        Full,
        Age,
        Name
    }
    class Person : IFormattable
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }

        #region IFormattable Members

        public string ToString(string format, IFormatProvider formatProvider)
        {
            //throw new NotImplementedException();
            string result= "";
            PersonFormats fmt = (PersonFormats) Enum.Parse(typeof(PersonFormats),format);
            switch (fmt)
            {
                case PersonFormats.Full:
                    result = "Age: " + Age + "Name: " + Name;
                    break;
                case PersonFormats.Age:
                    result = "Age: " + Age ;
                    break;
                case PersonFormats.Name:
                    result =  "Name: " + Name;
                    break;
            }

            return result;
        }

        #endregion
    }
}
