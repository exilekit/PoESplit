using Microsoft.Win32;
using PoESplit.ClientParser;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace PoESplit
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public readonly TimeTracker fTimeTracker;
        public readonly MapWindow fMapWindow;
        public readonly DebugWindow fDebugWindow;
        readonly LogReader fLogReader;
        LogEventController fLogEventController;
        private bool fIsMapVisible;
        private bool fIsDebugVisible;

        public MainWindow()
        {
            this.DataContext = this;
            fTimeTracker = new TimeTracker();
            fDebugWindow = new DebugWindow(this);
            fLogReader = new LogReader(this);
            fLogEventController = new LogEventController(this);
            fMapWindow = new MapWindow(this);
            InitializeComponent();

            fLogReader.TryLoadingAppDataConfig();

            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(300.0);
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Start();

            locateClient.Click += LocateClient_Click;
            UpdateLocateClientState();
        }

        private void UpdateLocateClientState()
        {
            if (fLogReader.IsOpen)
            {
                locateClient.Foreground = Brushes.Lime;
                locateClient.Content = "CONNECTED (click to stop)";
                locateClient.IsChecked = true;
            }
            else
            {
                locateClient.Foreground = Brushes.Red;
                locateClient.Content = "NOT CONNECTED (click to locate)";
                locateClient.IsChecked = false;
            }
        }

        private void LocateClient_Click(object sender, RoutedEventArgs e)
        {
            if (locateClient.IsChecked == true)
            {
                OpenFileDialog ofg = new OpenFileDialog();
                ofg.Title = "Locate \"Client.txt\" in your Path of Exile installation (it'll be in the \"logs\" subfolder):";
                ofg.Filter = "PoE Log|Client.txt";
                ofg.DefaultExt = "Client.txt";

                bool? result = ofg.ShowDialog();
                if (result == true)
                {
                    fLogReader.TryOpenClient(ofg.FileName);
                }
            }
            else
            {
                fLogReader.CloseClient();
            }

            UpdateLocateClientState();
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (fLogReader.IsOpen)
            {
                bool _ = fLogReader.TryCheckForChanges();

                string line;
                while ((line = fLogReader.TryReadLine()) != null)
                {
                    fLogEventController.DoThing(line);
                }
            }
        }

        public bool IsMapVisible
        {
            get
            {
                return fIsMapVisible;
            }
            set
            {
                fIsMapVisible = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("IsMapVisible"));
            }
        }

        public bool IsDebugVisible
        {
            get
            {
                return fIsDebugVisible;
            }
            set
            {
                fIsDebugVisible = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("IsDebugVisible"));
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Application.Current.Shutdown(0);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            Process.Start(e.Uri.AbsoluteUri);
            e.Handled = true;
        }
    }
}
