namespace Rels
{
    public class GrandchildAtom : RelAtom
    {
        public static readonly GrandchildAtom Get = new();

        private GrandchildAtom()
        { }

        public override RelValue WithChild => new GreatGrandchildMolecule(1);
        public override RelValue WithFullSibling => this;
        public override RelValue WithHalfSibling => new NestedMolecule(this, HalfSiblingAtom.Get);
        public override RelValue WithParent => new NestedMolecule(this, ParentAtom.Get);
        public override RelSubatomic[] SubatomicValues => new[] { RelSubatomic.Child, RelSubatomic.Child };

        public override string ToString(Gender? gender) => "grand" + ChildAtom.Get.ToString(gender);
    }
}