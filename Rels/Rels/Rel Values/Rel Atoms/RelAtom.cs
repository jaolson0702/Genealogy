namespace Rels
{
    public abstract class RelAtom : RelValue
    {
        public static implicit operator RelAtom(RelSubatomic subatomic) => subatomic switch
        {
            RelSubatomic.Child => ChildAtom.Get,
            RelSubatomic.FullSibling => SiblingAtom.Get,
            RelSubatomic.HalfSibling => HalfSiblingAtom.Get,
            RelSubatomic.Parent => ParentAtom.Get,
            RelSubatomic.Partner => PartnerAtom.Get,
            RelSubatomic.Spouse => SpouseAtom.Get,
            RelSubatomic.UnmarriedPartner => UnmarriedPartnerAtom.Get,
            _ => throw new NotImplementedException()
        };
    }
}