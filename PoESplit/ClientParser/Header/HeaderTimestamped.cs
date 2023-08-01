using System;
using System.Text.RegularExpressions;

namespace PoESplit.ClientParser.Header
{
    class HeaderTimestamped
    {
        // language=regex
        public const string kRegex = @"^(?<timestamp>[^\s]+\s[^\s]+)\s(?<remainder>.*)$";
        public readonly DateTime fDateTime;
        public readonly string fRemainder;
        private HeaderTimestamped(DateTime dateTime, string remainder)
        {
            fDateTime = dateTime;
            fRemainder = remainder;
        }

        public static HeaderTimestamped TryParse(string line)
        {
            Regex regex = new Regex(kRegex, RegexOptions.Multiline);
            Match match = regex.Match(line);
            if (match.Success)
            {
                return new HeaderTimestamped(
                    DateTime.Parse(match.Groups["timestamp"].Value),
                    match.Groups["remainder"].Value);
            }
            else
            {
                return null;
            }
        }
    }
}
