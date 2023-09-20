namespace Rels
{
    public class SpouseAtom : RelAtom
    {
        public static readonly SpouseAtom Get = new();

        private SpouseAtom()
        { }

        public override RelIdentifier[] WithSpouseAlternates => new RelIdentifier[] { SelfIdentifier.Get };
        public override RelIdentifier WithChildPrimary => new NestedMolecule(this, ChildAtom.Get);
        public override RelIdentifier[] WithChildAlternates => new RelIdentifier[] { ChildAtom.Get };
        public override RelIdentifier WithFullSiblingPrimary => SiblingInLawThroughSpouseAtom.Get;
        public override RelIdentifier WithHalfSiblingPrimary => HalfSiblingInLawThroughSpouseAtom.Get;
        public override RelIdentifier WithParentPrimary => ParentInLawAtom.Get;

        public override RelSubatomic[] SubatomicValues => new[] { RelSubatomic.Spouse };

        public override string ToString(Gender? gender) => gender switch
        {
            Gender.Male => "husband",
            Gender.Female => "wife",
            _ => "spouse"
        };
    }
}