using System.ComponentModel.DataAnnotations;

namespace Rels
{
    public class NiblingMolecule : RelMolecule
    {
        public readonly int Generation;
        public readonly bool IsHalf;

        public NiblingMolecule([Range(2, int.MaxValue)] int generation, bool isHalf = false) => (Generation, IsHalf) = (generation, isHalf);

        public override RelValue WithChild => new NiblingMolecule(Generation + 1, IsHalf);

        public override RelValue WithFullSibling => this;

        public override RelValue WithHalfSibling => new NestedMolecule(this, HalfSiblingAtom.Get);

        public override RelValue WithParent => new NestedMolecule(this, ParentAtom.Get);

        public override RelAtom[] Atoms
        {
            get
            {
                List<RelAtom> result = new() { IsHalf ? RelSubatomic.HalfSibling : RelSubatomic.FullSibling };
                result.AddRange(CalculationUtil.GetProgenyAtoms(Generation));
                return result.ToArray();
            }
        }

        public override string ToString(Gender? gender) => (IsHalf ? "half-" : string.Empty) + FormatUtil.GetGenerationPrefix(Generation) + NiblingAtom.Get.ToString(gender);
    }
}