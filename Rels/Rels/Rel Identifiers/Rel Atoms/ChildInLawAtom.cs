namespace Rels
{
    public class ChildInLawAtom : RelAtom
    {
        public static readonly ChildInLawAtom Get = new();

        private ChildInLawAtom()
        { }

        public override RelIdentifier[] WithSpouseAlternates => new RelIdentifier[] { ChildAtom.Get };
        public override RelIdentifier WithChildPrimary => new NestedMolecule(this, ChildAtom.Get);
        public override RelIdentifier[] WithChildAlternates => new RelIdentifier[] { GrandchildAtom.Get };
        public override RelIdentifier WithFullSiblingPrimary => new NestedMolecule(ChildAtom.Get, SiblingInLawThroughSpouseAtom.Get);
        public override RelIdentifier WithHalfSiblingPrimary => new NestedMolecule(ChildAtom.Get, HalfSiblingInLawThroughSpouseAtom.Get);
        public override RelIdentifier WithParentPrimary => new NestedMolecule(ChildAtom.Get, ParentInLawAtom.Get);
        public override RelSubatomic[] SubatomicValues => new[] { RelSubatomic.Child, RelSubatomic.Spouse };

        public override string ToString(Gender? gender) => ChildAtom.Get.ToString(gender) + "-in-law";
    }
}