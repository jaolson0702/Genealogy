namespace Rels
{
    public class SiblingAtom : RelAtom
    {
        public static readonly SiblingAtom Get = new();

        private SiblingAtom()
        { }

        public override RelValue WithChild => NiblingAtom.Get;
        public override RelValue WithFullSibling => this;
        public override RelValue WithHalfSibling => HalfSiblingAtom.Get;
        public override RelValue WithParent => ParentAtom.Get;
        public override RelValue WithSpouse => SiblingInLawThroughSiblingAtom.Get;
        public override RelSubatomic[] SubatomicValues => new[] { RelSubatomic.FullSibling };

        public override string ToString(Gender? gender) => gender switch
        {
            Gender.Male => "brother",
            Gender.Female => "sister",
            _ => "sibling"
        };
    }
}