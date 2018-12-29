using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReferanceEx
{
    class Program
    {
        static void Main(string[] args)
        {
            //StringBuilder changed
            StringBuilder sB1 = new StringBuilder("XXX");
            SbChange(sB1);         
            Console.WriteLine("StringBuilder changed (Change ref type) : " + sB1);

            //StringBuilder address does not changed
            StringBuilder sB2 = new StringBuilder("XXX");
            SbChangeAdr(sB2);
            Console.WriteLine("StringBuilder address does not changed : " + sB2);

            //StringBuilder address changed - when send ref parameter
            StringBuilder sB3 = new StringBuilder("XXX");
            SbChangeAdrRef(ref sB3);
            Console.WriteLine("StringBuilder address changed - when send ref parameter : " + sB3);

            //--------------------------------------------------------
            //String does not changed
            string str1 = "XXX";
            StrChange(str1);            
            Console.WriteLine("String does not changed (string is immutable) : " + str1);

            //String address does not changed 
            string str2 = "XXX";
            StrChangeAdr(str2);
            Console.WriteLine("String address does not changed  : " + str2);

            //String address changed - when send ref parameter 
            string str3 = "XXX";
            StrChangeAdrRef(ref str3);
            Console.WriteLine("String address changed - when send ref parameter : " + str3);
        }

        static void SbChange(StringBuilder strB2)
        {
            strB2.Append("YYY");
        }

        static void SbChangeAdr(StringBuilder str2)
        {
            str2 = new StringBuilder("YYY");
        }

        static void SbChangeAdrRef(ref StringBuilder str2)
        {
            str2 = new StringBuilder("YYY");
        }

        //--------------------------------------------------------

        static void StrChange(string str2)
        {
            str2 += "YYY";
        }

        static void StrChangeAdr(string str2)
        {
            str2 = "YYY";
        }

        static void StrChangeAdrRef(ref string str2)
        {
            str2 = "YYY";
        }
       

    }
}
