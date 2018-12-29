using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Win32;
using System.IO.Compression;
using System.Net.Mail;
using System.Net;
using System.Windows.Forms;

namespace UtilsLib
{
    public static class UtilsLib
    {

        /*-----------------------------------------------------------------------------------*/

        static public bool ZipPath(string _zipExeLocation, string _pathToZip, string _pathDest, string _zipFileName)
        {
            bool success = true;

            try
            {
                //create _pathDest if path does not exist
                if (!Directory.Exists(_pathDest))
                {
                    Directory.CreateDirectory(_pathDest);
                }

                //zip _pathToZip into _pathDest/_zipFileName
                CheckAndFixPath(ref _pathToZip);
                CheckAndFixPath(ref _pathDest);
                //remove last '\\'
                _pathToZip = _pathToZip.Substring(0, _pathToZip.Length - 1);
                string cmd = "7z";
                string args = "a -tzip " + _pathDest + _zipFileName + ".zip " + _pathToZip;
                CheckAndFixPath(ref _zipExeLocation);
                success = ExecuteCommand(cmd, args, _zipExeLocation);
            }
            catch
            {
                return false;
            }

            return success;
        }

        /*-----------------------------------------------------------------------------------*/

        static public void CheckAndFixPath(ref string _path)
        {
            _path = _path.Replace('/', '\\');

            if (! _path.EndsWith("\\"))
            {
                _path += "\\";
            }
        }

        /*-----------------------------------------------------------------------------------*/
        /*recursive deletion*/
        static public bool DeleteContentInFolder(string _folderName)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(_folderName);

                foreach (FileInfo fi in dir.GetFiles())
                {
                    fi.IsReadOnly = false;
                    fi.Delete();
                }

                foreach (DirectoryInfo di in dir.GetDirectories())
                {
                    DeleteContentInFolder(di.FullName);
                    di.Attributes = di.Attributes & (~FileAttributes.ReadOnly);
                    di.Delete();
                }
            }
            catch
            {
                return false;
            }

            return true; ;
        }

        /*-----------------------------------------------------------------------------------*/

        static public bool DeleteFolderAndContent(string _folderName)
        {
            try
            {
                DeleteContentInFolder(_folderName);
                Directory.Delete(_folderName);
            }
            catch
            {
                return false;
            }

            return true;
        }

        /*-----------------------------------------------------------------------------------*/

        static public bool RetrievePathFromFullFilePath(string _fullFilePath, out string _path)
        {
            _path = "";

            try
            {
                int indexTo = _fullFilePath.LastIndexOf('\\');
                _path = _fullFilePath.Substring(0, indexTo);
            }
            catch
            {
                return false;
            }

            return true;
        }

        /*-----------------------------------------------------------------------------------*/

        static public bool ExecuteCommand(string _cmd, string _args = "", string _fromPath = "")
        {
            try
            {
                if (_fromPath != "")
                {
                    System.Environment.CurrentDirectory = @_fromPath;
                }

                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                // the cmd program
                startInfo.FileName = _cmd;
                startInfo.UseShellExecute = true;
                if (_args != "")
                {
                    startInfo.Arguments = _args;
                }
                process.StartInfo = startInfo;
                process.Start();
                process.WaitForExit();
                if (process.ExitCode != 0)
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        /*-----------------------------------------------------------------------------------*/

        public struct MailDataStruct
        {
            public string s_from;
            public string s_to;
            public string s_subject;
            public string s_body;
            public string s_userDp;
            public string s_userPassward;
        }

        static public bool SendEmail(MailDataStruct _mailData)
        {
            try
            {
                MailMessage message = new MailMessage(
                       _mailData.s_from,
                       _mailData.s_to,
                       _mailData.s_subject,
                       _mailData.s_body);

                SmtpClient client = new SmtpClient("mxhfa.esl.corp.elbit.co.il");

                client.EnableSsl = true;
                NetworkCredential netCre = new NetworkCredential(_mailData.s_userDp, _mailData.s_userPassward);
                client.Credentials = netCre;

                client.Send(message);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
         }

        /*--------------------------------------------------------------------------*/

        static public string FixStrIfNull(string _str)
        {
            if (_str == null)
            {
                return "";
            }
            else
            {
                return _str;
            }
        }

        /*-----------------------------------------------------------------------------------*/       

        static public bool GetPathFromUser(string _desc, out string _txtFromFile)
        {
            _txtFromFile = "";

            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = _desc;

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                _txtFromFile = fbd.SelectedPath;
            }

            return true;
        }

        /*----------------------------------------------------------------------------*/

    }
}
