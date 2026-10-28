using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rels
{
    public class BloodRel : IEquatable<BloodRel>
    {
        private bool isHalf = false;

        public readonly int ProgenitorCount = 0;
        public readonly int DescendantCount = 0;

        public BloodRel(int pCount, int dCount, bool isHalf = false)
        {
            if (pCount < 0 || dCount < 0)
            {
                throw new ArgumentException($"Progenitor count and descendant count must not be negative.");
            }

            ProgenitorCount = pCount;
            DescendantCount = dCount;
            IsHalf = isHalf;
        }

        public BloodRel(params RelSubatomic[] subatomicValues)
        {
            if (subatomicValues.Contains(RelSubatomic.Partner)
                || subatomicValues.Contains(RelSubatomic.UnmarriedPartner)
                || subatomicValues.Contains(RelSubatomic.Spouse))
            {
            }
        }

        public bool IsHalf
        {
            get
            {
                return isHalf;
            }

            set
            {
                if (ProgenitorCount != 0 && DescendantCount != 0)
                {
                    isHalf = value;
                }
            }
        }

        public Rel Rel
        {
            get
            {
                if (ProgenitorCount == 0 ^ DescendantCount == 0)
                {
                    return (Rel)(ProgenitorCount != 0 ? ProgenitorCount : -DescendantCount);
                }

                return (Rel)(ProgenitorCount - 1)
                    + (Rel)(IsHalf ? RelSubatomic.HalfSibling : RelSubatomic.FullSibling)
                    + (Rel)(1 - DescendantCount);
            }
        }

        public static BloodRel GetClosestCommonProgenitor(BloodRel left, BloodRel right)
        {
            return new(Math.Max(left.ProgenitorCount, right.ProgenitorCount), 0);
        }

        public bool IsCommonProgenitorOf(BloodRel left, BloodRel right)
        {
            var closestCommonProgenitor = GetClosestCommonProgenitor(left, right);

            if (closestCommonProgenitor == null)
            {
                return false;
            }

            return ProgenitorCount >= closestCommonProgenitor.ProgenitorCount;
        }

        public bool Equals(BloodRel? other)
        {
            return other is not null
                && ProgenitorCount == other.ProgenitorCount
                && DescendantCount == other.DescendantCount
                && IsHalf == other.IsHalf;
        }

        public override bool Equals(object? obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();

        public static bool operator ==(BloodRel left, BloodRel right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(BloodRel left, BloodRel right)
        {
            return !left.Equals(right);
        }

        public static implicit operator BloodRel(int generation)
        {
            return generation > 0 ? new(generation, 0) : new(0, -generation);
        }

        /*
        public static BloodRel operator -(BloodRel end, BloodRel start)
        {
            if (start == end)
            {
                return 0;
            }

            var commonProgenitor = GetClosestCommonProgenitor(start, end);

            if (commonProgenitor != null)
            {
                var parentCount = commonProgenitor.ProgenitorCount -
                    (start.ProgenitorCount - start.DescendantCount);

                var childCount = commonProgenitor.ProgenitorCount -
                    (end.ProgenitorCount - end.DescendantCount);

                Rel possibleHalfResult = new(
                [
                    .. Enumerable.Repeat<RelIdentifier>(RelSubatomic.Parent, parentCount),
                            .. Enumerable.Repeat<RelIdentifier>(RelSubatomic.Child, childCount),
                        ]);

                if (startValues.Value.Item2 > 0
                    && endValues.Value.Item2 > 0
                    && end.SubatomicValues.Contains(RelSubatomic.FullSibling))
                {
                    RelSubatomic[] resultingSubatomicValues = [.. possibleHalfResult.SubatomicValues
                                .Select(sv => sv == RelSubatomic.HalfSibling ? RelSubatomic.FullSibling : sv)];

                    return (Rel)resultingSubatomicValues;
                }

                return possibleHalfResult;
            }
        }
        */

        private static (int, int, bool) ApplySubatomic((int PCount, int DCount, bool IsHalf) values, RelSubatomic subatomic)
        {
            if (subatomic == RelSubatomic.Partner
                || subatomic == RelSubatomic.UnmarriedPartner
                || subatomic == RelSubatomic.Spouse)
            {
                throw new Exception($"A relationship involving a partner is not guaranteed to be a blood relationship.");
            }

            if ((values.PCount == 0 || values.DCount == 0) && values.IsHalf)
            {
                throw new Exception($"A direct relationship cannot be a half-relationship.");
            }

            switch (subatomic)
            {
                case RelSubatomic.Parent:
                    if (values.DCount > 0)
                        return (values.PCount, values.DCount - 1, values.DCount != 1 && values.IsHalf);
                    else
                        return (values.PCount + 1, values.DCount, values.IsHalf);

                case RelSubatomic.Child:
                    return (values.PCount, values.DCount + 1, values.IsHalf);

                case RelSubatomic.FullSibling:
                    if (values.DCount == 0)
                        return (values.PCount + 1, values.DCount + 1, values.IsHalf);
                    else
                        return values;

                case RelSubatomic.HalfSibling:
                    if (values.DCount > values.PCount || values.IsHalf)
                        return (values.PCount, values.DCount, values.PCount != 0);
                    else if (values.PCount > 0 && values.DCount > 0)
                        return (values.PCount, values.DCount, !values.IsHalf);
                    else
                        return (values.PCount + 1, values.DCount + 1, !values.IsHalf);

                default:
                    throw new NotImplementedException();
            }
        }
    }
}