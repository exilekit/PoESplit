namespace PoESplit
{
    public class MapPin
    {
        public double X { get; set; }
        public double Y { get; set; }
        public bool IsTown { get; set; }
        public bool IsWaypoint { get; set; }
        public string Name { get; set; }
        public string[] Codes { get; set; }
    }
}
