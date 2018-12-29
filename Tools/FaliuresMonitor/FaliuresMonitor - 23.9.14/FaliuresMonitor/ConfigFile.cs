using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Windows.Forms;
using FaliuresMonitor;

namespace FailuresMonitor
{
    static class ConfigFile
    {
        public const string FAILURES_FILE_PATH = "J:\\EFVS\\Dassault\\Implementation\\Source\\SR1\\OFP_SR1\\GeneratedCode\\Scade\\kcg_types.h";

        private const string ERR_MSG_TITLE = "Configuration File Error";
        static private int dummy;

        //----------------------------------------------------------------------------------------------------

        static public bool ConfigGetFailuresFromPath()
        {
            string text = "";

            //get file from project path or from user
            try
            {
                text = File.ReadAllText(FAILURES_FILE_PATH);
            }
            catch
            {
                string fileName;
                if (!GetFileFromUser(out fileName))
                {
                    return false;
                }
                text = File.ReadAllText(fileName);
            }

            //parse failures from file
            try
            {                
                int indexStart = text.IndexOf("HUDManager::FailuresManager::FailureListEnum");
                text = text.Substring(indexStart);

                indexStart = text.IndexOf("{");
                int indexEnd = text.IndexOf("}");

                text = text.Substring(indexStart, indexEnd - indexStart);

                string[] lines = text.Split(',');

                int failureIndex = 0;

                foreach (string line in lines)
                {
                    if (line.Contains("NUMBER_OF_FAILURES"))
                    {
                        break;
                    }

                    indexStart = line.IndexOf(" ") + 1;
                    indexEnd = line.IndexOf("_HUDManager_FailuresManager");

                    string failureName = line.Substring(indexStart, indexEnd - indexStart);

                    Failure failure = new Failure(failureIndex, failureName);
                    Program.FailuresMap[failureIndex] = failure;

                    failureIndex++;
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        //----------------------------------------------------------------------------------------------------

        static public void ConfigGetFailuresFromXml(string _fileName)
        {
            if (!File.Exists(_fileName))
            {
                _fileName = "config.xml";
            }

            if (File.Exists(_fileName))
            {
                // Load document
                XmlDocument xml = new XmlDocument();
                xml.Load(_fileName);

                XmlNode rootNode = xml.GetElementsByTagName("failuresRoot").Item(0);  // SelectSingleNode("configurationRegisters");

                // Iterate all failures
                int failureIndex = 0;
                foreach (XmlNode tabNode in rootNode.ChildNodes)
                {
                    //add failure to map
                    string failureName = tabNode.Attributes["name"].Value; ;

                    try
                    {
                        Failure failure = new Failure(failureIndex, failureName);

                        Program.FailuresMap[failureIndex] = failure;
                        failureIndex++;
                    }
                    catch
                    {
                        MessageBox.Show("Duplicate failure name : " + failureName, ERR_MSG_TITLE);
                    }

                }//foreach (XmlNode tabNode in rootNode.ChildNodes)

            }// if (File.Exists(_fileName))
        }

        /*--------------------------------------------------------------------------------------------------*/

        static public string ConfigGetLogMode(string _fileName)
        {
            if (!File.Exists(_fileName))
            {
                _fileName = "config.xml";
            }

            if (File.Exists(_fileName))
            {
                // Load document
                XmlDocument xml = new XmlDocument();
                xml.Load(_fileName);

                XmlNode rootNode = xml.GetElementsByTagName("log").Item(0);  // SelectSingleNode("configurationRegisters");

                string logMode = rootNode.Attributes["loggerMode"].Value;

                return logMode;        
            }

            return Logger.LOG_MODE_ENUM.NONE.ToString();
        }

        /*--------------------------------------------------------------------------------------------------*/

        private static bool GetFileFromUser(out string _fileName)
        {
            _fileName = "";
            Stream stream = null;

            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Title = "Open File That Contains Failures List";
            openFileDialog1.InitialDirectory = "";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if ((stream = openFileDialog1.OpenFile()) == null)
                    {
                        return false;
                    }
                }

                FileStream fs = stream as FileStream;

                _fileName = fs.Name;
            }
            catch
            {
                return false;
            }

            return true;
        }

        /*-----------------------------------------------------------------------------------*/

    }  // class ConfigFile

} //namespace HWconfig