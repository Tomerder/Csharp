using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ConcurrencyVisualizer.Instrumentation;

namespace StupidNotepad
{
    public class SpellCheckService
    {
        private Random random = new Random();

        public bool CheckSpelling(string text)
        {
            using (var span = Markers.EnterSpan("SpellCheck"))
            {

                Task.Run(() =>
                {
                    Thread.Sleep(50);
                }).Wait();
                if (random.Next() % 20 == 0)
                {
                    int spellCheckDone = 0;
                    ThreadPool.QueueUserWorkItem(_ =>
                    {
                        ThoroughSpellCheck();
                        Interlocked.Exchange(ref spellCheckDone, 1);
                    });
                    while (Interlocked.CompareExchange(ref spellCheckDone, 0, 1) != 1) ;
                    return false;
                }
                return true;
            }

        }

        private void ThoroughSpellCheck()
        {
            Markers.WriteMessage("ThoroughSpellCheck");
            Thread.Sleep(800);
        }
    }
}
