using ECom.Application.Persistance;
using ECom.Application.Persistance.Seeds;
using ECom.Domain.Contract;
using ECom.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECom.Infrastructure.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly InMemoryDbContext _context;

        public CategoryRepository(InMemoryDbContext context)
        {
            _context = context;
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

        public Category AddCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return category;
        }

        public Category GetCategoryById(int id)
        {
            _ = GetCategoryInMemory();
            return _context.Categories.Find(id);
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            _ = GetCategoryInMemory();
            return await _context.Categories.ToListAsync();
        }
    }
}