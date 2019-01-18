using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QC = TDAPIOLELib;
using System.IO;
using GeneralLib;

namespace QcLib
{
    public class QualityCenterManager : IDisposable
    {

        #region Properties
        public string sQcLogdir  = string.Empty;
        #endregion

        #region Members
        static readonly System.Text.RegularExpressions.Regex REGEX = new System.Text.RegularExpressions.Regex("<[^>]+>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        string sQcProjectName = string.Empty;
        string sQcUserName = string.Empty;
        string sQcPassword = string.Empty;
        string sQcServerName = string.Empty;
        string sQcDomainName = string.Empty;
        QC.TDConnection m_QcConnection = null;
        #endregion

        #region Constructors

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="sProjectName"></param>
        /// <param name="sUserName"></param>
        /// <param name="sPassword"></param>
        /// <param name="sServer">url</param>
        /// <param name="sDomain"></param>
        /// <param name="sOutdir">Path to log file(CMD), default of empty(GUI)</param>
        public QualityCenterManager(string sProjectName, string sUserName, string sPassword, string sServer, string sDomain,string sOutdir = "")
        {
            sQcProjectName = sProjectName;
            sQcUserName = sUserName;
            sQcPassword = sPassword;
            sQcServerName = sServer;
            sQcDomainName = sDomain;
            sQcLogdir = sOutdir;
        }

        #endregion

        #region Interface

        public void Dispose()
        {
            if (m_QcConnection != null)
            {
                DisconnectFromQC();
            }
        }

        /// <summary>
        /// connect to QC using settingd defined in constructor
        /// </summary>
        /// <returns></returns>
        public bool ConnectToQC()
        {
            bool result = false;
            cSharpFuncs.WriteToLogFile(sQcLogdir,$"Connecting to QC ({sQcServerName}/{sQcDomainName}/{sQcProjectName})...");
            try
            {
                if (m_QcConnection != null)
                {
                    DisconnectFromQC();
                }
                m_QcConnection = new QC.TDConnection();
                m_QcConnection.InitConnectionEx(sQcServerName);
                m_QcConnection.ConnectProjectEx(sQcDomainName, sQcProjectName, sQcUserName, sQcPassword);
                result = m_QcConnection.Connected;
                cSharpFuncs.WriteToLogFile(sQcLogdir, result ? "Connected" : "Failed");
            }
            catch (Exception ex)
            {
                cSharpFuncs.WriteToLogFile(sQcLogdir,$"Failed connection with QC (Server:{sQcServerName}, Domain:{sQcDomainName}, Project:{sQcProjectName}) (Hint:{ex.Message})");
            }

            return result;
        }

        /// <summary>
        /// Disconnecting from QC
        /// </summary>
        /// <returns></returns>
        public bool DisconnectFromQC()
        {
            if ((m_QcConnection == null) || !m_QcConnection.Connected)
            {
                throw new Exception("No connection to QC");
            }

            bool result = false;
            cSharpFuncs.WriteToLogFile(sQcLogdir,$"Disconnecting from QC ({sQcServerName}/{sQcDomainName}/{sQcProjectName})...");
            try
            {
                m_QcConnection.DisconnectProject();
                m_QcConnection.Disconnect();
                m_QcConnection = null;
                result = true;
            }
            catch (Exception ex)
            {
                cSharpFuncs.WriteToLogFile(sQcLogdir,$"Failed disconnection from QC (Server:{sQcServerName}, Domain:{sQcDomainName}, Project:{sQcProjectName}) (Hint:{ex.Message})");
            }
            finally
            {
                cSharpFuncs.WriteToLogFile(sQcLogdir,result ? "Disconnected" : "Failed");
            }

            return result;
        }

        /// <summary>
        /// connecting list of requirements to their test and store traceability in map
        /// </summary>
        /// <param name="lsReqIds">input list of requirements</param>
        /// <param name="dicReqToTest">output map of traceability</param>
        /// <returns></returns>
        public bool TestCoverageForRequirement(List<string> lsReqIds, ref Dictionary<Requirement,List<Test>> dicReqToTest)
        {
            bool bSuccess = true;
            try
            {
                string sReqSrch = lsReqIds[0].Substring(0, lsReqIds[0].LastIndexOf('_'));
                cSharpFuncs.WriteToLogFile(sQcLogdir, "Connecting requirements of " + sReqSrch + " to tests...");
                QC.ReqFactory reqFactory = m_QcConnection.ReqFactory;
                QC.TDFilter reqFilter = reqFactory.Filter;

                //reqFilter["RQ_USER_15"] = "requirement"; // DOORS_Requirement_Type
                //reqFilter["RQ_USER_16"] = "EFVS Or DA"; // DOORS_Project
                foreach (string sReqId in lsReqIds)
                {
                    reqFilter["RQ_USER_02"] = sReqId; //DOORS_Object_Identifier
                    
                    QC.List reqList = reqFactory.NewList(reqFilter.Text);
                    //QC.List testList = reqFactory.GetCoverageTestsByReqFilter(filter.Text);
                    foreach(QC.Req req in reqList)
                    {
                        Requirement requirement = new Requirement(req);
                        // check if key already exist (req already defined before)
                        if(!dicReqToTest.Keys.Contains(requirement))
                        {
                            dicReqToTest.Add(new Requirement(requirement), new List<Test>());
                        }
                        QC.List testList = req.GetCoverList();
                        foreach(QC.Test test in testList)
                        {
                            dicReqToTest[requirement].Add(new Test(test));
                            cSharpFuncs.WriteToLogFile(sQcLogdir, "Found test " + test.ID);
                        }
                        if (testList.Count == 0)
                        {
                            cSharpFuncs.WriteToLogFile(sQcLogdir, "No test was found");
                        }
                    }
                    cSharpFuncs.WriteToLogFile(sQcLogdir,"Searching test for requirement " + sReqId + " ... ");
                }
                cSharpFuncs.WriteToLogFile(sQcLogdir, "Connecting requirements of " + sReqSrch + " to tests was completed");
            }
            catch(Exception ex)
            {
                cSharpFuncs.WriteToLogFile(sQcLogdir,"Failed linking requirement to test due to exception");
                cSharpFuncs.WriteToLogFile(sQcLogdir, ex.Message);
                Console.WriteLine(ex.Message);
                bSuccess = false;
            }

            return bSuccess;
        }

        /// <summary>
        /// Function extract list of open defect for VDD according to CSCI
        /// </summary>
        /// <param name="sCsci">chosen CSCI</param>
        /// <param name="lsDefects">Output list of defects</param>
        /// <returns></returns>
        public bool GetListOfOpenDefects(string sCsci, ref List<Defect> lsDefects)
        {
            bool bSuccess = true;
            try
            {
                QC.BugFactory bugFactory = m_QcConnection.BugFactory;
                QC.TDFilter bugFilter = bugFactory.Filter;
                bugFilter["BG_USER_07"] = "0 Or 1* Or 2"; //Supplier PR category
                bugFilter["BG_USER_12"] = "SW"; //PR domain
                bugFilter["BG_USER_32"] = "Open Or Open-New Or De-scoped Or \"Candidate for Descope\""; //PR status
                bugFilter["BG_USER_34"] = sCsci; //PR CSCI / HW
                QC.List bugList = bugFactory.NewList(bugFilter.Text);
                foreach(QC.Bug bug in bugList)
                {
                    lsDefects.Add(new Defect(bug));
                }
            }
            catch (Exception ex)
            {
                cSharpFuncs.WriteToLogFile(sQcLogdir, "Failed extracting open defects due to exception");
                cSharpFuncs.WriteToLogFile(sQcLogdir, ex.Message);
                Console.WriteLine(ex.Message);
                bSuccess = false;
            }
            return bSuccess;
        }

        /// <summary>
        /// Function to extract list of closed defects for VDD according to CSCI
        /// </summary>
        /// <param name="sCsci">chosen CSCI</param>
        /// <param name="sHandVer">latest version for release</param>
        /// <param name="lsDefects">output defects</param>
        /// <returns></returns>
        public bool GetListOfHandledDefects(string sCsci, string sHandVer, ref List<Defect> lsDefects)
        {
            bool bSuccess = true;
            try
            {
                QC.BugFactory bugFactory = m_QcConnection.BugFactory;
                QC.TDFilter bugFilter = bugFactory.Filter;
                bugFilter["BG_USER_07"] = "not *3B* and not *3A*"; //Supplier PR category
                //bugFilter["BG_USER_12"] = "SW"; //PR domain
                //bugFilter["BG_USER_32"] = "Handled"; //PR status
                bugFilter["BG_USER_37"] = "*" + sHandVer + "*"; //PR handled in version
                //bugFilter["BG_USER_34"] = sCsci; //PR CSCI / HW
                QC.List bugList = bugFactory.NewList(bugFilter.Text);
                foreach (QC.Bug bug in bugList)
                {
                    lsDefects.Add(new Defect(bug));
                }
            }
            catch(Exception ex)
            {
                cSharpFuncs.WriteToLogFile(sQcLogdir, "Failed extracting closed defects due to exception");
                cSharpFuncs.WriteToLogFile(sQcLogdir, ex.Message);
                Console.WriteLine(ex.Message);
                bSuccess = false;
            }
            return bSuccess;
        }

        #endregion

    }
}
