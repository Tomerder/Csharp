using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace GCNotifications
{
    internal sealed class GCWatcher
    {
        private Thread _watcherThread;

        public void Watch()
        {
            GC.RegisterForFullGCNotification(50, 50);
            _watcherThread = new Thread(() =>
                {
                    while (true)
                    {
                        GCNotificationStatus status = GC.WaitForFullGCApproach();
                        if (!WaitOnStatus(status))
                            return;
                        if (GCApproaches != null)
                            GCApproaches(this, EventArgs.Empty);
                        status = GC.WaitForFullGCComplete();
                        if (!WaitOnStatus(status))
                            return;
                        if (GCComplete != null)
                            GCComplete(this, EventArgs.Empty);
                    }
                });
            _watcherThread.IsBackground = true;
            _watcherThread.Start();
        }

        private bool WaitOnStatus(GCNotificationStatus status)
        {
            switch (status)
            {
                case GCNotificationStatus.Canceled:
                    return false;
                case GCNotificationStatus.Failed:
                    throw new ApplicationException("GC notification failed");
                case GCNotificationStatus.NotApplicable:
                    throw new ApplicationException("Concurrent GC is enabled");
                case GCNotificationStatus.Timeout:
                    throw new ApplicationException("unexpected");
                case GCNotificationStatus.Succeeded:
                    return true;
            }
            throw new ApplicationException("unexpected");
        }

        public void Cancel()
        {
            GC.CancelFullGCNotification();
            _watcherThread.Join();
        }

        public event EventHandler GCApproaches;
        public event EventHandler GCComplete;
    }

    class Program
    {
        static void Main(string[] args)
        {
            GCWatcher watcher = new GCWatcher();
            Stopwatch sw = null;
            watcher.GCApproaches += delegate
            {
                sw = Stopwatch.StartNew();
                Console.WriteLine("GC approaches");
            };
            watcher.GCComplete += delegate
            {
                Console.WriteLine("GC took " + sw.ElapsedMilliseconds + " ms");
            };
            watcher.Watch();

            while (!Console.KeyAvailable)
            {
                byte[] arr = new byte[100000];
                Thread.Sleep(50);
            }

            watcher.Cancel();
        }
    }
}
