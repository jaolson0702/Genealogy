namespace Rels
{
    public class HalfSiblingAtom : RelAtom
    {
        public static readonly HalfSiblingAtom Get = new();

        private HalfSiblingAtom()
        { }

        public override RelValue WithChild => HalfNiblingAtom.Get;
        public override RelValue WithFullSibling => this;
        public override RelValue WithHalfSibling => new NestedMolecule(this, this);
        public override RelValue WithParent => new NestedMolecule(this, ParentAtom.Get);
        public override RelValue WithSpouse => HalfSiblingInLawThroughSiblingAtom.Get;
        public override RelSubatomic[] SubatomicValues => new[] { RelSubatomic.HalfSibling };

        public override string ToString(Gender? gender) => "half-" + SiblingAtom.Get.ToString(gender);
    }
}