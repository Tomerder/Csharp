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
using Toolkit;


namespace Todo.App.Modules.Category
{
    public class EditCategoryVm : ActivateableViewModel
    {
        private NavigationManager _navigationManager;
        private DataService _dataService;
        private UnityContainer _container;

        #region Properties

        public int PageIndex { get; } = 2;

        private int _Uid;
        public int Uid { get => _Uid; set => SetProperty(ref _Uid, value); }


        private string _DisplayName;
        public string DisplayName { get => _DisplayName; set => SetProperty(ref _DisplayName, value); }


        private Color _Color;
        public Color Color { get => _Color; set => SetProperty(ref _Color, value); }



        private ObservableCollection<ItemVm> _Items;
        public ObservableCollection<ItemVm> Items { get => _Items; set => SetProperty(ref _Items, value); }


        public List<Color> PossibleColors { get; } = new List<Color>
        {
            Colors.Red,
            Colors.Blue,
            Colors.Green,
            Colors.Black,
            Colors.Purple,
            Colors.Orange,
            Colors.Cyan
        };

        #endregion

        #region Commands

        public RelayCommand BackCommand { get; }

        public RelayCommand<Color> SelectColorCommand { get; }

        public RelayCommand AddItemCommand { get; }

        #endregion

        public EditCategoryVm()
        {
            Items = new ObservableCollection<ItemVm>();

            BackCommand = new RelayCommand(async () =>
            {
                await _navigationManager[Regions.Main].NavigateTo<Main.MainVm>(null);
            });

            SelectColorCommand = new RelayCommand<Color>(c =>
            {
                Color = c;
                foreach (var item in Items)
                {
                    item.Color = Color;
                }
            });

            AddItemCommand = new RelayCommand(async () =>
            {
                var i = new Model.DataEntities.TodoItem
                {
                    DisplayName = "Todo",
                    CategoryUid = Uid,
                    Uid = -1,
                    IsDone = false,
                    Notes = "",
                    Priority = Model.DataEntities.Priority.Normal
                };

                await _dataService.SaveItem(i);
                var vm = await App.Resolve<ItemVm>().ReadData(i, this);
                Items.Add(vm);
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

        protected override async Task OnInitialized(object param)
        {
            await base.OnInitialized(param);
            Uid = (int)param;
            await Refresh();
        }

        public async Task Refresh()
        {
            var cat = await _dataService.GetCategory(Uid);

            Uid = cat.Uid;
            DisplayName = cat.DisplayName;
            Color = cat.Color;

            var items = await _dataService.GetItems(Uid);

            Items.Clear();
            foreach (var item in items)
            {
                var vm = await App.Resolve<ItemVm>().ReadData(item, this);
                Items.Add(vm);
            }
        }

        protected override async Task OnDeactivated()
        {
            await base.OnDeactivated();
            await _save();            
        }

        private async Task _save()
        {
            var c = new Model.DataEntities.Category
            {
                DisplayName = DisplayName, 
                Uid = Uid, 
                Color = Color
            };

            await _dataService.SaveCategory(c);

            foreach (var item in Items)
            {
                await item.Save();
            }
        }

    }
}
