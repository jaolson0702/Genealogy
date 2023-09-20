namespace Rels
{
    public class SiblingAtom : RelAtom
    {
        public static readonly SiblingAtom Get = new();

        private SiblingAtom()
        { }

        public override RelIdentifier WithChildPrimary => NiblingAtom.Get;
        public override RelIdentifier WithFullSiblingPrimary => this;
        public override RelIdentifier WithHalfSiblingPrimary => HalfSiblingAtom.Get;
        public override RelIdentifier WithParentPrimary => ParentAtom.Get;
        public override RelIdentifier WithSpousePrimary => SiblingInLawThroughSiblingAtom.Get;
        public override RelSubatomic[] SubatomicValues => new[] { RelSubatomic.FullSibling };

        public override string ToString(Gender? gender) => gender switch
        {
            Gender.Male => "brother",
            Gender.Female => "sister",
            _ => "sibling"
        };
    }
}