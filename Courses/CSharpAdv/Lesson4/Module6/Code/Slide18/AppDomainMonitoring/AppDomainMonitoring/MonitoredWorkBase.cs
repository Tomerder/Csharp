using System;

namespace AppDomainMonitoring
{
    /// <summary>
    /// The base class for all monitored work items. A work item that
    /// requires AppDomain monitoring derives from this class and overrides
    /// the abstract <see cref="Work"/> method.
    /// </summary>
    public abstract class MonitoredWorkBase : MarshalByRefObject
    {
        /// <summary>
        /// When overriden by a derived class, specifies the work to be
        /// performed by this monitored work item.
        /// </summary>
        public abstract void Work();

        /// <summary>
        /// Returns the result of the work (or null if there is no result).
        /// </summary>
        public abstract object Result { get; }
    }
}
