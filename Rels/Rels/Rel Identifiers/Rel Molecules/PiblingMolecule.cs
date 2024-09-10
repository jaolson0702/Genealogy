using System.ComponentModel.DataAnnotations;

namespace Rels
{
    public class PiblingMolecule : RelMolecule
    {
        public readonly int Generation;
        public readonly bool IsHalf;

        public PiblingMolecule([Range(2, int.MaxValue)] int generation, bool isHalf = false) => (Generation, IsHalf) = (generation, isHalf);

        #region Additive Properties

        public override RelIdentifier WithChildPrimary => new CousinMolecule(1, Generation - 1, IsHalf);

        public override RelIdentifier WithFullSiblingPrimary => this;

        public override RelIdentifier WithHalfSiblingPrimary => !IsHalf ? new PiblingMolecule(Generation, true) : new NestedMolecule(this, HalfSiblingAtom.Get);

        public override RelIdentifier[] WithHalfSiblingAlternates => IsHalf ? [this, new PiblingMolecule(Generation, false)] : [];

        public override RelIdentifier WithParentPrimary => !IsHalf ? new GreatGrandparentMolecule(Generation - 1) : new NestedMolecule(this, ParentAtom.Get);

        public override RelIdentifier[] WithParentAlternates => IsHalf ? [new GreatGrandparentMolecule(Generation - 1)] : [];

        #endregion Additive Properties

        public override RelAtom[] Atoms => new List<RelAtom>(CalculationUtil.GetProgenitorAtoms(Generation)) { IsHalf ? RelSubatomic.HalfSibling : RelSubatomic.FullSibling }.ToArray();

        public override string ToString(Gender? gender) => (IsHalf ? "half-" : string.Empty) + FormatUtil.GetGenerationPrefix(Generation) + PiblingAtom.Get.ToString(gender);
    }
}