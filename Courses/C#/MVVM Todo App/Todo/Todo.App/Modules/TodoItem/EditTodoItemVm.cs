using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Todo.App.Model.DataEntities;
using Toolkit;

namespace Todo.App.Modules.TodoItem
{
    public class EditTodoItemVm : ActivateableViewModel
    {

        #region Properties

        public int PageIndex { get; } = 3;

        private int _Uid;
        public int Uid { get => _Uid; set => SetProperty(ref _Uid, value); }


        private string _DisplayName;
        public string DisplayName { get => _DisplayName; set => SetProperty(ref _DisplayName, value); }


        private string _Notes;
        public string Notes { get => _Notes; set => SetProperty(ref _Notes, value); }


        private Priority _Priority;
        public Priority Priority { get => _Priority; set => SetProperty(ref _Priority, value); }

        #endregion


        protected override async Task OnInitialized(object param)
        {
            await base.OnInitialized(param);

            var uid = (int)param;
            Uid = uid;
        }

    }
}
