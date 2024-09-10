using System.ComponentModel.DataAnnotations;

namespace Rels
{
    public class CousinMolecule : RelMolecule
    {
        public readonly int Degree, TimesRemoved;
        public readonly bool IsHalf;

        #region Constructors

        public CousinMolecule([Range(1, int.MaxValue)] int degree, int timesRemoved, bool isHalf = false) => (Degree, TimesRemoved, IsHalf) = (degree, timesRemoved, isHalf);

        public CousinMolecule([Range(1, int.MaxValue)] int degree, bool isHalf = false) : this(degree, 0, isHalf)
        {
        }

        public CousinMolecule(bool isHalf = false) : this(1, isHalf)
        {
        }

        #endregion Constructors

        #region Additive Properties

        public override RelIdentifier WithChildPrimary => new CousinMolecule(Degree + (TimesRemoved > 0 ? 1 : 0), TimesRemoved - 1, IsHalf);

        public override RelIdentifier WithFullSiblingPrimary => this;

        public override RelIdentifier WithHalfSiblingPrimary => new NestedMolecule(this, HalfSiblingAtom.Get);

        public override RelIdentifier[] WithHalfSiblingAlternates => new RelIdentifier[] { this };

        public override RelIdentifier WithParentPrimary => new NestedMolecule(this, ParentAtom.Get);

        public override RelIdentifier[] WithParentAlternates
        {
            get
            {
                if (Degree == 1 && TimesRemoved == 0) return [IsHalf ? HalfPiblingAtom.Get : PiblingAtom.Get];
                if (Degree == 1) return [new PiblingMolecule(TimesRemoved + 1, IsHalf)];
                return [new CousinMolecule(Degree - (TimesRemoved > 0 ? 1 : 0), TimesRemoved + 1, IsHalf)];
            }
        }

        #endregion Additive Properties

        public override RelAtom[] Atoms
        {
            get
            {
                int up = TimesRemoved >= 0 ? Degree + TimesRemoved : Degree;
                int down = TimesRemoved >= 0 ? Degree : Degree - TimesRemoved;
                List<RelAtom> result = up == 1 ? new() { !IsHalf ? PiblingAtom.Get : HalfPiblingAtom.Get } : new(new PiblingMolecule(up, IsHalf).Atoms);
                result.AddRange(CalculationUtil.GetProgenyAtoms(down));
                return [.. result];
            }
        }

        public override string ToString(Gender? gender) => (IsHalf ? "half " : string.Empty) + FormatUtil.GetNumberEnding(Degree) + " cousin" + Math.Abs(TimesRemoved) switch
        {
            0 => string.Empty,
            1 => " once removed",
            2 => " twice removed",
            3 => " thrice removed",
            _ => $" {Math.Abs(TimesRemoved)} times removed"
        } + (TimesRemoved < 0 ? " down" : string.Empty) + gender switch
        {
            Gender.Male => " (male)",
            Gender.Female => " (female)",
            _ => string.Empty
        };
    }
}