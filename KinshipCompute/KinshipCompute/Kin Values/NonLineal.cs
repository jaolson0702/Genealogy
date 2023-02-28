using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace KinshipCompute
{
    public class NonLineal : Kin
    {
        private static readonly Dictionary<NonLinealTarget, ValueTuple<NonLineal, NonLineal>> values = new();

        private NonLineal(NonLinealTarget target, bool isHalf) => (Target, IsHalf) = (target, isHalf);

        public NonLinealTarget Target { get; }

        public bool IsHalf { get; }

        public override int ProgenitorCount => Target.ProgenitorCount;

        public override int ProgenyCount => Target.ProgenyCount;

        public override Kin With(Kin other)
        {
            if (other is NonLineal nlOther)
            {
                if (IsHalf && nlOther.IsHalf && nlOther.Target is Sibling) return Target;
                // if (Target is Sibling && (nlOther.Target is Pibling || nlOther.Target is Cousin))
                // CONTINUE HERE
            }
        }

        public override bool IsReflectiveWith(Kin other)
        {
            if (other is NonLineal nlOther && nlOther.Target is Sibling) return false;
            if (other.ProgenitorCount > 0) return true;
            bool result = ProgenyCount > 1 && (other is not NonLineal nl || nl.Target is not Sibling || nl.IsHalf) && other.ProgenitorCount > 0;
            return result;
        }

        public override bool Equals(Kin? other) => other is NonLineal non && non.Target.Equals(Target) && non.IsHalf == IsHalf;

        public override string ToString(Gender? gender) => (IsHalf ? "half" + (Target is Cousin ? " " : "-") : string.Empty) + Target.ToString(gender);

        public NonLineal ToHalf() => GetHalf(Target);

        public NonLineal ToFull() => GetFull(Target);

        public static NonLineal GetHalf(NonLinealTarget target) => GetSet(target).Item1;

        public static NonLineal GetFull(NonLinealTarget target) => GetSet(target).Item2;

        public static NonLineal Get(NonLinealTarget target, bool isHalf = false) => isHalf ? GetHalf(target) : GetFull(target);

        public new static NonLineal Get(int generation) => generation == 0 ? Sibling.Get : generation > 0 ? Pibling.Get(generation) : Nibling.Get(generation);

        public static NonLineal Get(ValueTuple<int, int> cousinValues) => (Cousin)cousinValues;

        public static ValueTuple<NonLineal, NonLineal> GetSet(NonLinealTarget target)
        {
            if (!values.ContainsKey(target))
                values.Add(target, (new(target, true), new(target, false)));
            return values[target];
        }

        public static implicit operator NonLineal(ValueTuple<NonLinealTarget, bool> values) => Get(values.Item1, values.Item2);

        public static implicit operator NonLineal(NonLinealTarget target) => Get(target);

        public static implicit operator NonLineal(int generation) => Get(generation);

        public static implicit operator NonLineal(ValueTuple<int, int> cousinValues) => Get(cousinValues);
    }
}