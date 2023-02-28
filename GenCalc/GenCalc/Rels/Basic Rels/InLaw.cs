using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCalc
{
    public abstract class InLaw : Rel.Basic
    {
        public new class Child : InLaw
        {
            public new static readonly Child Get = new();

            private Child()
            { }

            public override RelValue[] Values => new[] { RelValue.Child, RelValue.Spouse };

            public override Rel[] GetNext(RelValue value)
            {
                if (value == RelValue.Self) return new[] { this };
                List<Rel> result = new() { new SimpleSet(this, value) };
                if (value == RelValue.Spouse) result.Add(new Rel[] { RelValue.Child });
                if (value == RelValue.Child) result.Add(new Rel[] { -2 });
                return result.ToArray();
            }

            public override string ToString(Gender? gender) => gender switch
            {
                Gender.Male => "son",
                Gender.Female => "daughter",
                _ => "child"
            } + "-in-law";
        }

        public class FromSpouse : InLaw
        {
            public readonly Rel Next;

            public FromSpouse(Rel next) => Next = next;

            public override RelValue[] Values
            {
                get
                {
                    List<RelValue> result = new() { RelValue.Spouse };
                    result.AddRange(Next.Values);
                    return result.ToArray();
                }
            }

            public override Rel[] GetNext(RelValue value)
            {
                List<Rel> result = new();
                foreach (Rel rel in Next.GetNext(value))
                    result.Add(new FromSpouse(rel));
                return result.ToArray();
            }

            public override string ToString(Gender? gender)
            {
                if (Next == 1 || (Next is Ibling ibl && ibl.Generation == 0))
                    return Next.ToString(gender) + "-in-law";
                return Next.ToString(gender) + " of spouse";
            }
        }

        public new class SpouseOfSibling : InLaw, IHasSibling
        {
            private static readonly SpouseOfSibling half = new(true), full = new(false);

            private SpouseOfSibling(bool hasHalfSibling) => HasHalfSibling = hasHalfSibling;

            public bool HasHalfSibling { get; }

            public Rel AsHalf => (SpouseOfSibling)true;

            public Rel AsFull => (SpouseOfSibling)false;

            public override RelValue[] Values => new[] { HasHalfSibling ? RelValue.HalfSibling : RelValue.FullSibling, RelValue.Spouse };

            public override Rel[] GetNext(RelValue value)
            {
                List<Rel> result = new() { new SimpleSet(this, value) };
                if (value == RelValue.Spouse) result.Add((Ibling)HasHalfSibling);
                if (value == RelValue.Child) result.Add((Ibling)(-1, HasHalfSibling));
                return result.ToArray();
            }

            public override string ToString(Gender? gender) => (HasHalfSibling ? "half-" : string.Empty) + gender switch
            {
                Gender.Male => "brother",
                Gender.Female => "sister",
                _ => "sibling"
            } + $"-in-law (through {(HasHalfSibling ? "half-" : string.Empty)}sibling)";

            public static implicit operator SpouseOfSibling(bool hasHalfSibling) => hasHalfSibling ? half : full;
        }
    }
}