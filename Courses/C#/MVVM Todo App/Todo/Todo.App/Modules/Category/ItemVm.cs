using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DE = Todo.App.Model.DataEntities;
using Toolkit;
using System.Windows.Media;
using Microsoft.Practices.Unity;
using Todo.App.Model.Services;

namespace Todo.App.Modules.Category
{
    public class ItemVm : ObservableObject
    {
        EditCategoryVm _parent;
        DE.TodoItem _model;
        DataService _dataService;

        #region Properties

        private int _Uid;
        public int Uid { get => _Uid; set => SetProperty(ref _Uid, value); }


        private int _CategoryUid;
        public int CategoryUid { get => _CategoryUid; set => SetProperty(ref _CategoryUid, value); }


        private string _DisplayName;
        public string DisplayName { get => _DisplayName; set => SetProperty(ref _DisplayName, value); }


        private DE.Priority _Priority;
        public DE.Priority Priority { get => _Priority; set => SetProperty(ref _Priority, value); }


        private bool _IsDone;
        public bool IsDone { get => _IsDone; set => SetProperty(ref _IsDone, value); }

        private Color _Color;

        public Color Color { get => _Color; set => SetProperty(ref _Color, value); }


        #endregion

        #region Commands

        public RelayCommand DeleteCommand { get; }

        public RelayCommand TogglePriority { get; }

        #endregion

        public ItemVm()
        {
            DeleteCommand = new RelayCommand(async () =>
            {
                await _dataService.DeleteItem(Uid);
                await _parent.Refresh();
            });

            TogglePriority = new RelayCommand(() =>
            {
                int i = ((int)Priority + 1) % 4;
                Priority = (DE.Priority)i;
            });
        }

        [InjectionMethod]
        public Task Inject(DataService dataService)
        {
            _dataService = dataService;
            return Tasks.Null;
        }

        public Task<ItemVm> ReadData(DE.TodoItem i, EditCategoryVm parent)
        {
            _model = i;

            Uid = i.Uid;
            CategoryUid = i.CategoryUid;
            DisplayName = i.DisplayName;
            Priority = i.Priority;
            IsDone = i.IsDone;
            Color = parent.Color;

            _parent = parent;
            return Task.FromResult(this);
        }

        public async Task Save()
        {
            _model.DisplayName = DisplayName;
            _model.IsDone = IsDone;
            _model.Priority = Priority;
            await _dataService.SaveItem(_model);
            
        }
    }
}
