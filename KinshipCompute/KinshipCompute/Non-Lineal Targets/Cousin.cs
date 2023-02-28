using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinshipCompute
{
    public class Cousin : NonLinealTarget
    {
        private static readonly Dictionary<ValueTuple<int, int>, Cousin> values = new();

        public readonly int Degree, TimesRemoved;

        private Cousin(int degree = 1, int timesRemoved = 0) =>
            (Degree, TimesRemoved) = (degree, timesRemoved);

        public override int ProgenitorCount => Degree + TimesRemoved + 1;

        public override int ProgenyCount => Degree + 1 + (TimesRemoved < 0 ? Math.Abs(TimesRemoved) : 0);

#pragma warning disable CS8602 // Dereference of a possibly null reference.

        public override string ToString(Gender? gender) => FormatTools.GetNumberEnding(Degree) + " cousin" + Math.Abs(TimesRemoved) switch
        {
            0 => string.Empty,
            1 => " once removed",
            2 => " twice removed",
            3 => " thrice removed",
            _ => $" {Math.Abs(TimesRemoved)} times removed"
        } + (TimesRemoved < 0 ? " down" : string.Empty) + (gender is null ? string.Empty : $" ({gender.ToString().ToLower()})");

#pragma warning restore CS8602 // Dereference of a possibly null reference.

        public new static Cousin Get(int degree, int timesRemoved = 0)
        {
            degree = degree == 0 ? 1 : Math.Abs(degree);
            if (!values.ContainsKey((degree, timesRemoved)))
                values.Add((degree, timesRemoved), new(degree, timesRemoved));
            return values[(degree, timesRemoved)];
        }

        public new static Cousin Get(ValueTuple<int, int> values) => Get(values.Item1, values.Item2);

        public static implicit operator Cousin(ValueTuple<int, int> values) => Get(values);

        public static implicit operator Cousin(int degree) => Get(degree);
    }
}