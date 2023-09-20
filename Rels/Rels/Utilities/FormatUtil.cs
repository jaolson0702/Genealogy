using System.ComponentModel.DataAnnotations;

namespace Rels
{
    public static class FormatUtil
    {
        public static string GetGenerationPrefix([Range(1, int.MaxValue)] int generation) => generation switch
        {
            0 or 1 => string.Empty,
            2 => "grand",
            3 => "great-grand",
            _ => $"{GetNumberEnding(generation - 2)}-great-grand"
        };

        public static string GetNumberEnding([Range(1, int.MaxValue)] int number)
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