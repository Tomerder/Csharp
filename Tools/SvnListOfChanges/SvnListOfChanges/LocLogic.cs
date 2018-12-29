using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SvnFilesHistory;

namespace SvnListOfChanges
{
    static class LocLogic
    {
        /*--------------------------------------------------------------------------*/

        static string TITLE_FILE_NAME = "File Name";
        static string TITLE_CHANGE_TYPE = "Change type";

        /*--------------------------------------------------------------------------*/

        static public bool DoLocLogic(MainWindow _mainWindow)
        {
            bool success = true;

            //get LOC from SVN
            SvnLib.SvnInterface svn = new SvnLib.SvnInterface();
            string from = UtilsGui.GetTextBox(_mainWindow, _mainWindow.textBoxFrom);
            string to = UtilsGui.GetTextBox(_mainWindow, _mainWindow.textBoxTo);

            Dictionary<string, string> loc;
            success = svn.GetLocBetweenPathes(from, to, out loc);
            if (!success)
            {
                UtilsGui.AppendMessageToPanel(_mainWindow, _mainWindow.textBoxMessages, "Getting LOC from SVN has failed", UtilsGui.MessageTypeEnum.ERROR);
                return false;
            }

            //write LOC to file
            string outputFile = UtilsGui.GetTextBox(_mainWindow, _mainWindow.textBoxOutputFile);
            success = CsvLib.CsvInterface.WriteMapToCsv(outputFile, loc, TITLE_FILE_NAME, TITLE_CHANGE_TYPE);

            return success;
        }

        /*--------------------------------------------------------------------------*/
    }
}
