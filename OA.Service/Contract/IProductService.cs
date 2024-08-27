using ECom.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.Service.Contract
{
    public interface IProductService
    {
        Product AddProduct(Product product);
        Product GetProductById(int id);
        IEnumerable<Product> GetAllProducts();
        Product UpdateProduct(Product product);
        bool DeleteProduct(int id);
    }
}
