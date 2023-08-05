using System;
using System.ComponentModel;
using System.Linq;

namespace PoESplit
{
    public class MapPinMetrics : INotifyPropertyChanged
    {
        public const double kWidth = 100.0;

        public TimeSpan fTimeSpan;
        private readonly double fX;
        private readonly double fY;
        private readonly string fLevel;

        public event PropertyChangedEventHandler PropertyChanged;

        public MapPinMetrics()
        {

        }

        public MapPinMetrics(MapPin mapPin)
        {
            fX = mapPin.X;
            fY = mapPin.Y;
            if (mapPin.IsTown)
            {
                fLevel = "Town";
            }
            else
            {
                fLevel = "Lvl " + string.Join("/", mapPin.Areas.Select(w => w.Level));
            }
        }

        public string Level
        {
            get
            {
                return fLevel;
            }
        }

        public double X
        {
            get
            {
                return fX;
            }
        }

        public double Y
        {
            get
            {
                return fY;
            }
        }

        public bool IsVisible
        {
            get
            {
                return true;
                return fTimeSpan != TimeSpan.Zero;
            }
        }

        public void AddTime(TimeSpan span)
        {
            fTimeSpan += span;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Time)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsVisible)));
        }

        public string Time
        {
            get
            {
                return FormatTimeSpanMinimal(fTimeSpan);
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
