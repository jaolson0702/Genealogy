namespace Rels
{
    public class UnmarriedPartnerAtom : RelAtom
    {
        public static readonly UnmarriedPartnerAtom Get = new();

        private UnmarriedPartnerAtom()
        { }

        public override RelIdentifier[] WithUnmarriedPartnerAlternates => new RelIdentifier[] { SelfIdentifier.Get };
        public override RelIdentifier WithChildPrimary => new NestedMolecule(this, ChildAtom.Get);
        public override RelIdentifier[] WithChildAlternates => new RelIdentifier[] { ChildAtom.Get };
        public override RelIdentifier WithFullSiblingPrimary => new NestedMolecule(this, SiblingAtom.Get);
        public override RelIdentifier WithHalfSiblingPrimary => new NestedMolecule(this, HalfSiblingAtom.Get);
        public override RelIdentifier WithParentPrimary => new NestedMolecule(this, ParentAtom.Get);

        public override RelSubatomic[] SubatomicValues => new[] { RelSubatomic.UnmarriedPartner };

        public override string ToString(Gender? gender) => "unmarried " + PartnerAtom.Get.ToString(gender);
    }
}