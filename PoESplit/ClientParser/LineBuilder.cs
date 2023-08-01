using System.Collections.Generic;
using System.Text;

namespace PoESplit.ClientParser
{
    class LineBuilder
    {
        private StringBuilder fStringBuilder = new StringBuilder();
        private Queue<string> fQueueLines = new Queue<string>();

        public string TryReadLine()
        {
            if (fQueueLines.Count > 0)
            {
                return fQueueLines.Dequeue();
            }
            else
            {
                return null;
            }
        }

        public void AddCharacter(char c)
        {
            if (c == '\r' || c == '\n')
            {
                if (fStringBuilder.Length > 0)
                {
                    fQueueLines.Enqueue(fStringBuilder.ToString());
                    fStringBuilder.Clear();
                }
            }
            else
            {
                fStringBuilder.Append(c);
            }
        }
    }
}
