namespace Rels
{
    public class ChildInLawAtom : RelAtom
    {
        public static readonly ChildInLawAtom Get = new();

        private ChildInLawAtom()
        { }

        public override RelValue WithChild => new NestedMolecule(this, ChildAtom.Get);
        public override RelValue WithFullSibling => new NestedMolecule(ChildAtom.Get, SiblingInLawThroughSpouseAtom.Get);
        public override RelValue WithHalfSibling => new NestedMolecule(ChildAtom.Get, HalfSiblingInLawThroughSpouseAtom.Get);
        public override RelValue WithParent => new NestedMolecule(ChildAtom.Get, ParentInLawAtom.Get);
        public override RelSubatomic[] SubatomicValues => new[] { RelSubatomic.Child, RelSubatomic.Spouse };

        public override string ToString(Gender? gender) => ChildAtom.Get.ToString(gender) + "-in-law";
    }
}