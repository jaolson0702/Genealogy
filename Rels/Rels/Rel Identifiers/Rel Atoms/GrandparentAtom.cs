namespace Rels
{
    public class GrandparentAtom : RelAtom
    {
        public static readonly GrandparentAtom Get = new();

        private GrandparentAtom()
        { }

        public override RelIdentifier[] WithPartnerAlternates => new RelIdentifier[] { this };
        public override RelIdentifier[] WithUnmarriedPartnerAlternates => new RelIdentifier[] { this };
        public override RelIdentifier[] WithSpouseAlternates => new RelIdentifier[] { this };
        public override RelIdentifier WithChildPrimary => HalfPiblingAtom.Get;
        public override RelIdentifier[] WithChildAlternates => new RelIdentifier[] { PiblingAtom.Get, ParentAtom.Get };
        public override RelIdentifier WithFullSiblingPrimary => new PiblingMolecule(2, false);
        public override RelIdentifier WithHalfSiblingPrimary => new PiblingMolecule(2, true);
        public override RelIdentifier WithParentPrimary => new GreatGrandparentMolecule(1);
        public override RelSubatomic[] SubatomicValues => new[] { RelSubatomic.Parent, RelSubatomic.Parent };

        public override string ToString(Gender? gender) => "grand" + ParentAtom.Get.ToString(gender);
    }
}