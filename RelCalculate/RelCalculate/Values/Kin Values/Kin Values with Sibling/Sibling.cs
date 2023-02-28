using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelCalculate
{
    public class Sibling : KinWithCountedSibling
    {
        public static readonly Sibling Half = new(true), Full = new(false);

        private Sibling(bool isHalf) => IsHalf = isHalf;

        public override int Generation => 0;

        public override bool IsHalf { get; }

        public static Sibling Get(bool isHalf) => isHalf ? Half : Full;
    }
}