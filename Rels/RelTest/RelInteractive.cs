using Rels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelTest
{
    public class RelInteractive
    {
        private static readonly Dictionary<int, RelSubatomic> subatomicValueDict = new()
        {
            { 1, RelSubatomic.Parent },
            { 2, RelSubatomic.Child },
            { 3, RelSubatomic.FullSibling },
            { 4, RelSubatomic.HalfSibling },
            { 5, RelSubatomic.Partner },
            { 6, RelSubatomic.Spouse },
            { 7, RelSubatomic.UnmarriedPartner },
            { 8, RelSubatomic.Null }
        };

        private static Rel currentRel = Rel.Self;

        private static void Display()
        {
            Console.Clear();
            Console.WriteLine($"\n\n{currentRel}\n\n");
            Console.WriteLine("Options:");

            foreach (var kvp in subatomicValueDict.OrderBy(p => p.Key))
            {
                Console.WriteLine($"{kvp.Key}. {(kvp.Value != RelSubatomic.Null ? kvp.Value : "Reset")}");
            }

            Console.WriteLine("\n");
        }

        public static void Run()
        {
            while (true)
            {
                Display();
                var result = Console.ReadLine();
                bool intCheck = int.TryParse(result, out var resultInt);

                if (intCheck && subatomicValueDict.TryGetValue(resultInt, out RelSubatomic subatomicValue))
                {
                    if (subatomicValue != RelSubatomic.Null)
                    {
                        currentRel += subatomicValue;
                    }
                    else
                    {
                        currentRel = Rel.Self;
                    }
                }
            }
        }
    }
}