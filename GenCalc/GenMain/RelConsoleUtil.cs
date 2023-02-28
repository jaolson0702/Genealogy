using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenCalc;

namespace GenMain
{
    public static class RelConsoleUtil
    {
        public static void PrintSpecs(this Rel rel)
        {
            Console.WriteLine();
            rel.PrintRelTransition();
            if (rel is Kinship b) Console.WriteLine("\nCommon Progenitor: " + (Lineal)b.Count.ClosestCommonLineal.ACount);
        }

        public static void PrintRelTransition(this Rel rel)
        {
            Console.WriteLine("---------------------------");
            Console.WriteLine("Value Applied | Derived Rel");
            Console.WriteLine("---------------------------");
            Rel copy = 0;
            foreach (RelValue value in rel.Values)
            {
                copy += value;
                Console.WriteLine(string.Format("{0, 13} | {1, -16}", value.Formatted(), copy));
            }
            if (copy == 0)
                Console.WriteLine(string.Format("{0, 13} | {1, -16}", "none", copy));
        }
    }
}