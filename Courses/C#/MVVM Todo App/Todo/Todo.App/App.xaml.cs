using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Toolkit;
using Microsoft.Practices.Unity;
using Todo.App.Modules.Shell;
using Todo.App.Container;
using Todo.App.Model.Services;

namespace Todo.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static WindowRegionHandler MainRegion { get => NavigationManager[Regions.Window] as WindowRegionHandler; }

        public static NavigationManager NavigationManager { get => Container.Resolve<NavigationManager>(); }

        public static DataService DataService { get => Container.Resolve<DataService>(); }

        public static UnityContainer Container { get => Bootstrapper.Container; }

        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }

        protected async override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Bootstrapper.Start(this);
            Bootstrapper.Container.RegisterType<IViewResolver, ViewResolver>(new ContainerControlledLifetimeManager());
            Bootstrapper.Container.RegisterType<StorageService>(new ContainerControlledLifetimeManager());
            Bootstrapper.Container.RegisterType<DataService>(new ContainerControlledLifetimeManager());

            var data = DataService;
            await data.Initialize();

            var main = Bootstrapper.Container.Resolve<WindowRegionHandler>();
            await main.Start(Regions.Window);

            main.WindowStyle = Resources["MainWindowStyle"] as Style;

            await main.NavigateTo<ShellVm>(null);

        }
    }
}
