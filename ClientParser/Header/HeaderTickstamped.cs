using System.Text.RegularExpressions;

namespace PoESplit.ClientParser.Header
{
    class HeaderTickstamped
    {
        // language=regex
        public const string kRegex = @"^(?<servertick>\d+)\s\w*\s\[.*?\]\s(?<remainder>.*)$";
        public readonly ulong fTick;
        public readonly string fRemainder;
        private HeaderTickstamped(ulong tick, string remainder)
        {
            fTick = tick;
            fRemainder = remainder;
        }

        public static HeaderTickstamped TryParse(string line)
        {
            Regex regex = new Regex(kRegex, RegexOptions.Multiline);
            Match match = regex.Match(line);
            if (match.Success)
            {
                return new HeaderTickstamped(
                    ulong.Parse(match.Groups["servertick"].Value),
                    match.Groups["remainder"].Value);
            }
            else
            {
                return null;
            }
        }
    }
}
