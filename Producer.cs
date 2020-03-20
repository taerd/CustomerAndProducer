using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CustomerAndProducer
{
    class Producer
    {
        public static int MaxNum { get; set; }
        private Thread t;
        private CommonData _d;
        public int Num { get; private set; }
        private Random rand;
        private int delay;
        public static int Times { get; set; }
        public Producer(int num,CommonData d)
        {
            _d = d;
            Num =Math.Abs(num)%3;
            Start();
        }
        private void Generate()
        {
            int i = 0;
            while (i < Times)
            {
                if (rand == null) rand = new Random((int)DateTime.Now.Ticks);
                delay = rand.Next(1000, 5000);
                Thread.Sleep(delay);
                int result = rand.Next(0, 100);

                Monitor.Enter(_d);

                try
                {
                    while(i != _d.Wave)
                    {
                        Monitor.Wait(_d);
                    }
                    _d.Set(Num, result);
                }
                catch(Exception e)
                {
                    Console.WriteLine($"Exception at producer {e.Message}");
                }

                Console.WriteLine("Производитель создал число №{0} = {1} после {2} волны", Num, result, _d.Wave + 1);

                Monitor.PulseAll(_d);
                Monitor.Exit(_d);
                i++;
            }
        }
        public void Start()
        {
            if (t == null || !t.IsAlive)
            {
                ThreadStart th = new ThreadStart(Generate);
                t = new Thread(th);
                t.Start();
            }
        }


    }
}
