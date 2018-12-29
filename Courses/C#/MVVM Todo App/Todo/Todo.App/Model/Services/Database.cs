using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Todo.App.Model.DataEntities;

namespace Todo.App.Model.Services
{
    [DataContract(IsReference = true)]
    public class Database
    {
        [DataMember]
        public Category[] Categories { get; set; } = new Category[0];


        [DataMember]
        public TodoItem[] Items { get; set; } = new TodoItem[0];

    }
}
