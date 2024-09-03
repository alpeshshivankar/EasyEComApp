
using ECom.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECom.Domain.Contract
{
    public interface ICategoryRepository
    {
        Task<Category> AddAsync(Category category);
        Task<Category> GetByIdAsync(int id);
        Task<IEnumerable<Category>> GetAllAsync();
        Task UpdateAsync(Category category);
        Task DeleteAsync(int id);
    }
}