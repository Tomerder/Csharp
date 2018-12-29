using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FaliuresMonitor
{
    class Failure
    {
        public const int FAILURE_RESULT_BITS_LEN = 8;
        public const int FAILURE_STARTS_FROM_REG_BIT = 3 * 8 - 1; 

        //------------------------------------------

        private int m_failureNum;
        private string m_failureName;
        private bool m_failureResult;

        private bool m_isHudFail;      
        private bool m_isEvsFail;     
        private bool m_isSvsFail;
       
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

        public bool IsHudFail
        {
            get { return m_isHudFail; }
            set { m_isHudFail = value; }
        }

        public bool IsEvsFail
        {
            get { return m_isEvsFail; }
            set { m_isEvsFail = value; }
        }

        public bool IsSvsFail
        {
            get { return m_isSvsFail; }
            set { m_isSvsFail = value; }
        }

        //------------------------------------------
    }
}
