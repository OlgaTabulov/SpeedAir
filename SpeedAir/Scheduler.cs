using System;
using System.Collections.Generic;
using System.Linq;


namespace SpeedAir
{
    public class Scheduler
    {
        private IScheduleProvider _scheduleProvider = null;
        protected List<Itinerary> _itineraries;

        public Scheduler(IScheduleProvider scheduleProvider)
        {
            _scheduleProvider = scheduleProvider;
        }
        public void CreateItenary()
        {
            _itineraries = _scheduleProvider.CreateItenary().ToList();

        }
        public void PrintItenary()
        {
            foreach (var itinerary in _itineraries)
            {
                Console.WriteLine("Flight: " + itinerary.Day + ", departure: " + itinerary.Flight.From + ", arrival: " + itinerary.Flight.To + ", day:  " + itinerary.Day);
            }

            Console.WriteLine("Total number of flights " + _itineraries.Count);
        }
    }
}
