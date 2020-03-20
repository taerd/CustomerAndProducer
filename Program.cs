using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAndProducer
{
    class Program
    {
        static void Main(string[] args)
        {
            Producer.MaxNum = 3;
            Producer.Times = 10;
            CommonData cd = new CommonData();
            
            for (int i = 0; i < Producer.MaxNum; i++)
            {
                new Producer(i,cd);
            }
            new Consumer(cd);
            Console.ReadKey();
        }
    }
}
