using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Todo.App.Container;
using Todo.App.Model.Services;
using Todo.App.Modules.TodoItem;
using Toolkit;


namespace Todo.App.Modules.Main
{
    public class MainVm : ActivateableViewModel
    {
        private DataService _dataService;
        private NavigationManager _navigationManager;
        private UnityContainer _container;

        #region Properties

        public int PageIndex { get; } = 1;

        private ObservableCollection<CategoryVm> _Categories;
        public ObservableCollection<CategoryVm> Categories { get => _Categories; set => SetProperty(ref _Categories, value); }

        #endregion

        #region Commands

        public RelayCommand AddCategoryCommand { get; }

        #endregion

        public MainVm()
        {
            Categories = new ObservableCollection<CategoryVm>();
            AddCategoryCommand = new RelayCommand(async () =>
            {
                var c = new Model.DataEntities.Category
                {
                    DisplayName = "New Category",
                    Color = Colors.Red,
                    Uid = -1
                };

                await _dataService.SaveCategory(c);
                await _navigationManager[Regions.Main].NavigateTo<Modules.Category.EditCategoryVm>(c.Uid);
            });
        }

        [InjectionMethod]
        public Task Inject(NavigationManager navigationManager, DataService dataService, UnityContainer container)
        {
            _navigationManager = navigationManager;
            _dataService = dataService;
            _container = container;
            return Tasks.Null;
        }

        protected override async Task OnActivated()
        {
            await Refresh();
        }


        public async Task Refresh()
        {
            var categories = await _dataService.GetCategories();

            Categories.Clear();

            foreach (var item in categories)
            {
                var vm = await _container.Resolve<CategoryVm>().ReadData(item, this);
                Categories.Add(vm);
            }
        }
    }
}
