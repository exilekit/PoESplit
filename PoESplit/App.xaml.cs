using System;
using System.Windows;

namespace PoESplit
{
    public partial class App : Application
    {
        public const string kVersion = "0.3.0.0";

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
