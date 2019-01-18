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
//using System.Security.Cryptography.HashAlgorithm;

namespace UtilsLib
{
    //-------------------------------------------------
    public enum MessageType { NONE, SUCCESS, ERROR, HIGHLIGHT };
   
    //-------------------------------------------------

    public static class UtilsLib
    {            
        //-------------------------------------------------
        static public bool CreateCsv<T>(T[,] _data, string _csvFilePath, string separator = ",")
        {
            try
            {
                var csv = ToCsv(_data, _csvFilePath);
                File.WriteAllLines(_csvFilePath, csv);
            }
            catch(Exception e)
            {
                return false;
            }          

            return true;
        }
        private static IEnumerable<String> ToCsv<T>(T[,] data, string _csvFilePath, string separator = ",")
        {
            for (int i = 0; i < data.GetLength(0); ++i)
                yield return string.Join(separator, Enumerable
                  .Range(0, data.GetLength(1))
                  .Select(j => data[i, j])); // simplest, we don't expect ',' and '"' in the items
        }

        //-------------------------------------------------

        static Random random = new Random();

        static public int RandomNumber(int min, int max)
        {
            if (max != Int32.MaxValue)
            {
                max = max + 1;
            }

            return random.Next(min, max);
        }

        /*-----------------------------------------------------------------------------------*/

        static public string ShowFolderBrowseDialog()
        {
            string toRet = "";

            FolderBrowserDialog folderBrowserDlg = new FolderBrowserDialog();
            if (folderBrowserDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                toRet = folderBrowserDlg.SelectedPath;
            }

            return toRet;
        }

        /*-----------------------------------------------------------------------------------*/

        public struct FileDetails
        {
            public string fileName;
            public long fileSize;
            public string fileCrc;
        }

