using System;
using System.Collections.Generic;
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

        //--------------------------------------------------------------------------------------------------------------------------

        const int MAX_EMPTY_CELLS_IN_A_ROW = 5;

        public bool GetTableFromExcelToDicOf1Col(string _excelPath, int _tableDataRowNum, out Dictionary<string, List<string>> _outputDic)
        {
            Microsoft.Office.Interop.Excel.Application oXL = null;
            Microsoft.Office.Interop.Excel._Workbook oWB = null;
            Microsoft.Office.Interop.Excel._Worksheet oSheet = null;

            _outputDic = new Dictionary<string, List<string>>();
            //_outputList = new List<List<string>>();

            try
            {
                //Start Excel and get Application object.
                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = false;

                //Open excel
                oWB = (Microsoft.Office.Interop.Excel._Workbook)(oXL.Workbooks.Open(_excelPath));
                oSheet = (Microsoft.Office.Interop.Excel._Worksheet)oWB.ActiveSheet;

                //fill table 
                Microsoft.Office.Interop.Excel.Range ur = oSheet.UsedRange;
                int lastCol = ur.Columns.Count;
                int lastRow = ur.Rows.Count;              

                //iterate on table lines
                for(int i=_tableDataRowNum; i<=lastRow; i++)
                {                    
                    List<string> rowData = new List<string>();
                    bool isLineEmpty = true;

                    //iterate on line cols
                    int emptyCellsCounter = 0;
                    for(int j=1; j<=lastCol; j++)
                    {
                        //break and advance to next row if more than 5 consequtive cells are empty 
                        if(emptyCellsCounter > MAX_EMPTY_CELLS_IN_A_ROW)
                        {
                            //next row
                            break;
                        }

                        var cellValue = oSheet.Cells[i, j].Value;
                        if(cellValue == null)
                        {
                            rowData.Add(String.Empty);
                            emptyCellsCounter++;
                            continue;
                        }

                        string field = cellValue.ToString();
                        if (String.IsNullOrEmpty(field))
                        {
                            rowData.Add(String.Empty);
                            emptyCellsCounter++;
                            continue;
                        }
        
                         rowData.Add(field);
                         isLineEmpty = false;
                         emptyCellsCounter = 0;
                    }

                    //in case line is empty - finish
                    if(isLineEmpty)
                    {
                        break;
                    }

                    string firstColInRow = rowData[0];
                    //Add to dic
                    _outputDic[firstColInRow] = rowData;
                    //Add to list
                    //_outputList.Add(rowData);
                }
                   
                oWB.Save();
            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
                if (oWB != null)
                {
                    oWB.Close();
                }
                if (oXL != null)
                {
                    oXL.Quit();
                }

                oWB = null;
                oXL = null;
            }

            return true;
        }

        //--------------------------------------------------------------------------------------------------------------------------

        public bool GetTableFromExcelToListOfLists(string _excelPath, int _tableDataRowNum, out List<List<string>> _outputList)
        {
            Microsoft.Office.Interop.Excel.Application oXL = null;
            Microsoft.Office.Interop.Excel._Workbook oWB = null;
            Microsoft.Office.Interop.Excel._Worksheet oSheet = null;

            _outputList = new List<List<string>>();

            try
            {
                //Start Excel and get Application object.
                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = false;

                //Open excel
                oWB = (Microsoft.Office.Interop.Excel._Workbook)(oXL.Workbooks.Open(_excelPath));
                oSheet = (Microsoft.Office.Interop.Excel._Worksheet)oWB.ActiveSheet;

                //fill table 
                Microsoft.Office.Interop.Excel.Range ur = oSheet.UsedRange;
                int lastCol = ur.Columns.Count;
                int lastRow = ur.Rows.Count;

                //iterate on table lines
                for (int i = _tableDataRowNum; i <= lastRow; i++)
                {
                    List<string> rowData = new List<string>();
                    bool isLineEmpty = true;

                    //iterate on line cols
                    int emptyCellsCounter = 0;
                    for (int j = 1; j <= lastCol; j++)
                    {
                        //break and advance to next row if more than 5 consequtive cells are empty 
                        if (emptyCellsCounter > MAX_EMPTY_CELLS_IN_A_ROW)
                        {
                            //next row
                            break;
                        }

                        var cellValue = oSheet.Cells[i, j].Value;
                        if (cellValue == null)
                        {
                            rowData.Add(String.Empty);
                            emptyCellsCounter++;
                            continue;
                        }

                        string field = cellValue.ToString();
                        if (String.IsNullOrEmpty(field))
                        {
                            rowData.Add(String.Empty);
                            emptyCellsCounter++;
                            continue;
                        }

                        rowData.Add(field);
                        isLineEmpty = false;
                        emptyCellsCounter = 0;
                    }

                    //in case line is empty - finish
                    if (isLineEmpty)
                    {
                        break;
                    }

                    string firstColInRow = rowData[0];

                    //Add to list
                    _outputList.Add(rowData);
                }

                oWB.Save();
            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
                if (oWB != null)
                {
                    oWB.Close();
                }
                if (oXL != null)
                {
                    oXL.Quit();
                }

                oWB = null;
                oXL = null;
            }

            return true;
        }


        //--------------------------------------------------------------------------------------------------------------------------

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
         * Param[in]: DataTable OutputDataTable, string pathName, string sheetName 
         * Param[out]: None
         * Exception: None
         * Return: bool <success/failed>
         */
        public bool getTableData(out DataTable OutputDataTable, string pathName, string sheetName)
        {
            string startPosition = string.Empty;

            string finalPosition = string.Empty;

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
            string sheetName = string.Empty;

            string startPosition = string.Empty;

            string finalPosition = string.Empty;

            return getExelTableData(out OutputDataTable, pathName, sheetName, startPosition, finalPosition);
        }

        /* Method: initPosition 
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

            if (string.IsNullOrEmpty(sheetName)) 
            {
                Console.WriteLine("Set default sheet name to -Sheet1-");
                sheetName = "Sheet1";
            }

            switch (extension)
            {
                case ".xls":
                    strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pathName + ";Extended Properties='Excel 8.0;HDR=No;IMEX=1;'";
                    break;
                case ".xlsx":
                case ".xlsm":
                    strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + pathName + ";Extended Properties='Excel 12.0;HDR=No;IMEX=1;'";
                    break;
                case ".csv":
                    CsvHandler.read(pathName, out dataTableToFill);
                    return true;
                default:
                    strConn = "Provider=Microsoft.Jet.OleDb.4.0; Data Source = " + pathName + ";Extended Properties = \"Text;HDR=YES;FMT=Delimited\"";
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
                Console.WriteLine(ex.Message);

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
        public void createOutputFile(string pathName)
        {
            DataTable tableNames;
            OleDbConnection oleCon;
            string conStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pathName + ";Extended Properties='Excel 8.0;HDR=No;IMEX=1;'";
            object[] param = new object[] { null, null, null, "TABLE" };
            oleCon = new OleDbConnection(conStr);
            oleCon.Open();
            tableNames = oleCon.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, param);
        }

        /* Method: setExcelTableData 
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

                return;
            }
        }
    }
}
