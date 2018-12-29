using System;

namespace GenericsExercise
{
    class GenericsExerciseMain
    {
        static void Main(string[] args)
        {
            Stack<int> intStack = new Stack<int>(10);
            intStack.Push(5);
            intStack.Push(3);
            Console.WriteLine(intStack.Size);
            Console.WriteLine(intStack.Pop());
        }
    }
}
