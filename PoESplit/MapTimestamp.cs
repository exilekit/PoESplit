using System;
using System.ComponentModel;

namespace PoESplit
{
    public class MapTimestamp : INotifyPropertyChanged
    {
        TimeSpan fTimeSpan;

        public event PropertyChangedEventHandler PropertyChanged;

        public void AddTime(TimeSpan span)
        {
            fTimeSpan += span;
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(Time)));
        }

        public string Time
        {
            get
            {
                return fTimeSpan.ToString();
            }
        }

        public static string FormatTimeSpanMinimal(TimeSpan ts)
        {
            if (ts.Hours > 0)
            {
                return ts.ToString("h\\:mm\\:ss");
            }
            else
            {
                return ts.ToString("m\\:ss");
            }
        }
    }
}
