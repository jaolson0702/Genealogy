using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinshipCompute
{
    public abstract class NonLinealTarget
    {
        public abstract int ProgenitorCount { get; }

        public abstract int ProgenyCount { get; }

        public abstract string ToString(Gender? gender);

        public static NonLinealTarget Get(int generation) => generation == 0 ? Sibling.Get : (generation > 0 ? Pibling.Get(generation) : Nibling.Get(generation));

        public static NonLinealTarget Get(ValueTuple<int, int> cousinValues) => (Cousin)cousinValues;

        public static NonLinealTarget Get(int degree, int timesRemoved) => Get((degree, timesRemoved));

        public static implicit operator NonLinealTarget(int generation) => Get(generation);

        public static implicit operator NonLinealTarget(ValueTuple<int, int> cousinValues) => Get(cousinValues);
    }
}