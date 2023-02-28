using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCalc
{
    public class Partner : Rel.Basic
    {
        private static readonly Partner unmarriedPartner = new(false), unknownPartner = new(null);
        public readonly bool? Married;

        protected Partner(bool? married) => Married = married;

        public override RelValue[] Values => new[]
        {
            Married switch
            {
                true => RelValue.Spouse,
                false => RelValue.UnmarriedPartner,
                _ => RelValue.Partner
            }
        };

        public override Rel[] GetNext(RelValue value)
        {
            List<Rel> result = new() { new SimpleSet(this, (Basic)value) };
            if ((Basic)value is Partner ptr && ptr.Married == Married) result.Add(this);
            return result.ToArray();
        }

        public override string ToString(Gender? gender) => $"{(Married == false ? "unmarried " : string.Empty)}partner" + gender switch
        {
            Gender.Male => " (male)",
            Gender.Female => " (female)",
            _ => string.Empty
        };

        public static Partner Get(bool? married) => married switch
        {
            true => Spouse.Get,
            false => unmarriedPartner,
            _ => unknownPartner
        };

        public static implicit operator Partner(bool married) => Get(married);

        public new class Spouse : Partner
        {
            public new static readonly Spouse Get = new();

            private Spouse() : base(true)
            {
            }

            public override Rel[] GetNext(RelValue value) => new[] { new InLaw.FromSpouse(new RelValue[] { value }) };

            public override string ToString(Gender? gender) => gender switch
            {
                Gender.Male => "husband",
                Gender.Female => "wife",
                _ => "spouse"
            };
        }
    }
}