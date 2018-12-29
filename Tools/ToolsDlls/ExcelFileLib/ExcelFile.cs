using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using System.IO;

namespace ExcelFileLib
{
    /* Class: LocExcelParser
     * Brief: This class use to read data from excel file
     * Access: Public
     */
    public class ExcelFile
    {
        /* Method: ExcelFile 
         * Brief: Class constructor
         * Access: Public
         * Pre: None
         * Post: None
         * Param[in]: None
         * Param[out]: None
         * Exception: None
         * Return: None
         */
        public ExcelFile()
        {

        }

        /* Method: getTableData 
         * Brief: Use to set the desired ranges to read on the excel file when the endRow and endColumn unknowen
         * Access: Public
         * Pre: None
         * Post: Initialized startPosition and finalPosition ranges
         * Param[in]: DataTable OutputDataTable, string pathName, string sheetName, string startRow,
                           string startColumn
         * Param[out]: None
         * Exception: None
         * Return: bool <success/failed>
         */
        public bool getTableData(out DataTable OutputDataTable, string pathName, string sheetName, string startRow,
                           string startColumn)
        {
            string startPosition = initPosition(startColumn, startRow);

            string finalPosition = ":" + initPosition("AAA", "3000");

            return getExelTableData(out OutputDataTable, pathName, sheetName, startPosition, finalPosition);
        }

        /* Method: getTableData 
         * Brief: Use to set the desired ranges to read on the excel file
         * Access: Public
         * Pre: None
         * Post: Initialized startPosition and finalPosition ranges
         * Param[in]: DataTable OutputDataTable, string pathName, string sheetName, string startRow, 
                           string startColumn, string endRow, string endColumn
         * Param[out]: None
         * Exception: None
         * Return: bool <success/failed>
         */
        public bool getTableData(out DataTable OutputDataTable, string pathName, string sheetName, string startRow,
                           string startColumn, string endRow, string endColumn)
        {
            string startPosition = initPosition(startColumn, startRow);

            string finalPosition = ":" + initPosition(endColumn, endRow);

            return getExelTableData(out OutputDataTable, pathName, sheetName, startPosition, finalPosition);
        }

        /* Method: getTableData 
         * Brief: Use to set the desired ranges to read on the excel file
         * Access: Public
         * Pre: None
         * Post: Initialized startPosition and finalPosition ranges
         * Param[in]: DataTable OutputDataTable, string pathName
         * Param[out]: None
         * Exception: None
         * Return: bool <success/failed>
         */
        public bool getTableData(out DataTable OutputDataTable, string pathName)
        {
            string startPosition = string.Empty;

            string finalPosition = string.Empty;

            string sheetName = string.Empty;

            return getExelTableData(out OutputDataTable, pathName, sheetName, startPosition, finalPosition);
        }

        /* Method: getTableData 
         * Brief: Use to calculate start/end reading position
         * Access: Private
         * Pre: None
         * Post: Initialized position
         * Param[in]: string str1, string str2
         * Param[out]: None
         * Exception: None
         * Return: string
         */
        private string initPosition(string str1, string str2)
        {
            return (str1 + str2);
        }

        /* Method: getExelTableData 
         * Brief: Use to access excel file, read and fill DataTable
         * Access: Private
         * Pre: getTableData()
         * Post: Initialized DataTable
         * Param[in]: DataTable dataTableToFill, string pathName, string sheetName, string startPosition, string finalPosition
         * Param[out]: DataTable dataTableToFill
         * Exception: Genera exception, OleDbException
         * Return: bool <success/failed>
         */
        private bool getExelTableData(out DataTable dataTableToFill, string pathName, string sheetName, string startPosition, string finalPosition)
        {
            dataTableToFill = new DataTable();

            string strConn = string.Empty;

            FileInfo eFile = new FileInfo(pathName);

            if (!eFile.Exists) throw new FileNotFoundException();

            string extension = eFile.Extension;

            if (string.IsNullOrEmpty(sheetName)) { sheetName = "Sheet1"; }

            switch (extension)
            {
                case ".xls":
                    strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pathName + ";Extended Properties='Excel 8.0;HDR=No;IMEX=1;'";
                    break;
                case ".xlsx":
                    strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + pathName + ";Extended Properties='Excel 12.0;HDR=No;IMEX=1;'";
                    break;
                default:
                    strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pathName + ";Extended Properties='Excel 8.0;HDR=No;IMEX=1;'";
                    break;
            }
            try
            {
                OleDbConnection cnnxls = new OleDbConnection(strConn);

                OleDbDataAdapter oda = new OleDbDataAdapter(string.Format("select * from [{0}${1}{2}]", sheetName, startPosition, finalPosition), cnnxls);

                oda.Fill(dataTableToFill);

                cnnxls.Close();
            }
            catch (Exception ex)
            {
                if (ex is OleDbException) Console.WriteLine("Failed to create OLE DB Connection");

                else Console.WriteLine("{0}", ex.Message);

                return false;
            }
            return true;
        }

        /* Method: createOutputFile 
        * Brief: Use to create excel file
        * Access: Public
        * Pre: None
        * Post: Create new file
        * Param[in]: string sheetName
        * Param[out]: None
        * Exception: General exception, OleDbException
        * Return: None
        */
        public void createOutputFile(string pathName){
            DataTable tableNames;
            OleDbConnection oleCon;
            string conStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pathName + ";Extended Properties='Excel 8.0;HDR=No;IMEX=1;'";
            object[] param = new object[] { null, null, null, "TABLE" };
            oleCon = new OleDbConnection(conStr);
            oleCon.Open();
            tableNames = oleCon.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, param);
         }

        /* Method: setExelTableData 
         * Brief: Use to access excel file, create new sheet and set DataTable
         * Access: Public
         * Pre: None
         * Post: Initialized DataTable
         * Param[in]: DataTable dataTableToSet, string pathName, string sheetName
         * Param[out]: None
         * Exception: General exception, OleDbException
         * Return: None
         */
        public void setTableData(DataTable dataTableToSet, string pathName, string sheetName)
        {
            string strConn = string.Empty;

            FileInfo eFile = new FileInfo(pathName);

            if (!eFile.Exists) throw new FileNotFoundException();

            string extension = eFile.Extension;

            if (string.IsNullOrEmpty(sheetName)) { sheetName = "Sheet2"; }

            switch (extension)
            {
                case ".xls":
                    strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pathName + ";Extended Properties='Excel 8.0;HDR=No;IMEX=1;'";
                    break;
                case ".xlsx":
                    strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + pathName + ";Extended Properties='Excel 12.0;HDR=No;IMEX=1;'";
                    break;
                default:
                    strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pathName + ";Extended Properties='Excel 8.0;HDR=No;IMEX=1;'";
                    break;
            }
            try
            {
                OleDbConnection cnnxls = new OleDbConnection(strConn);

                OleDbDataAdapter oda = new OleDbDataAdapter(string.Format("select * from [{0}$]", sheetName), cnnxls);

                oda.Update(dataTableToSet);

                cnnxls.Close();
            }
            catch (Exception ex)
            {
                if (ex is OleDbException) Console.WriteLine("Failed to create OLE DB Connection");

                else Console.WriteLine("{0}", ex.Message);

                return ;
            }
        }
    }
}
