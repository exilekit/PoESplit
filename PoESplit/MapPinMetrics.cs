using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace PoESplit
{
    public class MapPinMetrics : INotifyPropertyChanged
    {
        public TimeSpan fTimeSpan;
        private readonly MapPin fMapPin;

        public event PropertyChangedEventHandler PropertyChanged;

        public MapPinMetrics(MapPin mapPin)
        {
            fMapPin = mapPin;
        }

        public ICollection<MapWorldArea> Areas
        {
            get
            {
                return fMapPin.Areas;
            }
        }

        public double X
        {
            get
            {
                return fMapPin.X;
            }
        }

        public double Y
        {
            get
            {
                return fMapPin.Y;
            }
        }

        public bool IsLevelVisible
        {
            get
            {
                return !fMapPin.IsTown;
            }
        }

        public bool IsSpacerNecessary
        {
            get
            {
                return !IsTimestampVisible;
            }
        }

        public bool IsTimestampVisible
        {
            get
            {
                return fTimeSpan != TimeSpan.Zero;
            }
        }

        public void AddTime(TimeSpan span)
        {
            fTimeSpan += span;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Time)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsSpacerNecessary)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsTimestampVisible)));
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
