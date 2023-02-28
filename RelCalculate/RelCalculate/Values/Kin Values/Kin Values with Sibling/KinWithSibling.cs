using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelCalculate
{
    public abstract class KinWithSibling : IKin
    {
        public abstract bool IsHalf { get; }
    }
}