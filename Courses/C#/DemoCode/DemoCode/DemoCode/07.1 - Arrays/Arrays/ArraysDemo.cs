using System;

namespace Arrays
{
    class ArraysDemo
    {
        // The following section demonstrates casts between arrays,
        //  given that the appropriate conversion exists.  In case
        //  of a value-type array, you must use Array.Copy instead.
        //
        #region Casting arrays

        static void CastingArrays()
        {
            string[] stringArr = { "Hello", "World" };
            object[] objArr = stringArr;

            // In the case of an up-cast, there is no need for
            //  an explicit cast.  Should we require a down-cast,
            //  an explicit cast is required.
        }

        #endregion

        // The following section demonstrates various methods
        //  derived from System.Array.  Note that these are
        //  not the generic methods (generics will be discussed
        //  later in this course).
        //
        #region System.Array methods

        static int SearchUnsortedArray(Array array, object searchValue)
        {
            // Sort the array using Array.Sort and then invoke
            //  Array.BinarySearch to find the element.
            //
            Array.Sort(array);
            return Array.BinarySearch(array, searchValue);

            // An alternative approach is using Array.Find.
        }

        static void CopyArray(int[] elements)
        {
            int[] newArray = new int[elements.Length];
            // Array.Copy copies elements from a source array
            //  to a destination array, and has overloads that
            //  allow us to specify the source/destination indices.
            Array.Copy(elements, newArray, elements.Length);
        }

        // Creates a two-dimensional array of integers with the
        //  specified size.
        //
        static Array CreateIntMatrix(int size)
        {
            // Other overloads of CreateInstance allow us to create
            //  dynamic arrays of any number of dimensions, and to
            //  specify the array lower bounds.
            // Note: while it is possible to create arrays that are
            //  not zero-based, it is more efficient to use them
            //  because there are special IL instructions for
            //  manipulating them.  In addition, it's important to
            //  note that only zero-based arrays are CLS-compliant.
            //
            return Array.CreateInstance(typeof(int), size, size);
        }

        #endregion

        static void Main(string[] args)
        {
            // The following three collapsed regions contain
            //  demonstrations for the various kinds of arrays.
            //
            #region Single-dimensional arrays

            // Create an array of 5 integers.  They are
            //  initialized to 0 by default (value-type).
            //
            int[] intArray = new int[5];

            // Iterate the array.  It has a Length property
            //  inherited from System.Array.
            //
            for (int i = 0; i < intArray.Length; ++i)
            {
                // Array access is range checked, so
                //  if you attempt to access an element
                //  outside the range, you get an
                //  IndexOutOfRangeException.
                //
                intArray[i] = i;
            }

            // Create an array of strings.  They are
            //  initialized to null by default (reference-type).
            //
            string[] stringArray = new string[2];

            // It is possible to provide initial values when
            //  initializing the array, thus omitting the new
            //  operator and/or the number of elements.  Both
            //  declarations are equivalent.
            //
            int[] newArr1 = new int[] { 2, 3 };
            int[] newArr2 = { 2, 3 };

            #endregion

            #region Multi-dimensional arrays
                        
            // Create a matrix of integers.  Each row must have
            //  exactly the same length.
            //
            int[,] matrix = new int[3, 3];
            // Note that we have to use Array.GetLength and specify
            //  the dimension for which we want the length.
            for (int i = 0; i < matrix.GetLength(0); ++i)
            {
                for (int j = 0; j < matrix.GetLength(1); ++j)
                {
                    matrix[i, j] = i * j;
                }
            }
            
            #endregion

            #region Jagged arrays
            
            // Create a jagged array.  A jagged array is actually
            //  an array of arrays, and so each row may have a
            //  different length, as it has to be allocated separately.
            //  Clearly, only the first dimension's length may be
            //  specified (and MUST be specified).
            // 
            string[][] employees = new string[4][];
            employees[0] = new string[] { "John", "Mary" };
            employees[1] = new string[] { "Robert", "Mike", "Sally" };
            employees[2] = new string[] { "Jane", "Sara", "John", "Mary" };
            employees[3] = new string[] { "Jack", "Don", "Roy", "Joan", "Bob" };

            // Accessing the elements in a jagged array is not
            //  done using [,] notation, but using [][] notation.
            //
            for (int i = 0; i < employees.Length; ++i)
            {
                for (int j = 0; j < employees[i].Length; ++j)
                {
                    Console.WriteLine(employees[i][j]);
                }
            }
            
            #endregion

            CastingArrays();

            // Using the foreach statement (which we have already
            //  seen before) is pretty straightforward using an array:
            //
            int[] ints = { 5, 3, 5, 6 };
            foreach (int i in ints)
            {
                Console.WriteLine(i);
            }

            int[,] intMatrix = new int[3, 3];
            foreach (int j in intMatrix)
            {
                Console.WriteLine(j);
            }

            string[] strings = { "Sasha", "Yona" };
            foreach (string name in strings)
            {
                Console.WriteLine(name);
            }

            // Finally, let's see an IndexOutOfRangeException
            //  when attempting to access an element outside of
            //  an array's bounds.
            //
            Console.WriteLine(ints[5]);
            // Here's the partial exception trace:
            //
            // Unhandled Exception: System.IndexOutOfRangeException:
            //      Index was outside the bounds of the array.
            //      at Arrays.ArraysDemo.Main(String[] args) in ArraysDemo.cs:line 166
        }
    }
}
