using KinshipCompute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelCompute
{
    public abstract class Rel : IEquatable<Rel>
    {
        public abstract RelValue[] Values { get; }

        public bool Equals(Rel? other)
        {
            if (other is null || Values.Length != other.Values.Length) return false;
            for (int a = 0; a < Values.Length; a++)
                if (Values[a] != other.Values[a]) return false;
            return true;
        }

        public abstract Rel With(RelValue value);

        public abstract string ToString(Gender? gender);

        public override bool Equals(object? obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();

        public override string ToString() => ToString(null);

        public static implicit operator Rel(Kin kin) => Kinship.Basic.Get(kin);

        public static implicit operator Rel(PartnerStatus status) => status.IsMarried switch
        {
            true => RelValue.Spouse,
            false => RelValue.UnmarriedPartner,
            _ => RelValue.Partner
        };

        public static implicit operator Rel(RelValue value) => value switch
        {
            RelValue.Parent => Kins.Parent,
            RelValue.Child => Kins.Child,
            RelValue.FullSibling => Kins.Sibling,
            RelValue.HalfSibling => Kins.Sibling.ToHalf(),
            RelValue.Spouse => new Partner(PartnerStatus.Married),
            RelValue.UnmarriedPartner => new Partner(PartnerStatus.Unmarried),
            _ => new Partner(PartnerStatus.Unknown)
        };

        public static implicit operator Rel(RelValue[] values)
        {
            Rel result = Kins.Self;
            foreach (RelValue value in values) result = result.With(value);
            return result;
        }

        public static implicit operator Rel(Rel[] rels)
        {
            Rel result = new Kinship();
            foreach (Rel rel in rels) result += rel;
            return result;
        }

        public static bool operator ==(Rel left, Rel right) => left.Equals(right);

        public static bool operator !=(Rel left, Rel right) => !(left == right);

        public static Rel operator +(Rel left, RelValue right) => left.With(right);

        public static Rel operator +(Rel left, Rel right)
        {
            List<RelValue> result = new(left.Values);
            result.AddRange(right.Values);
            return result.ToArray();
        }
    }
}