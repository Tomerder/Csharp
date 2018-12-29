namespace Interfaces
{
    // This is the definition of the IEmbeddable interface.  Similarly
    //  to a class, a struct or an enum, it is internal if not specified
    //  otherwise.  This interface contains three methods, which are all
    //  public.  It is not allowed to specify any other accessibility
    //  to interface members.
    //
    interface IEmbeddable
    {
        void Load();
        void Play();
        void Save();
    }
}
