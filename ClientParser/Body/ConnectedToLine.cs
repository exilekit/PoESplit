using System.Text.RegularExpressions;

namespace PoESplit.ClientParser.Body
{
    /// <summary>
    /// Means the user is in the character selection screen.
    /// </summary>
    class ConnectedToLine
    {
        // language=regex
        public const string kRegex = @"^Connected\sto\s.+?\.login\.pathofexile\.com\sin\s\d+ms\.$";

        private ConnectedToLine()
        {
        }

        public static ConnectedToLine TryParse(string line)
        {
            Regex regex = new Regex(kRegex, RegexOptions.Multiline);
            Match match = regex.Match(line);
            if (match.Success)
            {
                return new ConnectedToLine();
            }
            else
            {
                return null;
            }
        }
    }
}
