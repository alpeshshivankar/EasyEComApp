using AutoMapper;
using ECom.Domain.Contract;
using ECom.Domain.Entities;
using ECom.Infrastructure.Persistance;
using ECom.Infrastructure.Persistance.DataModels;
using ECom.Infrastructure.Persistance.Seeds;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECom.Infrastructure.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly InMemoryDbContext _context;
        private readonly IMapper _mapper;

        public CategoryRepository(InMemoryDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task GetCategoryInMemory()
        {
            await SeedData.Seed(_context).ConfigureAwait(false);
        }

        public async Task<Category> AddAsync(Category category)
        {
            var entity = _mapper.Map<CategoryEntity>(category);
            await _context.Categories.AddAsync(entity);
            await _context.SaveChangesAsync();

            return new Category
            {
                Id = entity.Id,
                CategoryName = entity.CategoryName
            };
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            _ = GetCategoryInMemory();
            var entity = await _context.Categories.FindAsync(id);
            if (entity == null) return null;

            return new Category
            {
                Id = entity.Id,
                CategoryName = entity.CategoryName
            };
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            _ = GetCategoryInMemory();
            var entities = await _context.Categories.ToListAsync();

            return entities.Select(e => new Category
            {
                Id = e.Id,
                CategoryName = e.CategoryName
            }).ToList();
        }

        public async Task UpdateAsync(Category category)
        {
            var entity = await _context.Categories.FindAsync(category.Id);
            if (entity == null) return;

            entity.CategoryName = category.CategoryName;
            _context.Categories.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Categories.FindAsync(id);
            if (entity != null)
            {
                _context.Categories.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}