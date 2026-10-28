using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rels
{
    public class NullAtom : RelAtom
    {
        public static readonly NullAtom Get = new();

        public override RelIdentifier ClosestCommonProgenitor => this;

        private NullAtom()
        { }

        public override RelSubatomic[] SubatomicValues => [RelSubatomic.Null];

        public override string ToString(Gender? gender) => "N/A";

        #region Additive Properties

        public override RelIdentifier WithChildPrimary => this;
        public override RelIdentifier WithFullSiblingPrimary => this;
        public override RelIdentifier WithHalfSiblingPrimary => this;
        public override RelIdentifier WithParentPrimary => this;

        #endregion Additive Properties
    }
}