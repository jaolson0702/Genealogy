namespace Rels
{
    public class UnmarriedPartnerAtom : RelAtom
    {
        public static readonly UnmarriedPartnerAtom Get = new();

        private UnmarriedPartnerAtom()
        { }

        public override RelValue WithChild => new NestedMolecule(this, ChildAtom.Get);
        public override RelValue WithFullSibling => new NestedMolecule(this, SiblingAtom.Get);
        public override RelValue WithHalfSibling => new NestedMolecule(this, HalfSiblingAtom.Get);
        public override RelValue WithParent => new NestedMolecule(this, ParentAtom.Get);

        public override RelSubatomic[] SubatomicValues => new[] { RelSubatomic.UnmarriedPartner };

        public override string ToString(Gender? gender) => "unmarried " + PartnerAtom.Get.ToString(gender);
    }
}