using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolkit
{
    // the base class for view models. While ObservableObject handles change notifications, 
    // ViewModel adds the notion of navigation.

    public class ActivateableViewModel : ObservableObject
    {

        private bool _IsActive;
        public bool IsActive { get => _IsActive; private set => SetProperty(ref _IsActive, value); }

        public event EventHandler Activated;

        public event EventHandler Deactivated;

        private void _notifyNavigatedTo()
        {
            Activated?.Invoke(this, EventArgs.Empty);
        }

        private void _notifyNavigatingFrom()
        {
            Deactivated?.Invoke(this, EventArgs.Empty);
        }

        public async Task Initialize(object param)
        {
            await OnInitialized(param);
        }

        public async Task Activate()
        {
            IsActive = true;
            _notifyNavigatedTo();
            await OnActivated();
        }

        public async Task Deactivate()
        {
            await OnDeactivated();
            _notifyNavigatingFrom();
            IsActive = false;
        }


        protected virtual Task OnInitialized(object param)
        {
            return Tasks.Null;
        }

        protected virtual Task OnActivated()
        {

            return Tasks.Null;
        }

        protected virtual Task OnDeactivated()
        {
            return Tasks.Null;
        }
    }
}
