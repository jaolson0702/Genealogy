using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinshipCompute
{
    public class Pibling : NonLinealTarget
    {
        private static readonly Dictionary<int, Pibling> values = new();

        private Pibling(int generation) => Generation = generation;

        public int Generation { get; }

        public override int ProgenitorCount => Generation + 1;

        public override int ProgenyCount => 1;

        public override string ToString(Gender? gender) => FormatTools.GetGenerationPrefix(Generation) + gender switch
        {
            Gender.Male => "uncle",
            Gender.Female => "aunt",
            _ => "pibling"
        };

        public new static Pibling Get(int generation)
        {
            generation = generation == 0 ? 1 : Math.Abs(generation);
            if (!values.ContainsKey(generation)) values.Add(generation, new Pibling(generation));
            return values[generation];
        }

        public static implicit operator Pibling(int generation) => Get(generation);
    }
}