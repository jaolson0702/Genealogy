using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GenCalc
{
    public static class RelUtil
    {
        public static bool IsCommonProgenitorOf(this Rel given, Kinship target, out int? deviation)
        {
            if (given is Kinship b) return b.IsCommonProgenitorOf(target, out deviation);
            deviation = null;
            return false;
        }

        public static Lineal? GetClosestCommonProgenitor(this Rel rel) => rel is Kinship b ? b.Count.ClosestCommonLineal.ACount : null;

        public static Lineal GreatGrandchild(this int given) => Rel.NthGreatGrandchild(given);

        public static Ibling GreatGrandnibling(this int given) => Rel.NthGreatGrandnibling(given);

        public static Lineal GreatGrandparent(this int given) => Rel.NthGreatGrandparent(given);

        public static Ibling GreatGrandpibling(this int given) => Rel.NthGreatGrandpibling(given);

        public static Cousin Up(this Cousin given) => (Cousin)(given.Degree, Math.Abs(given.TimesRemoved), given.HasHalfSibling);

        public static Cousin Down(this Cousin given) => (Cousin)(given.Degree, -Math.Abs(given.TimesRemoved), given.HasHalfSibling);

        public static Cousin Cousin(this int given, int timesRemoved = 0, bool hasHalfSibling = false) => (Cousin)(given, timesRemoved, hasHalfSibling);

        public static string Formatted(this RelValue value) => value switch
        {
            RelValue.HalfSibling => "half-sibling",
            RelValue.FullSibling => "full sibling",
            _ => value.ToString().ToLower()
        };

        public static bool Contains(this Rel rel, RelValue value) => new List<RelValue>(rel.Values).Contains(value);

        public static string GetGenerationPrefix(this int generation) => Math.Abs(generation) switch
        {
            0 or 1 => string.Empty,
            2 => "grand",
            3 => "great-grand",
            _ => $"{(generation - 2).WithNumberEnding()}-great-grand"
        };

        public static string WithNumberEnding(this int number)
        {
            string numStr = number.ToString();
            if (numStr.Length > 1)
            {
                int lastTwo = int.Parse(numStr[^2..]);
                if (lastTwo >= 11 && lastTwo <= 13) return numStr + "th";
            }
            return numStr + int.Parse(numStr[^1].ToString()) switch
            {
                1 => "st",
                2 => "nd",
                3 => "rd",
                _ => "th"
            };
        }
    }
}