using System;
using System.Reflection;

namespace Attributes
{
    class AttributesDemo
    {
        static void Main(string[] args)
        {
            // The following code goes over the Employee class
            //  and looks up for the LastModified attributes.
            //  It uses Reflection to obtain the attributes
            //  (the GetCustomAttributes method), and iterates
            //  all members of the type using Type.GetMembers.
            // The DisplayAttributes method helps display the
            //  results of the iteration.

            Type employeeType = typeof(Employee);
            
            LastModifiedAttribute[] attributes = 
                (LastModifiedAttribute[])employeeType.GetCustomAttributes(typeof(LastModifiedAttribute), false);
            DisplayAttributes(employeeType.Name, attributes);

            MemberInfo[] members =
                employeeType.GetMembers(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (MemberInfo member in members)
            {
                attributes =
                    (LastModifiedAttribute[])member.GetCustomAttributes(typeof(LastModifiedAttribute), false);
                DisplayAttributes(member.Name, attributes);
            }
        }

        private static void DisplayAttributes(string codeElement,
                                              LastModifiedAttribute[] attributes)
        {
            if (attributes.Length != 0)
            {
                // Now we can access the DateModified and DeveloperName
                //  properties as if it were a regular class instance.
                //
                Console.WriteLine("{0} last modified on {1} by {2}",
                                  codeElement, attributes[0].DateModified,
                                  attributes[0].DeveloperName);
            }
        }
    }
}
