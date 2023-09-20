namespace Rels
{
    public class Rel : IEquatable<Rel>
    {
        public readonly RelSubatomic[] SubatomicValues;

        public Rel(params RelIdentifier[] relValues)
        {
            RelIdentifier sum = SelfIdentifier.Get;
            Array.ForEach(relValues, relValue => sum += relValue);
            SubatomicValues = sum.SubatomicValues;
        }

        public Rel(int generation)
        {
            List<RelSubatomic> result = new();
            RelSubatomic subatomic = generation < 0 ? RelSubatomic.Child : RelSubatomic.Parent;
            for (int a = 0; a < Math.Abs(generation); a++) result.Add(subatomic);
            SubatomicValues = result.ToArray().GetIdentifier().SubatomicValues;
        }

        public bool Equals(Rel? other)
        {
            if (other is null) return false;
            if (SubatomicValues.Length != other.SubatomicValues.Length) return false;
            for (int a = 0; a < SubatomicValues.Length; a++)
                if (SubatomicValues[a] != other.SubatomicValues[a]) return false;
            return true;
        }

        public string ToString(Gender? gender) => SubatomicValues.GetIdentifier().ToString(gender);

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
            return result.ToArray().GetIdentifier();
        }

        public static Rel operator +(Rel left, RelSubatomic right) => new(new List<RelSubatomic>(left.SubatomicValues) { right }.ToArray().GetIdentifier());

        public static Rel operator +(Rel left, RelIdentifier right) => left + new Rel(right);

        public static Rel operator +(Rel left, Rel right)
        {
            Rel result = left;
            foreach (RelSubatomic subatomic in right.SubatomicValues) result += subatomic;
            return result;
        }

        public static implicit operator Rel(RelIdentifier[] values) => new(values);

        public static implicit operator Rel(RelIdentifier value) => new(value);

        public static implicit operator Rel(RelSubatomic[] subatomicValues) => new(subatomicValues.GetIdentifier());

        public static implicit operator Rel(RelSubatomic subatomic) => (RelIdentifier)subatomic;

        public static implicit operator Rel(int generation) => new(generation);
    }
}