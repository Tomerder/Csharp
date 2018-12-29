using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FaliuresMonitor
{
    class Failure
    {
        public const int FAILURE_RESULT_BITS_LEN = 8; 

        //------------------------------------------

        private int m_failureNum;
        private string m_failureName;
        private bool m_failureResult;

        //------------------------------------------

        public Failure() { }

        public Failure(int _failureNum, string _failureName)
        {
            m_failureNum = _failureNum;
            m_failureName = _failureName;
        }

        //------------------------------------------

        public string FailureName
        {
            get { return m_failureName; }
            set { m_failureName = value; }
        }

        public int FailureNum
        {
            get { return m_failureNum; }
            set { m_failureNum = value; }
        }

        public bool FailureResult
        {
            get { return m_failureResult; }
            set { m_failureResult = value; }
        }

        //------------------------------------------

    }
}
