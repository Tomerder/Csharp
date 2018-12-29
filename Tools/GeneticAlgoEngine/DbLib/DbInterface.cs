using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Data.SqlServerCe;

namespace DbLib
{
    public class DbInterface
    {
        //-------------------------------------------------

        SqlCeConnection m_connection;

        //-------------------------------------------------

        public void Connect()
        {
            string dbfile = new System.IO.FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).DirectoryName + "\\Database.sdf";
            dbfile = "C:\\Users\\DP27813\\Documents\\Visual Studio 2010\\Projects\\GeneticAlgoEngine\\GeneticAlgoTool\\Database.sdf";
            string connectStr = "datasource=" + dbfile;
            //connectStr += ";User Instance=True;";
            m_connection = new SqlCeConnection(connectStr);
            m_connection.Open();
        }

        //-------------------------------------------------

        public void CloseConnection()
        {
            // Close 
            m_connection.Close();
        }

        //-------------------------------------------------

        public void ReadRecords()
        {
            SqlCeDataReader rdr = null;
            SqlCeCommand cm = new SqlCeCommand("SELECT * FROM [Table]", m_connection);
            rdr = cm.ExecuteReader();

            while (rdr.Read())
            {
                int field0 = rdr.GetInt32(0);
                float field1 = rdr.GetFloat(1);
            }
            rdr.Close();
        }

        //-------------------------------------------------

        public void AddRecord2()
        {        
            // Read all rows from the table test_table into a dataset (note, the adapter automatically opens the connection)
            SqlCeDataAdapter adapter = new SqlCeDataAdapter("SELECT * FROM [Table]", m_connection);
            DataSet dataTable = new DataSet();
            adapter.Fill(dataTable);

            // Add a row to the test_table (assume that table consists of a text column)
            DataRow row = dataTable.Tables[0].NewRow();
            row[0] = 99;
            row[1] = 6.5;
            dataTable.Tables[0].Rows.Add(row);

            //insert command
            SqlCeCommandBuilder builder = new SqlCeCommandBuilder(adapter);
            builder.GetInsertCommand();

            // Save data back to the databasefile
            adapter.Update(dataTable);        
        }

        //-------------------------------------------------

        public bool QueryExecute(string _query)
        {
            try
            {
                SqlCeCommand cmd = new SqlCeCommand(_query);
                cmd.Connection = m_connection;
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        //-------------------------------------------------

        public bool QueryResult(string _query, out Object _result)
        {
            _result = 0;

            try
            {           
                SqlCeCommand cmd = new SqlCeCommand(_query);
                cmd.Connection = m_connection;
                _result =  cmd.ExecuteScalar();
            }
            catch(Exception e)
            {
                return false;
            }

            return true;
        }

        //-------------------------------------------------

    }
}
