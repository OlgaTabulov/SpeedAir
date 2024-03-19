using System;
using System.Collections.Generic;
using System.Linq;


namespace SpeedAir
{
    public class Scheduler
    {
        IOrderProvider _orderProvider = null;
        IScheduleProvider _scheduleProvider = null;
        List<Order> _orders;
        List<Itinerary> _itineraries;
        Dictionary<string, Itinerary> _orderDeliverySchedule;

        public Scheduler(IScheduleProvider scheduleProvider, IOrderProvider orderProvider = null)
        {
            _orderProvider = orderProvider;
            _scheduleProvider = scheduleProvider;
        }

        public void LoadOrders()
        {
            _orders = _orderProvider.GetOrders().ToList();
        }

        public void CreateItenary()
        {
            _itineraries = _scheduleProvider.CreateItenary().ToList();
            _orderDeliverySchedule = new Dictionary<string, Itinerary>();

        }

        public void PrintItenary()
        {
            foreach (var itinerary in _itineraries)
            {
                Console.WriteLine("Flight: " + itinerary.Day + ", departure: " + itinerary.Flight.Route.From + ", arrival: " + itinerary.Flight.Route.To + ", day:  " + itinerary.Day);
            }

            Console.WriteLine("Total number of flights " + _itineraries.Count);
        }

        public void PrintItenaryWithOrders()
        {
            foreach (var delivery in _orderDeliverySchedule)
            {
                var itinerary = delivery.Value;
                Console.WriteLine("order: "+delivery.Key+ ", flightNumber: " + itinerary.Flight.FlightId + ", departure: " + itinerary.Flight.Route.From + ", arrival:  " + itinerary.Flight.Route.To + ", day: " + itinerary.Day);
            } 

            Console.WriteLine("Total number of deliveries " + _orderDeliverySchedule.Count);
        }
        internal void PopulateItenary()
        {
            foreach(var order in _orders)
            {
                foreach(var itinerary in _itineraries)
                {
                    if (itinerary.Load < itinerary.Flight.AvailableLoad && itinerary.Flight.Route.To == order.To)
                    {
                        itinerary.Load++;
                        itinerary.Orders.Add(order);
                        _orderDeliverySchedule.Add(order.Id, itinerary);
                        break;
                    }
                }

            }
        }
    }
}
