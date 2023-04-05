namespace Rels
{
    public class ParentAtom : RelAtom
    {
        public static readonly ParentAtom Get = new();

        private ParentAtom()
        { }

        public override RelValue WithChild => GrandparentAtom.Get;
        public override RelValue WithFullSibling => PiblingAtom.Get;
        public override RelValue WithHalfSibling => HalfPiblingAtom.Get;
        public override RelValue WithParent => GrandparentAtom.Get;
        public override RelSubatomic[] SubatomicValues => new[] { RelSubatomic.Parent };

        public override string ToString(Gender? gender) => gender switch
        {
            Gender.Male => "father",
            Gender.Female => "mother",
            _ => "parent"
        };
    }
}