using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.IO;
using System.Text.RegularExpressions;
using System.Data;

namespace WordLib
{
    public class WordLibMgr
    {
        //------------------------------------------------------------

        string m_wordPath;

        //------------------------------------------------------------
        //CTOR
        public WordLibMgr(string _wordPath)
        {
            m_wordPath = _wordPath;
        }

        //------------------------------------------------------------

        public bool ReplaceLabel(string _labelToReplace, string _replaceWith)
        {
            try
            {
                using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(m_wordPath, true))
                {
                    string docText = null;
                    using (StreamReader sr = new StreamReader(wordDoc.MainDocumentPart.GetStream()))
                    {
                        docText = sr.ReadToEnd();
                    }

                    // Regex regexText = new Regex(_labelToReplace);
                    //docText = regexText.Replace(docText, _replaceWith);
                    docText = docText.Replace(_labelToReplace, _replaceWith);

                    using (StreamWriter sw = new StreamWriter(wordDoc.MainDocumentPart.GetStream(FileMode.Create)))
                    {
                        sw.Write(docText);
                    }
                }
            }
            catch(Exception e)
            {
                return false;
            }

            return true;
        }

        //------------------------------------------------------------

        public bool CreateWordDoc()
        {
            try
            {
                using (WordprocessingDocument package = WordprocessingDocument.Create(m_wordPath, WordprocessingDocumentType.Document))
                {
                    // Add a new main document part. 
                    package.AddMainDocumentPart();

                    // Create the Document DOM. 
                    package.MainDocumentPart.Document =
                        new Document(
                        new Body(
                            new Paragraph(
                            new Run(
                                new Text("")))));

                    // Save changes to the main document part. 
                    package.MainDocumentPart.Document.Save();
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        /*------------------------------------------------------------------------------------------------------------*/
        public bool CreateTable(List<string> _headers, List<List<string>> _tableData, string _keywordToInsertAfter = "")
        {
            try
            {
                using (var document = WordprocessingDocument.Open(m_wordPath, true))
                {
                    var doc = document.MainDocumentPart.Document;

                    // Create an empty table.
                    Table table = new Table();

                    // Create a TableProperties object and specify its border information.
                    TableProperties tblProp = new TableProperties(
                        new TableBorders(
                            new TopBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 10 },
                            new BottomBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 10 },
                            new LeftBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 10 },
                            new RightBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 10 },
                            new InsideHorizontalBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 10 },
                            new InsideVerticalBorder() { Val = new EnumValue<BorderValues>(BorderValues.Single), Size = 10 }
                        )
                    );

                    // Append the TableProperties object to the empty table.
                    table.AppendChild<TableProperties>(tblProp);

                    // Create a headers row
                    TableRow trheader = new TableRow();

                    //fill table headers
                    foreach (string header in _headers)
                    {
                        var tcHeader = new TableCell();

                        // Specify the width property of the table cell.
                        tcHeader.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2400" }));                       

                        // Specify the table cell content.
                        Text text = new Text(header);

                        Run run = new Run();
                        RunProperties runPro = new RunProperties();
                        RunFonts runFont = new RunFonts() { Ascii = "Cambria(Headings)", HighAnsi = "Cambria(Headings)" };
                        Bold bold = new Bold();
                        Color color = new Color() { Val = "365F91", ThemeColor = ThemeColorValues.Accent1, ThemeShade = "BF" };                    

                        runPro.Append(runFont);
                        runPro.Append(bold);
                        runPro.Append(color);
                        runPro.Append(text);

                        run.Append(runPro);

                        tcHeader.Append(new Paragraph(run));

                        trheader.Append(tcHeader);
                    }

                    // Append the table titles row
                    table.Append(trheader);

                    //fill table data
                    foreach (List<string> regData in _tableData)
                    {
                        TableRow tr = new TableRow();

                        foreach (string dataCol in regData)
                        {
                            var tc = new TableCell();
                            tc.Append(new TableCellProperties(new TableCellWidth() { Type = TableWidthUnitValues.Dxa, Width = "2400" }));
                            tc.Append(new Paragraph(new Run(new Text(dataCol))));
                            tr.Append(tc);
                        }

                        table.Append(tr);
                    }
                
                    //add table on first line or after specific keyword 
                    if (String.IsNullOrEmpty(_keywordToInsertAfter))
                    {
                        doc.Body.Append(table);
                    }
                    else
                    {
                        bool success = InsertElementAfterBookMark(doc, table, _keywordToInsertAfter);
                    }    

                    document.Close();
                }
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        /*------------------------------------------------------------------------------------------------------------*/
        public bool CreateTable(DataTable _dataTable, string _bookmarkToInsertAfter = "")
        {
            try
            {
                using (var document = WordprocessingDocument.Open(m_wordPath, true))
                {
                    var doc = document.MainDocumentPart.Document;

                    // Create an empty table.
                    Table table = new Table();

                    //header
                    TableRow row = new TableRow();
                    for (int j = 0; j < _dataTable.Columns.Count; j++)
                    {
                        TableCell cell = new TableCell();
                        cell.Append(new Paragraph(new DocumentFormat.OpenXml.Wordprocessing.Run(new DocumentFormat.OpenXml.Wordprocessing.Text(_dataTable.Columns[j].ToString()))));
                        cell.Append(new TableCellProperties(new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = "1200" }));
                        row.Append(cell);
                    }

                    table.Append(row);
                    //set word table as => DataTable _table
                    for (int i = 0; i < _dataTable.Rows.Count; ++i)
                    {
                        row = new TableRow();
                        for (int j = 0; j < _dataTable.Columns.Count; j++)
                        {
                            TableCell cell = new TableCell();
                            string text = _dataTable.Rows[i][j].ToString();
                            string[] lines = text.Split('\n');
                            foreach (string line in lines)
                            {
                                cell.Append(new Paragraph(new DocumentFormat.OpenXml.Wordprocessing.Run(new DocumentFormat.OpenXml.Wordprocessing.Text(line))));
                            }
                            cell.Append(new TableCellProperties(new TableCellWidth { Type = TableWidthUnitValues.Dxa, Width = "1200" }));
                            row.Append(cell);
                        }
                        table.Append(row);
                    }

                    //add table on first line or after specific keyword 
                    if (String.IsNullOrEmpty(_bookmarkToInsertAfter))
                    {
                        doc.Body.Append(table);
                    }
                    else
                    {
                        bool success = InsertElementAfterBookMark(doc, table, _bookmarkToInsertAfter);
                    }

                    document.Close();
                }
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        //------------------------------------------------------------------------------

        public bool InsertTitle(string _tableTitle, string _keywordToInsertAfter)
        {
            try
            {
                using (var document = WordprocessingDocument.Open(m_wordPath, true))
                {
                    var doc = document.MainDocumentPart.Document;
                    Paragraph para = new Paragraph();
                    Run runTitle = new Run();
                    RunProperties runProTitle = new RunProperties();
                    RunFonts runFontTitle = new RunFonts() { Ascii = "Cambria(Headings)", HighAnsi = "Cambria(Headings)" };
                    Color colorTitle = new Color() { Val = "365F91", ThemeColor = ThemeColorValues.Accent1, ThemeShade = "BF" };
                    Text textTitle = new Text(_tableTitle);
                    Bold boldTitle = new Bold();
                    Underline underlineTitle = new Underline() { Val = DocumentFormat.OpenXml.Wordprocessing.UnderlineValues.Single };

                    runProTitle.Append(runFontTitle);
                    runProTitle.Append(boldTitle);
                    runProTitle.Append(underlineTitle);
                    runProTitle.Append(new FontSize() { Val = "48" });
                    runProTitle.Append(colorTitle);
                    runProTitle.Append(textTitle);

                    runTitle.Append(runProTitle);

                    para.AppendChild(runTitle);

                    //add on first line or after specific keyword 
                    if (String.IsNullOrEmpty(_keywordToInsertAfter))
                    {
                        doc.Body.AppendChild(para);
                    }
                    else
                    {
                        bool success = InsertElementAfterBookMark(doc, para, _keywordToInsertAfter);
                    }
                }
            }
            catch(Exception e)
            {
                return false;
            }

            return true;
        }

        //------------------------------------------------------------------------------

        private bool InsertElementAfterBookMark(Document _doc, OpenXmlElement _elementToInsert, string _bookMarkToInsertAfter)
        {
            Body body = _doc.GetFirstChild<Body>();
            var bookMarkStarts = body.Descendants<BookmarkStart>();
            var bookMarkEnds = body.Descendants<BookmarkEnd>();

            try
            {
                foreach (BookmarkStart bookMarkStart in bookMarkStarts)
                {
                    if (bookMarkStart.Name == _bookMarkToInsertAfter)
                    {
                        //Get the id of the bookmark start to find the bookmark end
                        var id = bookMarkStart.Id.Value;
                        var bookmarkEnd = bookMarkEnds.Where(i => i.Id.Value == id).First();

                        var parent = bookmarkEnd.Parent;

                        parent.InsertAfter(_elementToInsert, bookmarkEnd);
                    }
                }

                _doc.Save();
            }
            catch
            {
                return false;
            }

            return true;
        }

        //------------------------------------------------------------------------------

    }
}
