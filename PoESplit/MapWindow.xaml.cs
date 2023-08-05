using PoESplit.ExileKit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PoESplit
{
    public partial class MapWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ImageSource[] fBackgroundImageSource;
        private ImageSource fWorldPanelVisitedPinIcon;
        private ImageSource fWorldPanelTownPinIcon;
        private ImageSource fWorldPanelActivatedWaypointPinIcon;
        private readonly MainWindow fMainWindow;

        private int fVisibleActIdx = 0;
        private double fPlayerX = 100.0;
        private double fPlayerY = 200.0;

        public MapWindow(MainWindow mainWindow)
        {
            fMainWindow = mainWindow;
            fBackgroundImageSource = new ImageSource[BakedData.fMapPins.Length];
            for (int i = 0; i < fBackgroundImageSource.Length; ++i)
            {
                fBackgroundImageSource[i] = new BitmapImage(new Uri($"/PoESplit;component/ExileKit/WorldPanelMapAct{i+1}.png", UriKind.RelativeOrAbsolute));
            }
            
            fWorldPanelVisitedPinIcon = new BitmapImage(new Uri(@"/PoESplit;component/ExileKit/WorldPanelVisitedPinIcon.png", UriKind.RelativeOrAbsolute));
            fWorldPanelTownPinIcon = new BitmapImage(new Uri(@"/PoESplit;component/ExileKit/WorldPanelTownPinIcon.png", UriKind.RelativeOrAbsolute));
            fWorldPanelActivatedWaypointPinIcon = new BitmapImage(new Uri(@"/PoESplit;component/ExileKit/WorldPanelActivatedWaypointPinIcon.png", UriKind.RelativeOrAbsolute));

            Binding binding = new Binding("IsMapVisible");
            binding.Source = mainWindow;
            binding.Mode = BindingMode.TwoWay;
            binding.Converter = new BooleanToVisibilityConverter();
            this.SetBinding(Window.VisibilityProperty, binding);

            InitializeComponent();
        }

        public void NotifyPlayerInformationChanged(bool focus)
        {
            if (PlayerInformation.fPlayerPositionKnown)
            {
                MapPin mapPin = BakedData.fMapPins[PlayerInformation.fActIdx][PlayerInformation.fPinIdx];
                fPlayerX = mapPin.X;
                fPlayerY = mapPin.Y;

                if (focus)
                {
                    fVisibleActIdx = PlayerInformation.fActIdx;
                }
            }

            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(PlayerPositionKnown)));
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(PlayerX)));
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(PlayerY)));
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(PlayerPinName)));
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(MapPins)));
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(MapConnections)));
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(BackgroundImageSource)));
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(CanvasWidth)));
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(CanvasHeight)));
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(MapPinMetrics)));
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(ActTimestamp)));
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(CampaignTimestamp)));
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(ActNumber)));
        }

        public void NotifyRunReset()
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(MapPinMetrics)));
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(ActTimestamp)));
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(CampaignTimestamp)));
        }

        public bool PlayerPositionKnown
        {
            get
            {
                return 
                    fVisibleActIdx == PlayerInformation.fActIdx &&
                    PlayerInformation.fPlayerPositionKnown;
            }
        }

        public double PlayerX
        {
            get
            {
                return fPlayerX;
            }
        }

        public double PlayerY
        {
            get
            {
                return fPlayerY;
            }
        }

        public string PlayerPinName
        {
            get
            {
                if (PlayerInformation.fPlayerPositionKnown && fVisibleActIdx == PlayerInformation.fActIdx)
                {
                    return BakedData.fMapPins[PlayerInformation.fActIdx][PlayerInformation.fPinIdx].Name;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public ICollection<MapPin> MapPins
        {
            get
            {
                return BakedData.fMapPins[fVisibleActIdx];
            }
        }

        public ICollection<MapPinMetrics> MapPinMetrics
        {
            get
            {
                return fMainWindow.fTimeTracker.fMapPinMetrics[fVisibleActIdx];
            }
        }

        public int ActNumber
        {
            get
            {
                return fVisibleActIdx + 1;
            }
        }

        public MapPinMetrics ActTimestamp
        {
            get
            {
                return fMainWindow.fTimeTracker.fActTimestamps[fVisibleActIdx];
            }
        }

        public MapPinMetrics CampaignTimestamp
        {
            get
            {
                return fMainWindow.fTimeTracker.fCampaignTime;
            }
        }

        public ICollection<MapConnection> MapConnections
        {
            get
            {
                if (fVisibleActIdx < BakedData.fConnections.Length)
                {
                    return BakedData.fConnections[fVisibleActIdx];
                }
                else
                {
                    return new List<MapConnection>();
                }
            }
        }

        public double CanvasWidth
        {
            get
            {
                return fBackgroundImageSource[fVisibleActIdx].Width;
            }

        }

        public double CanvasHeight
        {
            get
            {
                return fBackgroundImageSource[fVisibleActIdx].Height;
            }
        }

        public ImageSource BackgroundImageSource
        {
            get
            {
                return fBackgroundImageSource[fVisibleActIdx];
            }
        }

        public ImageSource WorldPanelVisitedPinIcon
        {
            get
            {
                return fWorldPanelVisitedPinIcon;
            }
        }

        public ImageSource WorldPanelTownPinIcon
        {
            get
            {
                return fWorldPanelTownPinIcon;
            }
        }

        public ImageSource WorldPanelActivatedWaypointPinIcon
        {
            get
            {
                return fWorldPanelActivatedWaypointPinIcon;
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                fVisibleActIdx = Math.Max(0, fVisibleActIdx - 1);
                NotifyPlayerInformationChanged(false);
            }
            else if (e.Key == Key.Right)
            {
                fVisibleActIdx = Math.Min(BakedData.fMapPins.Length - 1, fVisibleActIdx + 1);
                NotifyPlayerInformationChanged(false);
            }
        }
    }
}
