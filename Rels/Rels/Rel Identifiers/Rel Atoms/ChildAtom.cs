namespace Rels
{
    public class ChildAtom : RelAtom
    {
        public static readonly ChildAtom Get = new();

        private ChildAtom()
        { }

        public override RelSubatomic[] SubatomicValues => [RelSubatomic.Child];

        public override string ToString(Gender? gender) => gender switch
        {
            Gender.Male => "son",
            Gender.Female => "daughter",
            _ => "child"
        };

        #region Additive Properties

        public override RelIdentifier WithSpousePrimary => ChildInLawAtom.Get;
        public override RelIdentifier WithChildPrimary => GrandchildAtom.Get;
        public override RelIdentifier WithFullSiblingPrimary => this;
        public override RelIdentifier WithHalfSiblingPrimary => new NestedMolecule(this, HalfSiblingAtom.Get);
        public override RelIdentifier[] WithHalfSiblingAlternates => [this];
        public override RelIdentifier WithParentPrimary => new NestedMolecule(this, ParentAtom.Get);

        #endregion Additive Properties
    }
}