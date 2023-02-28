using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCalc
{
    public abstract class Pibling : Ibling
    {
        protected Pibling(int genCount) : base(genCount)
        {
        }

        public override RelValue[] Values
        {
            get
            {
                List<RelValue> result = new();
                for (int a = 0; a < Generation; a++)
                    result.Add(DirectValue);
                result.Add(SiblingValue);
                return result.ToArray();
            }
        }

        public override string ToString(Gender? gender) => Math.Abs(Generation).GetGenerationPrefix() + gender switch
        {
            Gender.Male => "uncle",
            Gender.Female => "aunt",
            _ => "pibling"
        };

        public new static Pibling Get(int genCount, bool hasHalfSibling = false) => hasHalfSibling ? (Half)genCount : (Full)genCount;

        public static Pibling Get(bool hasHalfSibling, int genCount = 0) => Get(genCount, hasHalfSibling);

        public static implicit operator Pibling(ValueTuple<int, bool> values) => Get(values.Item1, values.Item2);

        public static implicit operator Pibling(ValueTuple<bool, int> values) => Get(values.Item2, values.Item1);

        public static implicit operator Pibling(int genCount) => Get(genCount, false);

        public static implicit operator Pibling(bool hasHalfSibling) => Get(1, hasHalfSibling);

        public class Full : Pibling
        {
            private static readonly Dictionary<int, Full> fullPiblings = new();

            private Full(int genCount) : base(genCount)
            {
            }

            public override bool HasHalfSibling => false;

            public override Rel[] GetNext(RelValue value) => value == RelValue.Self ? new[] { this } : value switch
            {
                RelValue.Parent => new Rel[] { Generation + 1 },
                RelValue.Child => new[] { (Cousin)(1, Generation - 1) },
                RelValue.HalfSibling => new[] { (Half)Generation },
                _ => new[] { new SimpleSet(this, value) },
            };

            public new static Full Get(int genCount)
            {
                genCount = genCount == 0 ? 1 : Math.Abs(genCount);
                if (!fullPiblings.ContainsKey(genCount)) fullPiblings.Add(genCount, new(genCount));
                return fullPiblings[genCount];
            }

            public static implicit operator Full(int genCount) => Get(genCount);
        }

        public class Half : Pibling
        {
            private static readonly Dictionary<int, Half> halfPiblings = new();

            private Half(int genCount) : base(genCount)
            {
            }

            public override bool HasHalfSibling => true;

            public override Rel[] GetNext(RelValue value)
            {
                List<Rel> result = new();
                switch (value)
                {
                    case RelValue.Parent:
                        result.Add(new SimpleSet(this, value));
                        result.Add(Generation + 1);
                        break;

                    case RelValue.Child: return new[] { (Cousin.Half)(1, Generation - 1) };

                    case RelValue.HalfSibling:
                        result.Add(new SimpleSet(this, value));
                        result.Add(this);
                        result.Add((Ibling)Generation);
                        break;

                    default: return new[] { new SimpleSet(this, value) };
                }
                return result.ToArray();
            }

            public override string ToString(Gender? gender) => "half-" + base.ToString(gender);

            public new static Half Get(int genCount)
            {
                genCount = genCount == 0 ? 1 : Math.Abs(genCount);
                if (!halfPiblings.ContainsKey(genCount)) halfPiblings.Add(genCount, new(genCount));
                return halfPiblings[genCount];
            }

            public static implicit operator Half(int genCount) => Get(genCount);
        }
    }
}