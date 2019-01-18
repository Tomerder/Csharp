using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CsharpTest
{
    class MultiThreadStackActivator
    {
        //---------------------------------------------------------------------

        public void ActivateA()
        {
            MultiThreadStack<string> stack = new MultiThreadStack<string>();

            Action<MultiThreadStack<string>, int, string, int> pushThread = new Action<MultiThreadStack<string>, int, string, int>(PushThreadString);
            pushThread.BeginInvoke(stack, 10, "A", 1000, null, null);
            pushThread.BeginInvoke(stack, 10, "B", 500, null, null);

            Action<MultiThreadStack<string>, int> popThread = new Action<MultiThreadStack<string>, int>(PopThreadString);
            popThread.BeginInvoke(stack, 2000, null, null);

            Console.ReadLine();
        }

        void PushThreadString(MultiThreadStack<string> _stack, int _count, string _str, int _sleep)
        {
            for (int i = 0; i < _count; i++)
            {
                string toPush = _str + i;
                _stack.Push(toPush);
                Thread.Sleep(_sleep);
            }
        }

        void PopThreadString(MultiThreadStack<string> _stack, int _sleep)
        {
            while (true)
            {
                string str = _stack.Pop();
                Console.WriteLine(str);
                Thread.Sleep(_sleep);
            }
        }

        //---------------------------------------------------------------------

        public void ActivateB()
        {
            MultiThreadStack<int> stack = new MultiThreadStack<int>();

            //push
            Action<MultiThreadStack<int>, int, int, int> pushThread = new Action<MultiThreadStack<int>, int, int, int>(PushThreadInt);
            IAsyncResult push1res = pushThread.BeginInvoke(stack, 10000, 1, 0, null, null);
            IAsyncResult push2res = pushThread.BeginInvoke(stack, 20000, 2, 0, null, null);

            //wait for push threads to finish
            while(!push1res.IsCompleted || !push2res.IsCompleted)
            {
                Thread.Sleep(100);
            }

            //pop
            Func<MultiThreadStack<int>, int> popThread = new Func<MultiThreadStack<int>, int>(PopThreadInt);
            IAsyncResult popRes = popThread.BeginInvoke(stack, null, null);

            // Call EndInvoke to wait for the asynchronous call to complete,
            // and to retrieve the results.
            int retValue = popThread.EndInvoke(popRes);

            //print result from pop thread
            Console.WriteLine("Total = " + retValue);
        }

        void PushThreadInt(MultiThreadStack<int> _stack, int _count, int _valToPush, int _sleep)
        {
            for (int i = 0; i < _count; i++)
            {
                _stack.Push(_valToPush);
                if (_sleep > 0)
                {
                    Thread.Sleep(_sleep);
                }
            }
        }

        int PopThreadInt(MultiThreadStack<int> _stack)
        {
            int sum = 0;

            while (!_stack.IsEmpty())
            {
                int num = _stack.Pop();
                sum += num;
            }

            return sum;
        }

        //---------------------------------------------------------------------
    }
}
