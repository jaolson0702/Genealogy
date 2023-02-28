using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinshipCalculate.Tools
{
    public static class FormatTools
    {
        public static string GetGenerationPrefix(int generation) => Math.Abs(generation) switch
        {
            0 or 1 => string.Empty,
            2 => "grand",
            3 => "great-grand",
            _ => $"{GetNumberEnding(generation - 2)}-great-grand"
        };

        public static string GetNumberEnding(int number)
        {
            string numStr = number.ToString();
            if (numStr.Length > 1)
            {
                int lastTwo = int.Parse(numStr[^2..]);
                if (lastTwo >= 11 && lastTwo <= 13) return numStr + "th";
            }
            return numStr + int.Parse(numStr[^1].ToString()) switch
            {
                1 => "st",
                2 => "nd",
                3 => "rd",
                _ => "th"
            };
        }
    }
}