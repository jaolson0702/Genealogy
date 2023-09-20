using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rels
{
    public abstract class ValueIdentifier : RelIdentifier
    {
        public static implicit operator ValueIdentifier(RelSubatomic subatomic) => (RelAtom)subatomic;
    }
}