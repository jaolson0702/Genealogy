namespace Rels
{
    public class NiblingAtom : RelAtom
    {
        public static readonly NiblingAtom Get = new();

        private NiblingAtom()
        { }

        public override RelIdentifier WithChildPrimary => new NiblingMolecule(2, false);
        public override RelIdentifier WithFullSiblingPrimary => this;
        public override RelIdentifier WithHalfSiblingPrimary => new NestedMolecule(this, HalfSiblingAtom.Get);
        public override RelIdentifier[] WithHalfSiblingAlternates => new RelIdentifier[] { this };
        public override RelIdentifier WithParentPrimary => new NestedMolecule(this, ParentAtom.Get);
        public override RelIdentifier[] WithParentAlternates => new RelIdentifier[] { SiblingAtom.Get };
        public override RelSubatomic[] SubatomicValues => new[] { RelSubatomic.FullSibling, RelSubatomic.Child };

        public override string ToString(Gender? gender) => gender switch
        {
            Gender.Male => "nephew",
            Gender.Female => "niece",
            _ => "nibling"
        };
    }
}