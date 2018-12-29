using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Toolkit
{
    public static class Bootstrapper
    {
        public static UnityContainer Container { get; private set; }

        public static void Start<T>(T application)
            where T : Application
        {
            Container = new UnityContainer();
            Container.RegisterInstance(Container);
            Container.RegisterType<NavigationManager>(new ContainerControlledLifetimeManager());
            Container.RegisterType<WindowRegionHandler>();
            Container.RegisterType<VmRegionHandler>();
        }
    }
}
