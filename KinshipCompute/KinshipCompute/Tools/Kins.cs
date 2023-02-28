using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinshipCompute
{
    public static class Kins
    {
        public static Self Self => Self.Get;

        public static NonLineal Sibling => 0;

        public static Progenitor Parent => 1;
        public static Progenitor Grandparent => 2;
        public static Progenitor GreatGrandparent => 3;

        public static Progeny Child => -1;
        public static Progeny Grandchild => -2;
        public static Progeny GreatGrandchild => -3;

        public static NonLineal Pibling => 1;
        public static NonLineal Grandpibling => 2;
        public static NonLineal GreatGrandpibling => 3;

        public static NonLineal Nibling => -1;
        public static NonLineal Grandnibling => -2;
        public static NonLineal GreatGrandnibling => -3;

        public static NonLineal Cousin => (1, 0);

        public static Progenitor NthGreatGrandparent(int n) => n == 0 ? Grandparent : 2 + Math.Abs(n);

        public static Progeny NthGreatGrandchild(int n) => n == 0 ? Grandchild : 2 + Math.Abs(n);

        public static NonLineal NthGreatGrandpibling(int n) => n == 0 ? Grandpibling : 2 + Math.Abs(n);

        public static NonLineal NthGreatGrandnibling(int n) => n == 0 ? Grandnibling : -2 - Math.Abs(n);

        public static NonLineal NthCousin(int degree, int timesRemoved = 0) => (degree, timesRemoved);
    }
}