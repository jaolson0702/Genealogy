namespace Rels
{
    public class ChildAtom : RelAtom
    {
        public static readonly ChildAtom Get = new();

        private ChildAtom()
        { }

        public override RelValue WithChild => GrandchildAtom.Get;
        public override RelValue WithFullSibling => this;
        public override RelValue WithHalfSibling => new NestedMolecule(this, HalfSiblingAtom.Get);
        public override RelValue WithParent => new NestedMolecule(this, ParentAtom.Get);
        public override RelValue WithSpouse => ChildInLawAtom.Get;
        public override RelSubatomic[] SubatomicValues => new[] { RelSubatomic.Child };

        public override string ToString(Gender? gender) => gender switch
        {
            Gender.Male => "son",
            Gender.Female => "daughter",
            _ => "child"
        };
    }
}