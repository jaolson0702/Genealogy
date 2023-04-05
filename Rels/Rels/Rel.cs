namespace Rels
{
    public class Rel : IEquatable<Rel>
    {
        public readonly RelSubatomic[] SubatomicValues;

        public Rel(params RelSubatomic[] subatomicValues) => SubatomicValues = subatomicValues.Length > 0 ? CalculationUtil.GetValue(subatomicValues).SubatomicValues : subatomicValues;

        public Rel(RelValue relValue) : this(relValue.SubatomicValues)
        {
        }

        public bool Equals(Rel? other)
        {
            if (other is null) return false;
            if (SubatomicValues.Length != other.SubatomicValues.Length) return false;
            for (int a = 0; a < SubatomicValues.Length; a++)
                if (SubatomicValues[a] != other.SubatomicValues[a]) return false;
            return true;
        }

        public string ToString(Gender? gender) => SubatomicValues.Length > 0 ? CalculationUtil.GetValue(SubatomicValues).ToString(gender) : "self" + gender switch
        {
            Gender.Male => " (male)",
            Gender.Female => " (female)",
            _ => string.Empty
        };

        public override bool Equals(object? obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();

        public override string ToString() => ToString(null);

        public static bool operator ==(Rel left, Rel right) => left.Equals(right);

        public static bool operator !=(Rel left, Rel right) => !(left == right);

        public static Rel operator +(Rel given) => given;

        public static Rel operator !(Rel given)
        {
            List<RelSubatomic> result = new();
            foreach (RelSubatomic subatomic in given.SubatomicValues)
                result.Add(subatomic switch
                {
                    RelSubatomic.Child => RelSubatomic.Parent,
                    RelSubatomic.Parent => RelSubatomic.Child,
                    _ => subatomic
                });
            result.Reverse();
            return new(result.ToArray());
        }

        public static Rel operator +(Rel left, RelSubatomic right) => new(new List<RelSubatomic>(left.SubatomicValues) { right }.ToArray());

        public static Rel operator +(Rel left, Rel right)
        {
            Rel result = left;
            foreach (RelSubatomic subatomic in right.SubatomicValues) result += subatomic;
            return result;
        }

        public static implicit operator Rel(RelValue value) => new(value);

        public static implicit operator Rel(RelSubatomic subatomic) => (RelValue)subatomic;

        public static implicit operator Rel(int generation)
        {
            List<RelSubatomic> result = new();
            RelSubatomic subatomic = generation < 0 ? RelSubatomic.Child : RelSubatomic.Parent;
            for (int a = 0; a < Math.Abs(generation); a++) result.Add(subatomic);
            return new(result.ToArray());
        }
    }
}