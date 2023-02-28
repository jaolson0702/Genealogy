using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCalc
{
    public abstract class Lineal : Kinship.Simple
    {
        private static readonly Dictionary<int, Lineal> directRels = new();

        protected Lineal(int genCount) => Generation = genCount;

        public override int Generation { get; }

        public override KinshipCount Count => (Generation > 0 ? Generation : 0, Generation < 0 ? Generation : 0);

        public override RelValue[] Values
        {
            get
            {
                List<RelValue> result = new();
                RelValue value = Generation >= 0 ? RelValue.Parent : RelValue.Child;
                for (int a = 0; a < Math.Abs(Generation); a++)
                    result.Add(value);
                return result.ToArray();
            }
        }

        public bool IsCommonProgenitorOf(Kinship blood, out int? deviation)
        {
            Lineal ccProgenitor = blood.Count.ClosestCommonLineal.ACount;
            if (Generation >= ccProgenitor.Generation)
            {
                deviation = Generation - ccProgenitor.Generation;
                return true;
            }
            deviation = null;
            return false;
        }

        public override Rel[] GetNext(RelValue value)
        {
            if (value == RelValue.Self) return new[] { this };
            if (Generation == 0) return new Rel[] { value };
            List<Rel> result = new();
            if (Generation > 0)
            {
                switch (value)
                {
                    case RelValue.Parent: return new Rel[] { Generation + 1 };
                    case RelValue.Child:
                        result.Add((Ibling)(Generation - 1, true));
                        result.Add((Ibling)(Generation - 1));
                        result.Add(Generation - 1);
                        break;

                    case RelValue.FullSibling: return new Rel[] { (Ibling)Generation };
                    case RelValue.HalfSibling: return new Rel[] { (Ibling)(Generation, true) };
                    default:
                        result.Add(new SimpleSet(this, value));
                        result.Add(this);
                        break;
                }
            }
            else
            {
                switch (value)
                {
                    case RelValue.Parent:
                        result.Add(new SimpleSet(this, value));
                        result.Add(Generation + 1);
                        break;

                    case RelValue.Child: return new Rel[] { Generation - 1 };

                    case RelValue.FullSibling: return new Rel[] { this };

                    case RelValue.HalfSibling:
                        result.Add(new SimpleSet(this, value));
                        result.Add(this);
                        break;

                    default:
                        if (Generation == -1 && value == RelValue.Spouse)
                            return new[] { InLaw.Child.Get };
                        return new[] { new SimpleSet(this, value) };
                }
            }
            return result.ToArray();
        }

        public override string ToString(Gender? gender)
        {
            if (Generation == 0) return "self" + gender switch
            {
                Gender.Male => " (male)",
                Gender.Female => " (female)",
                _ => string.Empty
            };
            return Math.Abs(Generation).GetGenerationPrefix() + gender switch
            {
                Gender.Male => Generation > 0 ? "father" : "son",
                Gender.Female => Generation > 0 ? "mother" : "daughter",
                _ => Generation > 0 ? "parent" : "child"
            };
        }

        public new static Lineal Get(int given)
        {
            if (given == 0) return Self.Get;
            if (!directRels.ContainsKey(given))
                directRels.Add(given, given > 0 ? (Progenitor)given : (Progeny)given);
            return directRels[given];
        }

        public static implicit operator Lineal(int given) => Get(given);

        public new class Self : Lineal
        {
            public new static readonly Self Get = new();

            private Self() : base(0)
            {
            }

            public override string ToString(Gender? gender) => "self" + gender switch
            {
                Gender.Male => " (male)",
                Gender.Female => " (female)",
                _ => string.Empty
            };
        }

        public class Progenitor : Lineal
        {
            private static readonly Dictionary<int, Progenitor> progenitors = new();

            private Progenitor(int genCount) : base(genCount == 0 ? 1 : Math.Abs(genCount))
            {
            }

            public override RelValue[] Values
            {
                get
                {
                    RelValue[] result = new RelValue[Generation];
                    Array.Fill(result, RelValue.Parent);
                    return result;
                }
            }

            public override string ToString(Gender? gender) => Math.Abs(Generation).GetGenerationPrefix() + gender switch
            {
                Gender.Male => "father",
                Gender.Female => "mother",
                _ => "parent"
            };

            public static implicit operator Progenitor(int genCount)
            {
                genCount = genCount == 0 ? 1 : Math.Abs(genCount);
                if (!progenitors.ContainsKey(genCount)) progenitors.Add(genCount, new(genCount));
                return progenitors[genCount];
            }
        }

        public class Progeny : Lineal
        {
            private static readonly Dictionary<int, Progeny> progeny = new();

            private Progeny(int genCount) : base(genCount == 0 ? -1 : -Math.Abs(genCount))
            {
            }

            public override RelValue[] Values
            {
                get
                {
                    RelValue[] result = new RelValue[Math.Abs(Generation)];
                    Array.Fill(result, RelValue.Child);
                    return result;
                }
            }

            public override string ToString(Gender? gender) => Math.Abs(Generation).GetGenerationPrefix() + gender switch
            {
                Gender.Male => "son",
                Gender.Female => "daughter",
                _ => "child"
            };

            public static implicit operator Progeny(int genCount)
            {
                genCount = genCount == 0 ? -1 : -Math.Abs(genCount);
                if (!progeny.ContainsKey(genCount)) progeny.Add(genCount, new(genCount));
                return progeny[genCount];
            }
        }
    }
}