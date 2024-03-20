using System;
using System.Collections.Generic;
using System.Linq;


namespace SpeedAir
{
    public class OrderScheduler : Scheduler
    {
        private IOrderProvider _orderProvider = null;
        private List<Order> _orders;
        private Dictionary<string, Itinerary> _orderDeliverySchedule = new Dictionary<string, Itinerary>();

        public OrderScheduler(IScheduleProvider scheduleProvider, IOrderProvider orderProvider): base(scheduleProvider)
        {
            _orderProvider = orderProvider;
        }
        public void LoadOrders()
        {
            _orders = _orderProvider.GetOrders().ToList();
        }
        internal void PopulateItenary()
        {
            foreach (var order in _orders)
            {
                foreach (var itinerary in _itineraries)
                {
                    if (itinerary.Load < itinerary.Flight.AvailableLoad && itinerary.Flight.To == order.To)
                    {
                        itinerary.Load++;
                        _orderDeliverySchedule.Add(order.Id, itinerary);
                        break;
                    }
                }

            }
        }
        public void PrintItenaryWithOrders()
        {
            foreach (var delivery in _orderDeliverySchedule)
            {
                var itinerary = delivery.Value;
                Console.WriteLine("order: " + delivery.Key + ", flightNumber: " + itinerary.Flight.FlightId + ", departure: " + itinerary.Flight.From + ", arrival:  " + itinerary.Flight.To + ", day: " + itinerary.Day);
            }

            Console.WriteLine("Total number of deliveries " + _orderDeliverySchedule.Count);
        }
    }
}
