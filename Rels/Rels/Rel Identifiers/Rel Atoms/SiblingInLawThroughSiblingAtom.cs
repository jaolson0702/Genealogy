namespace Rels
{
    public class SiblingInLawThroughSiblingAtom : RelAtom
    {
        public static readonly SiblingInLawThroughSiblingAtom Get = new();

        private SiblingInLawThroughSiblingAtom()
        { }

        public override RelIdentifier[] WithSpouseAlternates => new RelIdentifier[] { SiblingAtom.Get };
        public override RelIdentifier WithChildPrimary => new NestedMolecule(this, ChildAtom.Get);
        public override RelIdentifier[] WithChildAlternates => new RelIdentifier[] { NiblingAtom.Get };
        public override RelIdentifier WithFullSiblingPrimary => new NestedMolecule(SiblingAtom.Get, SiblingInLawThroughSpouseAtom.Get);
        public override RelIdentifier WithHalfSiblingPrimary => new NestedMolecule(SiblingAtom.Get, HalfSiblingInLawThroughSpouseAtom.Get);
        public override RelIdentifier WithParentPrimary => new NestedMolecule(SiblingAtom.Get, ParentInLawAtom.Get);
        public override RelSubatomic[] SubatomicValues => new[] { RelSubatomic.FullSibling, RelSubatomic.Spouse };

        public override string ToString(Gender? gender) => SiblingAtom.Get.ToString(gender) + "-in-law (through sibling)";
    }
}