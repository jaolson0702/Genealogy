namespace Rels
{
    public class HalfNiblingAtom : RelAtom
    {
        public static readonly HalfNiblingAtom Get = new();

        private HalfNiblingAtom()
        { }

        public override RelIdentifier WithChildPrimary => new NiblingMolecule(2, true);
        public override RelIdentifier WithFullSiblingPrimary => this;
        public override RelIdentifier WithHalfSiblingPrimary => new NestedMolecule(this, HalfSiblingAtom.Get);
        public override RelIdentifier[] WithHalfSiblingAlternates => new RelIdentifier[] { this };
        public override RelIdentifier WithParentPrimary => new NestedMolecule(this, ParentAtom.Get);
        public override RelIdentifier[] WithParentAlternates => new RelIdentifier[] { HalfSiblingAtom.Get };
        public override RelSubatomic[] SubatomicValues => new[] { RelSubatomic.HalfSibling, RelSubatomic.Child };

        public override string ToString(Gender? gender) => "half-" + NiblingAtom.Get.ToString(gender);
    }
}