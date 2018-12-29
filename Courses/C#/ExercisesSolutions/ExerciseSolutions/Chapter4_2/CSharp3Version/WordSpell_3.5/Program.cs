using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Word;
using System.Reflection;

namespace WordSpell_3._5
{
    class Program
    {
        static void Main(string[] args)
        {
            Application word = new Application();

            object missing = Missing.Value;

            word.Documents.Add(ref missing, ref missing,
                ref missing, ref missing);

            SpellingSuggestions suggestions =
                word.GetSpellingSuggestions("placa",
                ref missing, ref missing, ref missing,
                ref missing, ref missing,
                ref missing, ref missing, ref missing,
                ref missing,
                ref missing, ref missing,
                ref missing, ref missing);

            foreach (SpellingSuggestion item in suggestions)
            {
                Console.WriteLine(item.Name);
            }
        }




    }
}
