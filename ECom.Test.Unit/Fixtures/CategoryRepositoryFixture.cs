using AutoMapper;
using ECom.Domain.Contract;
using ECom.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECom.Test.Unit.Fixtures
{
    public class CategoryRepositoryFixture
    {
        public Mock<ICategoryRepository> MockCategoryRepository { get; private set; }
        public Mock<IMapper> MoqMapper;
        public CategoryRepositoryFixture()
        {            
            MockCategoryRepository = new Mock<ICategoryRepository>();
            MoqMapper = new Mock<IMapper>();

            MockCategoryRepository.Setup(repo => repo.AddCategory(It.IsAny<Category>()))
                .Callback<Category>(category => category.Id = 1) 
                .Returns(Task<Category>)
                .Verifiable();
        }
    }
}
