using ECom.Domain.Entities;
using ECom.Persistence.Seeds;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECom.Persistence.CategoryRepository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly InMemoryDbContext _context;

        public CategoryRepository(InMemoryDbContext context)
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
            _ = GetCategoryInMemory();
            return _context.Categories.Find(id);
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            _ = GetCategoryInMemory();
            return await _context.Categories.ToListAsync();
        }
       
        public Category Update(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
            return category;
        }

        public bool Delete(int id)
        {
            _ = GetCategoryInMemory();
            var category = _context.Categories.Find(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public async Task GetCategoryInMemory()
        {
            await SeedData.Seed(_context).ConfigureAwait(false);
        }

    }
}
