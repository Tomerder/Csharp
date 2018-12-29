// using Microsoft.Office.Interop.Word;
using System;
using System.Reflection;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Document doc = new Document();
            //doc.SaveAs("Test.docx");

            Type docType = Type.GetTypeFromProgID("Word.Document");
            object docInstance = Activator.CreateInstance(docType);
            object[] parameters = { "Test.docx" };
            docType.InvokeMember("SaveAs", BindingFlags.InvokeMethod, null, docInstance, parameters);
        }
    }
}
