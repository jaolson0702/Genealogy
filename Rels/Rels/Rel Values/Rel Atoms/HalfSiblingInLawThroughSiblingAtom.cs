namespace Rels
{
    public class HalfSiblingInLawThroughSiblingAtom : RelAtom
    {
        public static readonly HalfSiblingInLawThroughSiblingAtom Get = new();

        private HalfSiblingInLawThroughSiblingAtom()
        { }

        public override RelValue WithChild => new NestedMolecule(this, ChildAtom.Get);
        public override RelValue WithFullSibling => new NestedMolecule(this, SiblingAtom.Get);
        public override RelValue WithHalfSibling => new NestedMolecule(this, HalfSiblingAtom.Get);
        public override RelValue WithParent => new NestedMolecule(HalfSiblingAtom.Get, ParentInLawAtom.Get);
        public override RelSubatomic[] SubatomicValues => new[] { RelSubatomic.HalfSibling, RelSubatomic.Spouse };

        public override string ToString(Gender? gender) => HalfSiblingAtom.Get.ToString(gender) + "-in-law (through sibling)";
    }
}