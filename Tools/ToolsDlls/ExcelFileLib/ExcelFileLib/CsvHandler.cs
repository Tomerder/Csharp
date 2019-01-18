using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;

namespace ExcelFileLib
{
    class CsvHandler
    {

        public static bool read(string path, out DataTable dt)
        {
            string filename;

            string provider = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=\"";

            string prop = "\";Extended Properties='text;HDR=Yes;FMT=Delimited(,)';";

            path = extractName(path, out filename);

            FileInfo file = new FileInfo(filename);

            dt = new DataTable();

            try
            {
                using (OleDbConnection con = new OleDbConnection(provider + path + prop))
                {
                    using (OleDbCommand cmd = new OleDbCommand(string.Format
                                              ("SELECT * FROM [{0}]", file.Name), con))
                    {
                        con.Open();

                        // Using a DataReader to process the data
                        using (OleDbDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Process the current reader entry...
                            }
                        }

                        // Using a DataTable to process the data
                        using (OleDbDataAdapter adp = new OleDbDataAdapter(cmd))
                        {
                            adp.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return true;
        }

        private static string extractName(string fullPath, out string fname)
        {
            string[] tokens = fullPath.Split('\\');

            fname = tokens.Last();

            string path = fullPath.Replace(fname, "");

            return path;
        }
    }
}
