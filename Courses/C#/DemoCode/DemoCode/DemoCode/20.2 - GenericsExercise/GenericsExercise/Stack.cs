using System;

namespace GenericsExercise
{
    class Stack<T>
    {
        private T[] _items;
        private int _topItem = -1;

        public Stack(int maxItems)
        {
            _items = new T[maxItems];
        }

        public void Push(T value)
        {
            if (_topItem + 1 >= _items.Length)
            {
                throw new InvalidOperationException("Stack is full");
            }
            _items[++_topItem] = value;
        }

        public T Pop()
        {
            if (_topItem < 0)
            {
                throw new InvalidOperationException("Stack is empty");
            }
            return _items[_topItem--];
        }

        public int Size
        {
            get { return _topItem + 1; }
        }
    }
}
