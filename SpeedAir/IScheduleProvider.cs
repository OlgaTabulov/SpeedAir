using System.Collections.Generic;


namespace SpeedAir
{
    public interface IScheduleProvider
    {
        public IEnumerable<Itinerary> CreateItenary();
    }
}
