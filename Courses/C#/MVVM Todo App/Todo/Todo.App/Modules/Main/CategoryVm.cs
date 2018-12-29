using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Todo.App.Container;
using Todo.App.Model.Services;
using Toolkit;
using DE = Todo.App.Model.DataEntities;

namespace Todo.App.Modules.Main
{
    public class CategoryVm : ObservableObject
    {
        private DataService _dataService;
        private NavigationManager _navigationManager;
        private MainVm _parent;


        #region Properties

        private int _Uid;
        public int Uid { get => _Uid; set => SetProperty(ref _Uid, value); }


        private string _DisplayName;
        public string DisplayName { get => _DisplayName; set => SetProperty(ref _DisplayName, value); }


        private Color _Color;
        public Color Color { get => _Color; set => SetProperty(ref _Color, value); }


        private int _ItemsCount;

        public int ItemsCount { get => _ItemsCount; set => SetProperty(ref _ItemsCount, value); }

        #endregion

        #region Commands

        public RelayCommand NavigateToCategoryCommand { get; }

        public RelayCommand DeleteCategoryCommand { get; }

        #endregion

        public CategoryVm()
        {
            NavigateToCategoryCommand = new RelayCommand(async () =>
            {
                await _navigationManager?[Regions.Main].NavigateTo<Modules.Category.EditCategoryVm>(Uid);
            });

            DeleteCategoryCommand = new RelayCommand(async () =>
            {
                await _dataService?.DeleteCategory(Uid);
                await _parent?.Refresh();
            });

        }

        [InjectionMethod]
        public Task Inject(DataService dataService, NavigationManager navigationManager)
        {
            _dataService = dataService;
            _navigationManager = navigationManager;
            return Tasks.Null;
        }

        public async Task<CategoryVm> ReadData(DE.Category c, MainVm parent)
        {
            Uid = c.Uid;
            DisplayName = c.DisplayName;
            Color = c.Color;
            ItemsCount = await _dataService?.GetItemsCount(c.Uid);
            _parent = parent;

            return this;
        }

    }
}
