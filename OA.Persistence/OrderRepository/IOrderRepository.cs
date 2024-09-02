using ECom.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECom.Persistence.OrderRepository
{
    public interface IOrderRepository
    {

        Order Add(Order Order);
        Order GetById(int id);
        Task<IEnumerable<Order>> GetAll();
        Order Update(Order order);
        bool Delete(int id);
    }
}
