using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SvnLib
{
    public class HistoryDataItem
    {
        /*--------------------------------------------------------------------------*/

        private string m_itemName;
        private long m_itemRevision;
        private long m_itemSize;
        private DateTime m_itemTime;
        private string m_changedBy;
        private string m_TagLastChanged;
     
        /*--------------------------------------------------------------------------*/

        public string TagLastChanged
        {
            get { return m_TagLastChanged; }
            set { m_TagLastChanged = value; }
        }

        public string ItemName
        {
            get { return m_itemName; }
            set { m_itemName = value; }
        }
      
        public long ItemRevision
        {
            get { return m_itemRevision; }
            set { m_itemRevision = value; }
        }
   
        public long ItemSize
        {
            get { return m_itemSize; }
            set { m_itemSize = value; }
        }
  
        public DateTime ItemTime
        {
            get { return m_itemTime; }
            set { m_itemTime = value; }
        }

        public string ChangedBy
        {
            get { return m_changedBy; }
            set { m_changedBy = value; }
        }

        /*--------------------------------------------------------------------------*/
    }
}
