namespace Rels
{
    public class PartnerAtom : RelAtom
    {
        public static readonly PartnerAtom Get = new();

        private PartnerAtom()
        { }

        public override RelValue WithChild => new NestedMolecule(this, ChildAtom.Get);
        public override RelValue WithFullSibling => new NestedMolecule(this, SiblingAtom.Get);
        public override RelValue WithHalfSibling => new NestedMolecule(this, HalfSiblingAtom.Get);
        public override RelValue WithParent => new NestedMolecule(this, ParentAtom.Get);

        public override RelSubatomic[] SubatomicValues => new[] { RelSubatomic.Partner };

        public override string ToString(Gender? gender) => "partner" + gender switch
        {
            Gender.Male => " (male)",
            Gender.Female => " (female)",
            _ => string.Empty
        };
    }
}