using KinshipCompute;

namespace RelCompute
{
    public class Kinship : Rel
    {
        public readonly Rel Target;
        public readonly Basic Applied;

        public Kinship(Rel target, Basic applied)
        {
            (Target, Applied) = (target, applied);
            if (Target is Kinship rkr && rkr.Applied.Target == 0) Target = rkr.Target;
            if (Target is Basic targetKin && !targetKin.Target.IsReflectiveWith(applied.Target))
            {
                Target = targetKin.Target.With(applied.Target);
                Applied = Kins.Self;
            }
            if (Target is Kinship targetRefKin && !targetRefKin.Applied.Target.IsReflectiveWith(applied.Target))
            {
                Target = new Kinship(targetRefKin.Target, targetRefKin.Applied.Target.With(applied.Target));
                Applied = Kins.Self;
            }
        }

        public Kinship(Basic applied) : this(Basic.Get(Kins.Self), applied)
        {
        }

        public Kinship() : this(Kins.Self)
        {
        }

        public override RelValue[] Values
        {
            get
            {
                if (Target is Basic kin && kin == Kins.Self) return Applied.Values;
                if (Applied == Kins.Self) return Array.Empty<RelValue>();
                List<RelValue> values = new();
                values.AddRange(Target.Values);
                values.RemoveAt(values.Count - 1);
                RelValue borderLValue = Target.Values[^1];
                RelValue borderRValue = Applied.Values[0];
                if (borderLValue == RelValue.Parent && borderRValue == RelValue.Child) values.Add(RelValue.HalfSibling);
                else if (borderLValue == RelValue.Child && borderRValue == RelValue.FullSibling) values.Add(RelValue.Child);
                else if (borderLValue == RelValue.FullSibling)
                {
                    switch (borderRValue)
                    {
                        case RelValue.Parent: values.Add(RelValue.Parent); break;
                        case RelValue.FullSibling: values.Add(RelValue.FullSibling); break;
                        case RelValue.HalfSibling: values.Add(RelValue.HalfSibling); break;
                    }
                }
                else if (borderLValue == RelValue.HalfSibling && borderRValue == RelValue.FullSibling) values.Add(RelValue.HalfSibling);
                else
                {
                    values.Add(borderLValue);
                    values.Add(borderRValue);
                }
                for (int a = 1; a < Applied.Values.Length; a++) values.Add(Applied.Values[a]);
                return values.ToArray();
            }
        }

        public override Rel With(RelValue value)
        {
            if ((Rel)value is Partner par) return new Partner(this, par.Status);
            Basic kin = (Basic)(Rel)value;
            if (!Applied.Target.IsReflectiveWith(kin.Target)) return new Kinship(Target, Applied.Target.With(kin.Target));
            return new Kinship(this, kin);
        }

        public override string ToString(Gender? gender) => Applied == Kins.Self ? Target.ToString(gender) : (Applied.ToString(gender) + " of " + Target);

        public class Basic : Rel, IEquatable<Basic>
        {
            private static readonly Dictionary<Kin, Basic> values = new();
            public readonly Kin Target;

            private Basic(Kin target) => Target = target;

            public override RelValue[] Values
            {
                get
                {
                    if (Target == Kins.Self) return Array.Empty<RelValue>();
                    List<RelValue> result = new();
                    int limit = Target.ProgenitorCount - (Target.ProgenitorCount > 0 && Target.ProgenyCount > 0 ? 1 : 0);
                    for (int a = 0; a < limit; a++) result.Add(RelValue.Parent);
                    if (Target is NonLineal nonLin) result.Add(nonLin.IsHalf ? RelValue.HalfSibling : RelValue.FullSibling);
                    limit = Target.ProgenyCount - (Target.ProgenitorCount > 0 && Target.ProgenyCount > 0 ? 1 : 0);
                    for (int a = 0; a < limit; a++) result.Add(RelValue.Child);
                    return result.ToArray();
                }
            }

            public bool Equals(Basic? other) => other is not null && Target == other.Target;

            public override Rel With(RelValue value) => (Rel)value is Partner par ? new Partner(this, par.Status) : new Kinship(this, (Kinship.Basic)(Rel)value);

            public override string ToString(Gender? gender) => Target.ToString(gender);

            public override bool Equals(object? obj) => base.Equals(obj);

            public override int GetHashCode() => base.GetHashCode();

            public static Basic Get(Kin target)
            {
                if (!values.ContainsKey(target)) values.Add(target, new(target));
                return values[target];
            }

            public static implicit operator Basic(Kin target) => Get(target);

            public static bool operator ==(Basic left, Basic right) => left.Equals(right);

            public static bool operator !=(Basic left, Basic right) => !(left == right);
        }
    }
}