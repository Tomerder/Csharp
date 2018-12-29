using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Dollar
{
    class Bank :IEnumerable
    {

        List<Dollar> _money = new List <Dollar>();
        public void add(Dollar d)
        {
            _money.Add(d);
        }

        
    
#region IEnumerable Members

public IEnumerator  GetEnumerator()
{
 	//throw new NotImplementedException();
    foreach (Dollar item in _money)
        yield return item;
}

#endregion
}
}
