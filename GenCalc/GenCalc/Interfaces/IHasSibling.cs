using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenCalc
{
    public interface IHasSibling
    {
        bool HasHalfSibling { get; }
        Rel AsHalf { get; }
        Rel AsFull { get; }
    }
}