using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelCalculate
{
    public abstract class KinWithCountedSibling : KinWithSibling
    {
        public abstract int Generation { get; }
    }
}