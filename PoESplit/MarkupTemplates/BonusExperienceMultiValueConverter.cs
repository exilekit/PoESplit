using PoESplit.ExileKit;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace PoESplit.MarkupTemplates
{
    class BonusExperienceMultiValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            int areaLevel = System.Convert.ToInt32(values[0]);
            int playerLevel = System.Convert.ToInt32(values[1]);

            int areaXP;
            int playerLevelXP;

            if (
                TryGetExperienceForZone(areaLevel, playerLevel, out areaXP) &&
                TryGetExperienceForZone(playerLevel, playerLevel, out playerLevelXP) &&
                areaXP != 0 && playerLevelXP != 0)
            {
                double percent = (double)areaXP / playerLevelXP - 1.0;
                if (percent >= 0.0)
                {
                    return "+" + percent.ToString("P0");
                }
                else
                {
                    return percent.ToString("P0");
                }
            }
            else
            {
                return string.Empty;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private static bool TryGetExperienceForZone(int zoneLevel, int playerLevel, out int experience)
        {
            if (BakedData.fExperienceForZoneLevel.TryGetValue(zoneLevel, out experience))
            {
                ExperiencePenality ep = new ExperiencePenality(playerLevel, zoneLevel);
                experience = (int)(experience * ep.fXpMultiplier);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
