using System;
using System.Collections.Generic;
using System.Linq;

namespace AppDomainMonitoring
{
    /// <summary>
    /// This demo shows how AppDomain monitoring can be used to determine
    /// whether a certain AppDomain exceeded a threshold of allowed CPU usage
    /// or memory usage. AppDomain monitoring is useful for isolating
    /// expensive or unreliable work into separate AppDomains and making sure
    /// they don't excessively utilize system resources.
    /// </summary>
    class Program
    {
        static void Main()
        {
            //Defer a lengthy calculation to a separate AppDomain, but
            //monitor its CPU time and memory usage and shut it down if
            //it exceeds a certain threshold.
            MonitoredWorkExecutor.InitializeMonitoring();
            IEnumerable<long> primes = (IEnumerable<long>)
                MonitoredWorkExecutor.ExecuteAndMonitor<PrimeNumberChecker>(
                    maxCpuTime: TimeSpan.FromSeconds(1),
                    maxMemoryUsage: 10 * 1048576,
                    parameters: new object[] { 14000000, 15000000 }
                    );
            Console.WriteLine(primes.Skip(100).First());
        }
    }
}
