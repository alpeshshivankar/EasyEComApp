using ECom.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECom.Domain.Contract
{
    public interface IProductRepository
    {
        Product AddProduct(Product product);
        Product GetProductById(int id);
        Task<IEnumerable<Product>> GetAllProducts();

    }
}
