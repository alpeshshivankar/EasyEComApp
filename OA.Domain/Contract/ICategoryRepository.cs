using ECom.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECom.Domain.Contract
{
    public interface ICategoryRepository
    {
        Category AddCategory(Category category);

        Category GetCategoryById(int id);

        Task<IEnumerable<Category>> GetAllCategories();
    }
}