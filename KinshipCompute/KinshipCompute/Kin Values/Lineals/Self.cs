using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinshipCompute
{
    public class Self : Lineal
    {
        public new static readonly Self Get = new();

        private Self() : base(0)
        {
        }

        public override int ProgenitorCount => 0;

        public override int ProgenyCount => 0;

        public override bool IsReflectiveWith(Kin other) => false;

#pragma warning disable CS8602 // Dereference of a possibly null reference.

        public override string ToString(Gender? gender) => "self" + (gender is null ? string.Empty : $" ({gender.ToString().ToLower()})");

#pragma warning restore CS8602 // Dereference of a possibly null reference.
    }
}