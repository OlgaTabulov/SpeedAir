using System.Configuration;

namespace SpeedAir
{
    public class Flight
    {
        public int FlightId { get; set; }
        public Destination From { get; set; }
        public Destination To { get; set; }
        public int AvailableLoad { get; set; }

        public Flight(int flightId, Destination from, Destination to, int availableLoad = 0)
        {
            FlightId = flightId;
            AvailableLoad = availableLoad == 0 ? Config.DefaultMaxLoad : availableLoad;
            From = from;
            To = to;
        }
    }
}
