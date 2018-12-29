using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoVersionRelease
{
    static class WrapUpLogic
    {
        /*----------------------------------------------------------------------------*/

        public static bool DoWrapUpLogic(Dictionary<string, Csci> _cscisMap, MainWindow _mainWindow)
        {
            bool success = true;
            string message = "";

            string versionToRelease = UtilsGui.GetTextBox(_mainWindow, _mainWindow.textBoxVersionToRelease);     

            //zip environment 
            string outputFileNameEnv = "EFVS_" + versionToRelease;
            string zipExeLocation = GeneralConfigs.Instance.InputsPath;
            string pathToZip = GeneralConfigs.Instance.EnvPath;
            string outputPathEnv = GeneralConfigs.Instance.OutputsPath;
            UtilsLib.UtilsLib.CheckAndFixPath(ref outputPathEnv);
            
            success = UtilsLib.UtilsLib.ZipPath(zipExeLocation, pathToZip, outputPathEnv, outputFileNameEnv);
            if (success)
            {
                message = "Environment ZIP file was created : " + outputPathEnv + outputFileNameEnv + ".zip";
                UtilsGui.AppendMessageToPanel(_mainWindow, _mainWindow.textBoxMessages, message, UtilsGui.MessageType.SUCCESS); 
            }
            else
            {
                message = "Environment ZIP file was NOT created";
                UtilsGui.AppendMessageToPanel(_mainWindow, _mainWindow.textBoxMessages, message, UtilsGui.MessageType.ERROR); 
            }
            
            //create VDD
            string outputPath = GeneralConfigs.Instance.OutputsPath;
            UtilsLib.UtilsLib.CheckAndFixPath(ref outputPath);
            string outputFileNameVdd = "VDD_" + versionToRelease;

            success = VersionsLibInterfaceLayer.CreateVddExcel(outputPath, outputFileNameVdd);  //input and output parameters : output:Success, input:Dest 
            if (success)
            {
                message = "VDD file was created  : " + outputPath + outputFileNameVdd + ".zip";
                UtilsGui.AppendMessageToPanel(_mainWindow, _mainWindow.textBoxMessages, message, UtilsGui.MessageType.SUCCESS); 
            }
            else
            {
                message = "Error VDD file was not created ";
                UtilsGui.AppendMessageToPanel(_mainWindow, _mainWindow.textBoxMessages, message, UtilsGui.MessageType.ERROR); 
            }

            //Create LOG file
            try
            {
                string strToLog =  UtilsGui.GetMessagesFromPanel(_mainWindow, _mainWindow.textBoxMessages);
                string logFilePathToCreate = outputPath + "Log_" + versionToRelease + ".txt";
                strToLog = strToLog.Replace("\r", "\r\n");
                System.IO.File.WriteAllText(logFilePathToCreate, strToLog);
            }
            catch
            {
                //create log failed
                message = "Write log to file has failed";
                UtilsGui.AppendMessageToPanel(_mainWindow, _mainWindow.textBoxMessages, message, UtilsGui.MessageType.ERROR); 
            }

            //copy OUTPUTS (products, env, vdd) to Network 
 
            //notify by email 
            success = SendEmail(versionToRelease, outputPath);
            if (success)
            {
                message = "Notification Email was sent successfuly";
                UtilsGui.AppendMessageToPanel(_mainWindow, _mainWindow.textBoxMessages, message, UtilsGui.MessageType.SUCCESS);
            }          

            return true;
        }

        /*----------------------------------------------------------------------------*/

        private static bool SendEmail(string _versionToRelease, string _outputPath)
        {
            bool success = false;

            UtilsLib.UtilsLib.MailDataStruct mailData;
            mailData.s_userDp = GeneralConfigs.Instance.MailuserDp;
            mailData.s_userPassward = GeneralConfigs.Instance.MailUserPassward;
            mailData.s_from = GeneralConfigs.Instance.MailFrom;
            mailData.s_to = GeneralConfigs.Instance.MailTo;
            mailData.s_subject = "Version Release : " + _versionToRelease;
            mailData.s_body = "Version " + _versionToRelease + " is ready at the following path : " + _outputPath;

            if (mailData.s_userDp != "" && mailData.s_userPassward != "" && mailData.s_from != "" && mailData.s_to != "")
            {
                success = UtilsLib.UtilsLib.SendEmail(mailData);
            }

            return success;
        }

        /*----------------------------------------------------------------------------*/

        

        /*----------------------------------------------------------------------------*/
    }
}
