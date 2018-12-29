using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UtilsLib;

namespace GeneticAlgoEngineLib
{
    //-------------------------------------------------
    //Defenitions 

    //callback func to show best gen on calling process : show to consule/to gui/write to file....
    public delegate void CallBackFuncToShowBestGen(string _str);

    public struct GeneticAlgoParams
    {
        public int populationSize;
        public int precentSurvive;
        public int precentMutate;
        public int precentMutateFromBest;
        public int precentParing;
        public int precentParingFromBest;
        public int numOfIterations;
        //callback func to show best gen on calling process : show to consule/to gui/write to file....
        public CallBackFuncToShowBestGen callBackFuncToShowBestGen;
    };
   
    //-------------------------------------------------

    public class GeneticAlgoEngine<GEN_DERIVED_TYPE, GEN_VALUE_TYPE, INPUT_TO_GRADE_GEN_BY> where GEN_DERIVED_TYPE : GenAbstract<GEN_VALUE_TYPE, INPUT_TO_GRADE_GEN_BY>, new()
    {
        //-------------------------------------------------
        //Definitions

        public delegate bool StopConditionFunc(GEN_DERIVED_TYPE _bestGen);

        //-------------------------------------------------
        //Data members

        List<GEN_DERIVED_TYPE> m_population;

        GeneticAlgoParams m_geneticAlgoParams;
        public INPUT_TO_GRADE_GEN_BY m_inputToGradeGenBy;
        public StopConditionFunc m_stopConditionFunc;
     
        //-------------------------------------------------

        public GeneticAlgoEngine(GeneticAlgoParams _genAlgoParams, INPUT_TO_GRADE_GEN_BY _inputToGradeGenBy, StopConditionFunc _stopConditionFunc)
        {           
            m_geneticAlgoParams = _genAlgoParams;
            m_inputToGradeGenBy = _inputToGradeGenBy;
            m_stopConditionFunc = _stopConditionFunc;

            //create population
            m_population = new List<GEN_DERIVED_TYPE>();
            for (int i = 0; i < m_geneticAlgoParams.populationSize; i++)
            {
                GEN_DERIVED_TYPE gen = new GEN_DERIVED_TYPE();
                gen.GenerateGenValue();
                m_population.Add(gen);
            }
        }

        //-------------------------------------------------

        public void ExecuteEvolution()
        {
            for (int i = 0; i < m_geneticAlgoParams.numOfIterations; i++)
            {
                DoIteration();

                //check stop condition
                if (m_stopConditionFunc(m_population[0]))
                {
                    break;
                }
            }
        }

        //-------------------------------------------------

        void DoIteration()
        {       
            GradePopulation();

            SortPopulationByGrade();

            ShowBestGen();

            EvolveToNextGeneration();
        }   
 
        //-------------------------------------------------

        private void GradePopulation()
        {
            foreach (GEN_DERIVED_TYPE gen in m_population)
            {
                gen.DoGradeGen(m_inputToGradeGenBy);
            }
        }

        //-------------------------------------------------

        private void SortPopulationByGrade()
        {
            m_population.Sort();
        }

        //-------------------------------------------------

        private void EvolveToNextGeneration()
        {
            //last generation 
            List<GEN_DERIVED_TYPE> lastGeneration = m_population;

            //next generation list 
            m_population = new List<GEN_DERIVED_TYPE>();
            int numOfGensInNewGeneration = 0;

            //survivers
            int numOfSurvivers = m_geneticAlgoParams.precentSurvive * m_geneticAlgoParams.populationSize / 100;
            for (int i = 0; i < numOfSurvivers; i++)
            {
                //copy ctor
                GEN_DERIVED_TYPE newGen = lastGeneration[i];
                m_population.Add(newGen);
                
                numOfGensInNewGeneration++;
            }

            //mutations 
            int numOfBestToMutateFrom = m_geneticAlgoParams.precentMutateFromBest * m_geneticAlgoParams.populationSize / 100;
            int numOfMutations = m_geneticAlgoParams.precentMutate * m_geneticAlgoParams.populationSize / 100;

            for (int i = 0; i < numOfMutations; i++)
            {
                int randomGenToMutate = UtilsLib.UtilsLib.RandomNumber(0, numOfBestToMutateFrom - 1);
                //mutate gen from last generation                
                GEN_DERIVED_TYPE genToMutate = lastGeneration[randomGenToMutate];
                //create gen for new population
                GEN_DERIVED_TYPE newGen = new GEN_DERIVED_TYPE();                
                //mutate gen
                newGen.DoMutate(genToMutate);
                //set mutated gen to next generation               
                m_population.Add(newGen);
                numOfGensInNewGeneration++;
            }

            //pairing
            int numOfBestToPairFrom = m_geneticAlgoParams.precentParingFromBest * m_geneticAlgoParams.populationSize / 100;
            int numOfParing = m_geneticAlgoParams.precentParing * m_geneticAlgoParams.populationSize / 100;

            for (int i = 0; i < numOfParing; i++)
            {
                int randomGenToPair1 = UtilsLib.UtilsLib.RandomNumber(0, numOfBestToPairFrom - 1);
                int randomGenToPair2 = UtilsLib.UtilsLib.RandomNumber(0, numOfBestToPairFrom - 1);
                //Pair gens from last generation
                GEN_DERIVED_TYPE genToPair1 = lastGeneration[randomGenToPair1];
                GEN_DERIVED_TYPE genToPair2 = lastGeneration[randomGenToPair2];
                //new gen for next generation
                GEN_DERIVED_TYPE newGen = new GEN_DERIVED_TYPE();
                newGen.DoPair(genToPair1, genToPair2);
                //set paired gen to next generation
                m_population.Add(newGen);
                numOfGensInNewGeneration++;
            }

            //New gens for filling the rest of the population
            while (numOfGensInNewGeneration < m_geneticAlgoParams.populationSize)
            {
                GEN_DERIVED_TYPE newGen = new GEN_DERIVED_TYPE();
                newGen.GenerateGenValue();
                m_population.Add(newGen);
                numOfGensInNewGeneration++;
            }
        }

        //-------------------------------------------------

        private void ShowBestGen()
        {
            string bestGen = m_population[0].ToString();

            try
            {
                //execute callback function from caller (to show best gen)
                m_geneticAlgoParams.callBackFuncToShowBestGen(bestGen);
            }
            catch (Exception e)
            {
                //callback failed
            }
        }
        
        //-------------------------------------------------
    }
}
