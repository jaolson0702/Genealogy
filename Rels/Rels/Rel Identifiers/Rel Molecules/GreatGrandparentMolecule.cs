using System.ComponentModel.DataAnnotations;

namespace Rels
{
    public class GreatGrandparentMolecule([Range(1, int.MaxValue)] int generation) : RelMolecule
    {
        public readonly int Generation = generation;

        #region Additive Properties

        public override RelIdentifier[] WithPartnerAlternates => new RelIdentifier[] { this };

        public override RelIdentifier[] WithUnmarriedPartnerAlternates => new RelIdentifier[] { this };

        public override RelIdentifier[] WithSpouseAlternates => new RelIdentifier[] { this };

        public override RelIdentifier WithChildPrimary => new PiblingMolecule(Generation + 1, true);

        public override RelIdentifier[] WithChildAlternates => new RelIdentifier[] { new PiblingMolecule(Generation + 1), Generation == 1 ? GrandparentAtom.Get : new GreatGrandparentMolecule(Generation - 1) };

        public override RelIdentifier WithFullSiblingPrimary => new PiblingMolecule(Generation + 2, false);

        public override RelIdentifier WithHalfSiblingPrimary => new PiblingMolecule(Generation + 2, true);

        public override RelIdentifier WithParentPrimary => new GreatGrandparentMolecule(Generation + 1);

        #endregion Additive Properties

        public override RelAtom[] Atoms
        {
            get
            {
                List<RelAtom> result = [GrandparentAtom.Get];
                for (int a = 0; a < Generation; a++)
                {
                    if (result[^1] == GrandparentAtom.Get) result.Add(ParentAtom.Get);
                    else result[^1] = GrandparentAtom.Get;
                }
                return [.. result];
            }
        }

        public override string ToString(Gender? gender) => FormatUtil.GetGenerationPrefix(Generation + 2) + ParentAtom.Get.ToString(gender);
    }
}