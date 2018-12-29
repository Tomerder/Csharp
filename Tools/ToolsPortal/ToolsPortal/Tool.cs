using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToolsPortal
{
    class Tool
    {
        string m_name;
        string m_exeFilePath;
        string m_iconFilePath;
        string m_readmeFilePath;
        string m_author;

        public string Author
        {
            get { return m_author; }
            set { m_author = value; }
        }

        public string ReadmeFilePath
        {
            get { return m_readmeFilePath; }
            set { m_readmeFilePath = value; }
        }

        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        public string ExeFilePath
        {
            get { return m_exeFilePath; }
            set { m_exeFilePath = value; }
        }

        public string IconFilePath
        {
            get { return m_iconFilePath; }
            set { m_iconFilePath = value; }
        }
    }
}
