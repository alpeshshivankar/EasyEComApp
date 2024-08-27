using ECom.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.Service.Contract
{
    public interface IProductRepository
    {
        Product Add(Product product);
        Product GetById(int id);
        IEnumerable<Product> GetAll();
        Product Update(Product product);
        bool Delete(int id);
    }
}
