namespace Rels
{
    public class PartnerAtom : RelAtom
    {
        public static readonly PartnerAtom Get = new();

        private PartnerAtom()
        { }

        public override RelIdentifier[] WithPartnerAlternates => new RelIdentifier[] { SelfIdentifier.Get };
        public override RelIdentifier WithChildPrimary => new NestedMolecule(this, ChildAtom.Get);
        public override RelIdentifier WithFullSiblingPrimary => new NestedMolecule(this, SiblingAtom.Get);
        public override RelIdentifier WithHalfSiblingPrimary => new NestedMolecule(this, HalfSiblingAtom.Get);
        public override RelIdentifier WithParentPrimary => new NestedMolecule(this, ParentAtom.Get);

        public override RelSubatomic[] SubatomicValues => new[] { RelSubatomic.Partner };

        public override string ToString(Gender? gender) => "partner" + gender switch
        {
            Gender.Male => " (male)",
            Gender.Female => " (female)",
            _ => string.Empty
        };
    }
}