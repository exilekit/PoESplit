using System.Text.RegularExpressions;

namespace PoESplit.ClientParser.Body
{
    /// <summary>
    /// The specified character in the zone leveled up.
    /// </summary>
    class LeveledUpLine
    {
        // language=regex
        public const string kRegex = @"^:\s(?<name>.+)\s\((?<class>.+)\)\sis\snow\slevel\s(?<level>\d+)$";

        public readonly string fName;
        public readonly string fCharacterClass;
        public readonly int fLevel;

        private LeveledUpLine(string name, string characterClass, int level)
        {
            fName = name;
            fCharacterClass = characterClass;
            fLevel = level;
        }

        public static LeveledUpLine TryParse(string line)
        {
            Regex regex = new Regex(kRegex, RegexOptions.Multiline);
            Match match = regex.Match(line);
            if (match.Success)
            {
                return new LeveledUpLine(
                    match.Groups["name"].Value,
                    match.Groups["class"].Value,
                    int.Parse(match.Groups["level"].Value));
            }
            else
            {
                return null;
            }
        }
    }
}
