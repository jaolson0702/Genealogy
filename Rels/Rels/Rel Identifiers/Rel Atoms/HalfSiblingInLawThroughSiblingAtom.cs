namespace Rels
{
    public class HalfSiblingInLawThroughSiblingAtom : RelAtom
    {
        public static readonly HalfSiblingInLawThroughSiblingAtom Get = new();

        private HalfSiblingInLawThroughSiblingAtom()
        { }

        public override RelIdentifier[] WithSpouseAlternates => new RelIdentifier[] { HalfSiblingAtom.Get };
        public override RelIdentifier WithChildPrimary => new NestedMolecule(this, ChildAtom.Get);
        public override RelIdentifier[] WithChildAlternates => new RelIdentifier[] { HalfNiblingAtom.Get };
        public override RelIdentifier WithFullSiblingPrimary => new NestedMolecule(HalfSiblingAtom.Get, SiblingInLawThroughSpouseAtom.Get);
        public override RelIdentifier WithHalfSiblingPrimary => new NestedMolecule(HalfSiblingAtom.Get, HalfSiblingInLawThroughSpouseAtom.Get);
        public override RelIdentifier WithParentPrimary => new NestedMolecule(HalfSiblingAtom.Get, ParentInLawAtom.Get);
        public override RelSubatomic[] SubatomicValues => new[] { RelSubatomic.HalfSibling, RelSubatomic.Spouse };

        public override string ToString(Gender? gender) => HalfSiblingAtom.Get.ToString(gender) + "-in-law (through sibling)";
    }
}