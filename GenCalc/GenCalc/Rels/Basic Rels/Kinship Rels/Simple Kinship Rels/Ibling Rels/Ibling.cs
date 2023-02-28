using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCalc
{
    public abstract class Ibling : Kinship.Simple, IHasSibling
    {
        protected Ibling(int genCount) => Generation = genCount;

        public override int Generation { get; }

        public abstract bool HasHalfSibling { get; }

        public Rel AsHalf => (Ibling)(Generation, true);

        public Rel AsFull => (Ibling)(Generation, false);

        public override KinshipCount Count => (Generation >= 0 ? Generation + 1 : 1, Generation < 0 ? Math.Abs(Generation) + 1 : 1);

        protected RelValue DirectValue => Generation >= 0 ? RelValue.Parent : RelValue.Child;

        protected RelValue SiblingValue => HasHalfSibling ? RelValue.HalfSibling : RelValue.FullSibling;

        public override string ToString(Gender? gender)
        {
            if (Generation == 0) return gender switch
            {
                Gender.Male => "brother",
                Gender.Female => "sister",
                _ => "sibling"
            };
            else return Math.Abs(Generation).GetGenerationPrefix() + gender switch
            {
                Gender.Male => Generation > 0 ? "uncle" : "nephew",
                Gender.Female => Generation > 0 ? "aunt" : "niece",
                _ => Generation > 0 ? "pibling" : "nibling"
            };
        }

        public static Ibling Get(int genCount, bool hasHalfSibling) => genCount == 0 ? (Sibling)hasHalfSibling : (genCount > 0 ? (Pibling)(genCount, hasHalfSibling) : (Nibling)(genCount, hasHalfSibling));

        public static implicit operator Ibling(int genCount) => Get(genCount, false);

        public static implicit operator Ibling(bool hasHalfSibling) => Get(0, hasHalfSibling);

        public static implicit operator Ibling(ValueTuple<int, bool> values) => Get(values.Item1, values.Item2);

        public static implicit operator Ibling(ValueTuple<bool, int> values) => Get(values.Item2, values.Item1);
    }
}