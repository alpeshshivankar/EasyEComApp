using ECom.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECom.Persistence.ProductRepository
{
    public interface IProductRepository
    {

        Product Add(Product Product);
        Product GetById(int id);
        Task<IEnumerable<Product>> GetAll();
        Product Update(Product Product);
        bool Delete(int id);
    }
}
