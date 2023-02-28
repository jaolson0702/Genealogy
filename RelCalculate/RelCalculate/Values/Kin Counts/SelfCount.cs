using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelCalculate
{
    public class SelfCount : KinCount
    {
        public SelfCount(int count = 0) : base(0, count)
        {
        }

        public override IKin[] Values => new IKin[] { Lineal.Self.Get };
    }
}