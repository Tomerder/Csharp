using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Windows.Forms;


namespace HWconfig
{
    class OpenXmlWordWrapper
    {
        string m_docFileName;

        /*---------------------------singleton----------------------------*/
        private static OpenXmlWordWrapper m_instance;

        private OpenXmlWordWrapper() 
        {
            m_docFileName = "";  
        }

        public static OpenXmlWordWrapper Instance
        {
            get 
            {
                if (m_instance == null)
                {
                    m_instance = new OpenXmlWordWrapper();
                }
                
                return m_instance;
            }
        }

        /*------------------------------------------------------------------------------------------------------------*/

        public bool CreateWordDoc(string _docName)
        {
            string docFileName = _docName + ".docx";

            try
            {
                using (WordprocessingDocument package = WordprocessingDocument.Create(docFileName, WordprocessingDocumentType.Document))
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

                    m_docFileName = docFileName;
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        /*------------------------------------------------------------------------------------------------------------*/
        public void CreateTable(string _tableTitle, List<string> _headers, List<List<string>> _tableData)
        {
            using (var document = WordprocessingDocument.Open(m_docFileName, true))
            {
                var doc = document.MainDocumentPart.Document;

                //-----------------------------------------------------------
                // Create a title

                Paragraph para = new Paragraph();
                //para.ParagraphProperties = new ParagraphProperties(new ParagraphStyleId() { Val = "Heading1" });

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
                doc.Body.AppendChild(para);

                //-----------------------------------------------------------
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

                // Add columns to the table.
                //TableGrid tg = new TableGrid(new GridColumn()[2]);
                //table.AppendChild(tg);

                //-----------------------------------------------------------
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
                //-----------------------------------------------------------

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
                //-----------------------------------------------------------

                // Append the table to the document.
                doc.Body.Append(table);

            }
        }
        /*------------------------------------------------------------------------------------------------------------*/

    }
}
