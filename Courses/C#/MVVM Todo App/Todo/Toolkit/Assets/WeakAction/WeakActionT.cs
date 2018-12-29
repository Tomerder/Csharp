using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolkit
{
    public class WeakAction<T> : WeakAction, IWeakActionWithParam1
    {
        public WeakAction(object target, Action<T> action)
            :base(target, action.Target, action.Method)
        {
        }

        public WeakAction(Action<T> action)
            :base(null, action.Target, action.Method)
        {

        }

        public new void Execute()
        {
            Execute(default(T));   
        }

        public void Execute(T param)
        {
            if (IsAlive)
            {
                _methodInfo.Invoke(Target, new object[] 
                {
                    param
                });
            }
            else
            {
                MarkForDeletion();
            }
        }

        public void ExecuteWithObject(object parameter)
        {
            var param = (T)parameter;
            Execute(param);
        }
    }
}
