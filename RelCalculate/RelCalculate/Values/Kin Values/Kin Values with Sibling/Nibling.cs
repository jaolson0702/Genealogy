using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelCalculate
{
    public class Nibling : KinWithCountedSibling
    {
        private static readonly Dictionary<int, ValueTuple<Nibling, Nibling>> values = new();

        private Nibling(int generation, bool isHalf) => (Generation, IsHalf) = (-generation, isHalf);

        public override int Generation { get; }

        public override bool IsHalf { get; }

        public static Nibling Get(int generation, bool isHalf = false)
        {
            generation = generation == 0 ? 1 : Math.Abs(generation);
            if (!values.ContainsKey(generation)) values.Add(generation, (new(generation, true), new(generation, false)));
            var result = values[generation];
            return isHalf ? result.Item1 : result.Item2;
        }
    }
}