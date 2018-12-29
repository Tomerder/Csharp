using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolkit
{
    public interface IRegionHandler
    {
        Region Region { get; }

        Task NavigateTo<T>(object param) where T : ActivateableViewModel;

    }
}
