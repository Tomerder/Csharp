using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordLib;

namespace Test
{
    class Program
    {
        const int COL_NUM = 5;
        const int LINE_NUM = 5;
        const string PATH_EXIST_DOC = @"C:\AutoRelease\Master_VDD_Template.docx";
        const string PATH_EMPTY_DOC = @"C:\test.docx";

        //------------------------------------------------------------

        static void Main(string[] args)
        {
            //TestCreateTable();
            TestReplaceLabel();
        }

        //------------------------------------------------------------

        private static void TestReplaceLabel()
        {
            WordLibMgr word = new WordLibMgr(PATH_EXIST_DOC);
            bool success = word.ReplaceLabel("DATE", DateTime.Today.ToShortDateString());
        }

        //------------------------------------------------------------

        static void TestCreateTable()
        {
            WordLibMgr word = new WordLibMgr(PATH_EXIST_DOC);

            //word.CreateWordDoc();

            //create headers
            List<string> titles = new List<string>();
            for (int i = 0; i < COL_NUM; i++)
            {
                titles.Add("col" + i);
            }

            //create lines
            List<List<string>> lines = new List<List<string>>();
            for (int i = 0; i < LINE_NUM; i++)
            {
                List<string> line = new List<string>();
                for (int j = 0; j < COL_NUM; j++)
                {
                    line.Add("col" + i + "_Val" + j);
                }

                lines.Add(line);
            }


            word.CreateTable(titles, lines, "KNOWN_ERRORS");
        }

        //------------------------------------------------------------
    }
}
