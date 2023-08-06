using PoESplit.ExileKit;
using System.Linq;

namespace PoESplit
{
    static class PlayerInformation
    {
        public static string fArea = null;
        public static bool fPlayerPositionKnown = false;
        public static int fActIdx;
        public static int fPinIdx;

        public static bool fPlayerNameAndLevelKnown = false;
        public static bool fLoggedToCharacterScreen = false;
        public static string fPlayerName = null;
        public static int fPlayerLevel = 1;

        public static void NotifyRunReset()
        {
            fPlayerNameAndLevelKnown = false;
            fLoggedToCharacterScreen = false;
            fPlayerName = null;
            fPlayerLevel = 1;
        }

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
            if (area != null)
            {
                for (actIdx = 0; actIdx < BakedData.fMapPins.Length; ++actIdx)
                {
                    for (pinIdx = 0; pinIdx < BakedData.fMapPins[actIdx].Count; ++pinIdx)
                    {
                        if(BakedData.fMapPins[actIdx][pinIdx].Areas.Any(w => w.Code == area))
                        {
                            return true;
                        }
                    }
                }
            }

            actIdx = default;
            pinIdx = default;
            return false;
        }
    }
}
