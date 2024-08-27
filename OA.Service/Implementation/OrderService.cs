using ECom.Domain.Entities;
using ECom.Service.Contract;
using System.Collections.Generic;

namespace ECom.Service.Implementation
{
    public class OrderService : IOrderService
    {

        public readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public Order AddOrder(Order order)
        {
            return _orderRepository.Add(order);
        }

        public Order GetOrderById(int id)
        {
            return _orderRepository.GetById(id);
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _orderRepository.GetAll();
        }

        public Order UpdateOrder(Order order)
        {
            return _orderRepository.Update(order);
        }

        public bool DeleteOrder(int id)
        {
            return _orderRepository.Delete(id);
        }

    }
}
