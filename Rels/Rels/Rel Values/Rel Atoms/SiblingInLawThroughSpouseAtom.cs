namespace Rels
{
    public class SiblingInLawThroughSpouseAtom : RelAtom
    {
        public static readonly SiblingInLawThroughSpouseAtom Get = new();

        private SiblingInLawThroughSpouseAtom()
        { }

        public override RelValue WithChild => new NestedMolecule(SpouseAtom.Get, NiblingAtom.Get);
        public override RelValue WithFullSibling => this;
        public override RelValue WithHalfSibling => HalfSiblingInLawThroughSpouseAtom.Get;
        public override RelValue WithParent => ParentInLawAtom.Get;
        public override RelSubatomic[] SubatomicValues => new[] { RelSubatomic.Spouse, RelSubatomic.FullSibling };

        public override string ToString(Gender? gender) => SiblingAtom.Get.ToString(gender) + "-in-law (through spouse)";
    }
}