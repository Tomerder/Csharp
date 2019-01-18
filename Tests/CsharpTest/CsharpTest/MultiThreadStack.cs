using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpTest
{
    class MultiThreadStack<T>
    {
        //---------------------------------------------------

        Stack<T> m_stack;
         
        //---------------------------------------------------

        public MultiThreadStack()
        {
            m_stack = new Stack<T>();
        }

        //---------------------------------------------------

        public void Push(T _toPush)
        {
            lock (m_stack)
            {
                m_stack.Push(_toPush);
            }
        }

        //---------------------------------------------------

        public T Pop()
        {
            T toRet = default(T);
          
            lock (m_stack)
            {
                if (m_stack.Count != 0)
                {
                    toRet = m_stack.Pop();
                }
            }

            return toRet;
        }

        //---------------------------------------------------

        public bool IsEmpty()
        {
            return (m_stack.Count == 0);
        }

        //---------------------------------------------------
    }
}
