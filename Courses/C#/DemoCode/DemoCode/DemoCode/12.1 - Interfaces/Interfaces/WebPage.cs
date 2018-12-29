using System;

namespace Interfaces
{
    // The WebPage class holds an array of IEmbeddable items.
    //  It couldn't care less what actual type they are, as long
    //  as they adhere to the contract defined by the IEmbeddable
    //  interface.
    //
    class WebPage
    {
        private IEmbeddable[] _items;

        public WebPage(int numberOfItems)
        {
            // We are not creating instances of the IEmbeddable
            //  type (it's impossible).  We're merely creating
            //  references that will be populated by our Main
            //  method.
            //
            _items = new IEmbeddable[numberOfItems];
        }

        public IEmbeddable this[int index]
        {
            get { return _items[index]; }
            set { _items[index] = value; }
        }

        public void Page_Load()
        {
            // Now, we can iterate the array of IEmbeddable objects
            //  and invoke their methods.  Again, we couldn't care
            //  less what the implementation is, as long as it adheres
            //  to the interface contract.
            //
            foreach (IEmbeddable item in _items)
            {
                item.Load();
                item.Play();
            }
        }

        public void Page_Close()
        {
            foreach (IEmbeddable item in _items)
            {
                item.Save();
            }
        }
    }
}
