
namespace DynamicDelegate
{
    /// <summary>
    /// A demo of the [LogMethod] attribute.
    /// </summary>
    class Box
    {
        public string Name { get; set; }
        public decimal Volume { get; set; }

        /// <summary>
        /// Invoked when a log-friendly representation of this instance
        /// is requested by the DynamicLogger class.
        /// </summary>
        [LogMethod]
        private string Log()
        {
            return Name + " contains " + Volume;
        }
    }
}
