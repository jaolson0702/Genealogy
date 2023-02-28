using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinshipCompute
{
    public class Progenitor : Lineal
    {
        private static readonly Dictionary<int, Progenitor> values = new();

        private Progenitor(int generation) : base(generation)
        {
        }

        public override int ProgenitorCount => Generation;

        public override int ProgenyCount => 0;

        public override bool IsReflectiveWith(Kin other) => false;

        public override string ToString(Gender? gender) => FormatTools.GetGenerationPrefix(Generation) + gender switch
        {
            Gender.Male => "father",
            Gender.Female => "mother",
            _ => "parent"
        };

        public new static Progenitor Get(int generation)
        {
            generation = generation == 0 ? 1 : Math.Abs(generation);
            if (!values.ContainsKey(generation)) values.Add(generation, new Progenitor(generation));
            return values[generation];
        }

        public static implicit operator Progenitor(int generation) => Get(generation);
    }
}