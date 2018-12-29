using System;
using System.Collections.Generic;
using System.Linq;

namespace APM
{
    /// <summary>
    /// Encapsulates a prime number calculations within a range of integers.
    /// </summary>
    class PrimeNumberCalculator
    {
        private readonly int _start, _end;
        private IEnumerable<int> _thePrimes;

        /// <summary>
        /// The number of elements in the range.
        /// </summary>
        public int Range { get { return _end - _start; } }

        /// <summary>
        /// The actual prime numbers in the range.
        /// </summary>
        public IEnumerable<int> ThePrimes
        {
            get { return _thePrimes; }
        }

        public PrimeNumberCalculator(int start, int end)
        {
            _start = start;
            _end = end;
        }

        /// <summary>
        /// Performs the calculation and returns the number of prime numbers
        /// in the range.  The numbers themselves can be retrieved using the
        /// ThePrimes property.
        /// </summary>
        /// <returns>The number of prime numbers in the range.</returns>
        public int Calculate()
        {
            _thePrimes = Enumerable.Range(_start, Range).Where(IsPrime);
            return _thePrimes.Count();  //This causes evaluation of the enumerable
        }

        private static bool IsPrime(int number)
        {
            if (number % 2 == 0)
                return false;

            int sqrt = (int)Math.Ceiling(Math.Sqrt(number));
            for (int i = 3; i <= sqrt; i += 2)
                if (number % i == 0) return false;

            return true;
        }
    }
}
