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

        public MapPinMetrics fCampaignTime;
        public MapPinMetrics[] fActTimestamps;
        public List<MapPinMetrics>[] fMapPinMetrics;

        private MapPinMetrics fUnknownArea = new MapPinMetrics();

        public TimeTracker()
        {
            fMapPinMetrics = BakedData.fMapPins.Select(a => a.Select(b => new MapPinMetrics(b)).ToList()).ToArray();
            fActTimestamps = BakedData.fMapPins.Select(a => new MapPinMetrics()).ToArray();
            fCampaignTime = new MapPinMetrics();
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
                    fMapPinMetrics[PlayerInformation.fActIdx][PlayerInformation.fPinIdx].AddTime(diff);
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
