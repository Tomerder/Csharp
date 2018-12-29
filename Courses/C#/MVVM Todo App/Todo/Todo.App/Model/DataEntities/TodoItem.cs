using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Todo.App.Model.DataEntities
{
    [DataContract(IsReference = true)]
    public class TodoItem : IEntity
    {
        [DataMember]
        public int Uid { get; set; }

        [DataMember]
        public string DisplayName { get; set; }

        [DataMember]
        public string Notes { get; set; }

        [DataMember]
        public Priority Priority { get; set; }

        [DataMember]
        public bool IsDone { get; set; }

        [DataMember]
        public int CategoryUid { get; set; }
    }
}
