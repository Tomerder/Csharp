
using System;
namespace Chapter4Exercise1
{
    public partial class LinkedList<T>
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

        public int Count { get; private set; }

        public void Add(T item)
        {
            Node node = new Node {Item = item, Next = Empty};

            _last.Next = node;
            _last = node;

            ++Count;

            Log("Add", item, Count);
        }

        public T RemoveAt(int index)
        {
            if (Count == 0)
                return default(T);

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

            if (index > Count)
                return default(T);

            Node node = First;

            for (int i = 0; i < index; ++i)
                node = node.Next;

            return node.Item;
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
