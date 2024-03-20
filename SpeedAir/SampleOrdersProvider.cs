using System;
using System.Collections.Generic;


namespace SpeedAir
{
    public class SampleOrdersProvider : IOrderProvider
    {
        private readonly string _sampleOrdersFilePath;

        public SampleOrdersProvider(string sampleOrdersFilePath)
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
                
                var isEnumParsed = Enum.TryParse(ordersSerialized[key].destination, true, out Destination outputDestination);

                if (!string.IsNullOrEmpty(key) && isEnumParsed)
                {
                    var order = new Order();
                    order.Id = key;
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
}
