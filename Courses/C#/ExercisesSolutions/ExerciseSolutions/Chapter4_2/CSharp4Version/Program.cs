using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Word;
using System.Reflection;

namespace WordSpelling
{
    class Program
    {
        static void Main(string[] args)
        {
            Application word = new Application();
            word.Documents.Add();

            SpellingSuggestions suggestions = word.GetSpellingSuggestions("placa");

            foreach (SpellingSuggestion item in suggestions)
            {
                Console.WriteLine(item.Name);
            }
        }
    }
}
