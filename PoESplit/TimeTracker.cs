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
        private int? fPlayerActIdx;
        private int fPlayerPinIdx;

        public readonly Stopwatch fStopwatch = new Stopwatch();
        private TimeSpan fLastElapsed;

        // display all the map pin times
        // display an act time
        // display a total running time (shared across all the acts)

        private MapTimestamp[] fActTimestamps;
        private List<MapTimestamp>[] fMapPinTimestamps;

        private MapTimestamp fUnknownArea = new MapTimestamp();

        public TimeTracker()
        {
            fMapPinTimestamps = BakedData.fMapPins.Select(a => a.Select(b => new MapTimestamp(b)).ToList()).ToArray();
        }

        public List<MapTimestamp>[] MapPinTimestamps
        {
            get
            {
                return fMapPinTimestamps;
            }
        }

        public void SetPlayerPosition(int? actIdx, int pinIdx)
        {
            fPlayerActIdx = actIdx;
            fPlayerPinIdx = pinIdx;
        }

        public void Tick()
        {
            if (fStopwatch.IsRunning)
            {
                TimeSpan currentElapsed = fStopwatch.Elapsed;
                TimeSpan diff = currentElapsed - fLastElapsed;
                fLastElapsed = currentElapsed;

                if (fPlayerActIdx.HasValue)
                {
                    fMapPinTimestamps[fPlayerActIdx.Value][fPlayerPinIdx].AddTime(diff);
                }
                else
                {
                    //fUnknownArea.AddTime(diff);
                }
            }
        }
    }
}
