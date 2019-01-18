using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VersionControlLib
{
    class Csci
    {
        /*----------------------------------------------------------------------------*/

        public enum VersionFileTypeEnum
        {
            TYPE_A,
            TYPE_B,
            TYPE_C,
            TYPE_D,
            TYPE_E
        };

        /*----------------------------------------------------------------------------*/

        private string m_CsciName;
        private string m_VersionFilePath;
        private VersionFileTypeEnum m_versionFileType;
        private List<string> m_NamesOnLuh;
        private string m_LuhFilePath;
        private string m_luhToupdateVersionOn;
        private bool m_isSysint;

        private VersionFileHandler m_versionFileHandler;

        /*----------------------------------------------------------------------------*/
      
        public Csci()
        {
            m_NamesOnLuh = new List<string>();
        }

        /*----------------------------------------------------------------------------*/

        public bool IsSysint
        {
            get { return m_isSysint; }
            set { m_isSysint = value; }
        }

        public string LuhToUpdateVersionOn
        {
            get { return m_luhToupdateVersionOn; }
            set { m_luhToupdateVersionOn = value; }
        }

        public VersionFileHandler VersionFileHandler
        {
            get { return m_versionFileHandler; }
            set { m_versionFileHandler = value; }
        }

        public string CsciName
        {
            get { return m_CsciName; }
            set { m_CsciName = value; }
        }

        public string VersionFilePath
        {
            get { return m_VersionFilePath; }
            set { m_VersionFilePath = value; }
        }

        internal VersionFileTypeEnum VersionFileType
        {
            get { return m_versionFileType; }
            set 
            {
                m_versionFileType = value;
                switch (m_versionFileType)
                {
                    case VersionFileTypeEnum.TYPE_A:
                        m_versionFileHandler = new VersionFileA(VersionFilePath);
                        break;
                    case VersionFileTypeEnum.TYPE_B:
                        m_versionFileHandler = new VersionFileB(VersionFilePath);
                        break;
                    case VersionFileTypeEnum.TYPE_C:
                        m_versionFileHandler = new VersionFileC(VersionFilePath);
                        break;
                    case VersionFileTypeEnum.TYPE_D:
                        m_versionFileHandler = new VersionFileD(VersionFilePath);
                        break;
                    case VersionFileTypeEnum.TYPE_E:
                        m_versionFileHandler = new VersionFileE(VersionFilePath);
                        break;
                    default:
                        break;
                }    
            }
        }

        /*----------------------------------------------------------------------------*/

        public List<string> NamesOnLuh
        {
            get { return m_NamesOnLuh; }
            set { m_NamesOnLuh = value; }
        }

        public string LuhFilePath
        {
            get { return m_LuhFilePath; }
            set { m_LuhFilePath = value; }
        }

        /*----------------------------------------------------------------------------*/
    }
}
