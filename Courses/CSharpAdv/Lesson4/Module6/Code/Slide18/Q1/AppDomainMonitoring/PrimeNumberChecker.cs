using System;
using System.Collections.Generic;

namespace AppDomainMonitoring
{
    public class C : MarshalByRefObject
    {
        public int A { get; set; }
    }
    /// <summary>
    /// A class that finds all the prime numbers in a given range.
    /// </summary>
    public sealed class PrimeNumberChecker : MonitoredWorkBase
    {
        private readonly long _end;
        private readonly long _start;
        private readonly List<long> _primesInRange;
        public PrimeNumberChecker(long start, long end)
        {
            _start = start;
            _end = end;
            _primesInRange = new List<long>();
        }

        public override object Result
        {
            get { return _primesInRange.ToArray(); }
        }

        public override void Work()
        {
            for (long current = _start; current < _end; ++current)
            {
                if (IsPrime(current))
                {
                    _primesInRange.Add(current);
                }
            }
        }

        private static bool IsPrime(long current)
        {
            if (current % 2 == 0)
            {
                return false;
            }
            long sqrt = (long)Math.Sqrt(current) + 1;
            for (long n = 3; n < sqrt; ++n)
            {
                if (current % n == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
