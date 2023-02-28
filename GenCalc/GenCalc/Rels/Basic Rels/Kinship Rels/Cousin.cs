using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCalc
{
    public abstract class Cousin : Kinship, IHasSibling
    {
        private static readonly Dictionary<Tuple<int, int>, Cousin> cousinRels = new();
        public readonly int Degree, TimesRemoved;

        protected Cousin(int degree, int timesRemoved) => (Degree, TimesRemoved) = (degree, timesRemoved);

        public Rel AsHalf => (Half)(Degree, TimesRemoved);

        public Rel AsFull => Get(Degree, TimesRemoved);

        public abstract bool HasHalfSibling { get; }

        public override KinshipCount Count
        {
            get
            {
                if (TimesRemoved < 0)
                {
                    KinshipCount basicCousin = ((Cousin)(Degree, 0, HasHalfSibling)).Count;
                    return (basicCousin.ACount, basicCousin.DCount + Math.Abs(TimesRemoved));
                }
                return (Degree + TimesRemoved + 1, Degree + 1);
            }
        }

        public override RelValue[] Values
        {
            get
            {
                var record = Count;
                List<RelValue> result = new();
                for (int a = 0; a < record.ACount - 1; a++) result.Add(RelValue.Parent);
                result.Add(HasHalfSibling ? RelValue.HalfSibling : RelValue.FullSibling);
                for (int a = 0; a < record.DCount - 1; a++) result.Add(RelValue.Child);
                return result.ToArray();
            }
        }

        public override Rel[] GetNext(RelValue value)
        {
            if (value == RelValue.Self || value == RelValue.FullSibling) return new[] { this };
            List<Rel> result = new();
            switch (value)
            {
                case RelValue.Parent:
                    result.Add(new SimpleSet(this, value));
                    if (Degree != 1)
                        result.Add((Cousin)(TimesRemoved < 0 ? Degree : Degree - 1, TimesRemoved + 1, HasHalfSibling));
                    else result.Add((Ibling)(Degree + 1, HasHalfSibling));
                    break;

                case RelValue.Child: return new[] { (Cousin)(TimesRemoved <= 0 ? Degree : Degree + 1, TimesRemoved - 1, HasHalfSibling) };

                default: return new[] { new SimpleSet(this, (Basic)value) };
            }
            return result.ToArray();
        }

        public override string ToString(Gender? gender) => Degree.WithNumberEnding() + " cousin" + Math.Abs(TimesRemoved) switch
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

        public static Cousin Get(int degree, int timesRemoved, bool hasHalfSibling = false) => hasHalfSibling ? (Half)(degree, timesRemoved) : (Full)(degree, timesRemoved);

        public static Cousin Get(bool hasHalfSibling, int degree, int timesRemoved = 0) => Get(degree, timesRemoved, hasHalfSibling);

        public static Cousin Get(int degree, bool hasHalfSibling, int timesRemoved = 0) => Get(degree, timesRemoved, hasHalfSibling);

        public static implicit operator Cousin(int degree) => Get(degree, 0);

        public static implicit operator Cousin(ValueTuple<int, int, bool> cousin) => Get(cousin.Item1, cousin.Item2, cousin.Item3);

        public static implicit operator Cousin(ValueTuple<bool, int, int> cousin) => Get(cousin.Item2, cousin.Item3, cousin.Item1);

        public static implicit operator Cousin(ValueTuple<int, bool, int> cousin) => Get(cousin.Item1, cousin.Item3, cousin.Item2);

        public static implicit operator Cousin(ValueTuple<int, int> cousin) => Get(cousin.Item1, cousin.Item2);

        public static implicit operator Cousin(ValueTuple<int, bool> cousin) => Get(cousin.Item1, 0, cousin.Item2);

        public static implicit operator Cousin(ValueTuple<bool, int> cousin) => Get(cousin.Item2, 0, cousin.Item1);

        public class Full : Cousin
        {
            private static readonly Dictionary<Tuple<int, int>, Full> fullCousinRels = new();

            private Full(int degree, int timesRemoved) : base(degree, timesRemoved)
            {
            }

            public override bool HasHalfSibling => false;

            public static Full Get(int degree, int timesRemoved)
            {
                if (degree == 0) degree = 1;
                else degree = Math.Abs(degree);
                if (new List<Tuple<int, int>>(fullCousinRels.Keys).Find(tuple => tuple.Item1 == degree && tuple.Item2 == timesRemoved) is null)
                    fullCousinRels.Add(new(degree, timesRemoved), new(degree, timesRemoved));
#pragma warning disable CS8604 // Possible null reference argument.
                return fullCousinRels[new List<Tuple<int, int>>(fullCousinRels.Keys).Find(tuple => tuple.Item1 == degree && tuple.Item2 == timesRemoved)];
#pragma warning restore CS8604 // Possible null reference argument.
            }

            public static implicit operator Full(int degree) => Get(degree, 0);

            public static implicit operator Full(ValueTuple<int, int> values) => Get(values.Item1, values.Item2);
        }

        public class Half : Cousin
        {
            private static readonly Dictionary<Tuple<int, int>, Half> halfCousinRels = new();

            private Half(int degree, int timesRemoved) : base(degree, timesRemoved)
            {
            }

            public override bool HasHalfSibling => true;

            public override string ToString(Gender? gender) => "half " + base.ToString(gender);

            private static Half Get(int degree, int timesRemoved)
            {
                if (degree == 0) degree = 1;
                else degree = Math.Abs(degree);
                if (new List<Tuple<int, int>>(halfCousinRels.Keys).Find(tuple => tuple.Item1 == degree && tuple.Item2 == timesRemoved) is null)
                    halfCousinRels.Add(new(degree, timesRemoved), new(degree, timesRemoved));
#pragma warning disable CS8604 // Possible null reference argument.
                return halfCousinRels[new List<Tuple<int, int>>(halfCousinRels.Keys).Find(tuple => tuple.Item1 == degree && tuple.Item2 == timesRemoved)];
#pragma warning restore CS8604 // Possible null reference argument.
            }

            public static implicit operator Half(int degree) => Get(degree, 0);

            public static implicit operator Half(ValueTuple<int, int> values) => Get(values.Item1, values.Item2);
        }
    }
}