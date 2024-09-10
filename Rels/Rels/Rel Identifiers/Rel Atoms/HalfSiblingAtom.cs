namespace Rels
{
    public class HalfSiblingAtom : RelAtom
    {
        public static readonly HalfSiblingAtom Get = new();

        private HalfSiblingAtom()
        { }

        public override RelSubatomic[] SubatomicValues => new[] { RelSubatomic.HalfSibling };

        public override string ToString(Gender? gender) => "half-" + SiblingAtom.Get.ToString(gender);

        #region Additive Properties

        public override RelIdentifier WithChildPrimary => HalfNiblingAtom.Get;
        public override RelIdentifier WithFullSiblingPrimary => this;
        public override RelIdentifier WithHalfSiblingPrimary => new NestedMolecule(this, this);
        public override RelIdentifier[] WithHalfSiblingAlternates => new RelIdentifier[] { SiblingAtom.Get, SelfIdentifier.Get };
        public override RelIdentifier WithParentPrimary => new NestedMolecule(this, ParentAtom.Get);
        public override RelIdentifier[] WithParentAlternates => new RelIdentifier[] { ParentAtom.Get };
        public override RelIdentifier WithSpousePrimary => HalfSiblingInLawThroughSiblingAtom.Get;

        #endregion Additive Properties
    }
}