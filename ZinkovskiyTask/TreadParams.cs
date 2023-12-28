using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZinkovskiyTask
{
    // Параметри потоку
    class TreadParams
    {
        public int begin, end; // Діапазон обчислюваних значень ряду в потоці
        public Formula formula;
        public TreadParams(int b, int e, Formula f)
        {
            begin = b;
            end = e;
            formula = f;    
        }
    }
}
