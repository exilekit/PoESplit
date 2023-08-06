﻿using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace PoESplit.MarkupTemplates
{
    class PenalityMultiValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (PlayerInformation.fPlayerNameAndLevelKnown)
            {
                int areaLevel = System.Convert.ToInt32(values[0]);
                int playerLevel = System.Convert.ToInt32(values[1]);

                if (areaLevel < playerLevel)
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

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}