﻿using System.ComponentModel.DataAnnotations;

namespace Rels
{
    public class GreatGrandchildMolecule([Range(1, int.MaxValue)] int generation) : RelMolecule
    {
        public readonly int Generation = generation;

        #region Additive Properties

        public override RelIdentifier WithChildPrimary => new GreatGrandchildMolecule(Generation + 1);

        public override RelIdentifier WithFullSiblingPrimary => this;

        public override RelIdentifier WithHalfSiblingPrimary => new NestedMolecule(this, HalfSiblingAtom.Get);

        public override RelIdentifier[] WithHalfSiblingAlternates => new RelIdentifier[] { this };

        public override RelIdentifier WithParentPrimary => new NestedMolecule(this, ParentAtom.Get);

        public override RelIdentifier[] WithParentAlternates => new RelIdentifier[] { GrandchildAtom.Get };

        #endregion Additive Properties

        public override RelAtom[] Atoms
        {
            get
            {
                List<RelAtom> result = [GrandchildAtom.Get];
                for (int a = 0; a < Generation; a++)
                {
                    if (result[^1] == GrandchildAtom.Get) result.Add(ChildAtom.Get);
                    else result[^1] = GrandchildAtom.Get;
                }
                return [.. result];
            }
        }

        public override string ToString(Gender? gender) => FormatUtil.GetGenerationPrefix(Generation + 2) + ChildAtom.Get.ToString(gender);
    }
}