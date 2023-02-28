using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinshipCompute
{
    public class Nibling : NonLinealTarget
    {
        private static readonly Dictionary<int, Nibling> values = new();

        private Nibling(int generation) => Generation = generation;

        public int Generation { get; }

        public override int ProgenitorCount => 1;

        public override int ProgenyCount => Generation + 1;

        public override string ToString(Gender? gender) => FormatTools.GetGenerationPrefix(Generation) + gender switch
        {
            Gender.Male => "nephew",
            Gender.Female => "niece",
            _ => "nibling"
        };

        public new static Nibling Get(int generation)
        {
            generation = generation == 0 ? 1 : Math.Abs(generation);
            if (!values.ContainsKey(generation)) values.Add(generation, new Nibling(generation));
            return values[generation];
        }

        public static implicit operator Nibling(int generation) => Get(generation);
    }
}