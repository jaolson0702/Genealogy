namespace Rels
{
    public class UnmarriedPartnerAtom : RelAtom
    {
        public static readonly UnmarriedPartnerAtom Get = new();

        private UnmarriedPartnerAtom()
        { }

        public override RelSubatomic[] SubatomicValues => [RelSubatomic.UnmarriedPartner];

        public override string ToString(Gender? gender) => "unmarried " + PartnerAtom.Get.ToString(gender);

        #region Additive Properties

        public override RelIdentifier[] WithUnmarriedPartnerAlternates => [SelfIdentifier.Get];
        public override RelIdentifier WithChildPrimary => new NestedMolecule(this, ChildAtom.Get);
        public override RelIdentifier[] WithChildAlternates => [ChildAtom.Get];
        public override RelIdentifier WithFullSiblingPrimary => new NestedMolecule(this, SiblingAtom.Get);
        public override RelIdentifier WithHalfSiblingPrimary => new NestedMolecule(this, HalfSiblingAtom.Get);
        public override RelIdentifier WithParentPrimary => new NestedMolecule(this, ParentAtom.Get);

        #endregion Additive Properties
    }
}