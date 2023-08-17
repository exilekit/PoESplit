using System;

namespace PoESplit
{
    struct ExperiencePenalty
    {
        public int fSafeZone;
        public double fEffectiveDifference;
        public double fXpMultiplier;
        
        // https://exilekit.github.io/ExperiencePenalty.html
        /// <summary>[95-99]</summary>
        static double[] kTableOfPenalties = new double[]
        {
            0.9957,
            0.9867,
            0.9644,
            0.9202,
            0.8400
        };

        public ExperiencePenalty(int playerLevel, int areaLevel)
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
                    fXpMultiplier *= kTableOfPenalties[playerLevel - 95];
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

        public bool Penalized 
        { 
            get 
            {
                const double kApproximatelyZero = 1.0 / 1_000_000;

                return fEffectiveDifference > kApproximatelyZero; 
            } 
        }

        public override string ToString()
        {
            return (1.0 - fXpMultiplier).ToString("P2");
        }
    }
}
