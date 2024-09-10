namespace Rels
{
    public class SiblingInLawThroughSpouseAtom : RelAtom
    {
        public static readonly SiblingInLawThroughSpouseAtom Get = new();

        private SiblingInLawThroughSpouseAtom()
        { }

        public override RelSubatomic[] SubatomicValues => new[] { RelSubatomic.Spouse, RelSubatomic.FullSibling };

        public override string ToString(Gender? gender) => SiblingAtom.Get.ToString(gender) + "-in-law (through spouse)";

        #region Additive Properties

        public override RelIdentifier WithChildPrimary => new NestedMolecule(SpouseAtom.Get, NiblingAtom.Get);
        public override RelIdentifier WithFullSiblingPrimary => this;
        public override RelIdentifier[] WithFullSiblingAlternates => new RelIdentifier[] { SpouseAtom.Get };
        public override RelIdentifier WithHalfSiblingPrimary => HalfSiblingInLawThroughSpouseAtom.Get;
        public override RelIdentifier WithParentPrimary => ParentInLawAtom.Get;

        #endregion Additive Properties
    }
}