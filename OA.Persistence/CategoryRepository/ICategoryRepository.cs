using ECom.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.Persistence.CategoryRepository
{
    public interface ICategoryRepository
    {

        Category Add(Category Category);
        Category GetById(int id);
        Task <IEnumerable<Category>> GetAll();
        Category Update(Category order);
        bool Delete(int id);

    }
}
