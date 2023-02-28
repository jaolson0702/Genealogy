using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinshipCompute
{
    public abstract class Lineal : Kin
    {
        protected Lineal(int generation) => Generation = generation;

        public new int Generation { get; }

        public override Kin With(Kin other)
        {
            Kin result = base.With(other);
            if (other is NonLineal nlOther && nlOther.IsHalf)
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                result = NonLineal.GetHalf((result as NonLineal).Target);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            return result;
        }

        public override bool Equals(Kin? other) => other is Lineal lin && lin.Generation == Generation;

        public new static Lineal Get(int generation) => generation == 0 ? Self.Get : (generation > 0 ? Progenitor.Get(generation) : Progeny.Get(generation));

        public static implicit operator Lineal(int generation) => Get(generation);
    }
}