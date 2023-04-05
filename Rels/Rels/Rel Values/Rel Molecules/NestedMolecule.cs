namespace Rels
{
    public class NestedMolecule : RelMolecule
    {
        public readonly RelValue Value1, Value2;

        public NestedMolecule(RelValue value1, RelValue value2)
        {
            (Value1, Value2) = (value1, value2);
            List<RelAtom> atoms = new(CalculationUtil.GetAtoms(value1));
            atoms.AddRange(CalculationUtil.GetAtoms(value2));
            Atoms = atoms.ToArray();
        }

        public override RelValue WithChild => new NestedMolecule(Value1, Value2.WithChild);

        public override RelValue WithFullSibling => new NestedMolecule(Value1, Value2.WithFullSibling);

        public override RelValue WithHalfSibling => new NestedMolecule(Value1, Value2.WithHalfSibling);

        public override RelValue WithParent => new NestedMolecule(Value1, Value2.WithParent);

        public override RelAtom[] Atoms { get; }

        public override string ToString(Gender? gender) => Value2.ToString(gender) + " of " + Value1;
    }
}