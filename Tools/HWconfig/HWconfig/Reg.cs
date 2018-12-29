using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HWconfig
{
    enum VarTypeEnum { INT, STRING, DOUBLE, FLOAT };
    enum ValidationEnum { NONE, MIN_MAX_VALUE, MIN_VALUE, MAX_VALUE, LIST_OF_VALUES };

    class Reg
    {
        //DM
        private string m_regTitleName;
        private string m_name;
        private string m_address;
        //private string m_offsetAddress;
        private string m_resetValue;
        private int m_startsFromBitNum;
        private int m_lengthBits;
        //private VarTypeEnum m_eType;
        //private bool m_isMand;
        private bool m_isReadOnly;
        private ValidationEnum m_eValidation;
        private double m_minVal;
        private double m_maxVal;
        private HashSet<string> m_possiableValues;

        private string m_tabName;

        //save last value that was updated in order to compere to value that is read after form reload
        private string m_valueUpdated;

        //save last value that was loaded from target
        private string m_valueLastLoaded;

        public Reg()
        {
            //defaults values
            m_startsFromBitNum = 0;
            m_lengthBits = 32;

            m_valueUpdated = String.Empty;
            m_valueLastLoaded = String.Empty;

        }
        /*---------getters and setters--------------------*/

        public string RegName
        {
            get { return m_regTitleName; }
            set { m_regTitleName = value; }
        }

        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        public string Address
        {
            get { return m_address; }
            set { m_address = value; }
        }

        /*public string OffsetAddress
        {
            get { return m_offsetAddress; }
            set { m_offsetAddress = value; }
        }*/

        public bool IsReadOnly
        {
            get { return m_isReadOnly; }
            set { m_isReadOnly = value; }
        }

        public ValidationEnum EValidation
        {
            get { return m_eValidation; }
            set { m_eValidation = value; }
        }

        public double MinVal
        {
            get { return m_minVal; }
            set { m_minVal = value; }
        }

        public double MaxVal
        {
            get { return m_maxVal; }
            set { m_maxVal = value; }
        }

        public HashSet<string> PossiableValues
        {
            get { return m_possiableValues; }
            set { m_possiableValues = value; }
        }

        public string ResetValue
        {
            get { return m_resetValue; }
            set { m_resetValue = value; }
        }

        public string TabName
        {
            get { return m_tabName; }
            set { m_tabName = value; }
        }

        public string ValueUpdated
        {
            get { return m_valueUpdated; }
            set { m_valueUpdated = value; }
        }

        public int StartsFromBitNum
        {
            get { return m_startsFromBitNum; }
            set { m_startsFromBitNum = value; }
        }

        public int LengthBits
        {
            get { return m_lengthBits; }
            set { m_lengthBits = value; }
        }

        public string ValueLastLoaded
        {
            get { return m_valueLastLoaded; }
            set { m_valueLastLoaded = value; }
        }

        /*--------------------------------------------------------------------*/
        //check if reg is leagel - all mandatory parameters are exist
        public bool IsLegal()
        {
            if (m_name == null || m_name == String.Empty)
            {
                return false;
            }
            
            if (m_address == null || m_address == String.Empty)
            {
                return false;
            }

            //if (m_resetValue == null || m_address == String.Empty)
            //{
            //    return false;
            //}

            return true;
        }

        /*--------------------------------------------------------------------*/

        public AutoCompleteStringCollection GetPossiableValues()
        {
            AutoCompleteStringCollection autoCompleteSource = new AutoCompleteStringCollection();

            foreach (string value in m_possiableValues)
            {
                autoCompleteSource.Add(value);
            }

            return autoCompleteSource;
        }

        /*--------------------------------------------------------------------*/

        public string GetBitsRangeStr()
        {
            string bitsRange = "";

            if (LengthBits == 1)
            {
                bitsRange = "TD" + StartsFromBitNum;
            }
            else
            {
                bitsRange = "TD" + StartsFromBitNum + "-" + "TD" + (StartsFromBitNum + LengthBits - 1);
            }

            return bitsRange;
        }

        /*--------------------------------------------------------------------*/

        public string GetRWstr()
        {
            string RWstr = "";

            if (IsReadOnly)
            {
                RWstr = "RO";
            }
            else
            {
                RWstr = "RW";
            }

            return RWstr;
        }

        /*--------------------------------------------------------------------*/

        public string GetAddressStr()
        {
            string addressStr = "";

            addressStr = "0X" + Address.Substring(4);  

            return addressStr;
        }

        /*--------------------------------------------------------------------*/
    }
       
}
