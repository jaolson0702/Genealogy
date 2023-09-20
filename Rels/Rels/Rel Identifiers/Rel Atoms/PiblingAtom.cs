namespace Rels
{
    public class PiblingAtom : RelAtom
    {
        public static readonly PiblingAtom Get = new();

        private PiblingAtom()
        { }

        public override RelIdentifier WithChildPrimary => new CousinMolecule(1, 0, false);
        public override RelIdentifier WithFullSiblingPrimary => this;
        public override RelIdentifier WithHalfSiblingPrimary => HalfPiblingAtom.Get;
        public override RelIdentifier WithParentPrimary => GrandparentAtom.Get;
        public override RelSubatomic[] SubatomicValues => new[] { RelSubatomic.Parent, RelSubatomic.FullSibling };

        public override string ToString(Gender? gender) => gender switch
        {
            Gender.Male => "uncle",
            Gender.Female => "aunt",
            _ => "pibling"
        };
    }
}