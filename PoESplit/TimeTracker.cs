using PoESplit.ExileKit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace PoESplit
{
    public class TimeTracker
    {
        public readonly Stopwatch fStopwatch = new Stopwatch();
        private TimeSpan fLastElapsed;

        public MapTimestamp fCampaignTime;
        public MapTimestamp[] fActTimestamps;
        public List<MapTimestamp>[] fMapPinTimestamps;

        private MapTimestamp fUnknownArea = new MapTimestamp();

        public TimeTracker()
        {
            fMapPinTimestamps = BakedData.fMapPins.Select(a => a.Select(b => new MapTimestamp(b)).ToList()).ToArray();
            fActTimestamps = BakedData.fMapPins.Select(a => new MapTimestamp()).ToArray();
            fCampaignTime = new MapTimestamp();
        }

        public void Tick()
        {
            if (fStopwatch.IsRunning)
            {
                TimeSpan currentElapsed = fStopwatch.Elapsed;
                TimeSpan diff = currentElapsed - fLastElapsed;
                fLastElapsed = currentElapsed;

                if (PlayerInformation.fPlayerPositionKnown)
                {
                    fMapPinTimestamps[PlayerInformation.fActIdx][PlayerInformation.fPinIdx].AddTime(diff);
                    fActTimestamps[PlayerInformation.fActIdx].AddTime(diff);
                }
                else
                {
                    //fUnknownArea.AddTime(diff);
                }

                fCampaignTime.AddTime(diff);
            }
        }
    }
}
