using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCalc
{
    public abstract class Kinship : Rel.Basic
    {
        public abstract KinshipCount Count { get; }

        public static implicit operator Kinship(int given) => (Simple)given;

        public static implicit operator Kinship(KinshipCount given)
        {
            if (given.ACount == 0) return -given.DCount;
            if (given.DCount == 0) return given.ACount;
            if (given.ACount == 1) return (Ibling)(1 - given.DCount);
            if (given.DCount == 1) return (Ibling)(given.ACount - 1);
            Rel result = 0;
            result += (Kinship)(given.ACount - 1, 0);
            result += (Kinship)(1, 1);
            result += (Kinship)(0, given.DCount - 1);
            return (Kinship)result;
        }

        public static implicit operator Kinship(ValueTuple<int, int> given) => (KinshipCount)(given.Item1, given.Item2);

        public abstract class Simple : Kinship
        {
            public abstract int Generation { get; }

            public static implicit operator Simple(int given) => (Lineal)given;
        }
    }
}