using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticAlgoEngineLib;

namespace GeneticAlgoTool
{
    class GenEnigma : GenAbstract<char[], string> 
    {
       //-------------------------------------------------

        public GenEnigma()
        {
            char[] enigmaKey = new char[256];

            //set Data member 
            GenValue = enigmaKey;
        }

      

        //-------------------------------------------------
        //override abstract methods

        public override bool GenerateGenValue()
        {
            GenerateKey();

            return true;
        }

        override public bool DoMutate(GenAbstract<char[], string> _genToCopyAndMutate)
        {
            //_genMutated.GenValue = 

            return true;
        }

        override public bool DoPair(GenAbstract<char[], string> _genToPairWith1, GenAbstract<char[], string> _genToPairWith2)
        {
            //_genPaired.GenValue = 

            return true;
        }

        override public bool DoGradeGen(string _inputToGradeBy)
        {

            //Grade = 
           
            return true;
        }

        //-------------------------------------------------

        private void GenerateKey()
        {

        }

        //-------------------------------------------------
    }
}
