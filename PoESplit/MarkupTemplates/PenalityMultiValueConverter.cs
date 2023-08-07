using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace PoESplit.MarkupTemplates
{
    class PenalityMultiValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            int areaLevel = System.Convert.ToInt32(values[0]);
            int playerLevel = System.Convert.ToInt32(values[1]);

            if (areaLevel < playerLevel)
            {
                return Brushes.Red;
            }
            else
            {
                ExperiencePenality ep = new ExperiencePenality(playerLevel, areaLevel);
                if (ep.Penalized)
                {
                    return Brushes.Yellow;
                }
                else
                {
                    return Brushes.Lime;
                }
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
