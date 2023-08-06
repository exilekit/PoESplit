using PoESplit.ClientParser.Body;
using PoESplit.ClientParser.Header;
using PoESplit.ExileKit;
using System;
using System.Diagnostics;
using System.Linq;

namespace PoESplit.ClientParser
{
    class LogEventController
    {
        private readonly MainWindow fMainWindow;

        public LogEventController(MainWindow mainWindow)
        {
            fMainWindow = mainWindow;
        }

        public void DoThing(string line)
        {
            // this can fail at the very beginning of observation of the file (starting observation during a write)
            HeaderTimestamped ts = HeaderTimestamped.TryParse(line);
            if (ts != null)
            {
                SpecialNoticeLine specialNotice = SpecialNoticeLine.TryParse(ts.fRemainder);
                if (specialNotice != null)
                {
                    fMainWindow.fDebugWindow.LogMessage($"Spotted notice \"{specialNotice.fNotice}\"");
                }
                else
                {
                    HeaderTickstamped tickstampedLine = HeaderTickstamped.TryParse(ts.fRemainder);
                    Debug.Assert(tickstampedLine != null, "tmk, this cannot fail, but don't crash if it did");

                    if (tickstampedLine != null)
                    {
                        bool _ =
                            TryProcessConnectedToLine(tickstampedLine.fRemainder) ||
                            TryProcessGeneratingLevelLine(tickstampedLine.fRemainder) ||
                            TryProcessLeveledUpLine(tickstampedLine.fRemainder);
                    }
                }
            }
        }

        private bool TryProcessConnectedToLine(string remainder)
        {
            ConnectedToLine connectedToLine = ConnectedToLine.TryParse(remainder);
            if (connectedToLine != null)
            {
                PlayerInformation.fLoggedToCharacterScreen = true;
                fMainWindow.fDebugWindow.LogMessage($"Connected to login server");
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool TryProcessLeveledUpLine(string remainder)
        {
            LeveledUpLine leveledUpLine = LeveledUpLine.TryParse(remainder);
            if (leveledUpLine != null)
            {
                fMainWindow.ActuallyProcessLevelup(leveledUpLine.fName, leveledUpLine.fLevel);
                fMainWindow.fDebugWindow.LogMessage($"\"{leveledUpLine.fName}\" ({leveledUpLine.fCharacterClass}) is now level {leveledUpLine.fLevel}");
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool TryProcessGeneratingLevelLine(string remainder)
        {
            GeneratingLevelLine generatingLevel = GeneratingLevelLine.TryParse(remainder);
            if (generatingLevel != null)
            {
                PlayerInformation.GeneratedLevel(generatingLevel.fArea);
                fMainWindow.fMapWindow.NotifyChanges(true);
                fMainWindow.fDebugWindow.LogMessage($"Generating {generatingLevel.fArea} at area level {generatingLevel.fLevel}");
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
