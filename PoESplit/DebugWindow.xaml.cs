using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace PoESplit
{
    public partial class DebugWindow : Window
    {
        public DebugWindow(MainWindow mainWindow)
        {
            Binding binding = new Binding("IsDebugVisible");
            binding.Source = mainWindow;
            binding.Mode = BindingMode.TwoWay;
            binding.Converter = new BooleanToVisibilityConverter();
            this.SetBinding(Window.VisibilityProperty, binding);

            InitializeComponent();

            LogMessage("PoESplit initialized");
            listboxLog.SelectionChanged += ListboxLog_SelectionChanged;
        }

        public void LogMessage(string message)
        {
            const int kMaxEntries = 500;
            DateTime now = DateTime.Now;

            ListBoxItem lbi = new ListBoxItem();
            lbi.Content = now.ToString("HH:mm:ss") + " " + message;

            listboxLog.Items.Insert(0, lbi);
            if (listboxLog.Items.Count > kMaxEntries)
            {
                listboxLog.Items.RemoveAt(listboxLog.Items.Count - 1);
            }
        }

        private void ListboxLog_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBoxItem lbi = listboxLog.SelectedItem as ListBoxItem;
            string tb = lbi.Content as string;
            if (tb != null)
            {
                Clipboard.SetText(tb);
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}
