using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QC = TDAPIOLELib;
using GeneralLib;

namespace QcLib
{
    abstract public class Filter
    {
        protected Dictionary<string, string> dicFields = new Dictionary<string, string>();
        protected QC.TDFilter qcFilter;

        public string this[string sKey]
        {
            get
            {
                string sResult = string.Empty;
                try
                {
                    if(dicFields.Keys.Contains(sKey))
                    {
                        sResult = qcFilter[dicFields[sKey]];
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return sResult;

            }
            set
            {
                try
                {
                    if (dicFields.Keys.Contains(sKey))
                    {
                        qcFilter[dicFields[sKey]] = value;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
            
        }
    }

    public class DefectFilter : Filter
    {
        public DefectFilter(ref QC.TDFilter filter)
        {
            string[] sKeys = { };
            string[] sFields = { };
            cSharpFuncs.ListsToMap<string, string>(sKeys, sFields, ref dicFields);
            qcFilter = filter;
        }
    }

    public class TestFilter : Filter
    {
        public TestFilter(ref QC.TDFilter filter)
        {
            string[] sKeys = { };
            string[] sFields = { };
            cSharpFuncs.ListsToMap<string, string>(sKeys, sFields, ref dicFields);
            qcFilter = filter;
        }
    }

    public class RequirementFilter : Filter
    {
        public RequirementFilter(ref QC.TDFilter filter)
        {
            string[] sKeys = { };
            string[] sFields = { };
            cSharpFuncs.ListsToMap<string, string>(sKeys, sFields, ref dicFields);
            qcFilter = filter;
        }
    }
}
