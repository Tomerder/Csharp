using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolkit
{
    internal class EditableGrouping<K, T> : IGrouping<K, T>
    {
        private K _key;
        private HashSet<T> _values;

        public EditableGrouping(K key)
        {
            _key = key;
            _values = new HashSet<T>();
        }

        public EditableGrouping(K key, T value)
        {
            _key = key;
            _values = new HashSet<T>(value.AsIEnumerable());
        }

        public EditableGrouping(K key, IEnumerable<T> values)
        {
            _key = key;
            _values = new HashSet<T>(values);
        }


        public K Key
        {
            get { return _key; }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _values.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T value)
        {
            _values.Add(value);
        }

        public void Remove(T value)
        {
            _values.Remove(value);
        }

        public void RemoveWhere(Predicate<T> predicate)
        {
            _values.RemoveWhere(predicate);
        }

        public int Count
        {
            get
            {
                return _values.Count;
            }
        }

        public bool ContainsValue(T value)
        {
            return _values.Contains(value);
        }

        public void Reset(IEnumerable<T> values)
        {
            _values = new HashSet<T>(values);
        }
    }

    public class EditableLookup<K, T> : ILookup<K, T>
    {
        private Dictionary<K, EditableGrouping<K, T>> _groups;

        private EditableGrouping<K, T> _ensureGroup(K key)
        {
            EditableGrouping<K, T> res = null;

            if (_groups.ContainsKey(key))
            {
                res = _groups[key];
            }
            else
            {
                res = new EditableGrouping<K, T>(key);
                _groups.Add(key, res);
            }

            return res;

        }

        public EditableLookup()
        {
            _groups = new Dictionary<K, EditableGrouping<K, T>>();
        }

        public bool Contains(K key)
        {
            return _groups.ContainsKey(key);
        }

        public bool ContainsPair(K key, T value)
        {
            return _groups.ContainsKey(key)
                        && _groups[key].ContainsValue(value);
        }

        public int Count
        {
            get { return _groups.Count; }
        }

        public IEnumerable<T> this[K key]
        {
            get
            {
                if (_groups.ContainsKey(key))
                    return _groups[key];

                return Enumerable.Empty<T>();
            }
        }

        public IEnumerator<IGrouping<K, T>> GetEnumerator()
        {
            return _groups.Select(kvp => kvp.Value).GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(K key, T value)
        {
            var group = _ensureGroup(key);
            group.Add(value);
        }

        public void Reset(K key, IEnumerable<T> values)
        {
            var group = _ensureGroup(key);
            group.Reset(values);
        }

        public void Remove(K key, T value)
        {
            if (_groups.ContainsKey(key))
            {
                var group = _groups[key];
                group.Remove(value);

                // if bucket is empty, remove it altogether
                if (group.Count == 0) _groups.Remove(key);
            }
        }

        public void RemoveKey(K key)
        {
            _groups.Remove(key);
        }

        public void RemoveWhere(K key, Predicate<T> predicate)
        {
            _groups[key].RemoveWhere(predicate);
            if (_groups[key].Count == 0) RemoveKey(key);
        }

        public void Clear()
        {
            _groups.Clear();
        }

        public IEnumerable<K> Keys
        {
            get
            {
                return _groups.Select(kvp => kvp.Key);
            }
        }

    }

    public static class EditableLookupExtensions
    {
        public static EditableLookup<K, T> ToEditableLookup<K, T>(this Dictionary<K, List<T>> source)
        {
            var res = new EditableLookup<K, T>();

            foreach (var k in source.Keys)
            {
                res.Reset(k, source[k]);
            }

            return res;
        }
    }
}
