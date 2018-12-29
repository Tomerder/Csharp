using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.App.Model.DataEntities
{
    public interface IEntity
    {
        int Uid { get; set; }
    }
}
