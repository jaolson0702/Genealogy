using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace KinshipCompute
{
    public class Sibling : NonLinealTarget
    {
        public new static readonly Sibling Get = new();

        private Sibling()
        { }

        public static int Generation => 0;

        public override int ProgenitorCount => 1;

        public override int ProgenyCount => 1;

        public override string ToString(Gender? gender) => gender switch
        {
            Gender.Male => "brother",
            Gender.Female => "sister",
            _ => "sibling"
        };
    }
}