using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Toolkit
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public static bool _isValueType;

        Func<bool> _canExecute = null;
        Action _executeAction = null;

        public RelayCommand(Action executeAction, Func<bool> canExecute = null)
        {
            _canExecute = canExecute;
            _executeAction = executeAction;
        }

        public bool CanExecute(object parameter)
        {
            return (_canExecute == null) || (_canExecute());
        }

        public void UpdateCanExecuteState()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }

        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                _executeAction();
            }
        }

        public void RaiseCanExecuteChanged()
        {
            UpdateCanExecuteState();
        }
    }
}
