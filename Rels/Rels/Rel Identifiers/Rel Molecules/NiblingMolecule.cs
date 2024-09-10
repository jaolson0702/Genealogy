using System.ComponentModel.DataAnnotations;

namespace Rels
{
    public class NiblingMolecule : RelMolecule
    {
        public readonly int Generation;
        public readonly bool IsHalf;

        public NiblingMolecule([Range(2, int.MaxValue)] int generation, bool isHalf = false) => (Generation, IsHalf) = (generation, isHalf);

        #region Additive Properties

        public override RelIdentifier WithChildPrimary => new NiblingMolecule(Generation + 1, IsHalf);

        public override RelIdentifier WithFullSiblingPrimary => this;

        public override RelIdentifier WithHalfSiblingPrimary => new NestedMolecule(this, HalfSiblingAtom.Get);

        public override RelIdentifier[] WithHalfSiblingAlternates => new RelIdentifier[] { this };

        public override RelIdentifier WithParentPrimary => new NestedMolecule(this, ParentAtom.Get);

        public override RelIdentifier[] WithParentAlternates
        {
            get
            {
                if (Generation == 2) return [IsHalf ? HalfNiblingAtom.Get : NiblingAtom.Get];
                return [new NiblingMolecule(Generation - 1, IsHalf)];
            }
        }

        #endregion Additive Properties

        public override RelAtom[] Atoms
        {
            get
            {
                List<RelAtom> result = [IsHalf ? RelSubatomic.HalfSibling : RelSubatomic.FullSibling, .. CalculationUtil.GetProgenyAtoms(Generation)];
                return [.. result];
            }
        }

        public override string ToString(Gender? gender) => (IsHalf ? "half-" : string.Empty) + FormatUtil.GetGenerationPrefix(Generation) + NiblingAtom.Get.ToString(gender);
    }
}