using System.Diagnostics.Contracts;

namespace CodeContracts
{
    /// <summary>
    /// Represents a stack of limited capacity with LIFO semantics.
    /// </summary>
    /// <typeparam name="T">The type of elements in the stack, must be 
    /// a reference type.</typeparam>
    public sealed class ArrayStack<T>
        where T : class
    {
        private readonly T[] _items;
        private int _top;

        /// <summary>
        /// Initializes a new instance of the stack with the specified
        /// capacity. Attempts to add more elements to the stack than the
        /// capacity specified will result in undefined behavior.
        /// </summary>
        /// <param name="capacity">The capacity of the stack. Must be 
        /// non-negative.</param>
        public ArrayStack(int capacity)
        {
            Contract.Requires(capacity >= 0);
            Contract.Ensures(_items != null);
            Contract.Ensures(_items.Length == capacity);
            _items = new T[capacity];
            _top = -1;
        }

        /// <summary>
        /// Returns <b>true</b> if the stack is full.
        /// </summary>
        public bool IsFull
        {
            get { return _items.Length == _top + 1; }
        }

        /// <summary>
        /// Returns <b>true</b> if the stack is empty.
        /// </summary>
        public bool IsEmpty
        {
            get { return _top == -1; }
        }

        /// <summary>
        /// Pushes a new element onto the stack. The stack must not be full.
        /// </summary>
        /// <param name="item">The element to push onto the stack.</param>
        public void Push(T item)
        {
            Contract.Requires(!IsFull);
            Contract.Requires(item != null);
            Contract.Ensures(Contract.OldValue(_top) + 1 == _top);
            _items[++_top] = item;
        }

        /// <summary>
        /// Pops an element off the top of the stack and returns it. The stack
        /// must not be empty.
        /// </summary>
        /// <returns>The element formerly at the top of the stack.</returns>
        public T Pop()
        {
            Contract.Requires(!IsEmpty);
            Contract.Ensures(Contract.Result<T>() != null);
            Contract.Ensures(Contract.OldValue(_top) - 1 == _top);
            return _items[_top--];
        }
    }
}
