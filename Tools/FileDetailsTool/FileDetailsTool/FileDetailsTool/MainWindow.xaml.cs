using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UtilsLib;

namespace FileDetailsTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //--------------------------------------------------------------------------------------------

        struct UserInputStruct
        {
            public string inputFolder;
            public string outputFilePath;
            public bool isIncludeSubfolders;
            public bool isDisplayFullFilePath;
            public string fileType;
        } 

        //--------------------------------------------------------------------------------------------
        public MainWindow()
        {
            InitializeComponent();
            SetDefaults();
        }

        //--------------------------------------------------------------------------------------------
        private void SetDefaults()
        {
            textBoxInputFolder.Text = @"C:\";
            textBoxOutputFolder.Text = @"C:\";
            textBoxOutputFileName.Text = "fileDetailsTable.csv";
            UtilsGui.ClearMessagesPanel(this, textBoxMessages);
        }

        //--------------------------------------------------------------------------------------------
        private void buttonBrowseInput_Click(object sender, RoutedEventArgs e)
        {
            textBoxInputFolder.Text = UtilsLib.UtilsLib.ShowFolderBrowseDialog();
        }

        private void buttonBrowseOutput_Click(object sender, RoutedEventArgs e)
        {
            textBoxOutputFolder.Text = UtilsLib.UtilsLib.ShowFolderBrowseDialog();
        }

        //--------------------------------------------------------------------------------------------
        private void buttonExecute_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UtilsGui.SetButtonEnable(this, buttonExecute, false);
                UtilsGui.AppendMessageToPanel(this, textBoxMessages, "Process started - please wait...", UtilsGui.MessageType.SUCCESS);

                //Fill UserInputStruct
                UserInputStruct userInputs;
                FillUserInput(out userInputs);

                Action<UserInputStruct> func = CreateFileDetailsOutput;
                func.BeginInvoke(userInputs, null, null);
            }
            catch(Exception ex)
            {
                UtilsGui.AppendMessageToPanel(this, textBoxMessages, "Unhandled error", UtilsGui.MessageType.ERROR);
            }
        }

        //--------------------------------------------------------------------------------------------


        private void FillUserInput(out UserInputStruct _userInputs)
        {
            _userInputs = new UserInputStruct();

            _userInputs.inputFolder = textBoxInputFolder.Text;

            string outputFilePath = textBoxOutputFolder.Text;
            outputFilePath = UtilsLib.UtilsLib.CheckAndFixPathToBackslesh(outputFilePath);
            outputFilePath += textBoxOutputFileName.Text;
            _userInputs.outputFilePath = outputFilePath;

            _userInputs.fileType = textBoxFileType.Text;

            _userInputs.isIncludeSubfolders = false;
            if(checkBoxIncludeSubfolders.IsChecked == true)
            {
                _userInputs.isIncludeSubfolders = true;
            }

            _userInputs.isDisplayFullFilePath = false;
            if (checkBoxFullFilePath.IsChecked == true)
            {
                _userInputs.isDisplayFullFilePath = true;
            }
        }

        //--------------------------------------------------------------------------------------------
        private void CreateFileDetailsOutput(UserInputStruct _userInputs)
        {
            try
            {
                //get files details 
                List<UtilsLib.UtilsLib.FileDetails> filesDetailsList;
                bool success = UtilsLib.UtilsLib.GetFilesDetailsInFolder(_userInputs.inputFolder, out filesDetailsList, _userInputs.isIncludeSubfolders, _userInputs.fileType, _userInputs.isDisplayFullFilePath) ;
                if(!success)
                {
                    DoReturnValue(false);
                    return;
                }

                //write results to output file
                success = WriteResultsToExcel(_userInputs.outputFilePath, filesDetailsList);
                if (!success)
                {
                    DoReturnValue(false);
                    return;
                }
            }
            catch(Exception e)
            {
                DoReturnValue(false);
                return;
            }

            DoReturnValue(true);
        }

        /*----------------------------------------------------------------------------*/

        private void DoReturnValue(bool _success)
        {
            string message = "";

            if (_success)
            {
                message = "Process finished successfully";
                UtilsGui.AppendMessageToPanel(this, textBoxMessages, message, UtilsGui.MessageType.SUCCESS);
            }
            else
            {
                message = "Process finished with errors";
                UtilsGui.AppendMessageToPanel(this, textBoxMessages, message, UtilsGui.MessageType.ERROR);
            }

            //re enable buttons upon process completed 
            UtilsGui.SetButtonEnable(this, buttonExecute, true);
        }

        //--------------------------------------------------------------------------------------------

        enum TABLE_COLOMNS_NAMES {File_Name, Size, CRC };

        private bool WriteResultsToExcel(string _outputFilePath, List<UtilsLib.UtilsLib.FileDetails> _filesDetailsList)
        {
            try
            { 
                //create data table 
                DataTable table = new DataTable();
                table.Columns.Add(TABLE_COLOMNS_NAMES.File_Name.ToString());
                table.Columns.Add(TABLE_COLOMNS_NAMES.Size.ToString());
                table.Columns.Add(TABLE_COLOMNS_NAMES.CRC.ToString());

                foreach (UtilsLib.UtilsLib.FileDetails fileDetails in _filesDetailsList)
                {
                    DataRow row = table.NewRow();
                    row[TABLE_COLOMNS_NAMES.File_Name.GetHashCode()] = fileDetails.fileName;
                    string sizeForVdd = UtilsLib.UtilsLib.GetFileSizeForVdd(fileDetails.fileSize);
                    row[TABLE_COLOMNS_NAMES.Size.GetHashCode()] = sizeForVdd;
                    row[TABLE_COLOMNS_NAMES.CRC.GetHashCode()] = fileDetails.fileCrc;

                    table.Rows.Add(row);
                }

                //create csv with data
                CsvLib.CsvInterface.WriteTableToCsv(_outputFilePath, table);
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        //--------------------------------------------------------------------------------------------
    }
}
