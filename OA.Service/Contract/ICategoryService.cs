using OA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Service.Contract
{
    public interface ICategoryService
    {
        Category AddCategory(Category category);
        Category GetCategoryById(int id);
        IEnumerable<Category> GetAllCategories();
        Category UpdateCategory(Category category);
        bool DeleteCategory(int id);
    }
}
