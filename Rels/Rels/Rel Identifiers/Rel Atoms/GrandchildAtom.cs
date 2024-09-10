namespace Rels
{
    public class GrandchildAtom : RelAtom
    {
        public static readonly GrandchildAtom Get = new();

        private GrandchildAtom()
        { }

        public override RelSubatomic[] SubatomicValues => new[] { RelSubatomic.Child, RelSubatomic.Child };

        public override string ToString(Gender? gender) => "grand" + ChildAtom.Get.ToString(gender);

        #region Additive Properties

        public override RelIdentifier WithChildPrimary => new GreatGrandchildMolecule(1);
        public override RelIdentifier WithFullSiblingPrimary => this;
        public override RelIdentifier WithHalfSiblingPrimary => new NestedMolecule(this, HalfSiblingAtom.Get);
        public override RelIdentifier[] WithHalfSiblingAlternates => new RelIdentifier[] { this };
        public override RelIdentifier WithParentPrimary => new NestedMolecule(this, ParentAtom.Get);
        public override RelIdentifier[] WithParentAlternates => new RelIdentifier[] { ChildAtom.Get, ChildInLawAtom.Get };

        #endregion Additive Properties
    }
}