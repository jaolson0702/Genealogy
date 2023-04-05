namespace Rels
{
    public class GrandparentAtom : RelAtom
    {
        public static readonly GrandparentAtom Get = new();

        private GrandparentAtom()
        { }

        public override RelValue WithChild => HalfPiblingAtom.Get;
        public override RelValue WithFullSibling => new PiblingMolecule(2, false);
        public override RelValue WithHalfSibling => new PiblingMolecule(2, true);
        public override RelValue WithParent => new GreatGrandparentMolecule(1);
        public override RelSubatomic[] SubatomicValues => new[] { RelSubatomic.Parent, RelSubatomic.Parent };

        public override string ToString(Gender? gender) => "grand" + ParentAtom.Get.ToString(gender);
    }
}