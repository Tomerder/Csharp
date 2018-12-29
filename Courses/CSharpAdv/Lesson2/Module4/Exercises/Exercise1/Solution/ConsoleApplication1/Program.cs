using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Soap;
using System.IO;

namespace ConsoleApplication1
{
	[Serializable]
	class Person
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
	}
	
	class Program
	{
		static void Main(string[] args)
		{
			// Serailize();
			Desrialize();
		}

		static void Serailize()
		{
			Person p = new Person
			{
				FirstName = "Amir",
				LastName = "Adler"
			};

			SoapFormatter formatter = new SoapFormatter();

			using (FileStream fs = new FileStream("file.xml", FileMode.Create))
			{
				formatter.Serialize(fs, p);
			}
		}

		private static void Desrialize()
		{
			SoapFormatter formatter = new SoapFormatter();

			using (FileStream fs = new FileStream("file.xml", FileMode.Open))
			{
				Person p = (Person)formatter.Deserialize(fs);
				Console.WriteLine(p.FirstName + " " + p.LastName);
			}
		}
	}
}
