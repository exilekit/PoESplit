﻿using PoESplit.ClientParser.Body;
using PoESplit.ClientParser.Header;
using PoESplit.ExileKit;
using System;
using System.Diagnostics;

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
                    // diagonostic print this
                    Console.WriteLine(specialNotice.fNotice);
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
                            TryProcessEnteredAreaLine(tickstampedLine.fRemainder);
                    }
                }
            }
        }

        private bool TryProcessConnectedToLine(string remainder)
        {
            ConnectedToLine connectedToLine = ConnectedToLine.TryParse(remainder);
            if (connectedToLine != null)
            {
                Console.WriteLine(connectedToLine);
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool TryProcessEnteredAreaLine(string remainder)
        {
            EnteredAreaLine enteredAreaLine = EnteredAreaLine.TryParse(remainder);
            if (enteredAreaLine != null)
            {                
                // TODO: log this event
                //Console.WriteLine(enteredAreaLine.fArea);
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
                int actIdx;
                int pinIdx;
                if (TryProcessGeneratingLevelLine_ZoneArea(generatingLevel.fArea, out actIdx, out pinIdx))
                {
                    MapPin mapPin = BakedData.fMapPins[actIdx][pinIdx];

                    fMainWindow.fMap.SetPlayerPosition(actIdx, mapPin.X, mapPin.Y);
                }
                else
                {
                    fMainWindow.fMap.SetPlayerPosition(null, default, default);
                }

                // TODO: log this
                //Console.WriteLine(generatingLevel.fArea);
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool TryProcessGeneratingLevelLine_ZoneArea(string area, out int actIdx, out int pinIdx)
        {
            for (actIdx = 0; actIdx < BakedData.fZoneToPins.Length; ++actIdx)
            {
                if (BakedData.fZoneToPins[actIdx].TryGetValue(area, out pinIdx))
                {
                    return true;
                }
            }

            actIdx = default;
            pinIdx = default;
            return false;
        }
    }
}
