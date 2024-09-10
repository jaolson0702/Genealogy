using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rels
{
    public partial class Rel
    {
        public static SelfIdentifier Self => SelfIdentifier.Get;

        #region Progenitors

        public static ParentAtom Parent => ParentAtom.Get;
        public static GrandparentAtom Grandparent => GrandparentAtom.Get;

        public static GreatGrandparentMolecule GreatGrandparent(int n = 1) => new(n);

        #endregion Progenitors

        #region Progeny

        public static ChildAtom Child => ChildAtom.Get;
        public static GrandchildAtom Grandchild => GrandchildAtom.Get;

        public static GreatGrandchildMolecule GreatGrandchild(int n = 1) => new(n);

        #endregion Progeny

        #region Rels With Siblings

        public static SiblingAtom Sibling => SiblingAtom.Get;
        public static HalfSiblingAtom HalfSibling => HalfSiblingAtom.Get;

        public static PiblingAtom Pibling => PiblingAtom.Get;

        public static PiblingMolecule Grandpibling => new(2);

        public static PiblingMolecule GreatGrandpibling(int n = 1) => new(n);

        public static HalfPiblingAtom HalfPibling => HalfPiblingAtom.Get;

        public static PiblingMolecule HalfGrandpibling => new(2, true);

        public static PiblingMolecule HalfGreatGrandpibling(int n = 1) => new(n, true);

        public static NiblingAtom Nibling => NiblingAtom.Get;

        public static NiblingMolecule Grandnibling => new(2);

        public static NiblingMolecule GreatGrandnibling(int n = 1) => new(n);

        public static HalfNiblingAtom HalfNibling => HalfNiblingAtom.Get;

        public static NiblingMolecule HalfGrandnibling => new(2, true);

        public static NiblingMolecule HalfGreatGrandnibling(int n = 1) => new(n, true);

        public static CousinMolecule Cousin(int degree = 1, int timesRemoved = 0) => new(degree, timesRemoved);

        public static CousinMolecule HalfCousin(int degree = 1, int timesRemoved = 0) => new(degree, timesRemoved, true);

        #endregion Rels With Siblings

        #region Rels With Partners

        public static PartnerAtom Partner => PartnerAtom.Get;

        public static SpouseAtom Spouse => SpouseAtom.Get;

        public static UnmarriedPartnerAtom UnmarriedPartner => UnmarriedPartnerAtom.Get;

        public static ParentInLawAtom ParentInLaw => ParentInLawAtom.Get;

        public static ChildInLawAtom ChildInLaw => ChildInLawAtom.Get;

        public static SiblingInLawThroughSpouseAtom SiblingInLawThroughSpouse => SiblingInLawThroughSpouseAtom.Get;

        public static SiblingInLawThroughSiblingAtom SiblingInLawThroughSibling => SiblingInLawThroughSiblingAtom.Get;

        public static HalfSiblingInLawThroughSpouseAtom HalfSiblingInLawThroughSpouse => HalfSiblingInLawThroughSpouseAtom.Get;

        public static HalfSiblingInLawThroughSiblingAtom HalfSiblingInLawThroughSibling => HalfSiblingInLawThroughSiblingAtom.Get;

        #endregion Rels With Partners
    }
}