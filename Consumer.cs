using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CustomerAndProducer
{
    class Consumer
    {
        private Thread t;
        private CommonData _d;
        public Consumer(CommonData d)
        {
            _d = d;
            Start();
        }

        private void Get()
        {
            
            int[] result = null;
            int cnt = 0;
            while (cnt < Producer.Times)
            {
                Monitor.Enter(_d);
                try
                {
                    while (_d.Filled < Producer.MaxNum)
                    {
                        Monitor.Wait(_d);
                    }
                    result = _d.Get();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Exception at consumer:  {e.Message}");
                }

                if (result != null)
                {
                    int d = 0;
                    for (int i = 0; i < Producer.MaxNum; i++)
                    {
                        d += result[i];
                    }
                    Console.WriteLine("Результат потребителя : {0} после {1} волны", d, _d.Wave);
                }
                Console.WriteLine();

                Monitor.PulseAll(_d);

                Monitor.Exit(_d);

                cnt++;
            }
        }

        public void Start()
        {
            if (t == null || !t.IsAlive)
            {
                ThreadStart th = new ThreadStart(Get);
                t = new Thread(th);
                t.Start();
            }
        }
    }
}
