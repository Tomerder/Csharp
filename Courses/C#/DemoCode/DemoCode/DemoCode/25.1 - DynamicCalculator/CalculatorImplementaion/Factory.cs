using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace CalculatorImplementaion
{
    public class Factory
    {
        private JSHost m_Host;
        private object m_Calculator;

        #region Singeltion
        private static Factory m_Instance;

        public static Factory Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new Factory();
                }

                return m_Instance;
            }
        }

        private Factory()
        {
            m_Host = new JSHost();
            m_Host.Show();
            m_Host.Hide();
        }
        #endregion

        public object CalculatorObject
        {
            get
            {
                if (CalcType == "Net")
                {
                    m_Calculator = new CalculatorNet();
                }
                else if (CalcType == "Js")
                {
                    m_Calculator = m_Host.webBrowser1.Document.InvokeScript("GetCalc");
                }

                return m_Calculator;
            }
        }

        public dynamic CalculatorDynamic
        {
            get
            {
                if (CalcType == "Net")
                {
                    m_Calculator = new CalculatorNet();
                }
                else if (CalcType == "Js")
                {
                    m_Calculator = m_Host.webBrowser1.Document.InvokeScript("GetCalc");
                }

                return m_Calculator;
            }
        }

        public string CalcType
        {
            get
            {
                return ConfigurationManager.AppSettings["Calc"];
            }
        }
    }
}
