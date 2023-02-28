using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelCalculate
{
    public abstract class Lineal : IKin
    {
        public readonly int Generation;

        protected Lineal(int generation) => Generation = generation;

        public static Lineal Get(int generation) => generation == 0 ? Self.Get : (generation > 0 ? Progenitor.Get(generation) : Progeny.Get(generation));

        public class Progenitor : Lineal
        {
            private static readonly Dictionary<int, Progenitor> values = new();

            private Progenitor(int generation) : base(generation)
            {
            }

            public new static Progenitor Get(int generation)
            {
                generation = generation == 0 ? 1 : Math.Abs(generation);
                if (!values.ContainsKey(generation)) values.Add(generation, new(generation));
                return values[generation];
            }
        }

        public class Progeny : Lineal
        {
            private static readonly Dictionary<int, Progeny> values = new();

            private Progeny(int generation) : base(generation)
            {
            }

            public new static Progeny Get(int generation)
            {
                generation = generation == 0 ? -1 : -Math.Abs(generation);
                if (!values.ContainsKey(generation)) values.Add(generation, new(generation));
                return values[generation];
            }
        }

        public class Self : Lineal
        {
            public new static readonly Self Get = new();

            private Self() : base(0)
            {
            }
        }
    }
}