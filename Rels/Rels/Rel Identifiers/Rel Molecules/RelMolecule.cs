namespace Rels
{
    public abstract class RelMolecule : ValueIdentifier
    {
        public abstract RelAtom[] Atoms { get; }

        public override RelSubatomic[] SubatomicValues
        {
            get
            {
                List<RelSubatomic> result = [];
                Array.ForEach(Atoms, value => result.AddRange(value.SubatomicValues));
                return [.. result];
            }
        }

        public override string ToString() => ToString(null);
    }
}