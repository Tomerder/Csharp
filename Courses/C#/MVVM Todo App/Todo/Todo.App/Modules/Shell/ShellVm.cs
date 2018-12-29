using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.App.Container;
using Todo.App.Modules.Main;
using Toolkit;

namespace Todo.App.Modules.Shell
{
    public class ShellVm : ActivateableViewModel
    {
        private ActivateableViewModel _Current;
        public ActivateableViewModel Current { get => _Current; set => SetProperty(ref _Current, value); }

        protected override async Task OnInitialized(object param)
        {
            await base.OnInitialized(param);
            var rh = App.Resolve<VmRegionHandler>();
            await rh.Start(Regions.Main, this, nameof(Current));
        }

        protected override async Task OnActivated()
        {
            await base.OnActivated();
            await App.NavigationManager[Regions.Main].NavigateTo<MainVm>(null);
        }

    }
}
