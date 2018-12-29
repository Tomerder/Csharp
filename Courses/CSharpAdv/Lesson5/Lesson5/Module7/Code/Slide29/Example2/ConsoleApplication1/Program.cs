using Microsoft.Office.Interop.Word;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Document doc = new Document();
            doc.SaveAs("Test.docx");
        }
    }
}
