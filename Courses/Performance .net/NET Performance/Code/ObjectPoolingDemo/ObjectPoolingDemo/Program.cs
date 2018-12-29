using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObjectPoolingDemo
{
    public static class ObjectPool<T> where T : new()
    {
        private static Stack<T> _pool = new Stack<T>();

        public static T Allocate()
        {
            if (_pool.Count == 0) return new T();
            return _pool.Pop();
        }
        public static void Deallocate(T obj)
        {
            _pool.Push(obj);
        }
    }

    class MyPooledConnection
    {
        public static MyPooledConnection Create()
        {
            return ObjectPool<MyPooledConnection>.Allocate();
        }
        ~MyPooledConnection()
        {
            ObjectPool<MyPooledConnection>.Deallocate(this);
            GC.ReRegisterForFinalize(this);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
