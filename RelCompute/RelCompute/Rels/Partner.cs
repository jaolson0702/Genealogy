using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KinshipCompute;

namespace RelCompute
{
    public class Partner : Rel
    {
        public readonly Rel Target;
        public readonly PartnerStatus Status;

        public Partner(Rel target, PartnerStatus status) => (Target, Status) = (target, status);

        public Partner(Rel target) : this(target, PartnerStatus.Unknown)
        {
        }

        public Partner(PartnerStatus status) : this(Kins.Self, status)
        {
        }

        public override RelValue[] Values => new RelValue[] { Status.IsMarried switch {
            true => RelValue.Spouse,
            false => RelValue.UnmarriedPartner,
            _ => RelValue.Partner
        } };

        public override Rel With(RelValue value) => (Rel)value is Partner par ? new Partner(this, par.Status) : new Kinship(this, (Kinship.Basic)(Rel)value);

        public override string ToString(Gender? gender) => Status.ToString(gender) + (Target is Kinship.Basic kin && kin == Kins.Self ? string.Empty : " of " + Target);

        public static implicit operator Partner(PartnerStatus status) => new(status);
    }
}