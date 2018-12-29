using System;

namespace Interfaces
{   
    class InterfacesDemo
    {
        static void Main(string[] args)
        {
            WebPage webPage = new WebPage(2);
            
            // Note that we assign Animation and Document objects
            //  to an IEmbeddable.  It is allowed because Animation
            //  and Document objects implement the IEmbeddable interface.
            //
            webPage[0] = new Animation();
            webPage[1] = new Document();

            webPage.Page_Load();
            webPage.Page_Close();
        }
    }
}
