using ECom.Domain.Entities;
using ECom.Infrastructure.CategoryRepo;
using ECom.Persistence;
using ECom.Service.Contract;
using System.Collections.Generic;
using System.Linq;

namespace ECom.Infrastructure.CategoryRepo
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Category Add(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return category;
        }

        public Category GetById(int id)
        {
            return _context.Categories.Find(id);
        }

        public IEnumerable<Category> GetAll()
        {
            return _context.Categories.ToList();
        }

        public Category Update(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
            return category;
        }

        public bool Delete(int id)
        {
            var category = _context.Categories.Find(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        
    }
}
