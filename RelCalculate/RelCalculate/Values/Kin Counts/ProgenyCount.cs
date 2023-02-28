using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelCalculate
{
    public class ProgenyCount : KinCount
    {
        public ProgenyCount(int generation = -1, int count = 0) : base(generation == 0 ? -1 : -Math.Abs(generation), count)
        {
        }

        public override IKin[] Values
        {
            get
            {
                List<IKin> result = new() { Lineal.Progeny.Get(Generation) };
                if (Count > 0) result.Add(Lineal.Progenitor.Get(Count));
                return result.ToArray();
            }
        }
    }
}