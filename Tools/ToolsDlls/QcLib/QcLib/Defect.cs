using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QC = TDAPIOLELib;

namespace QcLib
{
    public class Defect
    {
        #region Members

        private static readonly System.Text.RegularExpressions.Regex REGEX = new System.Text.RegularExpressions.Regex("<[^>]+>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);

        #endregion

        #region Properties

        public int iDefectId { get; private set; } = 0;
        public string sDefectSummary { get; private set; }
        public string sDefectType { get; set; }
        public string sDefectStatus { get; private set; }
        public string sDefectCsci{ get; private set; }
        public string sDefectDescription { get; private set; }
        public string sDefectDomain{ get; private set; }
        public string sDefectRCA{ get; private set; }
        public string sDefectCIA { get; private set; }
        public string sDefectDetectedBy { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bug">QC bug struct input</param>
        public Defect(QC.Bug bug)
        {
            iDefectId = ParseField<int>(bug, "BG_BUG_ID");
            sDefectSummary = ParseField<string>(bug, "BG_SUMMARY"); // BG_TYPE
            sDefectType = ParseField<string>(bug, "BG_USER_07");
            sDefectStatus = ParseField<string>(bug, "BG_USER_32") ?? string.Empty;
            sDefectCsci = ParseField<string>(bug, "BG_USER_34") ?? string.Empty;
            sDefectDescription = ParseField<string>(bug, "BG_DESCRIPTION") ?? string.Empty;
            sDefectDomain = ParseField<string>(bug, "BG_USER_12") ?? string.Empty;
            sDefectRCA = ParseField<string>(bug, "BG_USER_76") ?? string.Empty;
            sDefectCIA = ParseField<string>(bug, "BG_USER_77") ?? string.Empty;
            if (!string.IsNullOrEmpty(sDefectDescription))
            {
                sDefectDescription = REGEX.Replace(sDefectDescription, string.Empty).Replace("&nbsp;", string.Empty);
                sDefectDescription = sDefectDescription.TrimStart().TrimEnd();
            }
            if (!string.IsNullOrEmpty(sDefectRCA))
            {
                sDefectRCA = REGEX.Replace(sDefectRCA, string.Empty).Replace("&nbsp;", string.Empty);
                sDefectRCA = sDefectRCA.TrimStart().TrimEnd();
            }
            if (!string.IsNullOrEmpty(sDefectCIA))
            {
                sDefectCIA = REGEX.Replace(sDefectCIA, string.Empty).Replace("&nbsp;", string.Empty);
                sDefectCIA = sDefectCIA.TrimStart().TrimEnd();
            }

            sDefectDetectedBy = ParseField<string>(bug, "BG_DETECTED_BY") ?? string.Empty;
        }

        #endregion

        #region Interface

        /// <summary>
        /// print defect data for debug 
        /// </summary>
        public void PrintDefect()
        {
            Console.WriteLine("PR Id: " + iDefectId);
            Console.WriteLine("PR Summary: " + sDefectSummary);
            Console.WriteLine("PR Status: " + sDefectStatus);
            Console.WriteLine("PR Type: " + sDefectType);
            Console.WriteLine("CSCI/HW: " + sDefectCsci);
            Console.WriteLine("PR Description: " + sDefectDescription);
            Console.WriteLine("PR Domain: " + sDefectDomain);
        }

        /// <summary>
        /// check if defect has CIA
        /// </summary>
        /// <returns></returns>
        public bool CiaFilled()
        {
            return sDefectCIA != string.Empty;
        }

        /// <summary>
        /// check if defect has RCA
        /// </summary>
        /// <returns></returns>
        public bool RcaFilled()
        {
            return sDefectRCA != string.Empty;
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// function to extract data from QC map
        /// </summary>
        /// <typeparam name="T">type</typeparam>
        /// <param name="bug">QC bug struct</param>
        /// <param name="columnName">Bug value</param>
        /// <returns></returns>
        private T ParseField<T>(QC.Bug bug, string columnName)
        {
            try
            {
                return (T)bug[columnName];
            }
            catch
            {
                return default(T);
            }
        }

        #endregion
    }
}
