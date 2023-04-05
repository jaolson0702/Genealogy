namespace Rels
{
    public class SpouseAtom : RelAtom
    {
        public static readonly SpouseAtom Get = new();

        private SpouseAtom()
        { }

        public override RelValue WithChild => new NestedMolecule(this, ChildAtom.Get);
        public override RelValue WithFullSibling => SiblingInLawThroughSpouseAtom.Get;
        public override RelValue WithHalfSibling => HalfSiblingInLawThroughSpouseAtom.Get;
        public override RelValue WithParent => ParentInLawAtom.Get;

        public override RelSubatomic[] SubatomicValues => new[] { RelSubatomic.Spouse };

        public override string ToString(Gender? gender) => gender switch
        {
            Gender.Male => "husband",
            Gender.Female => "wife",
            _ => "spouse"
        };
    }
}