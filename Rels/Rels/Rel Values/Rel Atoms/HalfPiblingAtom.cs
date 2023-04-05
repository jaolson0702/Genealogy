namespace Rels
{
    public class HalfPiblingAtom : RelAtom
    {
        public static readonly HalfPiblingAtom Get = new();

        private HalfPiblingAtom()
        { }

        public override RelValue WithChild => new CousinMolecule(1, 0, true);
        public override RelValue WithFullSibling => this;
        public override RelValue WithHalfSibling => new NestedMolecule(this, HalfSiblingAtom.Get);
        public override RelValue WithParent => new NestedMolecule(this, ParentAtom.Get);
        public override RelSubatomic[] SubatomicValues => new[] { RelSubatomic.Parent, RelSubatomic.HalfSibling };

        public override string ToString(Gender? gender) => "half-" + PiblingAtom.Get.ToString(gender);
    }
}