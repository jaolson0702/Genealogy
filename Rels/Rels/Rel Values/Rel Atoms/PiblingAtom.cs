namespace Rels
{
    public class PiblingAtom : RelAtom
    {
        public static readonly PiblingAtom Get = new();

        private PiblingAtom()
        { }

        public override RelValue WithChild => new CousinMolecule(1, 0, false);
        public override RelValue WithFullSibling => this;
        public override RelValue WithHalfSibling => HalfPiblingAtom.Get;
        public override RelValue WithParent => GrandparentAtom.Get;
        public override RelSubatomic[] SubatomicValues => new[] { RelSubatomic.Parent, RelSubatomic.FullSibling };

        public override string ToString(Gender? gender) => gender switch
        {
            Gender.Male => "uncle",
            Gender.Female => "aunt",
            _ => "pibling"
        };
    }
}