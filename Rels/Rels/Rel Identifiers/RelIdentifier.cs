namespace Rels
{
    public abstract class RelIdentifier : IEquatable<RelIdentifier>
    {
        public abstract RelSubatomic[] SubatomicValues { get; }

        public virtual RelIdentifier WithPartnerPrimary => new NestedMolecule(this, PartnerAtom.Get);
        public virtual RelIdentifier[] WithPartnerAlternates => Array.Empty<RelIdentifier>();
        public virtual RelIdentifier WithUnmarriedPartnerPrimary => new NestedMolecule(this, UnmarriedPartnerAtom.Get);
        public virtual RelIdentifier[] WithUnmarriedPartnerAlternates => Array.Empty<RelIdentifier>();
        public virtual RelIdentifier WithSpousePrimary => new NestedMolecule(this, SpouseAtom.Get);
        public virtual RelIdentifier[] WithSpouseAlternates => Array.Empty<RelIdentifier>();
        public abstract RelIdentifier WithChildPrimary { get; }
        public virtual RelIdentifier[] WithChildAlternates => Array.Empty<RelIdentifier>();
        public abstract RelIdentifier WithFullSiblingPrimary { get; }
        public virtual RelIdentifier[] WithFullSiblingAlternates => Array.Empty<RelIdentifier>();
        public abstract RelIdentifier WithHalfSiblingPrimary { get; }
        public virtual RelIdentifier[] WithHalfSiblingAlternates => Array.Empty<RelIdentifier>();
        public abstract RelIdentifier WithParentPrimary { get; }
        public virtual RelIdentifier[] WithParentAlternates => Array.Empty<RelIdentifier>();

        public abstract string ToString(Gender? gender);

        public RelIdentifier DefaultWith(RelSubatomic subatomic) => subatomic switch
        {
            RelSubatomic.Partner => WithPartnerPrimary,
            RelSubatomic.UnmarriedPartner => WithUnmarriedPartnerPrimary,
            RelSubatomic.Spouse => WithSpousePrimary,
            RelSubatomic.Child => WithChildPrimary,
            RelSubatomic.FullSibling => WithFullSiblingPrimary,
            RelSubatomic.HalfSibling => WithHalfSiblingPrimary,
            RelSubatomic.Parent => WithParentPrimary,
            _ => throw new NotImplementedException()
        };

        public RelIdentifier[] AlternatesWith(RelSubatomic subatomic) => subatomic switch
        {
            RelSubatomic.Partner => WithPartnerAlternates,
            RelSubatomic.UnmarriedPartner => WithUnmarriedPartnerAlternates,
            RelSubatomic.Spouse => WithSpouseAlternates,
            RelSubatomic.Child => WithChildAlternates,
            RelSubatomic.FullSibling => WithFullSiblingAlternates,
            RelSubatomic.HalfSibling => WithHalfSiblingAlternates,
            RelSubatomic.Parent => WithParentAlternates,
            _ => throw new NotImplementedException()
        };

        public RelIdentifier[] AllWith(RelSubatomic subatomic)
        {
            List<RelIdentifier> result = new() { DefaultWith(subatomic) };
            result.AddRange(AlternatesWith(subatomic));
            return result.ToArray();
        }

        public bool Equals(RelIdentifier? other)
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

        public static RelIdentifier operator +(RelIdentifier left, RelSubatomic right) => left.DefaultWith(right);

        public static RelIdentifier operator +(RelIdentifier left, RelIdentifier right)
        {
            RelIdentifier result = left;
            foreach (RelSubatomic subatomic in right.SubatomicValues) result += subatomic;
            return result;
        }

        public static bool operator ==(RelIdentifier left, RelIdentifier right) => left.Equals(right);

        public static bool operator !=(RelIdentifier left, RelIdentifier right) => !(left == right);

        public static implicit operator RelIdentifier(RelSubatomic subatomic) => (RelAtom)subatomic;
    }
}