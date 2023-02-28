using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace RelCalculate
{
    public class ProgenitorCount : KinCount
    {
        public readonly bool IsHalf;

        public ProgenitorCount(int generation = 1, int count = 0, bool isHalf = false) : base(generation == 0 ? 1 : Math.Abs(generation), count) => IsHalf = Count > 0 && isHalf;

        public override IKin[] Values
        {
            get
            {
                if (Count == 0) return new IKin[] { Lineal.Progenitor.Get(Generation) };
                if (Generation == 1 && Count == 1) return new IKin[] { Sibling.Get(IsHalf) };
                if (Generation == 1) return new IKin[] { Nibling.Get(Count - 1, IsHalf) };
                if (Count == 1) return new IKin[] { Pibling.Get(Generation - 1, IsHalf) };
                return new IKin[] { Cousin.Get(Generation - (Generation - Count) - 1, Generation - Count, IsHalf) };
            }
        }
    }
}