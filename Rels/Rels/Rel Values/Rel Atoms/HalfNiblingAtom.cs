namespace Rels
{
    public class HalfNiblingAtom : RelAtom
    {
        public static readonly HalfNiblingAtom Get = new();

        private HalfNiblingAtom()
        { }

        public override RelValue WithChild => new NiblingMolecule(2, true);
        public override RelValue WithFullSibling => this;
        public override RelValue WithHalfSibling => new NestedMolecule(this, HalfSiblingAtom.Get);
        public override RelValue WithParent => new NestedMolecule(this, ParentAtom.Get);
        public override RelSubatomic[] SubatomicValues => new[] { RelSubatomic.HalfSibling, RelSubatomic.Child };

        public override string ToString(Gender? gender) => "half-" + NiblingAtom.Get.ToString(gender);
    }
}