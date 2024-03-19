using System.Collections.Generic;


namespace SpeedAir
{
    public interface IOrderProvider
    {
        IEnumerable<Order> GetOrders();
    }
}
