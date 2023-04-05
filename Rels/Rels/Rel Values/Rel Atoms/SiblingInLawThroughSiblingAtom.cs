namespace Rels
{
    public class SiblingInLawThroughSiblingAtom : RelAtom
    {
        public static readonly SiblingInLawThroughSiblingAtom Get = new();

        private SiblingInLawThroughSiblingAtom()
        { }

        public override RelValue WithChild => new NestedMolecule(this, ChildAtom.Get);
        public override RelValue WithFullSibling => new NestedMolecule(this, SiblingAtom.Get);
        public override RelValue WithHalfSibling => HalfSiblingInLawThroughSiblingAtom.Get;
        public override RelValue WithParent => new NestedMolecule(SiblingAtom.Get, ParentInLawAtom.Get);
        public override RelSubatomic[] SubatomicValues => new[] { RelSubatomic.FullSibling, RelSubatomic.Spouse };

        public override string ToString(Gender? gender) => SiblingAtom.Get.ToString(gender) + "-in-law (through sibling)";
    }
}