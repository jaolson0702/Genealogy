namespace Rels
{
    public class ParentAtom : RelAtom
    {
        public static readonly ParentAtom Get = new();

        private ParentAtom()
        { }

        public override RelIdentifier WithChildPrimary => HalfSiblingAtom.Get;
        public override RelIdentifier[] WithChildAlternates => new RelIdentifier[] { SiblingAtom.Get, SelfIdentifier.Get };
        public override RelIdentifier WithFullSiblingPrimary => PiblingAtom.Get;
        public override RelIdentifier WithHalfSiblingPrimary => HalfPiblingAtom.Get;
        public override RelIdentifier WithParentPrimary => GrandparentAtom.Get;
        public override RelSubatomic[] SubatomicValues => new[] { RelSubatomic.Parent };

        public override string ToString(Gender? gender) => gender switch
        {
            Gender.Male => "father",
            Gender.Female => "mother",
            _ => "parent"
        };
    }
}