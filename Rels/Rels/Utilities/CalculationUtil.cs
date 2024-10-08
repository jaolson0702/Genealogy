﻿using System.ComponentModel.DataAnnotations;

namespace Rels
{
    public static class CalculationUtil
    {
        public static RelAtom[] GetProgenitorAtoms([Range(1, int.MaxValue)] int generation) => generation switch
        {
            1 => new[] { ParentAtom.Get },
            2 => new[] { GrandparentAtom.Get },
            _ => new GreatGrandparentMolecule(generation - 2).Atoms
        };

        public static RelAtom[] GetProgenyAtoms([Range(1, int.MaxValue)] int generation) => generation switch
        {
            1 => new[] { ChildAtom.Get },
            2 => new[] { GrandchildAtom.Get },
            _ => new GreatGrandchildMolecule(generation - 2).Atoms
        };

        public static RelAtom[] GetAtoms(this RelIdentifier value)
        {
            if (value is RelAtom atom) return [atom];
            if (value is RelMolecule molecule) return molecule.Atoms;
            throw new NotImplementedException();
        }

        public static RelIdentifier GetIdentifier(this RelSubatomic[] subatomicValues)
        {
            if (subatomicValues.Length == 0) return SelfIdentifier.Get;
            RelIdentifier result = subatomicValues[0];
            for (int a = 1; a < subatomicValues.Length; a++)
                result = result.DefaultWith(subatomicValues[a]);
            return result;
        }
    }
}