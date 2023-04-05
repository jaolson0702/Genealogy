namespace Rels
{
    public class NiblingAtom : RelAtom
    {
        public static readonly NiblingAtom Get = new();

        private NiblingAtom()
        { }

        public override RelValue WithChild => new NiblingMolecule(2, false);
        public override RelValue WithFullSibling => this;
        public override RelValue WithHalfSibling => new NestedMolecule(this, HalfSiblingAtom.Get);
        public override RelValue WithParent => new NestedMolecule(this, ParentAtom.Get);
        public override RelSubatomic[] SubatomicValues => new[] { RelSubatomic.FullSibling, RelSubatomic.Child };

        public override string ToString(Gender? gender) => gender switch
        {
            Gender.Male => "nephew",
            Gender.Female => "niece",
            _ => "nibling"
        };
    }
}