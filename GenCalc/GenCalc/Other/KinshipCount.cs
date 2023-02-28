using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCalc
{
    public readonly struct KinshipCount : IEquatable<KinshipCount>
    {
        public readonly int ACount, DCount;

        private KinshipCount(int aCount, int dCount) => (ACount, DCount) = (Math.Abs(aCount), Math.Abs(dCount));

        public int Generation => ACount - DCount;

        public KinshipCount Parent => DCount > 0 ? new(ACount, DCount - 1) : new(ACount + 1, DCount);

        public KinshipCount Child => new(ACount, DCount + 1);

        public KinshipCount ChildForceLineal => ACount > 0 && DCount == 0 ? new(ACount - 1, DCount) : Child;

        public bool KnownSiblingStatus => ACount == 0 || DCount == 0;

        public KinshipCount ClosestCommonLineal => ClosestCommonLinealWith(0);

        public KinshipCount ClosestCommonLinealWith(KinshipCount other)
        {
            if (other == (KinshipCount)0) return ACount;
            Lineal result1 = ClosestCommonLineal.ACount;
            Lineal result2 = other.ClosestCommonLineal.ACount;
            int currentResult = Math.Max(result1.Generation, result2.Generation);
            int deviation = ACount == other.ACount ? Math.Min(DCount, other.DCount) : 0;
            return new(currentResult, deviation);
        }

        public bool Equals(KinshipCount other) => ACount == other.ACount && DCount == other.DCount;

        private bool IsLinealWith(KinshipCount other, out Lineal? difference)
        {
            KinshipCount ccLineal = ClosestCommonLinealWith(other);
            if (ccLineal != this && ccLineal != other)
            {
                difference = null;
                return false;
            }
            if (this == other)
            {
                difference = 0;
                return true;
            }
            int count = 0;
            KinshipCount current = Generation > other.Generation ? other : this;
            while (current != (Generation > other.Generation ? this : other))
            {
                current = current.Parent;
                count++;
            }
            if (current == this) count *= -1;
            difference = count;
            return true;
        }

        public override bool Equals([NotNullWhen(true)] object? obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();

        public override string ToString() => $"({ACount}, {DCount})";

        public static implicit operator KinshipCount(int given) => given >= 0 ? new(given, 0) : new(0, given);

        public static implicit operator KinshipCount(ValueTuple<int, int> values) => new(values.Item1, values.Item2);

        public static KinshipCount operator +(KinshipCount given) => given;

        public static KinshipCount operator +(KinshipCount left, KinshipCount right)
        {
            KinshipCount result = left;
            for (int a = 0; a < right.ACount; a++) result = result.Parent;
            for (int a = 0; a < right.DCount; a++) result = result.Child;
            return new(result.ACount, result.DCount);
        }

        public static KinshipCount operator -(KinshipCount destination, KinshipCount start)
        {
            if (destination == start) return 0;
            KinshipCount ccLineal = start.ClosestCommonLinealWith(destination);
            start.IsLinealWith(ccLineal, out Lineal? aCount);
            destination.IsLinealWith(ccLineal, out Lineal? dCount);
            aCount ??= 0;
            dCount ??= 0;
            return new(aCount.Generation, dCount.Generation);
        }

        public static KinshipCount operator !(KinshipCount given) => new(given.DCount, given.ACount);

        public static bool operator ==(KinshipCount left, KinshipCount right) => left.Equals(right);

        public static bool operator !=(KinshipCount left, KinshipCount right) => !(left == right);
    }
}