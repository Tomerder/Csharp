using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Toolkit
{
    public class VmRegionHandler : IRegionHandler
    {
        public Region Region { get; private set; }

        public ActivateableViewModel Parent { get; private set; }

        public NavigationManager NavigationManager { get; private set; }

        public PropertyInfo Property { get; private set; }

        public async Task NavigateTo<T>(object param) where T : ActivateableViewModel
        {
            var newVm = Bootstrapper.Container.Resolve<T>();
            await newVm.Initialize(param);

            var oldVm = _getViewModel();

            await _ensureActivationState(oldVm, newVm);

            _setViewModel(newVm);
        }

        private ActivateableViewModel _getViewModel()
        {
            return Property.GetValue(Parent) as ActivateableViewModel;
        }

        private void _setViewModel(ActivateableViewModel value)
        {
            Property.SetValue(Parent, value);
        }

        public async Task Start(Region region, ActivateableViewModel parent, string propertyName)
        {
            Region = region;
            Parent = parent;

            Property = Parent.GetType().GetProperty(propertyName);

            Parent.Activated += (s, e) => _onParentActivated();
            Parent.Deactivated += (s, e) => _onParentDeactivated();
            Parent.Observe<ActivateableViewModel>(propertyName, this, _onPropertyChanged);

            if (Parent.IsActive) _onParentActivated();

            var vm = _getViewModel();

            if ((vm != null) && (!vm.IsActive))
                await vm.Activate();
        }

        private async Task _ensureActivationState(ActivateableViewModel oldVm, ActivateableViewModel newVm)
        {
            if ((oldVm != null) && (oldVm.IsActive))
            {
                await oldVm.Deactivate();
            }

            if ((newVm != null) && (!newVm.IsActive))
            {
                await newVm.Activate();
            }
        }

        private async void _onPropertyChanged(ActivateableViewModel oldVal, ActivateableViewModel newVal)
        {
            // this is only to support cases where the owner view model 
            // changes the property without a proper call to Navigate
            // but when you do call navigate, it will have already happened
            // by the time we get here.

            // Note that when performed like this, the calls to this method
            // is not "awaited" so exceptions that occur here will not be caught
            await _ensureActivationState(oldVal, newVal);
        }

        private async void _onParentDeactivated()
        {
            var vm = _getViewModel();
            if (vm != null) await vm.Deactivate();
            await NavigationManager.UnregisterRegionHandler(this);
        }

        private async void _onParentActivated()
        {
            await NavigationManager.RegisterRegionHandler(this);
            var vm = _getViewModel();
            if (vm != null) await vm.Activate();

        }

        [InjectionMethod]
        public Task Initialize(NavigationManager navigationManager)
        {
            NavigationManager = navigationManager;
            return Tasks.Null;
        }

    }
}
