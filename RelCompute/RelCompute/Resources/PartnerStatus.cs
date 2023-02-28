using KinshipCompute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelCompute
{
    public class PartnerStatus
    {
        public static readonly PartnerStatus Married = new(true, "husband", "wife", "spouse");
        public static readonly PartnerStatus Unmarried = new(false, "unmarried partner (male)", "unmarried partner (female)", "unmarried partner");
        public static readonly PartnerStatus Unknown = new(null, "partner (male)", "partner (female)", "partner");
        private readonly string maleToString, femaleToString, defaultToString;
        public readonly bool? IsMarried;

        private PartnerStatus(bool? isMarried, string maleToString, string femaleToString, string defaultToString) => (IsMarried, this.maleToString, this.femaleToString, this.defaultToString) = (isMarried, maleToString, femaleToString, defaultToString);

        public string ToString(Gender? gender) => gender switch
        {
            Gender.Male => maleToString,
            Gender.Female => femaleToString,
            _ => defaultToString
        };
    }
}