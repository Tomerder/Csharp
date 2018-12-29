using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoVersionRelease
{
    class Csci
    {
        //----------------------------------------------------------------------------------------------------

        public enum SvnPathTypeEnum { NONE, TRUNK, BRANCH, TAG };
        public enum WorkingCopyStatusEnum { NONE, CHECKED_OUT, BUILT_AND_READY, WORKING_COPY_REV_AND_DEPENDENTS_ARE_EQUAL_TO_TAGS };

        //----------------------------------------------------------------------------------------------------
        
        private string m_csciName;

        //svn parameters
        private string m_svnUrlPath;
        private SvnPathTypeEnum m_svnPathType;        
        private string m_localPathToCheckout;
        private long m_revisionCheckedOut;
        private WorkingCopyStatusEnum m_workingCopyStatus;
        bool m_isCheckoutEvenIfAlreadyExists;
     
        //version
        private string m_workingCopyVersion;
        private string m_workingCopyVersionAfterIncrease;
   
        //build parameters
        private string m_PreBuildScriptPath;
        private string m_buildScriptPath;   
        private List<string> m_dependentCsciList;
        private bool m_isSysint;
       
        //----------------------------------------------------------------------------------------------------

        public Csci()
        {
            m_dependentCsciList = new List<string>();
        }

        //----------------------------------------------------------------------------------------------------

        public bool IsCheckoutIfAlreadyExists
        {
            get { return m_isCheckoutEvenIfAlreadyExists; }
            set { m_isCheckoutEvenIfAlreadyExists = value; }
        }

        public string WorkingCopyVersionAfterIncrease
        {
            get { return m_workingCopyVersionAfterIncrease; }
            set { m_workingCopyVersionAfterIncrease = value; }
        }

        public string BuildScriptPath
        {
            get { return m_buildScriptPath; }
            set { m_buildScriptPath = value; }
        }

        public string WorkingCopyVersion
        {
            get { return m_workingCopyVersion; }
            set { m_workingCopyVersion = value; }
        }

        internal WorkingCopyStatusEnum WorkingCopyStatus
        {
            get { return m_workingCopyStatus; }
            set { m_workingCopyStatus = value; }
        }

        public long RevisionCheckedOut
        {
            get { return m_revisionCheckedOut; }
            set { m_revisionCheckedOut = value; }
        }

        internal SvnPathTypeEnum SvnPathType
        {
            get { return m_svnPathType; }
            set { m_svnPathType = value; }
        }

        public string SvnUrlPath
        {
            get { return m_svnUrlPath; }
            set 
            { 
                //set svn path
                m_svnUrlPath = value;

                //set svn path type
                if (m_svnUrlPath.Contains("trunk"))
                {
                    m_svnPathType = SvnPathTypeEnum.TRUNK;
                }
                else if (m_svnUrlPath.Contains("branch"))
                {
                    m_svnPathType = SvnPathTypeEnum.BRANCH;
                }
                else if (m_svnUrlPath.Contains("tag"))
                {
                    m_svnPathType = SvnPathTypeEnum.TAG;
                }
                else
                {
                    m_svnPathType = SvnPathTypeEnum.NONE;
                }
            
            }
        }

        public string LocalPathToCheckout
        {
            get { return m_localPathToCheckout; }
            set { m_localPathToCheckout = value; }
        }

        //----------------------------------------------------------------------------------------------------

        public string CsciName
        {
            get { return m_csciName; }
            set { m_csciName = value; }
        }

        public List<string> DependentCsciList
        {
            get { return m_dependentCsciList; }
            set { m_dependentCsciList = value; }
        }

        public bool IsSysint
        {
            get { return m_isSysint; }
            set { m_isSysint = value; }
        }

        public string PreBuildScriptPath
        {
            get { return m_PreBuildScriptPath; }
            set { m_PreBuildScriptPath = value; }
        }

        //----------------------------------------------------------------------------------------------------
    }
}
