using ECom.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECom.Domain.Contract
{
    public interface IOrderRepository
    {
        Order AddOrder(Order product);
        Order GetOrderById(int id);
        Task<IEnumerable<Order>> GetAllOrders();
    }
}
