using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Todo.App.Model.DataEntities
{
    [DataContract(IsReference = true)]
    public class Category : IEntity
    {
        [DataMember]
        public int Uid { get; set; }

        [DataMember]
        public string DisplayName { get; set; }

        [DataMember]
        public Color Color { get; set; } = Colors.Black;
    }
}
