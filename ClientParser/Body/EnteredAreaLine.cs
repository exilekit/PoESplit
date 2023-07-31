using System.Text.RegularExpressions;

namespace PoESplit.ClientParser.Body
{
    /// <summary>
    /// Gives the area name, but the area name is not unique.
    /// </summary>
    class EnteredAreaLine
    {
        // language=regex
        public const string kRegex = @"^:\sYou\shave\sentered\s(?<areaname>.+?)\.$";
        public readonly string fArea;
        private EnteredAreaLine(string area)
        {
            fArea = area;
        }

        public static EnteredAreaLine TryParse(string line)
        {
            Regex regex = new Regex(kRegex, RegexOptions.Multiline);
            Match match = regex.Match(line);
            if (match.Success)
            {
                return new EnteredAreaLine(match.Groups["areaname"].Value);
            }
            else
            {
                return null;
            }
        }
    }
}
