namespace Rels
{
    public class ParentInLawAtom : RelAtom
    {
        public static readonly ParentInLawAtom Get = new();

        private ParentInLawAtom()
        { }

        public override RelSubatomic[] SubatomicValues => new[] { RelSubatomic.Spouse, RelSubatomic.Parent };

        public override string ToString(Gender? gender) => ParentAtom.Get.ToString(gender) + "-in-law";

        #region Additive Properties

        public override RelIdentifier WithChildPrimary => HalfSiblingInLawThroughSpouseAtom.Get;
        public override RelIdentifier[] WithChildAlternates => new RelIdentifier[] { SiblingInLawThroughSpouseAtom.Get, SpouseAtom.Get };
        public override RelIdentifier WithFullSiblingPrimary => new NestedMolecule(SpouseAtom.Get, PiblingAtom.Get);
        public override RelIdentifier WithHalfSiblingPrimary => new NestedMolecule(SpouseAtom.Get, HalfPiblingAtom.Get);
        public override RelIdentifier WithParentPrimary => new NestedMolecule(SpouseAtom.Get, GrandparentAtom.Get);

        #endregion Additive Properties
    }
}