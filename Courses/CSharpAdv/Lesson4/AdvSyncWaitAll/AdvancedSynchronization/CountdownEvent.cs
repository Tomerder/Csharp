using System.Threading;

namespace AdvancedSynchronization
{
    /// <summary>
    /// An event that becomes signaled after it is set for the specified number
    /// of times.  This resembles a "reverse" semaphore.
    /// </summary>
    class CountdownEvent
    {
        const int COUNT_MUL_EACH = 10000;
        ManualResetEvent m_notify;
        int m_count;

        public CountdownEvent(int count) 
        {
            m_notify = new ManualResetEvent(false);
            m_count = count * COUNT_MUL_EACH;
        }

        public void Set()
        {
            //save decrement of count
            //Interlocked.Decrement(ref m_count);

            lock (this)
            {
                for (int i = 0; i < COUNT_MUL_EACH; i++)
                {
                    m_count--;
                }
            }

            //if all events already worked -> open gate
            if (m_count == 0)
            {
                m_notify.Set();
            }                    
        }

        public void Wait()
        {
            //wait until gate will be opened
            m_notify.WaitOne();
        }
    }
}
