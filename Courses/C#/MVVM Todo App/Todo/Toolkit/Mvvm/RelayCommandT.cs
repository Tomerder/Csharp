using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Toolkit
{
    public class RelayCommand<T> : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public static bool _isValueType;

        Func<T, bool> _canExecute = null;
        Action<T> _executeAction = null;

        public RelayCommand(Action<T> executeAction, Func<T, bool> canExecute = null)
        {
            _canExecute = canExecute;
            _executeAction = executeAction;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null) return true;

            if ((parameter == null) && (_isValueType))
            {
                return _canExecute(default(T));
            }

            if (_canExecute != null)
                return _canExecute((T)parameter);

            return true;
        }

        public void UpdateCanExecuteState()
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, new EventArgs());
        }

        public void Execute(object parameter)
        {
            var val = parameter;

            if (CanExecute(val))
            {
                if ((val == null) && (_isValueType))
                {
                    _executeAction(default(T));
                }
                else
                {
                    _executeAction((T)val);
                }
            }

            UpdateCanExecuteState();
        }

        static RelayCommand()
        {
            _isValueType = typeof(T).IsValueType;
        }


        public void RaiseCanExecuteChanged()
        {
            UpdateCanExecuteState();
        }
    }
}
