using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticAlgoEngineLib;

namespace GeneticAlgoTool
{
    public class GenExample : GenAbstract<int, int> 
    {
        //-------------------------------------------------

        public GenExample()
        {
            //nothing to do
        }

        //-------------------------------------------------
        //override abstract methods

        public override bool GenerateGenValue()
        {
            //set Data member 
            GenValue = UtilsLib.UtilsLib.RandomNumber(Int32.MinValue, Int32.MaxValue);

            return true;
        }
         
        override public bool DoMutate(GenAbstract<int, int> _genToMutate)
        {
            GenValue = UtilsLib.UtilsLib.RandomNumber(_genToMutate.GenValue - 100, _genToMutate.GenValue + 100);

            return true;
        }

        override public bool DoPair(GenAbstract<int, int> _genToPairWith1, GenAbstract<int, int> _genToPairWith2)
        {
            GenValue = (_genToPairWith1.GenValue + _genToPairWith2.GenValue) / 2;

            return true;
        }

        override public bool DoGradeGen(int _inputToGradeBy)
        {
            try
            {
                Grade = -1 * Math.Abs(_inputToGradeBy - GenValue);
            }
            catch
            {
                Grade = Int32.MinValue;
            }

            return true;
        }

        //-------------------------------------------------

    }
}
