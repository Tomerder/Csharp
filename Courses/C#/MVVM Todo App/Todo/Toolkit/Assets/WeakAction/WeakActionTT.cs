using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolkit
{
    public class WeakAction<T1, T2> : WeakAction, IWeakActionWithParam2
    {
        public WeakAction(object target, Action<T1, T2> action)
            :base(target, action.Target, action.Method)
        {
        }

        public WeakAction(Action<T1, T2> action)
            :base(null, action.Target, action.Method)
        {

        }

        public void Execute(T1 param1, T2 param2)
        {
            if (IsAlive)
            {
                _methodInfo.Invoke(Target, new object[] 
                {
                    param1, 
                    param2
                });
            }
            else
            {
                MarkForDeletion();
            }
        }

        public void ExecuteWithObject(object parameter)
        {
            var param = (T1)parameter;
            Execute(param, default(T2));
        }

        public void ExecuteWithObject(object param1, object param2)
        {
            var p1 = (T1)param1;
            var p2 = (T2)param2;
            Execute(p1, p2);
        }
    }
}
