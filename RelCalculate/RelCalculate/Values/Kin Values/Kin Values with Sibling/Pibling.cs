using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelCalculate
{
    public class Pibling : KinWithCountedSibling
    {
        private static readonly Dictionary<int, ValueTuple<Pibling, Pibling>> values = new();

        private Pibling(int generation, bool isHalf) => (Generation, IsHalf) = (generation, isHalf);

        public override int Generation { get; }

        public override bool IsHalf { get; }

        public static Pibling Get(int generation, bool isHalf = false)
        {
            generation = generation == 0 ? 1 : Math.Abs(generation);
            if (!values.ContainsKey(generation)) values.Add(generation, (new(generation, true), new(generation, false)));
            var result = values[generation];
            return isHalf ? result.Item1 : result.Item2;
        }
    }
}