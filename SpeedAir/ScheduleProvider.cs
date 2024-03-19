using System.Collections.Generic;


namespace SpeedAir
{
    public class ScheduleProvider : IScheduleProvider
    {
        public IEnumerable<Itinerary> CreateItenary()
        {
            var itineraries = new List<Itinerary>();
            // normally this provider would be hooked up to a proper database

            itineraries.Add(new Itinerary(1, 1, Destination.YUL, Destination.YYZ));
            itineraries.Add(new Itinerary(1, 2, Destination.YUL, Destination.YYC));
            itineraries.Add(new Itinerary(1, 3, Destination.YUL, Destination.YVR));
            itineraries.Add(new Itinerary(2, 4, Destination.YUL, Destination.YYZ));
            itineraries.Add(new Itinerary(2, 5, Destination.YUL, Destination.YYC));
            itineraries.Add(new Itinerary(2, 6, Destination.YUL, Destination.YVR));

            return itineraries;

        }
    }
}
