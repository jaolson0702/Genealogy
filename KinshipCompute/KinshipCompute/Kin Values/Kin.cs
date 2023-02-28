using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinshipCompute
{
    public abstract class Kin : IEquatable<Kin>
    {
        public Kin ClosestCommonLineal => ClosestCommonLinealWith(0);

        public int Generation => ProgenitorCount - ProgenyCount;

        public Kin Parent
        {
            get
            {
                Kin result = ProgenyCount > 0 ? Get(ProgenitorCount, ProgenyCount - 1) : Get(ProgenitorCount + 1, ProgenyCount);
                return result is NonLineal nlResult ? NonLineal.Get(nlResult.Target, this is NonLineal nl && nl.IsHalf) : result;
            }
        }

        public Kin Child
        {
            get
            {
                Kin result = Get(ProgenitorCount, ProgenyCount + 1);
                return result is NonLineal nlResult ? NonLineal.Get(nlResult.Target, this is NonLineal nl && nl.IsHalf) : result;
            }
        }

        public abstract int ProgenitorCount { get; }
        public abstract int ProgenyCount { get; }

        public Kin ClosestCommonLinealWith(Kin other)
        {
            if (other == 0) return ProgenitorCount;
            Lineal result1 = ClosestCommonLineal.ProgenitorCount;
            Lineal result2 = other.ClosestCommonLineal.ProgenitorCount;
            int currentResult = Math.Max(result1.Generation, result2.Generation);
            int deviation = ProgenitorCount == other.ProgenitorCount ? ((this is NonLineal nl1 && other is NonLineal nl2 && nl1.IsHalf == nl2.IsHalf) || this is Lineal || other is Lineal ? Math.Min(ProgenyCount, other.ProgenyCount) : 0) : 0;
            Kin result = Get(currentResult, deviation);
            return result is NonLineal nlResult ? NonLineal.Get(nlResult.Target, this is NonLineal nl12 && other is NonLineal nl22 && nl12.IsHalf && nl22.IsHalf) : result;
        }

        public virtual Kin With(Kin other)
        {
            Kin result = this;
            for (int a = 0; a < other.ProgenitorCount; a++) result = result.Parent;
            for (int a = 0; a < other.ProgenyCount; a++) result = result.Child;
            return result;
        }

        public Kin From(Kin start)
        {
            if (this == start) return 0;
            Kin ccLineal = start.ClosestCommonLinealWith(this);
            start.IsLinealWith(ccLineal, out Lineal? progenitorCount);
            IsLinealWith(ccLineal, out Lineal? progenyCount);
            progenitorCount ??= 0;
            progenyCount ??= 0;
            Kin result = Get(progenitorCount.Generation, progenyCount.Generation);
            return result is NonLineal nlResult ? NonLineal.Get(nlResult.Target, this is NonLineal nl && nl.IsHalf) : result;
        }

        private bool IsLinealWith(Kin other, out Lineal? difference)
        {
            Kin ccLineal = ClosestCommonLinealWith(other);
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
            Kin current = Generation > other.Generation ? other : this;
            while (current != (Generation > other.Generation ? this : other))
            {
                current = current.Parent;
                count++;
            }
            if (current == this) count *= -1;
            difference = count;
            return true;
        }

        public abstract bool IsReflectiveWith(Kin other);

        public abstract bool Equals(Kin? other);

        public abstract string ToString(Gender? gender);

        public override bool Equals(object? obj) => base.Equals(obj);

        public override int GetHashCode() => base.GetHashCode();

        public override string ToString() => ToString(null);

        public static Lineal Get(int generation) => Lineal.Get(generation);

        public static Kin Get(int progenitorCount, int progenyCount)
        {
            (progenitorCount, progenyCount) = (Math.Abs(progenitorCount), Math.Abs(progenyCount));
            if (progenitorCount == 0 && progenyCount == 0) return Get(0);
            if (progenitorCount == 0) return Progeny.Get(progenyCount);
            if (progenyCount == 0) return Progenitor.Get(progenitorCount);
            if (progenitorCount == 1 && progenyCount == 1) return NonLineal.Get(Sibling.Get);
            if (progenitorCount == 1) return NonLineal.Get(Nibling.Get(progenyCount - 1));
            if (progenyCount == 1) return NonLineal.Get(Pibling.Get(progenitorCount - 1));
            return NonLineal.Get(Cousin.Get(-1 + (progenyCount >= 0 ? progenyCount : progenitorCount), progenitorCount - progenyCount));
        }

        public static implicit operator Kin(int generation) => Get(generation);

        public static implicit operator Kin(ValueTuple<int, int> values) => Get(values.Item1, values.Item2);

        public static implicit operator Kin(ValueTuple<NonLinealTarget, bool> nonLinealValues) => (NonLineal)nonLinealValues;

        public static implicit operator Kin(NonLinealTarget target) => (NonLineal)target;

        public static Kin operator +(Kin left, Kin right) => left.With(right);

        public static Kin operator -(Kin left, Kin right) => left.From(right);

        public static bool operator ==(Kin left, Kin right) => left.Equals(right);

        public static bool operator !=(Kin left, Kin right) => !(left == right);
    }
}