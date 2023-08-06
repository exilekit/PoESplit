using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace PoESplit.MarkupTemplates
{
    class PenalityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (PlayerInformation.fPlayerNameAndLevelKnown)
            {
                int areaLevel = System.Convert.ToInt32(value);
                if (areaLevel < PlayerInformation.fPlayerLevel)
                {
                    return Brushes.Red;
                }
                else
                {
                    return Brushes.Lime;
                }
            }
            else
            {
                return Brushes.White;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
