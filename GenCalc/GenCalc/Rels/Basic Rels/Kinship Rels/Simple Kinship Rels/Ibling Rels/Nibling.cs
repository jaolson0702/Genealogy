using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace GenCalc
{
    public abstract class Nibling : Ibling
    {
        protected Nibling(int genCount) : base(genCount)
        {
        }

        public override RelValue[] Values
        {
            get
            {
                List<RelValue> result = new() { SiblingValue };
                for (int a = 0; a < Math.Abs(Generation); a++)
                    result.Add(DirectValue);
                return result.ToArray();
            }
        }

        public override Rel[] GetNext(RelValue value)
        {
            if (value == RelValue.Self) return new[] { this };
            List<Rel> result = new();
            if (value == RelValue.FullSibling) return new[] { this };
            switch (value)
            {
                case RelValue.Parent:
                    result.Add(new SimpleSet(this, value));
                    result.Add((Ibling)(Generation + 1, HasHalfSibling));
                    break;

                case RelValue.Child: return new[] { (Ibling)(Generation - 1, HasHalfSibling) };
                case RelValue.HalfSibling:
                    result.Add(new SimpleSet(this, value));
                    result.Add(this);
                    break;

                default: return new[] { new SimpleSet(this, value) };
            }
            return result.ToArray();
        }

        public override string ToString(Gender? gender) => Math.Abs(Generation).GetGenerationPrefix() + gender switch
        {
            Gender.Male => "nephew",
            Gender.Female => "niece",
            _ => "nibling"
        };

        public new static Nibling Get(int genCount, bool hasHalfSibling = false) => hasHalfSibling ? (Half)genCount : (Full)genCount;

        public static Nibling Get(bool hasHalfSibling, int genCount = 0) => Get(genCount, hasHalfSibling);

        public static implicit operator Nibling(ValueTuple<int, bool> values) => Get(values.Item1, values.Item2);

        public static implicit operator Nibling(ValueTuple<bool, int> values) => Get(values.Item2, values.Item1);

        public static implicit operator Nibling(int genCount) => Get(genCount, false);

        public static implicit operator Nibling(bool hasHalfSibling) => Get(1, hasHalfSibling);

        public class Full : Nibling
        {
            private static readonly Dictionary<int, Full> fullNiblings = new();

            private Full(int genCount) : base(genCount)
            {
            }

            public override bool HasHalfSibling => false;

            public new static Full Get(int genCount)
            {
                genCount = genCount == 0 ? -1 : -Math.Abs(genCount);
                if (!fullNiblings.ContainsKey(genCount)) fullNiblings.Add(genCount, new(genCount));
                return fullNiblings[genCount];
            }

            public static implicit operator Full(int genCount) => Get(genCount);
        }

        public class Half : Nibling
        {
            private static readonly Dictionary<int, Half> halfNiblings = new();

            private Half(int genCount) : base(genCount)
            {
            }

            public override bool HasHalfSibling => true;

            public new static Half Get(int genCount)
            {
                genCount = genCount == 0 ? -1 : -Math.Abs(genCount);
                if (!halfNiblings.ContainsKey(genCount)) halfNiblings.Add(genCount, new(genCount));
                return halfNiblings[genCount];
            }

            public static implicit operator Half(int genCount) => Get(genCount);
        }
    }
}