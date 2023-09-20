using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rels
{
    public class SelfIdentifier : RelIdentifier
    {
        public static readonly SelfIdentifier Get = new();
        public override RelIdentifier WithPartnerPrimary => PartnerAtom.Get;
        public override RelIdentifier WithUnmarriedPartnerPrimary => UnmarriedPartnerAtom.Get;
        public override RelIdentifier WithSpousePrimary => SpouseAtom.Get;
        public override RelIdentifier WithChildPrimary => ChildAtom.Get;
        public override RelIdentifier WithFullSiblingPrimary => SiblingAtom.Get;
        public override RelIdentifier WithHalfSiblingPrimary => HalfSiblingAtom.Get;
        public override RelIdentifier WithParentPrimary => ParentAtom.Get;
        public override RelSubatomic[] SubatomicValues => Array.Empty<RelSubatomic>();

        private SelfIdentifier()
        { }

        public override string ToString(Gender? gender) => "self" + gender switch
        {
            Gender.Male => " (male)",
            Gender.Female => " (female)",
            _ => string.Empty
        };
    }
}