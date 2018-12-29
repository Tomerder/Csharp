using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneticAlgoEngineLib
{
    abstract public class GenAbstract<GEN_VALUE_TYPE, INPUT_TO_GRADE_GEN_BY_TYPE> : IComparable, ICloneable
    {
        //-------------------------------------------------

        private GEN_VALUE_TYPE m_genValue;
        private int m_grade;

        //-------------------------------------------------
      
        public GEN_VALUE_TYPE GenValue
        {
          get { return m_genValue; }
          set { m_genValue = value; }
        }

        public int Grade
        {
            get { return m_grade; }
            set { m_grade = value; }
        }

        //-------------------------------------------------
        //implement compare function for making the class sortable

        public int CompareTo(object _other)
        {
            GenAbstract<GEN_VALUE_TYPE, INPUT_TO_GRADE_GEN_BY_TYPE> objToCompare = _other as GenAbstract<GEN_VALUE_TYPE, INPUT_TO_GRADE_GEN_BY_TYPE>;

            return objToCompare.Grade.CompareTo(Grade);
        }

        //-------------------------------------------------

        virtual public object Clone()
        {
            return this.MemberwiseClone();
        }

        //-------------------------------------------------

        public override string ToString()
        {
            string toRet = "Grade = " + Grade + ", Gen =" + m_genValue.ToString();

            return toRet;
        }

        //-------------------------------------------------
      
        //To implement on derived class
        abstract public bool GenerateGenValue();
        abstract public bool DoMutate(GenAbstract<GEN_VALUE_TYPE, INPUT_TO_GRADE_GEN_BY_TYPE> _genToCopyAndMutate);
        abstract public bool DoPair(GenAbstract<GEN_VALUE_TYPE, INPUT_TO_GRADE_GEN_BY_TYPE> _genToPairWith1, GenAbstract<GEN_VALUE_TYPE, INPUT_TO_GRADE_GEN_BY_TYPE> _genToPairWith2);
        abstract public bool DoGradeGen(INPUT_TO_GRADE_GEN_BY_TYPE _inputToGradeBy);

        //-------------------------------------------------

    }
}
