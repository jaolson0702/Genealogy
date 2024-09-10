using System.Net.Http.Headers;

namespace Rels
{
    public sealed partial class Rel : IEquatable<Rel>
    {
        public readonly RelSubatomic[] SubatomicValues;

        #region Constructors

        private Rel(params RelIdentifier[] relValues)
        {
            RelIdentifier sum = SelfIdentifier.Get;
            Array.ForEach(relValues, relValue => sum += relValue);
            SubatomicValues = sum.SubatomicValues;
        }

        private Rel(int generation)
        {
            List<RelSubatomic> result = [];
            RelSubatomic subatomic = generation < 0 ? RelSubatomic.Child : RelSubatomic.Parent;
            for (int a = 0; a < Math.Abs(generation); a++) result.Add(subatomic);
            SubatomicValues = result.ToArray().GetIdentifier().SubatomicValues;
        }

        #endregion Constructors

        public RelType[] Types
        {
            get
            {
                if (SubatomicValues.Length == 0) return [];
                List<RelType> result = [GetTypeFrom(SubatomicValues[0])];
                for (int a = 1; a < SubatomicValues.Length; a++)
                {
                    switch (SubatomicValues[a])
                    {
                        case RelSubatomic.Child:
                            if (result[^1] == RelType.Child)
                                result[^1] = RelType.Descendant;
                            else if (result[^1] != RelType.Descendant)
                                result.Add(GetTypeFrom(SubatomicValues[a]));
                            break;

                        case RelSubatomic.FullSibling:
                            if (new RelType[] { RelType.Parent, RelType.Partner, RelType.Progenitor }.Contains(result[^1]))
                                result.Add(GetTypeFrom(SubatomicValues[a]));
                            break;

                        case RelSubatomic.HalfSibling:
                            if (result[^1] == RelType.FullSibling)
                                result[^1] = RelType.HalfSibling;
                            else
                                result.Add(GetTypeFrom(SubatomicValues[a]));
                            break;

                        case RelSubatomic.Parent:
                            if (result[^1] == RelType.FullSibling)
                                result[^1] = RelType.Parent;
                            else if (result[^1] == RelType.Parent)
                                result[^1] = RelType.Progenitor;
                            else if (result[^1] != RelType.Progenitor)
                                result.Add(GetTypeFrom(SubatomicValues[a]));
                            break;

                        default:
                            result.Add(GetTypeFrom(SubatomicValues[a]));
                            break;
                    }
                }

                return [.. result];
            }
        }

        private static RelType GetTypeFrom(RelSubatomic subatomic) => subatomic switch
        {
            RelSubatomic.Child => RelType.Child,
            RelSubatomic.FullSibling => RelType.FullSibling,
            RelSubatomic.HalfSibling => RelType.HalfSibling,
            RelSubatomic.Parent => RelType.Parent,
            _ => RelType.Partner
        };

        #region Overriden Methods

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

        #endregion Overriden Methods

        #region Operators

        public static bool operator ==(Rel left, Rel right) => left.Equals(right);

        public static bool operator !=(Rel left, Rel right) => !(left == right);

        public static Rel operator +(Rel given) => given;

        public static Rel operator !(Rel given)
        {
            List<RelSubatomic> result = [];
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

        public static bool operator >(Rel left, Rel right) => left.SubatomicValues.Length > right.SubatomicValues.Length;

        public static bool operator <(Rel left, Rel right)
        {
            if ((left == Partner || left == UnmarriedPartner || left == Spouse) && right.SubatomicValues.Length == 1) return true;
            if (left == Sibling && right.SubatomicValues.Length == 1) return true;
            return left.SubatomicValues.Length < right.SubatomicValues.Length;
        }

        #endregion Operators
    }
}