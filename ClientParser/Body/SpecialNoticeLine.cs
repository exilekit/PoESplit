using System.Text.RegularExpressions;

namespace PoESplit.ClientParser.Body
{
    /// <summary>
    /// Indicates that Path of Exile is starting up.
    /// </summary>
    class SpecialNoticeLine
    {
        // language=regex
        public const string kRegex = @"^\*{5}\s(?<specialnotice>.*?)\s\*{5}$";
        public readonly string fNotice;
        private SpecialNoticeLine(string notice)
        {
            fNotice = notice;
        }

        public static SpecialNoticeLine TryParse(string line)
        {
            Regex regex = new Regex(kRegex, RegexOptions.Multiline);
            Match match = regex.Match(line);
            if (match.Success)
            {
                return new SpecialNoticeLine(
                    match.Groups["specialnotice"].Value);
            }
            else
            {
                return null;
            }
        }
    }
}
