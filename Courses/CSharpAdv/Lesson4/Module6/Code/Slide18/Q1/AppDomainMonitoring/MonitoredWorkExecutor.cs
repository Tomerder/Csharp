using System;
using System.Reflection;
using System.Threading;

namespace AppDomainMonitoring
{
    /// <summary>
    /// Executes work items in a separate AppDomain, while monitoring them
    /// for CPU time and memory usage so that excessive resource utilizers
    /// are shut down in a timely fashion.
    /// </summary>
    public sealed class MonitoredWorkExecutor
    {
        /// <summary>
        /// Initializes the AppDomain monitoring mechanism.
        /// </summary>
        public static void InitializeMonitoring()
        {
            //Enable monitoring for all application domains. Once enabled,
            //monitoring cannot be turned off.
            AppDomain.MonitoringIsEnabled = true;
        }

        /// <summary>
        /// Executes a work item in a separate AppDomain and monitors its
        /// progress to determine whether it exceeds the allowed CPU time
        /// quota and memory usage quota. If this is the case, the work item's
        /// AppDomain is rudely unloaded.
        /// </summary>
        /// <typeparam name="TWorkItem">The type of the work item. It must
        /// derive from the <see cref="MonitoredWorkBase"/> class.</typeparam>
        /// <param name="parameters">Parameters to pass to the constructor
        /// of the work item. This array may be null or empty.</param>
        /// <param name="maxCpuTime">The maximum amount of CPU time the
        /// work item is allowed to use.</param>
        /// <param name="maxMemoryUsage">The maximum amount of memory (in 
        /// bytes) that the work item is allowed to use.</param>
        /// <returns>The result of the work.</returns>
        public static object ExecuteAndMonitor<TWorkItem>(
            object[] parameters, TimeSpan maxCpuTime, int maxMemoryUsage)
            where TWorkItem : MonitoredWorkBase
        {
            AppDomain domain = AppDomain.CreateDomain(Guid.NewGuid().ToString());
            TWorkItem workItem = (TWorkItem)domain.CreateInstanceAndUnwrap(
                typeof(TWorkItem).Assembly.FullName,
                typeof(TWorkItem).FullName,
                false, BindingFlags.CreateInstance, null, parameters, null, null);

            workItem.C1.A = 7;
            Thread executor = new Thread(workItem.Work);
            executor.Start();

            while (!executor.Join(0))
            {
                Thread.Sleep(TimeSpan.FromMilliseconds(100));
                if (domain.MonitoringTotalAllocatedMemorySize > maxMemoryUsage ||
                    domain.MonitoringTotalProcessorTime > maxCpuTime)
                {
                    Console.WriteLine("Quota exceeded in AD {0}", domain.FriendlyName);
                    executor.Abort();
                }
            }

            object result = workItem.Result;
            AppDomain.Unload(domain);
            return result;
        }
    }
}
