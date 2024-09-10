namespace Rels
{
    public class HalfSiblingInLawThroughSpouseAtom : RelAtom
    {
        public static readonly HalfSiblingInLawThroughSpouseAtom Get = new();

        private HalfSiblingInLawThroughSpouseAtom()
        { }

        public override RelSubatomic[] SubatomicValues => [RelSubatomic.Spouse, RelSubatomic.HalfSibling];

        public override string ToString(Gender? gender) => HalfSiblingAtom.Get.ToString(gender) + "-in-law (through spouse)";

        #region Additive Properties

        public override RelIdentifier WithChildPrimary => new NestedMolecule(SpouseAtom.Get, HalfNiblingAtom.Get);
        public override RelIdentifier WithFullSiblingPrimary => this;
        public override RelIdentifier WithHalfSiblingPrimary => new NestedMolecule(this, HalfSiblingAtom.Get);
        public override RelIdentifier[] WithHalfSiblingAlternates => [SiblingInLawThroughSpouseAtom.Get, SpouseAtom.Get];
        public override RelIdentifier WithParentPrimary => new NestedMolecule(this, ParentAtom.Get);
        public override RelIdentifier[] WithParentAlternates => [ParentInLawAtom.Get];

        #endregion Additive Properties
    }
}