using System.Collections.Generic;


namespace SpeedAir
{
    public class Itinerary
    {
        public int Day { get; set; }
        public Flight Flight;
        public int Load { get; set; }
        public Itinerary(int day, int flightId, Destination from, Destination to)
        {
            Day = day;
            Flight = new Flight(flightId, from, to);
        }
    }
}
