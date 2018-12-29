using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolkit;

namespace Todo.App.Container
{
    public static class Regions
    {
        public static Region Window { get; } = new Region(nameof(Window));

        public static Region Main { get; } = new Region(nameof(Main));

    }
}
