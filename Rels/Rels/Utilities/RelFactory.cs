using System.ComponentModel.DataAnnotations;

namespace Rels
{
    public static class RelFactory
    {
        public static readonly Rel Self = new();

        public static readonly RelAtom Sibling = SiblingAtom.Get;

        public static readonly RelAtom HalfSibling = HalfSiblingAtom.Get;

        public static RelIdentifier Progenitor([Range(1, int.MaxValue)] int generation) => generation switch
        {
            1 => ParentAtom.Get,
            2 => GrandparentAtom.Get,
            _ => new GreatGrandparentMolecule(generation - 2)
        };

        public static RelIdentifier Progeny([Range(1, int.MaxValue)] int generation) => generation switch
        {
            1 => ChildAtom.Get,
            2 => GrandchildAtom.Get,
            _ => new GreatGrandchildMolecule(generation - 2)
        };

        public static RelIdentifier Pibling([Range(1, int.MaxValue)] int generation = 1) => generation == 1 ? PiblingAtom.Get : new PiblingMolecule(generation);

        public static RelIdentifier HalfPibling([Range(1, int.MaxValue)] int generation = 1) => generation == 1 ? HalfPiblingAtom.Get : new PiblingMolecule(generation, true);

        public static RelIdentifier Nibling([Range(1, int.MaxValue)] int generation = 1) => generation == 1 ? NiblingAtom.Get : new NiblingMolecule(generation);

        public static RelIdentifier HalfNibling([Range(1, int.MaxValue)] int generation = 1) => generation == 1 ? HalfNiblingAtom.Get : new NiblingMolecule(generation, true);

        public static RelMolecule Cousin([Range(1, int.MaxValue)] int degree = 1, int timesRemoved = 0) => new CousinMolecule(degree, timesRemoved);

        public static RelMolecule HalfCousin([Range(1, int.MaxValue)] int degree = 1, int timesRemoved = 0) => new CousinMolecule(degree, timesRemoved, true);

        public static RelAtom Partner(bool? maritalStatus = null) => maritalStatus switch
        {
            true => SpouseAtom.Get,
            false => UnmarriedPartnerAtom.Get,
            _ => PartnerAtom.Get
        };

        public static RelAtom ParentInLaw() => ParentInLawAtom.Get;

        public static RelAtom ChildInLaw() => ChildInLawAtom.Get;

        public static RelAtom SiblingInLaw(bool throughSpouse) => throughSpouse ? SiblingInLawThroughSpouseAtom.Get : SiblingInLawThroughSiblingAtom.Get;

        public static RelAtom HalfSiblingInLaw(bool throughSpouse) => throughSpouse ? HalfSiblingInLawThroughSpouseAtom.Get : HalfSiblingInLawThroughSiblingAtom.Get;
    }
}