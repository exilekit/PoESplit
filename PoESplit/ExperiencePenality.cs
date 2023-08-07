using System;

namespace PoESplit
{
    struct ExperiencePenality
    {
        public int fSafeZone;
        public double fEffectiveDifference;
        public double fXpMultiplier;

        // https://www.poewiki.net/wiki/Experience
        /// <summary>[95-99]</summary>
        static double[] kTableOfPenalties_PoeWiki = new double[]
        {
            1.0 / 1.0650,
            1.0 / 1.1150,
            1.0 / 1.1870,
            1.0 / 1.2825,
            1.0 / 1.4000
        };

        // https://www.i-volve.net/jol/poe_xpdrop_en.php
        /// <summary>[95-99]</summary>
        static double[] kTableOfPenalties_Volve = new double[]
        {
            0.9350,
            0.8850,
            0.8125,
            0.7175,
            0.6000
        };

        // fyregrass https://www.youtube.com/watch?v=ZRqZHH4pskI
        /// <summary>[95-99]</summary>
        static double[] kTableOfPenalties_Fyregrass = new double[]
        {
            0.9957,
            0.9867,
            0.9644,
            0.9202,
            0.8400
        };

        public ExperiencePenality(int playerLevel, int areaLevel)
        {
            if (playerLevel == 100)
            {
                fSafeZone = 0;
                fEffectiveDifference = 0;
                fXpMultiplier = 1.0f;
            }
            else
            {
                double effectiveMonsterLevel = ComputeEffectiveMonsterLevel(areaLevel);

                fSafeZone = 3 + playerLevel / 16;
                fEffectiveDifference = Math.Max(0, Math.Abs(playerLevel - effectiveMonsterLevel) - fSafeZone);
                fXpMultiplier = Math.Pow((playerLevel + 5) / (playerLevel + 5 + Math.Pow(fEffectiveDifference, 2.5)), 1.5);
                if (playerLevel >= 95)
                {
                    fXpMultiplier *= (1.0 / (1.0 + 0.1 * (playerLevel - 94.0)));
                    fXpMultiplier *= kTableOfPenalties_Fyregrass[playerLevel - 95];
                }
                fXpMultiplier = Math.Max(0.01, fXpMultiplier);
            }
        }

        public static double ComputeEffectiveMonsterLevel(double areaLevel)
        {
            if (areaLevel > 70)
            {
                return -0.03 * areaLevel * areaLevel + 5.17 * areaLevel - 144.90;
            }
            else
            {
                return areaLevel;
            }
        }

        public bool Penalized { get { return fEffectiveDifference != 0; } }

        public override string ToString()
        {
            return (1.0 - fXpMultiplier).ToString("P2");
        }
    }
}
