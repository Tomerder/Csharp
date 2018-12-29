using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolkit
{
    public class Region
    {
        public string Name { get; private set; }

        public Region(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return $"Region: {Name}";
        }
    }
}
