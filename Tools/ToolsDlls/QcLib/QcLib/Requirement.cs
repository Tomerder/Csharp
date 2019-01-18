using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QC = TDAPIOLELib;

namespace QcLib
{
    public class Requirement
    {
        #region Properties

        public string sReqId { get; private set; } = string.Empty;
        public string sReqType { get; private set; } = string.Empty;
        public string sReqProject { get; private set; } = string.Empty;
        public string sReqCertTarg { get; private set; } = string.Empty;
        public string sReqBaseLine { get; private set; } = string.Empty;
        public string sReqTraced { get; private set; } = string.Empty;
        public string sReqStatus { get; private set; } = string.Empty;
        public string sReqPath { get; private set; } = string.Empty;
        public bool bHasCoverage { get; private set; } = false;

        #endregion

        #region Costructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="requirement">QC requirement type input</param>
        public Requirement(QC.Req requirement)
        {
            sReqId = requirement["RQ_USER_02"];
            sReqType = requirement["RQ_USER_15"];
            sReqProject = requirement["RQ_USER_16"];
            sReqCertTarg = requirement["RQ_USER_17"];
            sReqTraced = requirement["RQ_USER_59"];
            sReqBaseLine = requirement["RQ_USER_39"];
            sReqBaseLine = sReqBaseLine.Substring(0, sReqBaseLine.IndexOf('\r'));
            sReqBaseLine = sReqBaseLine.Substring(sReqBaseLine.LastIndexOf(' '), sReqBaseLine.Length - sReqBaseLine.LastIndexOf(' '));
            sReqPath = requirement.Path;
            sReqStatus = requirement.Status;
            bHasCoverage = requirement.HasCoverage;
        }

        /// <summary>
        /// Copy Constructor
        /// </summary>
        /// <param name="req"></param>
        public Requirement(Requirement req)
        {
            sReqId = req.sReqId;
            sReqType = req.sReqType;
            sReqProject = req.sReqProject;
            sReqCertTarg = req.sReqCertTarg;
            sReqTraced = req.sReqTraced;
            sReqBaseLine = req.sReqBaseLine;
            sReqPath = req.sReqPath;
            sReqStatus = req.sReqStatus;
            bHasCoverage = req.bHasCoverage;
        }

        #endregion

        #region Interface

        /// <summary>
        /// override fot Equals function
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            Requirement req = obj as Requirement;
            return this == req;
        }

        /// <summary>
        /// Override for GetHash function
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return sReqId.GetHashCode();
        }

        /// <summary>
        /// Override equals operator
        /// </summary>
        /// <param name="req1"></param>
        /// <param name="req2"></param>
        /// <returns></returns>
        public static bool operator == (Requirement req1, Requirement req2)
        {
            return ((req1.sReqId == req2.sReqId) && (req1.sReqBaseLine == req2.sReqBaseLine));
        }

        /// <summary>
        /// Override Diff Operator
        /// </summary>
        /// <param name="req1"></param>
        /// <param name="req2"></param>
        /// <returns></returns>
        public static bool operator != (Requirement req1, Requirement req2)
        {
            return req1.sReqId != req2.sReqId;
        }

        #endregion
    }
}
