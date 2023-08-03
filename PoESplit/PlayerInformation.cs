using PoESplit.ExileKit;

namespace PoESplit
{
    static class BakedDataHelper
    {
        public static bool TryFindMapPinForArea(string area, out int actIdx, out int pinIdx)
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
