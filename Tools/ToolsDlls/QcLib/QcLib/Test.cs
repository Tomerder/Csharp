using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QC = TDAPIOLELib;

namespace QcLib
{
    public class Test
    {
        #region Properties

        public int iTestID { get; private set; } =0;
        public string sTestName { get; private set; } =string.Empty;
        public string sTestState { get; private set; } = string.Empty;
        public string sTestPath { get; private set; } = string.Empty;
        public string sExcutionDate { get; private set; } = string.Empty;

        #endregion

        #region Constructors

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="test">QC test struct input</param>
        public Test(QC.Test test)
        {
            iTestID = test.ID;
            sTestName = test.Name;
            sTestState = test.ExecStatus;
            sTestPath = test.FullPath;
            sExcutionDate = test.ExecDate.ToString();
        }

        #endregion


    }
}
