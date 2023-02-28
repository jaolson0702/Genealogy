using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCalc
{
    public abstract class Sibling : Ibling, IHasSibling
    {
        protected Sibling() : base(0)
        {
        }

        public override string ToString(Gender? gender) => gender switch
        {
            Gender.Male => "brother",
            Gender.Female => "sister",
            _ => "sibling"
        };

        public static Sibling Get(bool hasHalfSibling) => hasHalfSibling ? Half.Get : Full.Get;

        public static implicit operator Sibling(bool hasHalfSibling) => Get(hasHalfSibling);

        public class Full : Sibling
        {
            public new static readonly Full Get = new();

            private Full() : base()
            {
            }

            public override bool HasHalfSibling => false;

            public override RelValue[] Values => new[] { RelValue.FullSibling };

            public override Rel[] GetNext(RelValue value) => value == RelValue.Self ? new[] { this } : value switch
            {
                RelValue.Parent => new Rel[] { Generation + 1 },
                RelValue.Child => new[] { (Ibling)(Generation - 1) },
                RelValue.HalfSibling => new[] { (Half)Generation },
                RelValue.Spouse => new[] { (InLaw.SpouseOfSibling)false },
                _ => new[] { new SimpleSet(this, value) }
            };
        }

        public class Half : Sibling
        {
            public new static readonly Half Get = new();

            private Half() : base()
            {
            }

            public override bool HasHalfSibling => true;

            public override RelValue[] Values => new[] { RelValue.HalfSibling };

            public override Rel[] GetNext(RelValue value)
            {
                List<Rel> result = new();
                switch (value)
                {
                    case RelValue.Parent:
                        result.Add(new SimpleSet(this, value));
                        result.Add(Generation + 1);
                        break;

                    case RelValue.Child: return new[] { (Half)(Generation - 1) };

                    case RelValue.HalfSibling:
                        result.Add(new SimpleSet(this, value));
                        result.Add(this);
                        result.Add((Ibling)Generation);
                        result.Add(Generation);
                        break;

                    case RelValue.Spouse: return new[] { (InLaw.SpouseOfSibling)true };

                    default: return new[] { new SimpleSet(this, value) };
                }
                return result.ToArray();
            }

            public override string ToString(Gender? gender) => "half-" + base.ToString(gender);
        }
    }
}