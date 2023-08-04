using PoESplit.ExileKit;
using System;
using System.IO;
using System.Linq;

namespace PoESplit
{
    static class RunExporter
    {
        public static void Export(string path, TimeTracker timeTracker)
        {
            using (StreamWriter sw = File.CreateText(path))
            {
                for (int actIdx = 0; actIdx < BakedData.fMapPins.Length; ++actIdx)
                {
                    WriteAct(sw, $"Act {actIdx + 1}", timeTracker.fActTimestamps[actIdx]);

                    for (int pinIdx = 0; pinIdx < BakedData.fMapPins[actIdx].Count(); ++pinIdx)
                    {
                        WriteMapPin(sw, BakedData.fMapPins[actIdx][pinIdx].Name, timeTracker.fMapPinTimestamps[actIdx][pinIdx]);
                    }
                }
            }
        }

        private static void WriteAct(StreamWriter sw, string act, MapTimestamp time)
        {
            WriteRow(sw, act, time.fTimeSpan);
        }

        private static void WriteMapPin(StreamWriter sw, string pinName, MapTimestamp time)
        {
            WriteRow(sw, string.Empty, string.Empty, pinName, time.fTimeSpan);
        }

        private static void WriteRow(StreamWriter sw, params object[] cells)
        {
            sw.WriteLine(string.Join(",", cells.Select(w => FormatCellValue(w))));
        }

        private static string FormatCellValue(object o)
        {
            if (o is string str)
            {
                return $"\"{str}\"";
            }
            else if (o is TimeSpan ts)
            {
                return ts.ToString(@"hh\:mm\:ss");
            }
            else
            {
                return o.ToString();
            }
        }
    }
}
