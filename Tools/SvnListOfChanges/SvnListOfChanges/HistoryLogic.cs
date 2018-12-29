using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CsvLib;
using System.Data;
using SvnLib;
using SvnFilesHistory;

namespace SvnListOfChanges
{
    static class HistoryLogic
    {
        /*--------------------------------------------------------------------------*/

        public static bool DoHistoryLogic(MainWindow _mainWindow)
        {
            SvnLib.SvnInterface svn = new SvnLib.SvnInterface();
            string trunkPath = UtilsGui.GetTextBox(_mainWindow, _mainWindow.textBoxFrom);
            string tagsPath = UtilsGui.GetTextBox(_mainWindow, _mainWindow.textBoxTo);

            //get history of trunk
            bool isDepthInfinity = true;
            bool isOnlyFiles = true;
            Dictionary<string, HistoryDataItem> trunkHistoryMap = null;
          
            bool success = svn.GetHistory(trunkPath, isDepthInfinity, isOnlyFiles, out trunkHistoryMap);
            if (!success)
            {
                UtilsGui.AppendMessageToPanel(_mainWindow, _mainWindow.textBoxMessages, "Retriving trunk history has failed", UtilsGui.MessageTypeEnum.ERROR);
                return false;
            }

            //get history of tags
            isDepthInfinity = false;
            isOnlyFiles = false;
            Dictionary<string, HistoryDataItem> tagsHistoryMap = null;
         
            success = svn.GetHistory(tagsPath, isDepthInfinity, isOnlyFiles, out tagsHistoryMap);
            if (!success)
            {
                UtilsGui.AppendMessageToPanel(_mainWindow, _mainWindow.textBoxMessages, "Retriving tags history has failed", UtilsGui.MessageTypeEnum.ERROR);
                return false;
            }

            //update "last tag changed" for every history item
            UpdateLastTagChanged(ref trunkHistoryMap, tagsHistoryMap);

            //get revision from "Tag to start"
            string tagToStartFrom = UtilsGui.GetTextBox(_mainWindow, _mainWindow.textBoxTagStart);
            long revTagToStart = GetRevForTag(tagToStartFrom, tagsHistoryMap);

            //Write History To File
            string outputFile = UtilsGui.GetTextBox(_mainWindow,  _mainWindow.textBoxOutputFile);
            success = WriteHistoryToFile(trunkHistoryMap, revTagToStart, outputFile, _mainWindow);

            return success;
        }

        /*--------------------------------------------------------------------------*/

        private static long GetRevForTag(string _tagToGetRevFor, Dictionary<string, HistoryDataItem> _tagsHistoryMap)
        {
            long toRet = -1;

            if (_tagToGetRevFor == "")
            {
                return toRet;
            }

            foreach (HistoryDataItem histItem in _tagsHistoryMap.Values)
            {
                if (histItem.ItemName == _tagToGetRevFor)
                {
                    toRet =  histItem.ItemRevision;
                    break;
                }
            }

            return toRet;
        }

        /*--------------------------------------------------------------------------*/

        static private bool WriteHistoryToFile(Dictionary<string, HistoryDataItem> _trunkHistoryMap, long _revTagToStart, string _outputFile, MainWindow _mainWindow)
        {
            CsvInterface csv = new CsvInterface();

            //prepare data for wrting to file
            DataTable dataTableToWrite;
            bool success = PrepareHistoryDataTable(_trunkHistoryMap, _revTagToStart, out dataTableToWrite);
            if (!success)
            {
                UtilsGui.AppendMessageToPanel(_mainWindow, _mainWindow.textBoxMessages, "Prepare history data has failed", UtilsGui.MessageTypeEnum.ERROR);
                return false;
            }

            //write to csv
            string outputFile = UtilsGui.GetTextBox(_mainWindow, _mainWindow.textBoxOutputFile);
            success = CsvLib.CsvInterface.WriteTableToCsv(outputFile, dataTableToWrite);
            if (!success)
            {
                UtilsGui.AppendMessageToPanel(_mainWindow, _mainWindow.textBoxMessages, "Writting history to file has failed", UtilsGui.MessageTypeEnum.ERROR);
            }
            else
            {
                UtilsGui.AppendMessageToPanel(_mainWindow, _mainWindow.textBoxMessages, "Finished successfuly", UtilsGui.MessageTypeEnum.SUCCESS);
            }

            return success;
        }

        /*--------------------------------------------------------------------------*/

        static private bool PrepareHistoryDataTable(Dictionary<string, HistoryDataItem> _trunkHistoryMap, long _revTagToStart, out DataTable _dataTableToWrite)
        {
            _dataTableToWrite = new DataTable();
            _dataTableToWrite.Columns.Add("Name");
            _dataTableToWrite.Columns.Add("Revision");
            _dataTableToWrite.Columns.Add("Size");
            _dataTableToWrite.Columns.Add("Date");
            _dataTableToWrite.Columns.Add("TAG");

            foreach (HistoryDataItem historyItem in _trunkHistoryMap.Values)
            {
                //add only items after _revTagToStart
                if (_revTagToStart == -1 || _revTagToStart <= historyItem.ItemRevision)
                {
                    DataRow row = _dataTableToWrite.NewRow();

                    row[0] = UtilsLib.UtilsLib.FixStrIfNull(historyItem.ItemName);
                    row[1] = UtilsLib.UtilsLib.FixStrIfNull(historyItem.ItemRevision.ToString());
                    row[2] = UtilsLib.UtilsLib.FixStrIfNull(historyItem.ItemSize.ToString());
                    row[3] = UtilsLib.UtilsLib.FixStrIfNull(historyItem.ItemTime.ToString());
                    string tagToWrite = UtilsLib.UtilsLib.FixStrIfNull(historyItem.TagLastChanged);
                    if (tagToWrite == "")
                    {
                        tagToWrite = "TRUNK";
                    }
                    row[4] = tagToWrite;

                    _dataTableToWrite.Rows.Add(row);
                }
            }

            return true;
        }

        /*--------------------------------------------------------------------------*/

        static private void UpdateLastTagChanged(ref Dictionary<string, HistoryDataItem> trunkHistoryMap, Dictionary<string, HistoryDataItem> tagsHistoryMap)
        {
            //sort tags by date
            var sortedTags = from pair in tagsHistoryMap
                             orderby pair.Value.ItemRevision ascending
                             select pair;

            //for each history item update tag
            foreach (HistoryDataItem historyItem in trunkHistoryMap.Values)
            {
                foreach (KeyValuePair<string, HistoryDataItem> historyTag in sortedTags)
                {
                    //checks if tag was created after item was changed
                    if (historyItem.ItemRevision < historyTag.Value.ItemRevision)
                    {
                        historyItem.TagLastChanged = historyTag.Value.ItemName;
                        break;
                    }
                }
            }
        }

        /*--------------------------------------------------------------------------*/
    }
}
