namespace Rels
{
    public class NestedMolecule : RelMolecule
    {
        public readonly RelIdentifier Value1, Value2;

        public NestedMolecule(RelIdentifier value1, RelIdentifier value2)
        {
            (Value1, Value2) = (value1, value2);
            List<RelAtom> atoms = new(value1.GetAtoms());
            atoms.AddRange(value2.GetAtoms());
            Atoms = atoms.ToArray();
        }

        public override RelIdentifier[] WithPartnerAlternates => GetAlternates(Value2.WithPartnerAlternates);

        public override RelIdentifier[] WithUnmarriedPartnerAlternates => GetAlternates(Value2.WithUnmarriedPartnerAlternates);

        public override RelIdentifier[] WithSpouseAlternates => GetAlternates(Value2.WithSpouseAlternates);

        public override RelIdentifier WithChildPrimary => new NestedMolecule(Value1, Value2.WithChildPrimary);

        public override RelIdentifier[] WithChildAlternates => GetAlternates(Value2.WithChildAlternates);

        public override RelIdentifier WithFullSiblingPrimary => new NestedMolecule(Value1, Value2.WithFullSiblingPrimary);

        public override RelIdentifier[] WithFullSiblingAlternates => GetAlternates(Value2.WithFullSiblingAlternates);

        public override RelIdentifier WithHalfSiblingPrimary => new NestedMolecule(Value1, Value2.WithHalfSiblingPrimary);

        public override RelIdentifier[] WithHalfSiblingAlternates => GetAlternates(Value2.WithHalfSiblingAlternates);

        public override RelIdentifier WithParentPrimary => new NestedMolecule(Value1, Value2.WithParentPrimary);

        public override RelIdentifier[] WithParentAlternates => GetAlternates(Value2.WithParentAlternates);

        public override RelAtom[] Atoms { get; }

        public override string ToString(Gender? gender) => Value2.ToString(gender) + " of " + Value1;

        private RelIdentifier[] GetAlternates(RelIdentifier[] value2Alt)
        {
            List<NestedMolecule> nested = new();
            foreach (RelIdentifier value in value2Alt) nested.Add(new NestedMolecule(Value1, value));
            List<RelIdentifier> result = new();
            nested.ForEach(n => result.Add(n.Value2 == SelfIdentifier.Get ? Value1 : n));
            return result.ToArray();
        }
    }
}