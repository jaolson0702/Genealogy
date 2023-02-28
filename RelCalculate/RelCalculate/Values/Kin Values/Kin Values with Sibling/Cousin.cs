using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelCalculate
{
    public class Cousin : KinWithSibling
    {
        private static readonly Dictionary<ValueTuple<int, int>, ValueTuple<Cousin, Cousin>> values = new();
        public readonly int Degree, TimesRemoved;

        private Cousin(int degree, int timesRemoved, bool isHalf) => (Degree, TimesRemoved, IsHalf) = (degree, timesRemoved, isHalf);

        public override bool IsHalf { get; }

        public static Cousin Get(int degree, int timesRemoved, bool isHalf = false)
        {
            degree = degree == 0 ? 1 : Math.Abs(degree);
            ValueTuple<int, int> foundKey = (0, 0);
            foreach (ValueTuple<int, int> key in values.Keys)
                if (key.Item1 == degree && key.Item2 == timesRemoved)
                {
                    foundKey = key;
                    break;
                }
            if (foundKey.Item1 == 0)
            {
                foundKey = (degree, timesRemoved);
                values.Add(foundKey, (new(degree, timesRemoved, true), new(degree, timesRemoved, false)));
            }
            var result = values[foundKey];
            return isHalf ? result.Item1 : result.Item2;
        }
    }
}