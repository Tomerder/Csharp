using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GeneticAlgoEngineLib;
using System.Data.SqlClient;

namespace GeneticAlgoTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //-------------------------------------------------

        private void buttonExecute_Click(object sender, EventArgs e)
        {
            //ExecuteGenExample();

            //AddRecordsToTable();

            //AddBestRecordsToTable();

            try
            {
                ExecuteGenCriterion();
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.ToString());
            }
                
        }

        //-------------------------------------------------

        void ExecuteGenCriterion()
        {
            //set params for genetic algo engine
            GeneticAlgoParams geneticAlgoEngineparams = new GeneticAlgoParams();
            geneticAlgoEngineparams.numOfIterations = 50;
            geneticAlgoEngineparams.populationSize = 1000;
            geneticAlgoEngineparams.precentSurvive = 30;
            geneticAlgoEngineparams.precentMutate = 20;
            geneticAlgoEngineparams.precentMutateFromBest = 30;
            geneticAlgoEngineparams.precentParing = 30;
            geneticAlgoEngineparams.precentParingFromBest = 30;
            geneticAlgoEngineparams.callBackFuncToShowBestGen = CallBackFuncToShowBestGen;

            //create DB connection
            DbLib.DbInterface dbInterface = new DbLib.DbInterface();
            dbInterface.Connect();

            //create engine
            GeneticAlgoEngine<GenCriterions, List<CRIT_ENUM>, DbLib.DbInterface> engine = new GeneticAlgoEngine<GenCriterions, List<CRIT_ENUM>, DbLib.DbInterface>(geneticAlgoEngineparams, dbInterface, StopConditionGenCriterion);

            //execute engine
            engine.ExecuteEvolution();

            //close DB
            dbInterface.CloseConnection();
        }

        //-------------------------------------------------

        void GetAvgResultFromDbExample()
        {
            DbLib.DbInterface dbInterface = new DbLib.DbInterface();
            dbInterface.Connect();

            string query = "SELECT AVG(a2) FROM [Table]";
            Object result;
            dbInterface.QueryResult(query, out result);

            //convert to decimal
            Decimal fnum = Convert.ToDecimal(result);

            dbInterface.CloseConnection();
        }

        //-------------------------------------------------

        void ExecuteGenExample()
        {
            //set params for genetic algo engine
            GeneticAlgoParams geneticAlgoEngineparams = new GeneticAlgoParams();
            geneticAlgoEngineparams.numOfIterations = 500;
            geneticAlgoEngineparams.populationSize = 10000;
            geneticAlgoEngineparams.precentSurvive = 30;
            geneticAlgoEngineparams.precentMutate = 30;
            geneticAlgoEngineparams.precentMutateFromBest = 30;
            geneticAlgoEngineparams.precentParing = 10;
            geneticAlgoEngineparams.precentParingFromBest = 30;
            geneticAlgoEngineparams.callBackFuncToShowBestGen = CallBackFuncToShowBestGen;

            //create engine
            int solution = UtilsLib.UtilsLib.RandomNumber(Int32.MinValue, Int32.MaxValue);
            Console.WriteLine("Solution is : " + solution);
            GeneticAlgoEngine<GenExample, int, int> engine = new GeneticAlgoEngine<GenExample, int, int>(geneticAlgoEngineparams, solution, StopConditionFuncGenExample);

            engine.ExecuteEvolution();
        }  

        //-------------------------------------------------

        void CallBackFuncToShowBestGen(string _str)
        {
            Console.WriteLine(_str);
        }

        //-------------------------------------------------

        bool StopConditionFuncGenExample(GenExample _bestGen)
        {
            if (_bestGen.Grade == 0)
            {
                return true;
            }

            return false;
        }

        bool StopConditionGenCriterion(GenCriterions _bestGen)
        {         
            return false;
        }

        //-------------------------------------------------
        //queries for insert records to table
        //-------------------------------------------------

        private void AddBestRecordsToTable()
        {
            //create DB connection
            DbLib.DbInterface dbInterface = new DbLib.DbInterface();
            dbInterface.Connect();

            //random set of crits 
            string critQuery = "";
            for (int j = 0; j < 10; j++)
            {
                int critRes = UtilsLib.UtilsLib.RandomNumber(0, 1);
                if (critRes == 1)
                {
                    critQuery += "'True',";
                }
                else
                {
                    critQuery += "'False',";
                }
            }

            for (int i = 0; i < 30; i++)
            {
                int key = i;
                string query = "INSERT INTO [Table] VALUES (" + key + ",";
                query += critQuery;

                int critProfit = 50; //max profit
                query += critProfit + ")";

                //execute query
                dbInterface.QueryExecute(query);
            }

        }

        private void AddRecordsToTable()
        {
            //create DB connection
            DbLib.DbInterface dbInterface = new DbLib.DbInterface();
            dbInterface.Connect();

            //run query 
            int initialey = 58;

            for (int i = 0; i < 10000; i++)
            {
                int key = initialey + i;
                string query = "INSERT INTO [Table] VALUES (" + key + ",";
                for (int j = 0; j < 10; j++)
                {
                    int critRes = UtilsLib.UtilsLib.RandomNumber(0, 1);
                    if (critRes == 1)
                    {
                        query += "'True',";
                    }
                    else
                    {
                        query += "'False',";
                    }
                }
                int critProfit = UtilsLib.UtilsLib.RandomNumber(0, 5);
                query += critProfit + ")";

                //execute query
                dbInterface.QueryExecute(query);
            }

            //close DB
            dbInterface.CloseConnection();
        }

        //-------------------------------------------------
    }
}
