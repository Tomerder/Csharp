namespace Interfaces
{
    // The Animation class does not derive from the IEmbeddable class,
    //  because IEmbeddable is not a class.  Instead, we say that the
    //  Animation class implements the IEmbeddable interface.  Note the
    //  Visual Studio 2005 feature that allows us to easily implement
    //  an interface.
    //
    class Animation : IEmbeddable
    {
        #region IEmbeddable Members

        public void Load()
        {
        }

        public void Play()
        {
        }

        public void Save()
        {
        }

        #endregion
    }

    // The Document class implements the IEmbeddable interface as well.
    //  Try to comment one of the methods and see the compiler complain
    //  that the Document class doesn't implement all the methods of the
    //  IEmbeddable interface.
    //
    class Document : IEmbeddable
    {
        #region IEmbeddable Members

        public void Load()
        {
        }

        public void Play()
        {
        }

        public void Save()
        {
        }

        #endregion
    }
}
