namespace Rels
{
    public class HalfPiblingAtom : RelAtom
    {
        public static readonly HalfPiblingAtom Get = new();

        private HalfPiblingAtom()
        { }

        public override RelSubatomic[] SubatomicValues => new[] { RelSubatomic.Parent, RelSubatomic.HalfSibling };

        public override string ToString(Gender? gender) => "half-" + PiblingAtom.Get.ToString(gender);

        #region Additive Properties

        public override RelIdentifier WithChildPrimary => new CousinMolecule(1, 0, true);
        public override RelIdentifier WithFullSiblingPrimary => this;
        public override RelIdentifier WithHalfSiblingPrimary => new NestedMolecule(this, HalfSiblingAtom.Get);
        public override RelIdentifier[] WithHalfSiblingAlternates => new RelIdentifier[] { PiblingAtom.Get, ParentAtom.Get };
        public override RelIdentifier WithParentPrimary => new NestedMolecule(this, ParentAtom.Get);
        public override RelIdentifier[] WithParentAlternates => new RelIdentifier[] { GrandparentAtom.Get };

        #endregion Additive Properties
    }
}