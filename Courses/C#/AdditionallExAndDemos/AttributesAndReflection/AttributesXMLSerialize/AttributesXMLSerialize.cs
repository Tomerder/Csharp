using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Xml.Serialization;
using System.IO;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace AttributesXMLSerialize
{
    class Program
    {
        static void Main(string[] args)
        {
            Person p = new Person() { FirstName = "John", LastName = "Smith" };

            XmlSerializer x = new XmlSerializer(typeof(Person));
            x.Serialize(new FileStream("C:\\dd.xml", FileMode.OpenOrCreate), p);
        }



    }
    // display a different sting in debugger, not toString!
    [DebuggerDisplay("FName: {FirstName}, LName: {LastName}")]

    [Serializable]
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // no sence in storing combined name as well!
        [XmlIgnore]
        public string Name
        {
            get
            {
                return FirstName + " " + LastName;
            }
            set
            {
            }
        }

        
        public override string ToString()
        {
            return Name;

            
        }

       

    }
}
