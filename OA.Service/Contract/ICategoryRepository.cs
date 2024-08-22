using OA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OA.Service.Contract
{
    public interface ICategoryRepository
    {

        Category Add(Category category);
        Category GetById(int id);
        IEnumerable<Category> GetAll();
        Category Update(Category category);
        bool Delete(int id);
    }
}
