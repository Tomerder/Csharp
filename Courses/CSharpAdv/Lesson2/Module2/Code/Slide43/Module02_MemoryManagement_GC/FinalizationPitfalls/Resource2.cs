
namespace FinalizationPitfalls
{
    /// <summary>
    /// Demonstrates the deadlock scenario.
    /// </summary>
    class Resource2
    {
        ~Resource2()
        {
            lock (typeof(Resource2))
            {
            }
        }
    }
}
