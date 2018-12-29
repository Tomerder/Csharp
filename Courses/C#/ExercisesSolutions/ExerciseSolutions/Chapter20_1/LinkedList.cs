
using System;

namespace Chapter20Exercise1
{
    public partial class LinkedList<T> : System.Collections.Generic.IList<T> where T :IComparable<T>
    {
        private static readonly Node _emptyNode = new Node();
        private Node _last = _emptyNode;

        static partial void Log(string message, T item, int count);

        public LinkedList()
        {
            _last.Next = _last;
        }

        private Node Empty
        {
            get
            {
                return _last.Next;
            }
        }

        private Node First
        {
            get
            {
                return Empty.Next;
            }
        }

        public bool Remove(T item)
        {
            int index = Find(item);

            if (index == -1)
                return false;

            RemoveAt(index);

            return true;
        }

        public int Count { get; private set; }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public void Add(T item)
        {
            Node node = new Node {Item = item, Next = Empty};

            _last.Next = node;
            _last = node;

            ++Count;

            Log("Add", item, Count);
        }

        public void Clear()
        {
            _last = _emptyNode;
            _emptyNode.Next = _emptyNode;
            Count = 0;
        }

        public bool Contains(T item)
        {
            return Find(item) != -1;
        }


        public void CopyTo(T[] array, int arrayIndex)
        {
            int index = 0;
            foreach (T item in this)
            {
                array[arrayIndex + index] = item;
                ++index;
            }
        }

        public int IndexOf(T item)
        {
            return Find(item);
        }

        public void Insert(int index, T item)
        {
            ValidateRange(index);
            
            Node node = Empty;

            for (int i = 0; i < index; ++i)
            {
                node = node.Next;
            }

            Node newNode = new Node {Item = item, Next = node.Next };
            
            node.Next = newNode;
        }

        void System.Collections.Generic.IList<T>.RemoveAt(int index)
        {
            RemoveAt(index);
        }

        public T this[int index]
        {
            get { return GetAt(index); }
            set
            {
                Node node = First;

                for (int i = 0; i < index; ++i)
                {
                    node = node.Next;
                }
                node.Item = value;

            }
        }

        public T RemoveAt(int index)
        {
            ValidateRange(index);

            Node prev = Empty;
            Node node = First;

            for (int i = 0; i < index && node != null; ++i)
            {
                prev = node;
                node = node.Next;
            }

            prev.Next = node.Next;
            --Count;

            Log("Remove", node.Item, Count);

            return node.Item;
        }

        public T GetAt(int index)
        {

            ValidateRange(index);

            Node node = First;

            for (int i = 0; i < index; ++i)
                node = node.Next;

            return node.Item;
        }

        public int Find(T element)
        {
            Node node = First;

            for (int index = 0; index < Count; ++index, node = node.Next)
            {
                if (node.Item.CompareTo(element) == 0)
                {
                    return index;
                }
            }
            return -1;
        }

        public System.Collections.Generic.IEnumerator<T> GetEnumerator()
        {
            for (Node node = First; node != Empty; node = node.Next)
                yield return node.Item;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void ValidateRange(int index)
        {
            if (index < 0 || index >= Count)
                throw new IndexOutOfRangeException("Index was outside the boundary of the list.");
        }


        //public void Print()
        //{
        //    for (Node node = First; node != Empty; node = node.Next)
        //        Console.Write("{0}, ",node.Item);
        //    Console.WriteLine();
        //}
        
        private class Node
        {
            public Node Next { get; set; }
            public T Item { get; set; }
        }
    }
}