        public static bool GetFilesDetailsInFolder(string _folder, out List<FileDetails> _fileDetailsList, bool _isIncludeSubFolders = false, string _fileTypes = "*.*", bool _isDisplayFullFilePath = false)
        {
            _fileDetailsList = new List<FileDetails>();

            try
            {
                //get file names
                string[] fileFullPathes;
                bool success = GetFilesNamesInFolder(_folder, out fileFullPathes, _isIncludeSubFolders, _fileTypes, true);

                foreach (string fileFullPath in fileFullPathes)
                {
                    FileDetails fileDetails = new FileDetails();
                    //name
                    string path;
                    if (_isDisplayFullFilePath)
                    {
                        fileDetails.fileName = fileFullPath;
                    }
                    else
                    {
                        SplitPathAndFileName(fileFullPath, out path, out fileDetails.fileName);
                    }
                    //size
                    long size;
                    GetFileSize(fileFullPath, out fileDetails.fileSize);
                    //CRC
                    UInt32 crc;
                    GetFileCrc(fileFullPath, out crc);
                    fileDetails.fileCrc = String.Format("0x{0:X8}", crc);
                    //add to details list
                    _fileDetailsList.Add(fileDetails);
                }
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        /*-----------------------------------------------------------------------------------*/

        private static void GetFileCrc(string _fileFullPath, out uint _fileCrc)
        {
            byte[] fileBytes = File.ReadAllBytes(_fileFullPath);
            _fileCrc = CalculateCRC(fileBytes, fileBytes.Length);
        }

        /*-----------------------------------------------------------------------------------*/

        private static UInt32[] m_CrcArr = new UInt32[] 
        {
            0x00000000, 0x77073096, 0xEE0E612C, 0x990951BA,
            0x076DC419, 0x706AF48F, 0xE963A535, 0x9E6495A3,
            0x0EDB8832, 0x79DCB8A4, 0xE0D5E91E, 0x97D2D988,
            0x09B64C2B, 0x7EB17CBD, 0xE7B82D07, 0x90BF1D91,
            0x1DB71064, 0x6AB020F2, 0xF3B97148, 0x84BE41DE,
            0x1ADAD47D, 0x6DDDE4EB, 0xF4D4B551, 0x83D385C7,
            0x136C9856, 0x646BA8C0, 0xFD62F97A, 0x8A65C9EC,
            0x14015C4F, 0x63066CD9, 0xFA0F3D63, 0x8D080DF5,
            0x3B6E20C8, 0x4C69105E, 0xD56041E4, 0xA2677172,
            0x3C03E4D1, 0x4B04D447, 0xD20D85FD, 0xA50AB56B,
            0x35B5A8FA, 0x42B2986C, 0xDBBBC9D6, 0xACBCF940,
            0x32D86CE3, 0x45DF5C75, 0xDCD60DCF, 0xABD13D59,
            0x26D930AC, 0x51DE003A, 0xC8D75180, 0xBFD06116,
            0x21B4F4B5, 0x56B3C423, 0xCFBA9599, 0xB8BDA50F,
            0x2802B89E, 0x5F058808, 0xC60CD9B2, 0xB10BE924,
            0x2F6F7C87, 0x58684C11, 0xC1611DAB, 0xB6662D3D,
            0x76DC4190, 0x01DB7106, 0x98D220BC, 0xEFD5102A,
            0x71B18589, 0x06B6B51F, 0x9FBFE4A5, 0xE8B8D433,
            0x7807C9A2, 0x0F00F934, 0x9609A88E, 0xE10E9818,
            0x7F6A0DBB, 0x086D3D2D, 0x91646C97, 0xE6635C01,
            0x6B6B51F4, 0x1C6C6162, 0x856530D8, 0xF262004E,
            0x6C0695ED, 0x1B01A57B, 0x8208F4C1, 0xF50FC457,
            0x65B0D9C6, 0x12B7E950, 0x8BBEB8EA, 0xFCB9887C,
            0x62DD1DDF, 0x15DA2D49, 0x8CD37CF3, 0xFBD44C65,
            0x4DB26158, 0x3AB551CE, 0xA3BC0074, 0xD4BB30E2,
            0x4ADFA541, 0x3DD895D7, 0xA4D1C46D, 0xD3D6F4FB,
            0x4369E96A, 0x346ED9FC, 0xAD678846, 0xDA60B8D0,
            0x44042D73, 0x33031DE5, 0xAA0A4C5F, 0xDD0D7CC9,
            0x5005713C, 0x270241AA, 0xBE0B1010, 0xC90C2086,
            0x5768B525, 0x206F85B3, 0xB966D409, 0xCE61E49F,
            0x5EDEF90E, 0x29D9C998, 0xB0D09822, 0xC7D7A8B4,
            0x59B33D17, 0x2EB40D81, 0xB7BD5C3B, 0xC0BA6CAD,
            0xEDB88320, 0x9ABFB3B6, 0x03B6E20C, 0x74B1D29A,
            0xEAD54739, 0x9DD277AF, 0x04DB2615, 0x73DC1683,
            0xE3630B12, 0x94643B84, 0x0D6D6A3E, 0x7A6A5AA8,
            0xE40ECF0B, 0x9309FF9D, 0x0A00AE27, 0x7D079EB1,
            0xF00F9344, 0x8708A3D2, 0x1E01F268, 0x6906C2FE,
            0xF762575D, 0x806567CB, 0x196C3671, 0x6E6B06E7,
            0xFED41B76, 0x89D32BE0, 0x10DA7A5A, 0x67DD4ACC,
            0xF9B9DF6F, 0x8EBEEFF9, 0x17B7BE43, 0x60B08ED5,
            0xD6D6A3E8, 0xA1D1937E, 0x38D8C2C4, 0x4FDFF252,
            0xD1BB67F1, 0xA6BC5767, 0x3FB506DD, 0x48B2364B,
            0xD80D2BDA, 0xAF0A1B4C, 0x36034AF6, 0x41047A60,
            0xDF60EFC3, 0xA867DF55, 0x316E8EEF, 0x4669BE79,
            0xCB61B38C, 0xBC66831A, 0x256FD2A0, 0x5268E236,
            0xCC0C7795, 0xBB0B4703, 0x220216B9, 0x5505262F,
            0xC5BA3BBE, 0xB2BD0B28, 0x2BB45A92, 0x5CB36A04,
            0xC2D7FFA7, 0xB5D0CF31, 0x2CD99E8B, 0x5BDEAE1D,
            0x9B64C2B0, 0xEC63F226, 0x756AA39C, 0x026D930A,
            0x9C0906A9, 0xEB0E363F, 0x72076785, 0x05005713,
            0x95BF4A82, 0xE2B87A14, 0x7BB12BAE, 0x0CB61B38,
            0x92D28E9B, 0xE5D5BE0D, 0x7CDCEFB7, 0x0BDBDF21,
            0x86D3D2D4, 0xF1D4E242, 0x68DDB3F8, 0x1FDA836E,
            0x81BE16CD, 0xF6B9265B, 0x6FB077E1, 0x18B74777,
            0x88085AE6, 0xFF0F6A70, 0x66063BCA, 0x11010B5C,
            0x8F659EFF, 0xF862AE69, 0x616BFFD3, 0x166CCF45,
            0xA00AE278, 0xD70DD2EE, 0x4E048354, 0x3903B3C2,
            0xA7672661, 0xD06016F7, 0x4969474D, 0x3E6E77DB,
            0xAED16A4A, 0xD9D65ADC, 0x40DF0B66, 0x37D83BF0,
            0xA9BCAE53, 0xDEBB9EC5, 0x47B2CF7F, 0x30B5FFE9,
            0xBDBDF21C, 0xCABAC28A, 0x53B39330, 0x24B4A3A6,
            0xBAD03605, 0xCDD70693, 0x54DE5729, 0x23D967BF,
            0xB3667A2E, 0xC4614AB8, 0x5D681B02, 0x2A6F2B94,
            0xB40BBE37, 0xC30C8EA1, 0x5A05DF1B, 0x2D02EF8D
        };

        static public UInt32 CalculateCRC(Byte[] _pBuffer, int _len)
        {
            UInt32 retValue = 0xFFFFFFFFU;

            for (int i = 0; i < _len; i++)
            {
                if (_pBuffer != null)
                {
                    retValue = m_CrcArr[(retValue ^ _pBuffer[i]) & 0xFFU] ^ (retValue >> 8);
                }
            }

            return (retValue ^ 0xFFFFFFFF);
        }

        /*-----------------------------------------------------------------------------------*/
        public static bool GetFilesNamesInFolder(string _folder, out string[] _filesNames, bool _isIncludeSubFolders = false, string _fileTypes = "*.*", bool _isGetFullPath = false)
        {
            _filesNames = new string[0];

            try
            {
                SearchOption searchOption = SearchOption.TopDirectoryOnly;
                if (_isIncludeSubFolders)
                {
                    searchOption = SearchOption.AllDirectories;
                }

                string[] fileTypes = _fileTypes.Split('|');
                List<string> files = new List<string>();
                foreach (string fileType in fileTypes)
                {
                    string[] filesToAdd = Directory.GetFiles(_folder, fileType, searchOption);
                    if (!_isGetFullPath)
                    {
                        filesToAdd.Select(Path.GetFileName).ToArray();
                    }
                    files.AddRange(filesToAdd);
                }

                files.Sort();

                _filesNames = files.ToArray();

            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        public static string[] CsvReadToStrArr(string _csvFilePath)
        {
            string[] strArrToRet = File.ReadAllText(_csvFilePath).Split('\n');
            strArrToRet = strArrToRet.Select(x => x.Replace("\r", string.Empty)).ToArray();
            strArrToRet = strArrToRet.Select(x => x.Replace("\n", string.Empty)).ToArray();
            strArrToRet = strArrToRet.Select(x => x.Replace(",", string.Empty)).ToArray();

            return strArrToRet;
        }

        /*-----------------------------------------------------------------------------------*/
        public static string GetFileSizeForVdd(long _size)
        {
            string sizeStr = GetBytesReadable(_size);
            string finalSizeStr = sizeStr + " (" + _size.ToString("#,##0") + " bytes)";

            return finalSizeStr;
        }

        /*-----------------------------------------------------------------------------------*/
        public static string GetBytesReadable(long i)
        {
            // Get absolute value
            long absolute_i = (i < 0 ? -i : i);
            // Determine the suffix and readable value
            string suffix;
            double readable;
            if (absolute_i >= 0x1000000000000000) // Exabyte
            {
                suffix = "EB";
                readable = (i >> 50);
            }
            else if (absolute_i >= 0x4000000000000) // Petabyte
            {
                suffix = "PB";
                readable = (i >> 40);
            }
            else if (absolute_i >= 0x10000000000) // Terabyte
            {
                suffix = "TB";
                readable = (i >> 30);
            }
            else if (absolute_i >= 0x40000000) // Gigabyte
            {
                suffix = "GB";
                readable = (i >> 20);
            }
            else if (absolute_i >= 0x100000) // Megabyte
            {
                suffix = "MB";
                readable = (i >> 10);
            }
            else if (absolute_i >= 0x400) // Kilobyte
            {
                suffix = "KB";
                readable = i;
            }
            else
            {
                return i.ToString("0 B"); // Byte
            }
            // Divide by 1024 to get fractional value
            readable = (readable / 1024);
            // Return formatted number with suffix
            return readable.ToString("0.# ") + suffix;
        }

        /*-----------------------------------------------------------------------------------*/

        static public bool GetFileSize(string _filePath, out long _size)
        {
            _size = 0;

            try
            {
                _size = new System.IO.FileInfo(_filePath).Length;
            }
            catch(Exception e)
            {
                return false;
            }

            return true;
        }

        /*-----------------------------------------------------------------------------------*/
        static public bool ZipPath(string _zipExeLocation, string _pathToZip, string _pathDest, string _zipFileName, string[] _foldersToExclude = null)
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
                _pathToZip = CheckAndFixPathToDoubleSlesh(_pathToZip);
                _pathDest = CheckAndFixPathToDoubleSlesh(_pathDest);
                //remove last '\\
                _pathToZip = _pathToZip.Substring(0, _pathToZip.Length - 1);
                string cmd = "7z";

                //get folders to exclude
                StringBuilder excludeStr = new StringBuilder("");
                if (_foldersToExclude != null)
                {                  
                    foreach(string folder in _foldersToExclude)
                    {
                        excludeStr.Append("-xr!" + folder + " ");
                    }
                }

                string args = "a -tzip " + excludeStr.ToString() + " " + _pathDest + _zipFileName + ".zip " + _pathToZip;
                _zipExeLocation = CheckAndFixPathToDoubleSlesh(_zipExeLocation);
                success = ExecuteCommand(cmd, args, _zipExeLocation);
            }
            catch
            {
                return false;
            }

            return success;
        }

        /*-----------------------------------------------------------------------------------*/
        static public bool ExtractPath(string _zipExeLocation, string _filePathToExtract, string _ExtractToPath)
        {
            bool success = true;

            try
            {
                //create _pathDest if path does not exist
                if (!Directory.Exists(_ExtractToPath))
                {
                    Directory.CreateDirectory(_ExtractToPath);
                }

                //zip _pathToZip into _pathDest/_zipFileName
                _filePathToExtract = CheckAndFixPathToDoubleSlesh(_filePathToExtract);
                _filePathToExtract = CheckAndFixPathToDoubleSlesh(_filePathToExtract);
                //remove last '\\
                _filePathToExtract = _filePathToExtract.Substring(0, _filePathToExtract.Length - 1);
                string cmd = "7z";

                string args = "x " + _filePathToExtract + " -y -o" + _ExtractToPath;
                _zipExeLocation = CheckAndFixPathToDoubleSlesh(_zipExeLocation);
                success = ExecuteCommand(cmd, args, _zipExeLocation);
            }
            catch
            {
                return false;
            }

            return success;
        }

        /*-----------------------------------------------------------------------------------*/

        public static bool CopyFolderWithAllContent(string _source, string _target)
        {
            try
            {
                DirectoryInfo source = new DirectoryInfo(_source);
                DirectoryInfo target = new DirectoryInfo(_target);


                Directory.CreateDirectory(target.FullName);

                // Copy each file into the new directory.
                foreach (FileInfo fi in source.GetFiles())
                {
                    //Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
                    fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
                }

                // Copy each subdirectory using recursion.
                foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
                {
                    DirectoryInfo nextTargetSubDir =
                        target.CreateSubdirectory(diSourceSubDir.Name);

                    //recursion
                    CopyFolderWithAllContent(diSourceSubDir.ToString(), nextTargetSubDir.ToString());
                }
            }
            catch(Exception e)
            {
                return false;
            }

            return true;
        }

        /*-----------------------------------------------------------------------------------*/

        static public string CheckAndFixPathToDoubleSlesh(string _path, bool _shouldEndWithSlash = true)
        {
            string pathToret = _path;

            pathToret = pathToret.Replace('/', '\\');

            return FixPathEnd(pathToret, _shouldEndWithSlash, false);
        }

        static public string CheckAndFixPathToBackslesh(string _path, bool _shouldEndWithSlash = true)
        {
            string pathToret = _path;

            pathToret = pathToret.Replace('\\', '/');

            return FixPathEnd(pathToret, _shouldEndWithSlash, true);
        }

        /*-----------------------------------------------------------------------------------*/

        static public string FixPathEnd(string _path, bool _shouldEndSlash, bool isBackSlash = false)
        {
            string pathToRet = _path;

            if (_shouldEndSlash)
            {
                if (!isBackSlash)
                {
                    if (!pathToRet.EndsWith("\\"))
                    {
                        pathToRet += "\\";
                    }
                }
                else
                {
                    if (!pathToRet.EndsWith("/"))
                    {
                        pathToRet += "/";
                    }
                }
            }
            else
            {
                if (!isBackSlash)
                {
                    if (pathToRet.EndsWith("\\"))
                    {
                        pathToRet = pathToRet.TrimEnd('\\');
                    }
                }
                else
                {
                    if (pathToRet.EndsWith("/"))
                    {
                        pathToRet = pathToRet.TrimEnd('/');
                    }
                }
            }

            return pathToRet;
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

        static public bool ExecuteCommand(string _cmd, string _args = "", string _fromPath = "", bool _isBlocking = true)
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
                startInfo.UseShellExecute = true;
                if (_args == "")
                {
                    TryParseToArgs(ref _cmd, ref _args);
                }
                if (_args != "")
                {
                    startInfo.Arguments = _args;
                }
                startInfo.FileName = _cmd;
                process.StartInfo = startInfo;
                process.Start();
 				if (_isBlocking)
                {
	                process.WaitForExit();
	                if (process.ExitCode != 0)
	                {
	                    return false;
	                }
				}
            }
            catch
            {
                return false;
            }

            return true;
        }

        /*-----------------------------------------------------------------------------------*/
        public static bool CopyFile(string _srcFilePath, string _dstFolderPath, string _dstFileName = "")
        {
            bool success = true;

            try
            {
                if (!Directory.Exists(_dstFolderPath))
                {
                    Directory.CreateDirectory(_dstFolderPath);
                }

                string dstFilePath = CheckAndFixPathToBackslesh(_dstFolderPath);
                if (String.IsNullOrEmpty(_dstFileName))
                {
                    string folderPath;
                    string origFileName;
                    success = SplitPathAndFileName(_srcFilePath, out folderPath, out origFileName);
                    if(!success)
                    {
                        return false;
                    }

                    dstFilePath += origFileName;
                }
                else
                {
                    dstFilePath += _dstFileName;
                }

                File.Copy(_srcFilePath, dstFilePath, true);
            }
            catch(Exception e)
            {
                return false;
            }

            return true;
        }

        /*-----------------------------------------------------------------------------------*/

        static public bool SplitPathAndFileName(string _fullFilePath, out string _path, out string _fileName)
        {
            _path = "";
            _fileName = "";

            try
            {
                string fixedPath = CheckAndFixPathToBackslesh(_fullFilePath, false);
                int lastIndexOfBackslesh = fixedPath.LastIndexOf('/');

                _path = fixedPath.Substring(0, lastIndexOfBackslesh);
                int startFileNameIndex = lastIndexOfBackslesh + 1;
                _fileName = fixedPath.Substring(startFileNameIndex, fixedPath.Length - startFileNameIndex);
            }
            catch(Exception e)
            {
                return false;
            }

            return true;
        }

        /*-----------------------------------------------------------------------------------*/

        static public void TryParseToArgs(ref string _cmd, ref string _args)
        {
            string strArgsTitle = "Arguments";
            string strArgs;
            int indOfStr = _cmd.LastIndexOf(strArgsTitle);

            if (indOfStr > 0)
            {
                strArgs = _cmd.Remove(0, indOfStr + strArgsTitle.Length);
                _cmd = _cmd.Remove(indOfStr);
                _cmd = _cmd.TrimEnd();
                indOfStr = strArgs.IndexOf("=");
                strArgs = strArgs.Remove(0, indOfStr + 1);
                if (strArgs.Length > 0)
                {
                    strArgs = strArgs.TrimStart();
                    _args = strArgs;
                }
            }
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

        /*-----------------------------------------------------------------------------------*/

        public static bool IsPathesEqual(string _path1, string _path2)
        {
            string path1ToCompare = _path1.Trim('\\','/');
            string path2ToCompare = _path2.Trim('\\', '/');

            return path1ToCompare.Equals(path2ToCompare);
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
		
		/*----------------------------------------------------------------------------*/

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
