using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelFileLib;
using System.Data;
using UtilsLib;
using System.IO;

namespace BuildInstructionExecuter
{
    class Program
    {
        const string DEFAULT_FILE_PATH = "BuildInstructions.xlsx";
        const int ROW_DATA_START = 2;

        const int COL_FILE_NAME = 1;
        const int COL_FOLDER_TO_EXTRACT = 2;
        const int COL_RENAME_FROM = 3;
        const int COL_RENAME_TO = 4;


        const string DEFAULT_ZIPS_LOCATION = "./";
        const string DEFAULT_ZIP_EXE_LOCATION = "./";

        //------------------------------------------------------------------------------------------------------------------

        static void Main(string[] args)
        {
            bool success;

            string excelInputFilePath = DEFAULT_FILE_PATH;
            string inputZipsFilesFolder = DEFAULT_ZIPS_LOCATION;

            //get inputs from command line
            if(args.Length > 1)
            {
                excelInputFilePath = args[1];
            }

            if (args.Length > 2)
            {             
                inputZipsFilesFolder = args[2];
            }

            //excute build instructions
            try
            {
                //read excel
                ExcelFile excel = new ExcelFile();
                List<List<string>> tableData;
                excel.GetTableFromExcelToListOfLists(excelInputFilePath, ROW_DATA_START, out tableData);

                //execute build instructions according to table from excel
                success = ExecuteBuildInstruction(tableData, inputZipsFilesFolder);
            }
            catch(Exception e)
            {
                Console.WriteLine("ERROR - Unhandled exception !!!");
            }

            //wait for key press
            Console.WriteLine("Process finished successfuly - press any key");
            Console.ReadLine();
        }

        //------------------------------------------------------------------------------------------------------------------

        private static bool ExecuteBuildInstruction(List<List<string>> tableData, string _inputZipFilesPath)
        {
            bool success;

            //execute for each row
            foreach (List<string> row in tableData)
            {
                //unzip file to required path 
                _inputZipFilesPath = UtilsLib.UtilsLib.CheckAndFixPathToBackslesh(_inputZipFilesPath, true);
                string zipToExtractPath = _inputZipFilesPath + row[COL_FILE_NAME];
                string extractToPath = row[COL_FOLDER_TO_EXTRACT];
                UtilsLib.UtilsLib.ExtractPath(DEFAULT_ZIP_EXE_LOCATION, zipToExtractPath, extractToPath);

                //move folders/files
                string pathesMoveFrom = row[COL_RENAME_FROM];
                string pathesMoveTo = row[COL_RENAME_TO];

                if (!String.IsNullOrEmpty(pathesMoveFrom) && !String.IsNullOrEmpty(pathesMoveTo))
                {
                    string[] moveFromArr = pathesMoveFrom.Split('\n');
                    string[] moveToArr = pathesMoveTo.Split('\n');

                    //sanity check
                    if (moveFromArr.Length != moveToArr.Length)
                    {
                        Console.WriteLine("Wrong input : Folders count doesn not match for - " + row[COL_FILE_NAME]);
                        continue;
                    }

                    //iterate each folder
                    for (int i = 0; i < moveFromArr.Length; i++)
                    {
                        string moveFrom = UtilsLib.UtilsLib.CheckAndFixPathToBackslesh(moveFromArr[i], false);
                        string moveTo = UtilsLib.UtilsLib.CheckAndFixPathToBackslesh(moveToArr[i], false);

                        //check if file or dir
                        if (Directory.Exists(moveFrom))
                        {
                            success = MoveDirectory(moveFrom, moveTo);
                        }
                        else if (File.Exists(moveFrom))
                        {
                            success = MoveFile(moveFrom, moveTo);
                        }
                    }
                }
            }

            return true;
        }

        //------------------------------------------------------------------------------------------------------------------

        private static bool MoveFile(string _moveFrom, string _moveTo)
        {
            //make sure parent folder for dest folder exists, and create if not
            string parentDir = Directory.GetParent(_moveTo).ToString();
            if (!Directory.Exists(parentDir))
            {
                Directory.CreateDirectory(parentDir);
            }

            //move file
            File.Move(_moveFrom, _moveTo);

            return true;
        }

        //------------------------------------------------------------------------------------------------------------------

        static bool MoveDirectory(string _moveFrom, string _moveTo)
        {
            try
            {
                //make sure source folder exists
                if (!Directory.Exists(_moveFrom))
                {
                    Console.WriteLine("Directory does not exists : " + _moveFrom);
                    return false;
                }

                //make sure parent folder for dest folder exists, and create if not
                string parentDir = Directory.GetParent(_moveTo).ToString();
                if (!Directory.Exists(parentDir))
                {
                    Directory.CreateDirectory(parentDir);
                }

                //if dir already exist - copy dir, else - move dir 
                if (Directory.Exists(_moveTo))
                {
                    //copy dir
                    bool success = UtilsLib.UtilsLib.CopyFolderWithAllContent(_moveFrom, _moveTo);
                    if (!success)
                    {
                        Console.WriteLine("Error copy folder : " + _moveFrom);
                        return false;
                    }
                }
                else
                {
                    //move dir
                    Directory.Move(_moveFrom, _moveTo);
                }
            }
            catch(Exception e)
            {
                return false;
            }

            return true;
        }

    //------------------------------------------------------------------------------------------------------------------

    }
}
