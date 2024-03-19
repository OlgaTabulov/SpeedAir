using System;
using System.IO;


namespace SpeedAir
{
    class Program
    {
        static IOrderProvider _orderProvider = null;
        static IScheduleProvider _scheduleProvider = null;
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Speed Air Story # 1! ");

            RunEmptyScedule();

            Console.WriteLine("Welcome to Speed Air Story # 2! ");

            RunOrdersSchedule();
            
        }

        private static void RunOrdersSchedule()
        {
            _orderProvider = new SampleOrders(Path.GetFullPath(@"coding-assigment-orders.json"));
            _scheduleProvider = new ScheduleProvider();
            var scheduler = new Scheduler(_scheduleProvider, _orderProvider);
            scheduler.LoadOrders();
            scheduler.CreateItenary();
            scheduler.PopulateItenary();
            scheduler.PrintItenaryWithOrders();
        }

        private static void RunEmptyScedule()
        {
            _orderProvider = new SampleOrders(Path.GetFullPath(@"coding-assigment-orders.json"));
            _scheduleProvider = new ScheduleProvider();
            var scheduler = new Scheduler(_scheduleProvider);
            scheduler.CreateItenary();
            scheduler.PrintItenary();
        }

        public static readonly int DEFAULT_MAX_LOAD = 20;

        
    }
}
