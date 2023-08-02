using System;
using System.Globalization;
using System.Windows.Data;

namespace PoESplit
{
    class AddHalfMultiValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (double.IsNaN((double)values[1]))
            {
                values[1] = 0.0;
            }
            return (double)values[0] + ((double)values[1] / 2.0);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
