using PoESplit.ExileKit;

namespace PoESplit
{
    static class PlayerInformation
    {
        public static string fArea = null;
        public static bool fPlayerPositionKnown = false;
        public static int fActIdx;
        public static int fPinIdx;

        public static void GeneratedLevel(string area)
        {
            int actIdx;
            int pinIdx;
            if (TryFindMapPinForArea(area, out actIdx, out pinIdx))
            {
                fPlayerPositionKnown = true;
                fActIdx = actIdx;
                fPinIdx = pinIdx;
            }
            else
            {
                fPlayerPositionKnown = false;
            }
        }

        private static bool TryFindMapPinForArea(string area, out int actIdx, out int pinIdx)
        {
            for (actIdx = 0; actIdx < BakedData.fZoneToPins.Length; ++actIdx)
            {
                if (area != null && BakedData.fZoneToPins[actIdx].TryGetValue(area, out pinIdx))
                {
                    return true;
                }
            }

            actIdx = default;
            pinIdx = default;
            return false;
        }
    }
}
