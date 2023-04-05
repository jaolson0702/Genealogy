namespace Rels
{
    public abstract class RelValue : IEquatable<RelValue>
    {
        public abstract RelSubatomic[] SubatomicValues { get; }

        public RelValue WithPartner => new NestedMolecule(this, PartnerAtom.Get);
        public RelValue WithUnmarriedPartner => new NestedMolecule(this, UnmarriedPartnerAtom.Get);
        public virtual RelValue WithSpouse => new NestedMolecule(this, SpouseAtom.Get);
        public abstract RelValue WithChild { get; }
        public abstract RelValue WithFullSibling { get; }
        public abstract RelValue WithHalfSibling { get; }
        public abstract RelValue WithParent { get; }

        public abstract string ToString(Gender? gender);

        public RelValue With(RelSubatomic subatomic) => subatomic switch
        {
            RelSubatomic.Partner => WithPartner,
            RelSubatomic.UnmarriedPartner => WithUnmarriedPartner,
            RelSubatomic.Spouse => WithSpouse,
            RelSubatomic.Child => WithChild,
            RelSubatomic.FullSibling => WithFullSibling,
            RelSubatomic.HalfSibling => WithHalfSibling,
            RelSubatomic.Parent => WithParent,
            _ => throw new NotImplementedException()
        };

        public bool Equals(RelValue? other)
        {
            if (other is null) return false;
            if (SubatomicValues.Length != other.SubatomicValues.Length) return false;
            for (int a = 0; a < SubatomicValues.Length; a++)
                if (SubatomicValues[a] != other.SubatomicValues[a]) return false;
            return true;
        }

        public override bool Equals(object? obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();

        public override string ToString() => ToString(null);

        public static RelValue operator +(RelValue left, RelSubatomic right) => left.With(right);

        public static RelValue operator +(RelValue left, RelValue right)
        {
            RelValue result = left;
            foreach (RelSubatomic subatomic in right.SubatomicValues) result += subatomic;
            return result;
        }

        public static bool operator ==(RelValue left, RelValue right) => left.Equals(right);

        public static bool operator !=(RelValue left, RelValue right) => !(left == right);

        public static implicit operator RelValue(RelSubatomic subatomic) => (RelAtom)subatomic;
    }
}