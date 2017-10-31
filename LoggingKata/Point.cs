namespace LoggingKata
{
    public struct Point
    {
        public Point(double lat, double lon)
        {
            Latitude = lat;
            Longitude = lon;
        }

        public double Longitude { get; set; }
        public double Latitude { get; set; }
    }
}