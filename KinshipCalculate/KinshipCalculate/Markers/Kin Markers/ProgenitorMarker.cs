using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinshipCalculate.Markers
{
    public class ProgenitorMarker : KinMarker
    {
        public ProgenitorMarker(int generation = 1, int count = 0) : base(generation == 0 ? 1 : Math.Abs(generation), count)
        {
        }
    }
}