using ECom.Domain.Entities;
using ECom.Service.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.Service.Implementation
{
    public class ProductService : IProductService
    {
        public readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public Product AddProduct(Product product)
        {
            return _productRepository.Add(product);
        }

        public Product GetProductById(int id)
        {
            return _productRepository.GetById(id);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _productRepository.GetAll();
        }

        public Product UpdateProduct(Product product)
        {
            return _productRepository.Update(product);
        }

        public bool DeleteProduct(int id)
        {
            return _productRepository.Delete(id);
        }
    }
}
