namespace Rels
{
    public abstract class RelMolecule : RelValue
    {
        public abstract RelAtom[] Atoms { get; }

        public override RelSubatomic[] SubatomicValues
        {
            get
            {
                List<RelSubatomic> result = new();
                Array.ForEach(Atoms, value => result.AddRange(value.SubatomicValues));
                return result.ToArray();
            }
        }

        public override string ToString() => ToString(null);
    }
}