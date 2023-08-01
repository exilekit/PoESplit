using System.Text.RegularExpressions;

namespace PoESplit.ClientParser.Body
{
    /// <summary>
    /// Gives the code for the area, which is unique.
    /// </summary>
    class GeneratingLevelLine
    {
        // language=regex
        public const string kRegex = @"^Generating\slevel\s(?<level>\d+)\sarea\s""(?<area>\w+)""\swith\sseed\s(?<seed>\d+)$";
        
        public readonly int fLevel;
        public readonly string fArea;
        public readonly ulong fSeed;

        private GeneratingLevelLine(int level, string area, ulong seed)
        {
            fLevel = level;
            fArea = area;
            fSeed = seed;
        }

        public static GeneratingLevelLine TryParse(string line)
        {
            Regex regex = new Regex(kRegex, RegexOptions.Multiline);
            Match match = regex.Match(line);
            if (match.Success)
            {
                return new GeneratingLevelLine(
                    int.Parse(match.Groups["level"].Value),
                    match.Groups["area"].Value,
                    ulong.Parse(match.Groups["seed"].Value));
            }
            else
            {
                return null;
            }
        }
    }
}
