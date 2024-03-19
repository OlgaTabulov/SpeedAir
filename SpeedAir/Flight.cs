namespace SpeedAir
{
    public class Flight
    {
        public int FlightId { get; set; }
        public Route Route { get; set; }
        public int AvailableLoad { get; set; }

        public Flight(int flightId, Destination from, Destination to, int availableLoad = 0)
        {
            FlightId = flightId;
            AvailableLoad = availableLoad == 0 ? Program.DEFAULT_MAX_LOAD : availableLoad;
            Route = new Route() { From = from, To = to };
        }
    }
}
