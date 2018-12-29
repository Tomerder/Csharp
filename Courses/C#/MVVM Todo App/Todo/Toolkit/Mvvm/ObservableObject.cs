using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Toolkit
{
    public class ObservableObject : INotifyPropertyChanged
    {
        private EditableLookup<string, WeakAction> _listeners;

        private EditableLookup<string, WeakAction> _ensureListeners()
        {
            if (_listeners == null)
            {
                _listeners = new EditableLookup<string, WeakAction>();
            }

            return _listeners;
        }

        private void _callListener(WeakAction listener, object oldValue, object newValue)
        {
            if (listener is IWeakActionWithParam2)
            {
                (listener as IWeakActionWithParam2).ExecuteWithObject(oldValue, newValue);
            }
            else if (listener is IWeakActionWithParam2)
            {
                (listener as IWeakActionWithParam2).ExecuteWithObject(newValue);
            }
            else
            {
                listener.Execute();
            }
        }

        private void _callListeners(string propertyName, object oldValue, object newValue)
        {
            // faster to avoid lock if there are no listeners for this property
            if ((_listeners == null) || (!_listeners.Contains(propertyName))) return;
            List<WeakAction> actions = null;

            if ((_listeners == null) || (!_listeners.Contains(propertyName))) return;
            _listeners.RemoveWhere(propertyName, wa => !wa.IsAlive);
            actions = _listeners[propertyName].ToList();

            foreach (var action in actions)
            {
                _callListener(action, oldValue, newValue);
            }
        }

        public void Observe(string propertyName, object observer, Action a)
        {
            _ensureListeners();
            _listeners.Add(propertyName, new WeakAction(observer, a));
        }

        public void Observe<T>(string propertyName, object observer, Action<T> a)
        {
            _ensureListeners();
            _listeners.Add(propertyName, new WeakAction<T>(observer, a));
        }

        public void Observe<T>(string propertyName, object observer, Action<T, T> a)
        {
            _ensureListeners();
            _listeners.Add(propertyName, new WeakAction<T, T>(observer, a));
        }

        public void Unobserve(string propertyName, object observer, Action a)
        {
            _listeners.RemoveWhere(propertyName, (wa => ((wa.Target == observer) && (wa.Method == a.Method))));
        }

        public void Unobserve<T>(string propertyName, object observer, Action<T> a)
        {
            _listeners.RemoveWhere(propertyName, (wa => ((wa.Target == observer) && (wa.Method == a.Method))));
        }

        public void Unobserve<T>(string propertyName, object observer, Action<T, T> a)
        {
            _listeners.RemoveWhere(propertyName, (wa => ((wa.Target == observer) && (wa.Method == a.Method))));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T storage, T value, 
                                    Action onChanged = null, 
                                    [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value)) return false;
            var oldVal = storage;
            storage = value;
            onChanged?.Invoke();
            NotifyPropertyChanged(propertyName);

            _callListeners(propertyName, oldVal, value);
            return true;

        }

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
