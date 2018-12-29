using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;
using Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace PerformanceHarness
{
    interface IOutputProvider
    {
        TextWriter Output { get; set; }
        string OutputFileName { get; set; }
        void WriteHeader();
        void WriteEntry(string className, float elapsedTime);
        void Finish();
    }

    class CSVOutputProvider : IOutputProvider
    {
        private TextWriter _output;
        private string _outputFileName;

        public TextWriter Output
        {
            get { return _output; }
            set { _output = value; }
        }

        public string OutputFileName
        {
            get { return _outputFileName; }
            set
            {
                _outputFileName = value;
                if (Output == null)
                {
                    Output = new StreamWriter(_outputFileName);
                }
            }
        }

        public void WriteHeader()
        {
            _output.WriteLine("Class, Time");
        }

        public void WriteEntry(string className, float elapsedTime)
        {
            _output.WriteLine(className + ", " + elapsedTime.ToString());
        }

        public void Finish()
        {
            Output.Flush();
        }
    }

    class XMLOutputProvider : IOutputProvider
    {
        private TextWriter _output;
        private XmlWriterSettings _settings;

        public XMLOutputProvider()
        {
            _settings = new XmlWriterSettings();
            _settings.ConformanceLevel = ConformanceLevel.Fragment;
            _settings.Indent = true;
            _settings.NewLineOnAttributes = true;            
        }

        public TextWriter Output
        {
            get { return _output; }
            set
            {
                _output = value;
                _xmlWriter = XmlWriter.Create(_output, _settings);
            }
        }

        public string OutputFileName
        {
            get { return _outputFileName; }
            set
            {
                _outputFileName = value;
                if (Output == null)
                {
                    Output = new StreamWriter(_outputFileName);
                }
            }
        }

        private XmlWriter _xmlWriter;
        private string _outputFileName;

        public void WriteHeader()
        {
        }

        public void WriteEntry(string className, float elapsedTime)
        {
            _xmlWriter.WriteStartElement("Entry");
            _xmlWriter.WriteAttributeString("ClassName", className);
            _xmlWriter.WriteAttributeString("ElapsedTime", elapsedTime.ToString());
            _xmlWriter.WriteEndElement();
        }

        public void Finish()
        {
            _xmlWriter.Flush();
            Output.Flush();
        }
    }

    class ExcelOutputProvider : IOutputProvider
    {
        private TextWriter _output;
        private object _missing = Missing.Value;
        private Application _excelApplication;
        private Workbook _workBook;
        private Worksheet _workSheet;
        private char _nextCol = 'B';
        private int _maxRow = 2;
        private Dictionary<string, char> _classColumnMapping = new Dictionary<string, char>();
        private Dictionary<string, int> _classRowMapping = new Dictionary<string, int>();
        private string _outputFileName;

        public ExcelOutputProvider()
        {
            _excelApplication = new ApplicationClass();
            _workBook = _excelApplication.Workbooks.Add(_missing);
            // Remove the unnecessary worksheets.
            foreach (Worksheet workSheet in _workBook.Sheets)
            {
                if (_workBook.Sheets.Count == 1)
                    break;
                workSheet.Delete();
            }
            _workSheet = (Worksheet) _workBook.ActiveSheet;
        }

        public TextWriter Output
        {
            get { return _output; }
            set { _output = value; }
        }

        public string OutputFileName
        {
            get { return _outputFileName; }
            set { _outputFileName = value; }
        }

        public void WriteHeader()
        {
        }

        private void WriteValueToCell(char col, int row, string value)
        {
            Range range = _workSheet.get_Range(col.ToString() + row.ToString(), _missing);
            range.Value2 = value;
        }

        public void WriteEntry(string className, float elapsedTime)
        {
            // If the column was not added yet, add it.
            if (!_classColumnMapping.ContainsKey(className))
            {
                _classColumnMapping.Add(className, _nextCol);
                ++_nextCol;
                _classRowMapping.Add(className, 3);

                WriteValueToCell(_classColumnMapping[className], 2, className);
            }

            WriteValueToCell(_classColumnMapping[className], _classRowMapping[className], elapsedTime.ToString());
            ++_classRowMapping[className];

            if (_classRowMapping[className] > _maxRow)
                _maxRow = _classRowMapping[className];
        }

        public void Finish()
        {
            // Calculate and display average values under each column.
            _workSheet.get_Range("A" + _maxRow, _missing).Value2 = "AVG";
            foreach (char col in _classColumnMapping.Values)
            {
                string formula = string.Format("=AVERAGE({0}3:{0}{1})", col, _maxRow - 1);
                _workSheet.get_Range(col.ToString() + _maxRow.ToString(), _missing).Formula = formula;
            }

            // Display a line chart of the data (excluding the average).
            _Chart chart = (_Chart) _workBook.Sheets.Add(_missing, _missing, _missing, XlSheetType.xlChart);
            chart.ChartType = XlChartType.xlLineMarkers;
            chart.SetSourceData(_workSheet.get_Range("B2", ((char)(_nextCol - 1)).ToString() + _maxRow.ToString()), _missing);

            // Save the workbook and quit Excel.
            if (File.Exists(OutputFileName))
                File.Delete(OutputFileName);
            _workBook.SaveAs(OutputFileName, _missing, _missing, _missing, _missing,
                             _missing, XlSaveAsAccessMode.xlExclusive, _missing, _missing,
                             _missing, _missing, _missing);
            _excelApplication.Quit();
        }

        ~ExcelOutputProvider()
        {
            Marshal.ReleaseComObject(_workSheet);
            Marshal.ReleaseComObject(_workBook);
            Marshal.ReleaseComObject(_excelApplication);
        }
    }
}
