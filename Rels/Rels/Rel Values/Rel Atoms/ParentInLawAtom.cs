namespace Rels
{
    public class ParentInLawAtom : RelAtom
    {
        public static readonly ParentInLawAtom Get = new();

        private ParentInLawAtom()
        { }

        public override RelValue WithChild => HalfSiblingInLawThroughSpouseAtom.Get;
        public override RelValue WithFullSibling => new NestedMolecule(SpouseAtom.Get, PiblingAtom.Get);
        public override RelValue WithHalfSibling => new NestedMolecule(SpouseAtom.Get, HalfPiblingAtom.Get);
        public override RelValue WithParent => new NestedMolecule(SpouseAtom.Get, GrandparentAtom.Get);
        public override RelSubatomic[] SubatomicValues => new[] { RelSubatomic.Spouse, RelSubatomic.Parent };

        public override string ToString(Gender? gender) => ParentAtom.Get.ToString(gender) + "-in-law";
    }
}