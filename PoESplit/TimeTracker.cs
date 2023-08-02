using PoESplit.ExileKit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace PoESplit
{
    public class TimeTracker
    {
        private string fPlayerAreaCode;
        private Stopwatch fStopwatch;
        private TimeSpan fLastElapsed;

        // display all the map pin times
        // display an act time
        // display a total running time (shared across all the acts)

        private MapTimestamp[] fActTimestamps;
        private List<MapTimestamp>[] fMapPinTimestamps;

        private MapTimestamp fUnknownArea = new MapTimestamp();

        public TimeTracker()
        {
            fMapPinTimestamps = BakedData.fMapPins.Select(a => a.Select(b => new MapTimestamp()).ToList()).ToArray();
        }

        // to reset just remake this whole object
        public void Start()
        {
            fStopwatch.Start();
        }

        public void Stop()
        {
            fStopwatch.Stop();
        }

        public List<MapTimestamp>[] MapPinTimestamps
        {
            get
            {
                return fMapPinTimestamps;
            }
        }

        public void SetPlayerLocation(string areaCode)
        {
            fPlayerAreaCode = areaCode;
        }

        public void Tick()
        {
            if (fStopwatch.IsRunning)
            {
                TimeSpan currentElapsed = fStopwatch.Elapsed;
                TimeSpan diff = fLastElapsed - currentElapsed;
                fLastElapsed = currentElapsed;

                int actIdx;
                int pinIdx;

                if (BakedDataHelper.TryFindMapPinForArea(fPlayerAreaCode, out actIdx, out pinIdx))
                {
                    fMapPinTimestamps[actIdx][pinIdx].AddTime(diff);
                }
                else
                {
                    fUnknownArea.AddTime(diff);
                }
            }
        }
    }
}
