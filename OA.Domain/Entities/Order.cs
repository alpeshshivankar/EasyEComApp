using System;
using System.Collections.Generic;

namespace ECom.Domain.Entities
{
    public class Order : BaseEntity
    {
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? OrderFulfillmentDate { get; set; }
        public List<Product> ProductDetails { get; set; }
        public string OrderStatus { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
