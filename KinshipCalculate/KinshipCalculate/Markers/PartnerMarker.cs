using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KinshipCalculate;

namespace KinshipCalculate.Markers
{
    public class PartnerMarker : IGenMarker
    {
        public readonly IGenMarker Target;
        public readonly PartnerStatus Status;

        public PartnerMarker(IGenMarker target, PartnerStatus status)
        {
        }
    }
}