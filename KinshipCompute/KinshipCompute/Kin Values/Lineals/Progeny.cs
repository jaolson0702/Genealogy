using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinshipCompute
{
    public class Progeny : Lineal
    {
        private static readonly Dictionary<int, Progeny> values = new();

        private Progeny(int generation) : base(generation)
        {
        }

        public override int ProgenitorCount => 0;

        public override int ProgenyCount => Math.Abs(Generation);

        public override bool IsReflectiveWith(Kin other) => (other is not NonLineal nl || nl.Target is not Sibling || nl.IsHalf) && other.ProgenitorCount > 0;

        public override string ToString(Gender? gender) => FormatTools.GetGenerationPrefix(Generation) + gender switch
        {
            Gender.Male => "son",
            Gender.Female => "daughter",
            _ => "child"
        };

        public new static Progeny Get(int generation)
        {
            generation = generation == 0 ? 1 : Math.Abs(generation);
            if (!values.ContainsKey(generation)) values.Add(generation, new(generation));
            return values[generation];
        }

        public static implicit operator Progeny(int generation) => Get(generation);
    }
}