using System;
using System.Collections;

namespace Collections
{
    class CollectionsDemo
    {
        static void Main(string[] args)
        {
            #region ArrayList Sample
            
            ArrayList list = new ArrayList();

            list.Add(5);
            list.AddRange(new int[] { 2, 3, 4 });
            
            Console.WriteLine(list.IndexOf(3));
            
            list.Sort();
            list.Reverse();

            Display(list);

	        #endregion

            #region Hashtable Sample

            Hashtable map = new Hashtable();

            map.Add("Mattan", 9);
            map.Add("Ilan", 23);
            map.Add("Boris", 49);

            foreach (string name in map.Keys)
            {
                Console.WriteLine(name + "=" + map[name]);
            }

            int mattanAge = (int)map["Mattan"];
            map["Mattan"] = mattanAge + 1;

            int ageSum = 0;
            foreach (int age in map.Values)
            {
                ageSum += age;
            }

            #endregion

            #region Queue and Stack Sample

            Queue queue = new Queue();

            queue.Enqueue("A");
            queue.Enqueue("B");
            queue.Enqueue("C");

            Display(queue);

            Stack stack = new Stack();

            stack.Push("A");
            stack.Push("B");
            stack.Push("C");

            Display(stack);

            #endregion
        }

        static void Display(IEnumerable items)
        {
            foreach (object o in items)
            {
                Console.WriteLine(o);
            }
        }
    }
}
