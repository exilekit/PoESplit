﻿using PoESplit.ExileKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace PoESplit
{
    static class CsvExporter
    {
        public static void Export(string path, MetricsTracker metricTracker)
        {
            try
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    // write the total campaign time
                    WriteRow(sw, new List<object>() { "Total Campaign", metricTracker.fCampaignTime.fTimeSpan });
                    WriteRow(sw, new List<object>() { });

                    // write the act sums
                    {
                        List<object> row = new List<object>();
                        for (int actIdx = 0; actIdx < BakedData.fMapPins.Length; ++actIdx)
                        {
                            row.Add($"Act {actIdx + 1}");
                            row.Add(metricTracker.fActTimestamps[actIdx].fTimeSpan);
                        }
                        WriteRow(sw, row);
                        WriteRow(sw, new List<object>() { });
                    }

                    // write the map pins
                    {
                        int pinIdx = 0;
                        bool rowHadData;
                        do
                        {
                            rowHadData = false;
                            List<object> row = new List<object>();

                            for (int actIdx = 0; actIdx < BakedData.fMapPins.Length; ++actIdx)
                            {
                                if (pinIdx < BakedData.fMapPins[actIdx].Count)
                                {
                                    row.Add(BakedData.fMapPins[actIdx][pinIdx].Name);
                                    row.Add(metricTracker.fMapPinMetrics[actIdx][pinIdx].fTimeSpan);
                                    rowHadData = true;
                                }
                                else
                                {
                                    row.Add(string.Empty);
                                    row.Add(string.Empty);
                                }
                            }
                            WriteRow(sw, row);

                            pinIdx++;
                        }
                        while (rowHadData);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Failed to export CSV. Perhaps the destination file is in use?", "Error during export", MessageBoxButton.OK);
            }
        }

        private static void WriteRow(StreamWriter sw, List<object> cells)
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
