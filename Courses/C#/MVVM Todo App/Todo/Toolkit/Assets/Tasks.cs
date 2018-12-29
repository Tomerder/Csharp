using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolkit
{
    public static class Tasks
    {
        public static Task Null
        {
            get
            {
                return Task.FromResult(false);
            }
        }
    }
}
