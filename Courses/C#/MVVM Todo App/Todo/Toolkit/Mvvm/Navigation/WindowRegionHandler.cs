using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Practices.Unity;
using System.ComponentModel;
using System.Windows.Controls;

namespace Toolkit
{
    public class WindowRegionHandler : IRegionHandler
    {
        public Region Region { get; private set; }

        public NavigationManager NavigationManager { get; private set; }

        public IViewResolver ViewResolver { get; private set; }

        public ActivateableViewModel ViewModel { get; private set; }

        public UserControl View { get; private set; }

        public Window Window { get; private set; }

        public Style WindowStyle { get; set; }



        public async Task NavigateTo<T>(object param) where T : ActivateableViewModel
        {
            if (Window == null)
            {
                Window = new Window();
                Window.Closing += Window_Closing;
            }

            if ((ViewModel != null) && (ViewModel.IsActive))
            {
                await ViewModel.Deactivate();
            }

            var vm = Bootstrapper.Container.Resolve<T>();
            await vm.Initialize(param);
            await vm.Activate();

            var viewType = ViewResolver.GetViewType(typeof(T));
            View = Bootstrapper.Container.Resolve(viewType) as UserControl;
            ViewModel = vm;

            Window.Style = WindowStyle;
            Window.Content = View;
            View.DataContext = ViewModel;
            Window.Show();
        }

        private async void Window_Closing(object sender, CancelEventArgs e)
        {
            if (View != null)
            {
                View.DataContext = null;
                Window.Content = null;
            }

            if ((ViewModel != null) && (ViewModel.IsActive))
            {
                await ViewModel.Deactivate();
                ViewModel = null;
            }

            Window = null;
        }

        [InjectionMethod]
        public Task Initialize(NavigationManager navigationManager, IViewResolver viewResolver)
        {
            NavigationManager = navigationManager;
            ViewResolver = viewResolver;
            return Tasks.Null;
        }

        public Task Start(Region region)
        {
            Region = region;
            NavigationManager.RegisterRegionHandler(this);
            return Tasks.Null;
        }

        public Task Stop(Region region)
        {
            NavigationManager.UnregisterRegionHandler(this);
            return Tasks.Null;

        }
    }
}
