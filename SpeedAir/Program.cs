using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace SpeedAir
{
    class Program
    {
        static IOrderProvider _orderProvider = null;
        static IScheduleProvider _scheduleProvider = null;
        static void Main(string[] args)
        {
            int.TryParse((args == null || args.Length == 0) ? "0" : args[0], out int story);

            switch (story) {
                case 0:
                    RunEmptyScedule();
                    break;
                default:
                    RunOrdersSchedule();
                    break;
            }
            
        }

        private static void RunOrdersSchedule()
        {
            Console.WriteLine("Welcome to Speed Air Story # 2! ");
            _orderProvider = new SampleOrders(Path.GetFullPath(@"coding-assigment-orders.json"));
            _scheduleProvider = new ScheduleProvider();
            var scheduler = new Scheduler(_scheduleProvider, _orderProvider);
            scheduler.LoadOrders();
            scheduler.PrintOrders();
            scheduler.CreateItenary();
            scheduler.PopulateItenary();
            scheduler.PrintItenaryWithOrders();
        }

        private static void RunEmptyScedule()
        {
            Console.WriteLine("Welcome to Speed Air Story # 1! ");
            _orderProvider = new SampleOrders(Path.GetFullPath(@"coding-assigment-orders.json"));
            _scheduleProvider = new ScheduleProvider();
            var scheduler = new Scheduler(_scheduleProvider);
            scheduler.CreateItenary();
            scheduler.PrintItenary();
        }

        public static readonly int DEFAULT_MAX_LOAD = 20;

        
    }

    public class Scheduler
    {
        IOrderProvider _orderProvider = null;
        IScheduleProvider _scheduleProvider = null;
        List<Order> _orders;
        List<Itenarary> _itenaries;

        public Scheduler(IScheduleProvider scheduleProvider, IOrderProvider orderProvider = null)
        {
            _orderProvider = orderProvider;
            _scheduleProvider = scheduleProvider;
        }

        public void LoadOrders()
        {
            _orders = _orderProvider.GetOrders().ToList();
        }

        public void PrintOrders()
        {

            foreach(var order in _orders)
            {
                Console.WriteLine("Order#" + order.Id + " to " + order.To);
            }

            Console.WriteLine("Total number of orders " + _orders.Count);
        }

        public void CreateItenary()
        {
            _itenaries = _scheduleProvider.CreateItenary().ToList(); 

        }

        public void PrintItenary()
        {
            foreach (var itenarary in _itenaries)
            {
                Console.WriteLine("Flight#" + itenarary.Day + " to " + itenarary.Flight.Route.To + " Load " + itenarary.Load);
            }

            Console.WriteLine("Total number of flights " + _itenaries.Count);
        }

        public void PrintItenaryWithOrders()
        {
            foreach (var itenarary in _itenaries)
            {
                Console.WriteLine("Flight: " + itenarary.Flight.FlightId + ", departure: " + itenarary.Flight.Route.From + ", arrival: " + itenarary.Flight.Route.To + ", day: " + itenarary.Day);
            }

            Console.WriteLine("Total number of flights " + _itenaries.Count);
        }
        internal void PopulateItenary()
        {
            foreach(var order in _orders)
            {
                foreach(var itenary in _itenaries)
                {
                    if (itenary.Load < itenary.Flight.AvailableLoad && itenary.Flight.Route.To == order.To)
                    {
                        itenary.Load++;
                        itenary.Orders.Add(order);
                        break;
                    }
                }

            }
        }
    }
    
    public enum Destination
    {
        YYZ,
        YUL,
        YVR,
        YYC
    }
    public class Route {
        public Destination From { get; set; }
        public Destination To { get; set; }

    }

    public class Flight {
        public int FlightId { get; set; }
        public Route Route { get; set; }
        public int AvailableLoad { get; set; }

        public Flight(int flightId, Destination from, Destination to, int availableLoad = 0)
        {
            AvailableLoad = availableLoad == 0 ? Program.DEFAULT_MAX_LOAD : availableLoad;
            Route = new Route() { From = from, To = to };
        }
    }

    public class Itenarary { 
        public int Day { get; set; }
        public Flight Flight;
        public int Load { get; set; }
        public List<Order> Orders { get; set; }
        public Itenarary(int day, int flightId, Destination from, Destination to)
        {
            Day = day;
            Flight = new Flight(flightId, from, to);
            Orders = new List<Order>();
        }
    }

    public interface IScheduleProvider
    {
        public IEnumerable<Itenarary> CreateItenary();
    }

    public class ScheduleProvider : IScheduleProvider
    {
        public IEnumerable<Itenarary> CreateItenary()
        {
            var itenaries = new List<Itenarary>();
            // normally this provider would be hooked up to a proper database

            itenaries.Add(new Itenarary(1, 1, Destination.YUL, Destination.YYZ));
            itenaries.Add(new Itenarary(1, 2, Destination.YUL, Destination.YYC));
            itenaries.Add(new Itenarary(1, 3, Destination.YUL, Destination.YVR));
            itenaries.Add(new Itenarary(2, 4, Destination.YUL, Destination.YYZ));
            itenaries.Add(new Itenarary(2, 5, Destination.YUL, Destination.YYC));
            itenaries.Add(new Itenarary(2, 6, Destination.YUL, Destination.YVR));

            return itenaries;

        }
    }

    public class Order
    {
        public int Id { get; set; }
        public Destination To { get; set; }
    }

    public interface IOrderProvider
    {
        IEnumerable<Order> GetOrders();
    }

    public class SampleOrders : IOrderProvider
    {
        private readonly string _sampleOrdersFilePath;

        public SampleOrders(string sampleOrdersFilePath)
        {
            _sampleOrdersFilePath = sampleOrdersFilePath;
        }
        public IEnumerable<Order> GetOrders()
        {
            var orders = new List<Order>();

            var ordersSerialized = JsonFileReader.Read<Dictionary<string, OrdersInputFileItem>>(_sampleOrdersFilePath);

            foreach (var key in ordersSerialized.Keys)
            {
                // implement validation and logging of errors
                
                var orderIdParsed = int.TryParse(key.Replace("order-", ""), out int outputOrderId);
                var isEnumParsed = Enum.TryParse(ordersSerialized[key].destination, true, out Destination outputDestination);

                if (orderIdParsed && isEnumParsed)
                {
                    var order = new Order();
                    order.Id = outputOrderId;
                    order.To = outputDestination;
                    orders.Add(order);
                }
                else
                {
                    //log invalid order
                }
               
            }
            return orders;
        }
    }

    public class OrdersInputFileItem
    {
        public string destination;
    }


    public static class JsonFileReader
    {
        public static T Read<T>(string filePath)
        {
            string text = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<T>(text);
        }
    }
}
