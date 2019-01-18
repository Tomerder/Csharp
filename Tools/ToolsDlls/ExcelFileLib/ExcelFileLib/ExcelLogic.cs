using System;
using System.Data;
using System.Reflection;

namespace ExcelFileLib
{
    public class ExcelLogic
    {
        private int numberOfSheets;

        /* Method: ExcelLogic 
         * Brief: Class constructor
         * Access: Public
         * Pre: None
         * Post: None
         * Param[in]: None
         * Param[out]: None
         * Exception: None
         * Return: None
         */
        public ExcelLogic()
        {
            numberOfSheets = 0;
        }

        /* Method: CreateExcelFile 
         * Brief: Create excel file
         * Access: Public
         * Pre: None
         * Post: None
         * Param[in]: string excelFilePath, string sheetName
         * Param[out]: None
         * Exception: Interop excel exceptions
         * Return: None
         */
        public void createExcelFile(string excelFilePath, string sheetName)
        {
            /*Set up work book, work sheets, and excel application*/
            Microsoft.Office.Interop.Excel.Application oexcel = new Microsoft.Office.Interop.Excel.Application();

            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory;

                object misValue = System.Reflection.Missing.Value;

                oexcel.SheetsInNewWorkbook = 1;

                Microsoft.Office.Interop.Excel.Workbook obook = oexcel.Workbooks.Add(misValue);

                Microsoft.Office.Interop.Excel.Worksheet osheet = oexcel.Sheets.get_Item(1);

                numberOfSheets = 1;

                osheet.Name = sheetName;

                obook.SaveAs(excelFilePath);

                obook.Close();

                oexcel.Quit();

                releaseObject(osheet);

                releaseObject(obook);

                releaseObject(oexcel);

                GC.Collect();
            }

            catch (Exception ex)
            {
                oexcel.Quit();

                Console.WriteLine(ex);
            }
        }

        /* Method: releaseObject 
         * Brief:Decrements the reference count of the Runtime Callable Wrapper (RCW) associated with the specified COM object
         * Access: Private
         * Pre: None
         * Post: None
         * Param[in]: object o
         * Param[out]: None
         * Exception: Interop excel exceptions
         * Return: None
         */
        private void releaseObject(object o)
        {
            try
            {
                while (System.Runtime.InteropServices.Marshal.ReleaseComObject(o) > 0) { }
            }
            catch { }
            finally
            {
                o = null;
            }
        }

        /* Method: ExportDataSetToExcel 
         * Brief: Create new sheet and set data table
         * Access: Public
         * Pre: None
         * Post: None
         * Param[in]: DataTable dt, string excelFilePath, string sheetName
         * Param[out]: None
         * Exception: Interop excel exceptions
         * Return: None
         */
        public void exportDataSetToExcel(DataTable dt, string excelFilePath, string sheetName)
        {
            /*Set up work book, work sheets, and excel application*/
            Microsoft.Office.Interop.Excel.Application oexcel = new Microsoft.Office.Interop.Excel.Application();

            try
            {
                Microsoft.Office.Interop.Excel.Workbook obook = oexcel.Workbooks.Open(excelFilePath);

                if (numberOfSheets > 1)
                {
                    obook.Sheets.Add(After: obook.Sheets[obook.Sheets.Count]);

                    obook.Sheets.Move(Missing.Value, obook.Sheets[obook.Sheets.Count]);
                }
                
                Microsoft.Office.Interop.Excel.Worksheet osheet = obook.Sheets[obook.Sheets.Count];

                numberOfSheets++;

                osheet.Name = sheetName;

                int colIndex = 0;

                int rowIndex = 1;

                foreach (DataColumn dc in dt.Columns)
                {
                    colIndex++;

                    osheet.Cells[1, colIndex] = dc.ColumnName;
                }
                foreach (DataRow dr in dt.Rows)
                {
                    rowIndex++;

                    colIndex = 0;

                    foreach (DataColumn dc in dt.Columns)
                    {
                        colIndex++;

                        osheet.Cells[rowIndex, colIndex] = dr[dc.ColumnName];
                    }
                }

                osheet.Columns.AutoFit();

                var startCell = (Microsoft.Office.Interop.Excel.Range)osheet.Cells[1, 1];

                var endCell = (Microsoft.Office.Interop.Excel.Range)osheet.Cells[rowIndex, colIndex];

                var writeRange = osheet.Range[startCell, endCell].Cells.Borders.Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlMedium;


                //Release and terminate excel
                obook.Save();

                obook.Close();

                oexcel.Quit();

                releaseObject(osheet);

                releaseObject(obook);

                releaseObject(oexcel);

                GC.Collect();
            }
            catch (Exception ex)
            {
                oexcel.Quit();

                Console.WriteLine(ex);
            }
        }

        /* Method: borderAround 
         * Brief: Put borders around cell
         * Access: Public
         * Pre: None
         * Post: None
         * Param[in]: Range range, int colour
         * Param[out]: None
         * Exception: None
         * Return: None
         */
        private void borderAround(Microsoft.Office.Interop.Excel.Range range, int colour)
        {
            Microsoft.Office.Interop.Excel.Borders borders = range.Borders;
            borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeRight].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            borders.Color = colour;
            borders = null;
        }
    }
}
