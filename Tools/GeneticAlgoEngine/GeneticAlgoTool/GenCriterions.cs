using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticAlgoEngineLib;


namespace GeneticAlgoTool
{    
    public enum CRIT_ENUM
    {
        CRIT_1,
        CRIT_2,
        CRIT_3,
        CRIT_4,
        CRIT_5,
        CRIT_6,
        CRIT_7,
        CRIT_8,
        CRIT_9,
        CRIT_10,
        /*
        CRIT_11,
        CRIT_12,
        CRIT_13,
        CRIT_14,
        CRIT_15,
        CRIT_16,
        CRIT_17,
        CRIT_18,
        CRIT_19,
        */
        NUM_OF_CRIT
    }

    class GenCriterions : GenAbstract<List<CRIT_ENUM>, DbLib.DbInterface> 
    {
        //-------------------------------------------------
        //defines

        public static int MIN_NUM_OF_CRITS_IN_LIST = 3;
        public static int MAX_NUM_OF_CRITS_IN_LIST = (CRIT_ENUM.NUM_OF_CRIT.GetHashCode() * 3 / 4  );

        public static string RESULT_FIELD_NAME = "PROFIT";
        public static string DB_TABLE_NAME = "Table";

        enum QUERY_TYPE_ENUM
        {
            AVG,
            COUNT
        }

        //-------------------------------------------------

        //DM

        int m_countForQuerryResult;

        //-------------------------------------------------

        public GenCriterions()
        {
            List<CRIT_ENUM> critList = new List<CRIT_ENUM>();           

            //set Data member 
            GenValue = critList;
        }

        //-------------------------------------------------

        //copy ctor
        public GenCriterions(GenCriterions _genToCopy) 
        {
            GenValue = new List<CRIT_ENUM>();

            //clone list
            foreach (CRIT_ENUM crit in _genToCopy.GenValue)
            {
                GenValue.Add(crit);
            }
            Grade = _genToCopy.Grade;
        }

        //-------------------------------------------------

        public void CloneList(List<CRIT_ENUM> _listToClone)
        {
            //clone list
            foreach (CRIT_ENUM crit in _listToClone)
            {
                GenValue.Add(crit);
            }
        }

        //-------------------------------------------------
        //override abstract methods

        public override bool GenerateGenValue()
        {
            int numOfCriterionsInList = UtilsLib.UtilsLib.RandomNumber(MIN_NUM_OF_CRITS_IN_LIST, MAX_NUM_OF_CRITS_IN_LIST);

            while (GenValue.Count < numOfCriterionsInList)
            {
                int critToAdd = UtilsLib.UtilsLib.RandomNumber(0, CRIT_ENUM.NUM_OF_CRIT.GetHashCode() - 1);
                AddCritIfNotExist((CRIT_ENUM)critToAdd, GenValue);
            }

            return true;
        }

        override public bool DoMutate(GenAbstract<List<CRIT_ENUM>, DbLib.DbInterface> _genToCopyAndMutate)
        {
            //clone list of _genToCopyAndMutate
            CloneList(_genToCopyAndMutate.GenValue);
          
            //choose crit to replace
            int numOfCrits = GenValue.Count;
            int critToReplace = UtilsLib.UtilsLib.RandomNumber(0, GenValue.Count - 1);

            //choose crit to replace with
            int critToReplaceWith = 0;
            do
            {
                critToReplaceWith = UtilsLib.UtilsLib.RandomNumber(0, CRIT_ENUM.NUM_OF_CRIT.GetHashCode() - 1);
            } while (GenValue.Contains((CRIT_ENUM)critToReplaceWith));

            //replace crit
            GenValue[critToReplace] = (CRIT_ENUM)critToReplaceWith;

            return true;
        }

        override public bool DoPair(GenAbstract<List<CRIT_ENUM>, DbLib.DbInterface> _genToPairWith1, GenAbstract<List<CRIT_ENUM>, DbLib.DbInterface> _genToPairWith2)
        {
            List<CRIT_ENUM> listToPair1 = _genToPairWith1.GenValue; 
            List<CRIT_ENUM> listToPair2 = _genToPairWith2.GenValue; 

            //pair lists
            int numOfCritsInList1 = listToPair1.Count;
            int numOfCritsInList2 = listToPair2.Count;
            int numOfCritsInPairedList = (numOfCritsInList1 + numOfCritsInList2) / 2;

            int iterOfAdd = 0;
            CRIT_ENUM critToadd = CRIT_ENUM.CRIT_1;
            while (GenValue.Count < numOfCritsInPairedList)
            {                
                if (numOfCritsInList1 > iterOfAdd)
                {
                    critToadd = listToPair1[iterOfAdd];
                    AddCritIfNotExist(critToadd, GenValue);
                }
                if (numOfCritsInList2 > iterOfAdd && GenValue.Count < numOfCritsInPairedList)
                {
                    critToadd = listToPair2[iterOfAdd];
                    AddCritIfNotExist(critToadd, GenValue);
                }
                iterOfAdd++;              
            }

            return true;
        }

        override public bool DoGradeGen(DbLib.DbInterface _db)
        {
            //prepere query to run on DB
            string query = PrepareQuery(GenValue, QUERY_TYPE_ENUM.AVG);
           
            //run query on DB - calculate avg for grade
            Object result;
            _db.QueryResult(query, out result);
            Decimal fnum;
            try
            {
                fnum = Convert.ToDecimal(result);
            }
            catch
            {
                fnum = 0;
            }

            //set gen grade
            Grade = (int)(fnum*1000);

            //update count result
            UpdateCountResult(_db);
           
            return true;
        }

        //-------------------------------------------------

        private void UpdateCountResult(DbLib.DbInterface _db)
        {
            //prepere query to run on DB
            string query = PrepareQuery(GenValue, QUERY_TYPE_ENUM.COUNT);

            //run query on DB - calculate avg for grade
            Object result;
            _db.QueryResult(query, out result);
            int count;
            try
            {
                count = Convert.ToInt32(result);
            }
            catch
            {
                count = 0;
            }

            //update count of records which match the criterion
            m_countForQuerryResult = count;

        }
     
        //-------------------------------------------------

        public override string ToString()
        {
            string toRet = "Grade = " + Grade;
            toRet += ", Count = " + m_countForQuerryResult;
            toRet += " ** Criterions : ";

            foreach (CRIT_ENUM crit in GenValue)
            {
                toRet += crit.GetHashCode() + ", ";
            }

            return toRet;
        }

        //-------------------------------------------------

        private string PrepareQuery(List<CRIT_ENUM> _genValue, QUERY_TYPE_ENUM _queryType)
        {
            string query = "SELECT ";
            query += _queryType.ToString();
            query +=  "(" + RESULT_FIELD_NAME + ")";
            query += " FROM [" + DB_TABLE_NAME + "]";

            query += " WHERE ";
            int numOfCritAddedToQuery = 0;
            foreach (CRIT_ENUM crit in _genValue)
            {
                query += crit.ToString() + " = 'True' ";
                numOfCritAddedToQuery++;

                if (numOfCritAddedToQuery < _genValue.Count)
                {
                    query += " AND ";
                }
            }

            return query;
        }

        //-------------------------------------------------

        private void AddCritIfNotExist(CRIT_ENUM _critToAdd, List<CRIT_ENUM> _critList)
        {
            if (!_critList.Contains(_critToAdd))
            {
                _critList.Add(_critToAdd);
            }
        }

        //-------------------------------------------------
    }
}
