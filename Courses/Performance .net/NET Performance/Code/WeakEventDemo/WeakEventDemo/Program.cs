using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace WeakEventDemo
{
    class MyWeakButton
    {
        class WeakDelegate
        {
            public WeakReference Target { get; set; }
            public MethodInfo Method { get; set; }
        }

        private List<WeakDelegate> _weakClickSubscribers = new List<WeakDelegate>();

        public event Action WeakClick
        {
            add
            {
                WeakDelegate wd = new WeakDelegate
                {
                    Target = new WeakReference(value.Target),
                    Method = value.Method
                };
                _weakClickSubscribers.Add(wd);
            }
            remove
            {
                //TODO
            }
        }

        public void RaiseWeakClickEvent()
        {
            foreach (WeakDelegate wd in _weakClickSubscribers)
            {
                object realTarget = wd.Target.Target;
                if (realTarget != null)
                {
                    wd.Method.Invoke(realTarget, new object[] { });
                }
                else
                {
                    //The target is dead. Remove from invocation list.
                    //TODO
                }
            }
        }
    }

    class Subscriber
    {
        public void OnClick() { Console.WriteLine("CLICK!!!"); }
    }

    class Program
    {
        static void Main(string[] args)
        {
            MyWeakButton mwb = new MyWeakButton();
            Subscriber s = new Subscriber();
            mwb.WeakClick += s.OnClick;
            GC.Collect();
            mwb.RaiseWeakClickEvent();
            s = null;
            GC.Collect();
            mwb.RaiseWeakClickEvent();
        }
    }
}
