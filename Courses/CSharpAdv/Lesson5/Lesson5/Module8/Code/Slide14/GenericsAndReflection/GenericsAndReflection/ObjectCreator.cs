
namespace GenericsAndReflection
{
    /// <summary>
    /// A delegate which can reference the ObjectCreator.Create method.
    /// </summary>
    delegate T CreatorDelegate<T>();

    /// <summary>
    /// Creates object of a specific type.
    /// </summary>
    /// <typeparam name="T">The type of object to create.</typeparam>
    class ObjectCreator<T> where T : new()
    {
        public T Create()
        {
            return new T();
        }
    }
}
