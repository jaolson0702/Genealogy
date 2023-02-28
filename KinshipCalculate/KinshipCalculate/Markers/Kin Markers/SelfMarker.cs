using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinshipCalculate.Markers
{
    public class SelfMarker : KinMarker
    {
        public new static readonly SelfMarker Get = new();

        private SelfMarker() : base(0, 0)
        {
        }
    }
}