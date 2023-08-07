using System;
using System.ComponentModel;

namespace PoESplit
{
    public class MapTime : INotifyPropertyChanged
    {
        public TimeSpan fTimeSpan;
        public event PropertyChangedEventHandler PropertyChanged;
        public void AddTime(TimeSpan span)
        {
            fTimeSpan += span;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Time)));
        }

        public string Time
        {
            get
            {
                return App.FormatTimeSpanMinimal(fTimeSpan);
            }
        }
    }
}
