using ECom.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.Service.Contract
{
    public interface IOrderRepository
    {
        Order Add(Order category);
        Order GetById(int id);
        IEnumerable<Order> GetAll();
        Order Update(Order category);
        bool Delete(int id);
    }
}
