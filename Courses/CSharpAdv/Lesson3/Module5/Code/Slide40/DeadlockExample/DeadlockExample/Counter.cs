
namespace DeadlockExample
{
    /// <summary>
    /// A simple counter which is used for demonstrating potential
    /// synchronization problems.
    /// </summary>
    class Counter
    {
        private int _value;

        public int Next() { return ++_value; }

        public int Current { get { return _value; } }
    }
}
