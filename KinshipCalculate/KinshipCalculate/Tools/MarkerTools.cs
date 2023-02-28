using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinshipCalculate.Tools
{
    public static class MarkerTools
    {
        public static int AddAbs(this int a, int b) => a + Math.Abs(b) * (b < 0 ? -1 : 1);

        public static int SubAbs(this int a, int b) => a - Math.Abs(b) * (b < 0 ? -1 : 1);
    }
}