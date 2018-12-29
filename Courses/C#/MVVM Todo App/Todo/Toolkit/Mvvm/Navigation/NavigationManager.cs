using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolkit
{
    public class NavigationManager
    {
        private Dictionary<Region, IRegionHandler> _regions;

        public Task RegisterRegionHandler(IRegionHandler regionHandler)
        {
            _regions.Add(regionHandler.Region, regionHandler);
            return Tasks.Null;
        }

        public Task UnregisterRegionHandler(IRegionHandler regionHandler)
        {
            _regions.Remove(regionHandler.Region);
            return Tasks.Null;
        }

        public IRegionHandler this[Region reg]
        {
            get
            {
                return _regions[reg];
            }
        }

        public NavigationManager()
        {
            _regions = new Dictionary<Region, IRegionHandler>();
        }
    }
}
