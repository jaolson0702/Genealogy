using System.ComponentModel.DataAnnotations;

namespace Rels
{
    public class GreatGrandparentMolecule : RelMolecule
    {
        public readonly int Generation;

        public GreatGrandparentMolecule([Range(1, int.MaxValue)] int generation) => Generation = generation;

        public override RelValue WithChild => new PiblingMolecule(Generation + 1, true);

        public override RelValue WithFullSibling => new PiblingMolecule(Generation + 2, false);

        public override RelValue WithHalfSibling => new PiblingMolecule(Generation + 2, true);

        public override RelValue WithParent => new GreatGrandparentMolecule(Generation + 1);

        public override RelAtom[] Atoms
        {
            get
            {
                List<RelAtom> result = new() { GrandparentAtom.Get };
                for (int a = 0; a < Generation; a++)
                {
                    if (result[^1] == GrandparentAtom.Get) result.Add(ParentAtom.Get);
                    else result[^1] = GrandparentAtom.Get;
                }
                return result.ToArray();
            }
        }

        public override string ToString(Gender? gender) => FormatUtil.GetGenerationPrefix(Generation + 2) + ParentAtom.Get.ToString(gender);
    }
}