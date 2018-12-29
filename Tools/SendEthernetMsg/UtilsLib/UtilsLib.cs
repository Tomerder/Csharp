using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Win32;
using System.IO.Compression;
using System.Net;
using System.Net.Sockets;
using System.Globalization;

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

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }

            return "";
        }

        /*-----------------------------------------------------------------------------------*/

        public static byte[] ConvertHexStringToByteArray(string hexString)
        {
            if (hexString.Length % 2 != 0)
            {
                hexString = "0" + hexString;
            }

            byte[] HexAsBytes = new byte[hexString.Length / 2];
            for (int index = 0; index < HexAsBytes.Length; index++)
            {
                string byteValue = hexString.Substring(index * 2, 2);
                HexAsBytes[index] = byte.Parse(byteValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            }

            return HexAsBytes;
        }

        /*-----------------------------------------------------------------------------------*/

        public static bool SendUdpMessage(int _portToSendFrom, string _ipToSendTo, int _portToSendTo, string _msgToSend)
        {
            UdpClient udp;

            try
            {
                udp = new UdpClient(_portToSendFrom);
            }
            catch
            {
                udp = new UdpClient();
            }

            try
            {
                //to ip 
                IPAddress ipToSendTo = IPAddress.Parse(_ipToSendTo);
                //to port
                
                //set ip and port to send to
                IPEndPoint groupEP = new IPEndPoint(ipToSendTo, _portToSendTo);
                //msg to send
                string msgToSend = _msgToSend;  
                byte[] msgToSendBytes = ConvertHexStringToByteArray(msgToSend);

                //send message
                udp.Send(msgToSendBytes, msgToSendBytes.Length, groupEP);
                udp.Close();
            }
            catch
            {
                return false;
            }

            return true;
        }
        /*-----------------------------------------------------------------------------------*/
    }
}
