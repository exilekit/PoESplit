using System;
using System.ComponentModel;

namespace PoESplit
{
    public class MapTimestamp : INotifyPropertyChanged
    {
        public const double kWidth = 100.0;

        TimeSpan fTimeSpan;
        private readonly double fX;
        private readonly double fY;

        public event PropertyChangedEventHandler PropertyChanged;

        public MapTimestamp()
        {

        }

        public MapTimestamp(MapPin mapPin)
        {
            fX = mapPin.X;
            fY = mapPin.Y;
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
