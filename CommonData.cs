using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAndProducer
{
    class CommonData
    {
        public int[] _results;
        public int Filled { get; private set; }
        public int Wave { get; private set; }
        public CommonData()
        {
            _results = new int[Producer.MaxNum];
            Filled = 0;
            Wave = 0;
        }
        public void Set(int num,int value)
        {
            _results[num] = value;
            Filled++;
        }
        public  int[] Get()
        {
            Filled = 0;
            Wave++;
            return _results;
        }
    }
}
