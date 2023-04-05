namespace Rels
{
    public class HalfSiblingInLawThroughSpouseAtom : RelAtom
    {
        public static readonly HalfSiblingInLawThroughSpouseAtom Get = new();

        private HalfSiblingInLawThroughSpouseAtom()
        { }

        public override RelValue WithChild => new NestedMolecule(SpouseAtom.Get, HalfNiblingAtom.Get);
        public override RelValue WithFullSibling => this;
        public override RelValue WithHalfSibling => new NestedMolecule(this, HalfSiblingAtom.Get);
        public override RelValue WithParent => new NestedMolecule(this, ParentAtom.Get);
        public override RelSubatomic[] SubatomicValues => new[] { RelSubatomic.Spouse, RelSubatomic.HalfSibling };

        public override string ToString(Gender? gender) => HalfSiblingAtom.Get.ToString(gender) + "-in-law (through spouse)";
    }
}